using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using MyBatchProcessor.TaskObjects;
using MyBatchProcessor.AppObjects;

namespace MyBatchProcessor
{
    public partial class BatchProcessorForm : Form
    {
        private static Boolean isRunning = false;   // keep track of whether or not there is a process currently running
        BackgroundWorker bw;                        // the background worker to run the process on such that ui thread isn't blocked
        TaskObject task;                            // Abstract class which implements each possible task, currently PropertyUpdate and MassOutput
        Message msg;                                // message object used for updating the ui with messages

        #region Form Load

        public BatchProcessorForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set the default application
            this.loadDefaults();

            // create the message object for update the ui with log messages
            msg = new Message(this, textBox_messages);

            msg.doMsg("Enter the inputs, then press Start to begin.");
        }

        #endregion

        #region Handle Load and Save of Input Values

        private void loadDefaults()
        {
            this.comboBox1.SelectedIndex = Properties.Settings.Default.AppIndex;
            this.tabControl1.SelectedIndex = Properties.Settings.Default.TaskIndex;
            this.textBox_path.Text = Properties.Settings.Default.Path;
            this.textBox_propName.Text = Properties.Settings.Default.PropName;
            this.textBox_propValue.Text = Properties.Settings.Default.PropValue;
        }

        private void saveDefaults()
        {
            Properties.Settings.Default.AppIndex = this.comboBox1.SelectedIndex;
            Properties.Settings.Default.TaskIndex = this.tabControl1.SelectedIndex;
            Properties.Settings.Default.Path = this.textBox_path.Text;
            Properties.Settings.Default.PropName = this.textBox_propName.Text;
            Properties.Settings.Default.PropValue = this.textBox_propValue.Text;

            Properties.Settings.Default.Save();
        }

        #endregion

        #region Ensure Word is only available for property update

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2)
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
                tabControl1.SelectTab(0);
        }

        #endregion

        #region Handle path buttons

        private void button1_Click(object sender, EventArgs e)
        {
            // the clear path button was clicked
            this.textBox_path.Text = "";
        }

        private void button_browse_fldr_Click(object sender, EventArgs e)
        {
            // the add folder button was clicked
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.Desktop;
            Boolean hasExistingPath = false;
            String path = this.textBox_path.Text;
            if (path != null && path.Length > 0)
            {
                try
                {
                    String[] paths = path.Split(new string[] { Constants.DELIMETER }, StringSplitOptions.None);
                    if (paths.Length > 0)
                    {
                        String currentPath = Path.GetDirectoryName(paths[0]);
                        if (currentPath != null && Directory.Exists(currentPath))
                        {
                            dlg.SelectedPath = currentPath;
                            hasExistingPath = true;
                        }
                    }
                }
                catch
                {

                }
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (hasExistingPath && path.EndsWith(Constants.DELIMETER) == false)
                    path += Constants.DELIMETER;
                //add the new folder the text box
                this.textBox_path.Text = path + dlg.SelectedPath + Constants.DELIMETER;

                Properties.Settings.Default.Path = this.textBox_path.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void button_browse_file_Click(object sender, EventArgs e)
        {
            // the browse for files button was clicked
            OpenFileDialog ofd = new OpenFileDialog();

            // the filter that should be used is dependent on the application selected
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    ofd.Filter = "Files |*.sldprt; *.prt; *.slddrw; *.drw; *.sldasm; *.asm";
                    break;
                case 1:
                    ofd.Filter = "Files |*.docx; *.doc;";
                    break;
            }
            
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String[] fileNames = ofd.FileNames;
                String fileValue = "";
                foreach (String filename in fileNames)
                {
                    fileValue += filename + Constants.DELIMETER;
                }
                this.textBox_path.Text = fileValue;

                Properties.Settings.Default.Path = fileValue;
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        private void button_start_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                this.stopTask();
            }
            else
            {
                this.showResultTab();
                this.saveDefaults();

                isRunning = true;
                msg.doDivider();
                msg.doMsg("Starting Batch Process...");
                this.button_start.Text = "Stop";
                this.startProcessing();
            }
        }

        private void startProcessing()
        {
            TaskObject task = this.createTaskObject();
            if (task == null)
            {
                this.exitWithCode(Constants.RESULT_MISSING_INPUT);
                return;
            }

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += (sender, args) =>
            {
                args.Result = this.processTask(task);
            };

            bw.RunWorkerCompleted += (sender, e) =>
            {
                int result;
                if ((e.Cancelled == true))
                {
                    result = Constants.RESULT_CANCELED;
                }
                else if (!(e.Error == null))
                {
                    msg.doErrorMsg(e.Error.Message);
                    result = Constants.RESULT_ERROR_SHOWN;
                }
                else
                {
                    result = (int)e.Result;                                      
                }
                this.exitWithCode(result);
            };

            bw.RunWorkerAsync();  
        }

        private void exitWithCode(int code)
        {
            switch (code)
            {
                case Constants.RESULT_SUCCESS:
                    msg.doMsg("Task completed successfully.");
                    break;
                case Constants.RESULT_FAILED:
                    msg.doErrorMsg("Task failed.");
                    break;
                case Constants.RESULT_MISSING_INPUT:
                    msg.doErrorMsg("Task is missing inputs.");
                    break;
                case Constants.RESULT_CANCELED:
                    msg.doMsg("Task was canceled!");
                    break;
                case Constants.RESULT_ERROR_SHOWN:
                    // error message has already been shown.
                    break;
                case Constants.RESULT_PARTIAL_FAILURE:
                    msg.doErrorMsg("Some documents were not successful.");
                    break;
            }
            msg.doDivider();
            msg.doMsg("Press Start to begin another task.");

            isRunning = false;
            this.button_start.Text = "Start";
        }

        private int processTask(TaskObject task)
        {
            // determine if inputs are missing
            if (task == null || task.isTaskMissingInputs())
            {
                return Constants.RESULT_MISSING_INPUT;
            }
            else
            {
                // perform the selected task
                return task.start();
            }
        }

        private void stopTask()
        {
            if (task != null)
            {
                task.canceled = true;
            }

            if (bw != null)
                bw.CancelAsync();
        }

        private TaskObject createTaskObject()
        {
            task = null;
            // determine which tab is displayed and thus the task that will be completed
            if (TaskObject.isPropertyUpdateTaskSelected(this.tabControl1.SelectedIndex))
            {
                task = new PropertyUpdate(this.textBox_path.Text, this.textBox_propName.Text, this.textBox_propValue.Text, AppObject.determineAppID(comboBox1.SelectedIndex), msg);
            }
            else if (TaskObject.isMassOutputTaskSelected(this.tabControl1.SelectedIndex))
            {
                task = new MassOutput(this.textBox_path.Text, msg);
            }
            else if (TaskObject.isClearPropertyTaskSelected(this.tabControl1.SelectedIndex))
                task = new ClearProperty(this.textBox_path.Text, AppObject.determineAppID(comboBox1.SelectedIndex), msg);

            return task;
        }

        #region Result Tab

        private void showResultTab()
        {
            this.tabControl2.SelectTab(1);

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean visible = (this.tabControl2.SelectedIndex == 1);
            this.button_clear.Visible = visible;
            this.button_save.Visible = visible;
            
        }

        #endregion

        #region Implement Result Save and Clear

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.showResultTab();

            using (StreamWriter outfile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AllMessages.txt", false))
            {
                outfile.Write(this.textBox_messages.Text);
            }
            msg.doMsg("Messages were saved to AllMessages.txt on your desktop.");
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            this.textBox_messages.Text = "";
        }

        #endregion
    }
}
