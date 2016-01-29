using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBatchProcessor
{
    public class Message
    {
        public static String DIVIDER = " - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
        Control formControl;
        TextBox textBox_messages;
        public Message(Control ctrl, TextBox tb)
        {
            formControl = ctrl;
            textBox_messages = tb;
        }

        public void doMsg(String text)
        {
            DateTime testTime = DateTime.Now;
            String timeStamp = DateTime.Now.ToString("HH:mm:ss");
            String entry = timeStamp + " >" + text;

            this.addTextEntry(entry);
        }

        public void doErrorMsg(String text)
        {
            this.doMsg("Error: " + text);
        }

        public void doNoTimeMsg(String text)
        {
            this.addTextEntry(text);
        }

        public void doDivider()
        {
            this.addTextEntry(DIVIDER);
        }


        private void addTextEntry(String entry)
        {
            formControl.Invoke((MethodInvoker)delegate {
                // runs on UI thread
                if (textBox_messages.Text.Equals(""))
                {
                    this.textBox_messages.Text = entry;
                }
                else
                {
                    this.textBox_messages.Text += Environment.NewLine + entry;
                }
                int length = textBox_messages.Text.Length;
                if (length > 1)
                {
                    this.textBox_messages.SelectionStart = textBox_messages.Text.Length - 2;
                    this.textBox_messages.SelectionLength = 2;
                    this.textBox_messages.ScrollToCaret();
                }

                this.textBox_messages.Refresh();
            });
        }
    }
}
