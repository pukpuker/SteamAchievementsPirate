namespace SteamAchievementsPirate
{
    partial class GameList
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
            dataGridView1 = new DataGridView();
            AppID = new DataGridViewTextBoxColumn();
            AppName = new DataGridViewTextBoxColumn();
            Emulator = new DataGridViewTextBoxColumn();
            Path = new DataGridViewTextBoxColumn();
            Language = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { AppID, AppName, Emulator, Path, Language });
            dataGridView1.Location = new Point(2, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(934, 300);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // AppID
            // 
            AppID.HeaderText = "AppID";
            AppID.Name = "AppID";
            AppID.ReadOnly = true;
            AppID.Width = 110;
            // 
            // AppName
            // 
            AppName.HeaderText = "AppName";
            AppName.Name = "AppName";
            AppName.ReadOnly = true;
            AppName.Width = 130;
            // 
            // Emulator
            // 
            Emulator.HeaderText = "Emulator";
            Emulator.Name = "Emulator";
            Emulator.ReadOnly = true;
            // 
            // Path
            // 
            Path.HeaderText = "Path";
            Path.Name = "Path";
            Path.ReadOnly = true;
            Path.Width = 450;
            // 
            // Language
            // 
            Language.HeaderText = "Language";
            Language.Name = "Language";
            Language.ReadOnly = true;
            // 
            // GameList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(938, 304);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameList";
            ShowIcon = false;
            Text = "GameList";
            Load += GameList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn AppID;
        private DataGridViewTextBoxColumn AppName;
        private DataGridViewTextBoxColumn Emulator;
        private DataGridViewTextBoxColumn Path;
        private DataGridViewTextBoxColumn Language;
    }
}