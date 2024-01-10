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
            comboBox2 = new ComboBox();
            steamapiTextBox = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // startup_checkbox
            // 
            startup_checkbox.AutoSize = true;
            startup_checkbox.Location = new Point(12, 12);
            startup_checkbox.Name = "startup_checkbox";
            startup_checkbox.Size = new Size(67, 19);
            startup_checkbox.TabIndex = 0;
            startup_checkbox.Text = "Startup ";
            startup_checkbox.UseVisualStyleBackColor = true;
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
            LanguageComboBox.Location = new Point(88, 62);
            LanguageComboBox.Name = "LanguageComboBox";
            LanguageComboBox.Size = new Size(147, 23);
            LanguageComboBox.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(12, 159);
            button1.Name = "button1";
            button1.Size = new Size(223, 34);
            button1.TabIndex = 3;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 65);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 4;
            label1.Text = "Language:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 94);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 6;
            label2.Text = "Overlay Loc:";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Right Down", "Right Up", "Left Up", "Left Down" });
            comboBox2.Location = new Point(88, 91);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(147, 23);
            comboBox2.TabIndex = 5;
            // 
            // steamapiTextBox
            // 
            steamapiTextBox.Location = new Point(88, 120);
            steamapiTextBox.Name = "steamapiTextBox";
            steamapiTextBox.Size = new Size(147, 23);
            steamapiTextBox.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 123);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 8;
            label3.Text = "Steam API: ";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(247, 201);
            Controls.Add(label3);
            Controls.Add(steamapiTextBox);
            Controls.Add(label2);
            Controls.Add(comboBox2);
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
        private ComboBox comboBox2;
        private TextBox steamapiTextBox;
        private Label label3;
    }
}