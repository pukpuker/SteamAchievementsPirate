namespace SteamAchievementsPirate
{
    partial class SettingsForm
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
            startup_checkbox = new CheckBox();
            start_threads_checkbox = new CheckBox();
            LanguageComboBox = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            locationoverlay_box = new ComboBox();
            steamapiTextBox = new TextBox();
            label3 = new Label();
            pathBox = new TextBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label5 = new Label();
            NotifStyleComboBox = new ComboBox();
            button5 = new Button();
            button6 = new Button();
            startparserbox1 = new CheckBox();
            SuspendLayout();
            // 
            // startup_checkbox
            // 
            startup_checkbox.AutoSize = true;
            startup_checkbox.Location = new Point(12, 12);
            startup_checkbox.Name = "startup_checkbox";
            startup_checkbox.Size = new Size(64, 19);
            startup_checkbox.TabIndex = 0;
            startup_checkbox.Text = "Startup";
            startup_checkbox.UseVisualStyleBackColor = true;
            startup_checkbox.CheckedChanged += startup_checkbox_CheckedChanged;
            // 
            // start_threads_checkbox
            // 
            start_threads_checkbox.AutoSize = true;
            start_threads_checkbox.Location = new Point(12, 37);
            start_threads_checkbox.Name = "start_threads_checkbox";
            start_threads_checkbox.Size = new Size(170, 19);
            start_threads_checkbox.TabIndex = 1;
            start_threads_checkbox.Text = "Start threads from start app";
            start_threads_checkbox.UseVisualStyleBackColor = true;
            // 
            // LanguageComboBox
            // 
            LanguageComboBox.FormattingEnabled = true;
            LanguageComboBox.Items.AddRange(new object[] { "russian", "english", "arabic", "bulgarian", "schinese", "tchinese", "czech", "danish", "dutch", "finnish", "french", "german", "greek", "hungarian", "indonesian", "italian", "japanese", "koreana", "norwegian", "polish", "portuguese", "brazilian", "romanian", "spanish", "latam", "swedish", "thai", "turkish", "ukrainian", "vietnamese" });
            LanguageComboBox.Location = new Point(88, 86);
            LanguageComboBox.Name = "LanguageComboBox";
            LanguageComboBox.Size = new Size(147, 23);
            LanguageComboBox.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(12, 238);
            button1.Name = "button1";
            button1.Size = new Size(256, 34);
            button1.TabIndex = 3;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 89);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 4;
            label1.Text = "Language:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 118);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 6;
            label2.Text = "Overlay Loc:";
            // 
            // locationoverlay_box
            // 
            locationoverlay_box.FormattingEnabled = true;
            locationoverlay_box.Items.AddRange(new object[] { "Right Down", "Right Up", "Left Up", "Left Down" });
            locationoverlay_box.Location = new Point(88, 115);
            locationoverlay_box.Name = "locationoverlay_box";
            locationoverlay_box.Size = new Size(147, 23);
            locationoverlay_box.TabIndex = 5;
            // 
            // steamapiTextBox
            // 
            steamapiTextBox.Location = new Point(88, 144);
            steamapiTextBox.Name = "steamapiTextBox";
            steamapiTextBox.Size = new Size(147, 23);
            steamapiTextBox.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 8;
            label3.Text = "Steam API: ";
            // 
            // pathBox
            // 
            pathBox.Location = new Point(88, 173);
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(147, 23);
            pathBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 176);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 10;
            label4.Text = "Path(s): ";
            // 
            // button2
            // 
            button2.Location = new Point(241, 173);
            button2.Name = "button2";
            button2.Size = new Size(27, 23);
            button2.TabIndex = 11;
            button2.Text = "?";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(241, 144);
            button3.Name = "button3";
            button3.Size = new Size(27, 23);
            button3.TabIndex = 12;
            button3.Text = "?";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(241, 86);
            button4.Name = "button4";
            button4.Size = new Size(27, 23);
            button4.TabIndex = 13;
            button4.Text = "?";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 205);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 15;
            label5.Text = "Notif Style: ";
            // 
            // NotifStyleComboBox
            // 
            NotifStyleComboBox.FormattingEnabled = true;
            NotifStyleComboBox.Items.AddRange(new object[] { "Steam New", "Steam Old" });
            NotifStyleComboBox.Location = new Point(88, 202);
            NotifStyleComboBox.Name = "NotifStyleComboBox";
            NotifStyleComboBox.Size = new Size(147, 23);
            NotifStyleComboBox.TabIndex = 14;
            // 
            // button5
            // 
            button5.Location = new Point(241, 202);
            button5.Name = "button5";
            button5.Size = new Size(27, 23);
            button5.TabIndex = 16;
            button5.Text = "?";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(241, 114);
            button6.Name = "button6";
            button6.Size = new Size(27, 23);
            button6.TabIndex = 17;
            button6.Text = "?";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // startparserbox1
            // 
            startparserbox1.AutoSize = true;
            startparserbox1.Location = new Point(12, 62);
            startparserbox1.Name = "startparserbox1";
            startparserbox1.Size = new Size(168, 19);
            startparserbox1.TabIndex = 18;
            startparserbox1.Text = "Start Parser From Start App";
            startparserbox1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 284);
            Controls.Add(startparserbox1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(label5);
            Controls.Add(NotifStyleComboBox);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(pathBox);
            Controls.Add(label3);
            Controls.Add(steamapiTextBox);
            Controls.Add(label2);
            Controls.Add(locationoverlay_box);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(LanguageComboBox);
            Controls.Add(start_threads_checkbox);
            Controls.Add(startup_checkbox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SettingsForm";
            ShowIcon = false;
            Text = "Settings SAP";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox startup_checkbox;
        private CheckBox start_threads_checkbox;
        private ComboBox LanguageComboBox;
        private Button button1;
        private Label label1;
        private Label label2;
        private ComboBox locationoverlay_box;
        private TextBox steamapiTextBox;
        private Label label3;
        private TextBox pathBox;
        private Label label4;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label5;
        private ComboBox NotifStyleComboBox;
        private Button button5;
        private Button button6;
        private CheckBox startparserbox1;
    }
}