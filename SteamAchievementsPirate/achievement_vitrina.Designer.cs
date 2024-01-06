namespace SteamAchievementsPirate
{
    partial class achievement_vitrina
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
            label_information = new Label();
            SuspendLayout();
            // 
            // label_information
            // 
            label_information.AutoSize = true;
            label_information.Location = new Point(786, 9);
            label_information.Name = "label_information";
            label_information.Size = new Size(0, 15);
            label_information.TabIndex = 0;
            // 
            // achievement_vitrina
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 463);
            Controls.Add(label_information);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "achievement_vitrina";
            ShowIcon = false;
            Text = "Achievements";
            Load += achievement_vitrina_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_information;
    }
}