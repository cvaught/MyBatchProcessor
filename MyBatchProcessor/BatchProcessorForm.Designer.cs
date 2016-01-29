namespace MyBatchProcessor
{
    partial class BatchProcessorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchProcessorForm));
            this.button_start = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_propertyUpdate = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox_propValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_propName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_mass = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button_clearPath = new System.Windows.Forms.Button();
            this.button_browse_fldr = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_browse_file = new System.Windows.Forms.Button();
            this.textBox_messages = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_clear = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_propertyUpdate.SuspendLayout();
            this.tab_mass.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(303, 397);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_propertyUpdate);
            this.tabControl1.Controls.Add(this.tab_mass);
            this.tabControl1.Location = new System.Drawing.Point(10, 142);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(346, 205);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_propertyUpdate
            // 
            this.tab_propertyUpdate.BackColor = System.Drawing.SystemColors.Menu;
            this.tab_propertyUpdate.Controls.Add(this.textBox4);
            this.tab_propertyUpdate.Controls.Add(this.textBox_propValue);
            this.tab_propertyUpdate.Controls.Add(this.label3);
            this.tab_propertyUpdate.Controls.Add(this.textBox_propName);
            this.tab_propertyUpdate.Controls.Add(this.label1);
            this.tab_propertyUpdate.Location = new System.Drawing.Point(4, 22);
            this.tab_propertyUpdate.Name = "tab_propertyUpdate";
            this.tab_propertyUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tab_propertyUpdate.Size = new System.Drawing.Size(338, 179);
            this.tab_propertyUpdate.TabIndex = 0;
            this.tab_propertyUpdate.Text = "Property Update";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(17, 9);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(303, 30);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "This method will update a specified property to a specific value and save it if v" +
    "alue was changed.";
            // 
            // textBox_propValue
            // 
            this.textBox_propValue.Location = new System.Drawing.Point(165, 81);
            this.textBox_propValue.Name = "textBox_propValue";
            this.textBox_propValue.Size = new System.Drawing.Size(100, 20);
            this.textBox_propValue.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Property Value:";
            // 
            // textBox_propName
            // 
            this.textBox_propName.Location = new System.Drawing.Point(165, 55);
            this.textBox_propName.Name = "textBox_propName";
            this.textBox_propName.Size = new System.Drawing.Size(100, 20);
            this.textBox_propName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Property Name:";
            // 
            // tab_mass
            // 
            this.tab_mass.BackColor = System.Drawing.SystemColors.Menu;
            this.tab_mass.Controls.Add(this.textBox5);
            this.tab_mass.Location = new System.Drawing.Point(4, 22);
            this.tab_mass.Name = "tab_mass";
            this.tab_mass.Padding = new System.Windows.Forms.Padding(3);
            this.tab_mass.Size = new System.Drawing.Size(338, 179);
            this.tab_mass.TabIndex = 1;
            this.tab_mass.Text = "Output Mass";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(17, 9);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(302, 30);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "This method will process each model file to determine the mass and output it belo" +
    "w.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Application:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "SOLIDWORKS",
            "WORD"});
            this.comboBox1.Location = new System.Drawing.Point(136, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button_clearPath
            // 
            this.button_clearPath.Location = new System.Drawing.Point(91, 91);
            this.button_clearPath.Name = "button_clearPath";
            this.button_clearPath.Size = new System.Drawing.Size(76, 23);
            this.button_clearPath.TabIndex = 15;
            this.button_clearPath.Text = "Clear Path";
            this.button_clearPath.UseVisualStyleBackColor = true;
            this.button_clearPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_browse_fldr
            // 
            this.button_browse_fldr.Location = new System.Drawing.Point(173, 91);
            this.button_browse_fldr.Name = "button_browse_fldr";
            this.button_browse_fldr.Size = new System.Drawing.Size(76, 23);
            this.button_browse_fldr.TabIndex = 12;
            this.button_browse_fldr.Text = "Add Folder";
            this.button_browse_fldr.UseVisualStyleBackColor = true;
            this.button_browse_fldr.Click += new System.EventHandler(this.button_browse_fldr_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(13, 67);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(111, 18);
            this.textBox3.TabIndex = 14;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "Enter the Folder or File:";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(136, 65);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(213, 20);
            this.textBox_path.TabIndex = 11;
            // 
            // button_browse_file
            // 
            this.button_browse_file.Location = new System.Drawing.Point(255, 91);
            this.button_browse_file.Name = "button_browse_file";
            this.button_browse_file.Size = new System.Drawing.Size(94, 23);
            this.button_browse_file.TabIndex = 13;
            this.button_browse_file.Text = "Browse for Files";
            this.button_browse_file.UseVisualStyleBackColor = true;
            this.button_browse_file.Click += new System.EventHandler(this.button_browse_file_Click);
            // 
            // textBox_messages
            // 
            this.textBox_messages.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_messages.Location = new System.Drawing.Point(6, 6);
            this.textBox_messages.Multiline = true;
            this.textBox_messages.Name = "textBox_messages";
            this.textBox_messages.ReadOnly = true;
            this.textBox_messages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_messages.Size = new System.Drawing.Size(350, 341);
            this.textBox_messages.TabIndex = 16;
            this.textBox_messages.TabStop = false;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(16, 397);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(58, 23);
            this.button_save.TabIndex = 17;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Visible = false;
            this.button_save.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(12, 12);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(370, 379);
            this.tabControl2.TabIndex = 18;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Menu;
            this.tabPage1.Controls.Add(this.tabControl1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button_clearPath);
            this.tabPage1.Controls.Add(this.button_browse_file);
            this.tabPage1.Controls.Add(this.button_browse_fldr);
            this.tabPage1.Controls.Add(this.textBox_path);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Menu;
            this.tabPage2.Controls.Add(this.textBox_messages);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 353);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Result";
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(80, 397);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(58, 23);
            this.button_clear.TabIndex = 19;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Visible = false;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 433);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyBatchProcessor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_propertyUpdate.ResumeLayout(false);
            this.tab_propertyUpdate.PerformLayout();
            this.tab_mass.ResumeLayout(false);
            this.tab_mass.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_propertyUpdate;
        private System.Windows.Forms.TabPage tab_mass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button_clearPath;
        private System.Windows.Forms.Button button_browse_fldr;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_browse_file;
        private System.Windows.Forms.TextBox textBox_messages;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox_propValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_propName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_clear;
    }
}

