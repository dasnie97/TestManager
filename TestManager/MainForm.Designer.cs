namespace TestManager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.topFailuresButton = new System.Windows.Forms.Button();
            this.breakdownTimeStartedLabel = new System.Windows.Forms.Label();
            this.timer1000ms = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.operatorLoginLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.stationNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.TestedQtyLabel = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.FailedQtyLabel = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.YieldLabel = new System.Windows.Forms.Label();
            this.detailsButton = new System.Windows.Forms.Button();
            this.breakdownButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLoggingSwitchButton = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topFailuresButton
            // 
            this.topFailuresButton.BackColor = System.Drawing.SystemColors.Control;
            this.topFailuresButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.topFailuresButton.Location = new System.Drawing.Point(191, 178);
            this.topFailuresButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.topFailuresButton.Name = "topFailuresButton";
            this.topFailuresButton.Size = new System.Drawing.Size(92, 38);
            this.topFailuresButton.TabIndex = 0;
            this.topFailuresButton.Text = "Top błędy";
            this.topFailuresButton.UseVisualStyleBackColor = false;
            this.topFailuresButton.Click += new System.EventHandler(this.breakdownButton_Click);
            // 
            // breakdownTimeStartedLabel
            // 
            this.breakdownTimeStartedLabel.AutoSize = true;
            this.breakdownTimeStartedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.breakdownTimeStartedLabel.Location = new System.Drawing.Point(154, 333);
            this.breakdownTimeStartedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.breakdownTimeStartedLabel.Name = "breakdownTimeStartedLabel";
            this.breakdownTimeStartedLabel.Size = new System.Drawing.Size(57, 20);
            this.breakdownTimeStartedLabel.TabIndex = 1;
            this.breakdownTimeStartedLabel.Text = "label1";
            this.breakdownTimeStartedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1000ms
            // 
            this.timer1000ms.Interval = 1000;
            this.timer1000ms.Tick += new System.EventHandler(this.timer1000ms_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(26, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Zalogowano jako";
            // 
            // operatorLoginLabel
            // 
            this.operatorLoginLabel.AutoSize = true;
            this.operatorLoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operatorLoginLabel.Location = new System.Drawing.Point(61, 53);
            this.operatorLoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.operatorLoginLabel.Name = "operatorLoginLabel";
            this.operatorLoginLabel.Size = new System.Drawing.Size(57, 20);
            this.operatorLoginLabel.TabIndex = 7;
            this.operatorLoginLabel.Text = "label5";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(247, 375);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // stationNameLabel
            // 
            this.stationNameLabel.AutoSize = true;
            this.stationNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.stationNameLabel.Location = new System.Drawing.Point(165, 83);
            this.stationNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stationNameLabel.Name = "stationNameLabel";
            this.stationNameLabel.Size = new System.Drawing.Size(115, 20);
            this.stationNameLabel.TabIndex = 6;
            this.stationNameLabel.Text = "Stacja robocza";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Stacja robocza:";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SkyBlue;
            this.panel7.Controls.Add(this.TestedQtyLabel);
            this.panel7.Location = new System.Drawing.Point(12, 178);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(111, 38);
            this.panel7.TabIndex = 13;
            // 
            // TestedQtyLabel
            // 
            this.TestedQtyLabel.AutoSize = true;
            this.TestedQtyLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TestedQtyLabel.Location = new System.Drawing.Point(0, 0);
            this.TestedQtyLabel.Name = "TestedQtyLabel";
            this.TestedQtyLabel.Size = new System.Drawing.Size(67, 25);
            this.TestedQtyLabel.TabIndex = 0;
            this.TestedQtyLabel.Text = "Tested";
            this.TestedQtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Tomato;
            this.panel8.Controls.Add(this.FailedQtyLabel);
            this.panel8.Location = new System.Drawing.Point(12, 222);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(111, 38);
            this.panel8.TabIndex = 14;
            // 
            // FailedQtyLabel
            // 
            this.FailedQtyLabel.AutoSize = true;
            this.FailedQtyLabel.BackColor = System.Drawing.Color.Tomato;
            this.FailedQtyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailedQtyLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FailedQtyLabel.Location = new System.Drawing.Point(0, 0);
            this.FailedQtyLabel.Name = "FailedQtyLabel";
            this.FailedQtyLabel.Size = new System.Drawing.Size(61, 25);
            this.FailedQtyLabel.TabIndex = 0;
            this.FailedQtyLabel.Text = "Failed";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel9.Controls.Add(this.YieldLabel);
            this.panel9.Location = new System.Drawing.Point(12, 266);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(111, 38);
            this.panel9.TabIndex = 15;
            // 
            // YieldLabel
            // 
            this.YieldLabel.AutoSize = true;
            this.YieldLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.YieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YieldLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.YieldLabel.Location = new System.Drawing.Point(0, 0);
            this.YieldLabel.Name = "YieldLabel";
            this.YieldLabel.Size = new System.Drawing.Size(54, 25);
            this.YieldLabel.TabIndex = 0;
            this.YieldLabel.Text = "Yield";
            // 
            // detailsButton
            // 
            this.detailsButton.BackColor = System.Drawing.SystemColors.Control;
            this.detailsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.detailsButton.Location = new System.Drawing.Point(191, 222);
            this.detailsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.detailsButton.Name = "detailsButton";
            this.detailsButton.Size = new System.Drawing.Size(92, 38);
            this.detailsButton.TabIndex = 0;
            this.detailsButton.Text = "Szczegóły";
            this.detailsButton.UseVisualStyleBackColor = false;
            this.detailsButton.Click += new System.EventHandler(this.breakdownButton_Click);
            // 
            // breakdownButton
            // 
            this.breakdownButton.BackColor = System.Drawing.SystemColors.Control;
            this.breakdownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.breakdownButton.Location = new System.Drawing.Point(191, 266);
            this.breakdownButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.breakdownButton.Name = "breakdownButton";
            this.breakdownButton.Size = new System.Drawing.Size(92, 38);
            this.breakdownButton.TabIndex = 0;
            this.breakdownButton.Text = "Awaria";
            this.breakdownButton.UseVisualStyleBackColor = false;
            this.breakdownButton.Click += new System.EventHandler(this.breakdownButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(313, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.configToolStripMenuItem.Text = "Config Info";
            // 
            // dataLoggingSwitchButton
            // 
            this.dataLoggingSwitchButton.BackColor = System.Drawing.SystemColors.Control;
            this.dataLoggingSwitchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataLoggingSwitchButton.Location = new System.Drawing.Point(15, 106);
            this.dataLoggingSwitchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataLoggingSwitchButton.Name = "dataLoggingSwitchButton";
            this.dataLoggingSwitchButton.Size = new System.Drawing.Size(280, 48);
            this.dataLoggingSwitchButton.TabIndex = 0;
            this.dataLoggingSwitchButton.Text = "ON";
            this.dataLoggingSwitchButton.UseVisualStyleBackColor = false;
            this.dataLoggingSwitchButton.Click += new System.EventHandler(this.breakdownButton_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.BackColor = System.Drawing.SystemColors.Control;
            this.logOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logOutButton.Location = new System.Drawing.Point(191, 27);
            this.logOutButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(104, 38);
            this.logOutButton.TabIndex = 0;
            this.logOutButton.Text = "Wyloguj";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(27, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "Awaria trwa już";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 418);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.operatorLoginLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stationNameLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.breakdownTimeStartedLabel);
            this.Controls.Add(this.breakdownButton);
            this.Controls.Add(this.dataLoggingSwitchButton);
            this.Controls.Add(this.detailsButton);
            this.Controls.Add(this.logOutButton);
            this.Controls.Add(this.topFailuresButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "TestManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DowntimeForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button topFailuresButton;
        private System.Windows.Forms.Label breakdownTimeStartedLabel;
        private System.Windows.Forms.Timer timer1000ms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label operatorLoginLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label stationNameLabel;
        private System.Windows.Forms.Label label1;
        private Panel panel7;
        private Label TestedQtyLabel;
        private Panel panel8;
        private Label FailedQtyLabel;
        private Panel panel9;
        private Label YieldLabel;
        private Button detailsButton;
        private Button breakdownButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem configToolStripMenuItem;
        private Button dataLoggingSwitchButton;
        private Button logOutButton;
        private Label label2;
    }
}

