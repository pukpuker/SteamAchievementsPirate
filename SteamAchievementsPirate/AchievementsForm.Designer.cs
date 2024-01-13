namespace SteamAchievementsPirate
{
    partial class AchievementsForm
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
            exitbutton = new Button();
            panel1 = new Panel();
            minimizebutton = new Button();
            SuspendLayout();
            // 
            // exitbutton
            // 
            exitbutton.BackColor = Color.Transparent;
            exitbutton.FlatStyle = FlatStyle.Flat;
            exitbutton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            exitbutton.ForeColor = Color.Red;
            exitbutton.Location = new Point(807, -1);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(35, 37);
            exitbutton.TabIndex = 0;
            exitbutton.Text = "X";
            exitbutton.UseVisualStyleBackColor = false;
            exitbutton.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(830, 385);
            panel1.TabIndex = 1;
            // 
            // minimizebutton
            // 
            minimizebutton.BackColor = Color.Transparent;
            minimizebutton.FlatStyle = FlatStyle.Flat;
            minimizebutton.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            minimizebutton.ForeColor = Color.Red;
            minimizebutton.Location = new Point(770, -10);
            minimizebutton.Name = "minimizebutton";
            minimizebutton.Size = new Size(31, 45);
            minimizebutton.TabIndex = 2;
            minimizebutton.Text = "-";
            minimizebutton.UseVisualStyleBackColor = false;
            minimizebutton.Click += minimizebutton_Click;
            // 
            // AchievementsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 439);
            Controls.Add(minimizebutton);
            Controls.Add(panel1);
            Controls.Add(exitbutton);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "AchievementsForm";
            ShowIcon = false;
            Text = "AchievementsForm";
            Load += AchievementsForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button exitbutton;
        private Panel panel1;
        private Button minimizebutton;
    }
}