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
            this.topFailuresButton = new System.Windows.Forms.Button();
            this.timer1000ms = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.operatorLoginLabel = new System.Windows.Forms.Label();
            this.stationNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.FailedQtyLabel = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.YieldLabel = new System.Windows.Forms.Label();
            this.detailsButton = new System.Windows.Forms.Button();
            this.breakdownButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLoggingSwitchButton = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sendOptionCombobox = new System.Windows.Forms.ComboBox();
            this.TestedQtyLabel = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // topFailuresButton
            // 
            this.topFailuresButton.BackColor = System.Drawing.SystemColors.Control;
            this.topFailuresButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.topFailuresButton.Location = new System.Drawing.Point(125, 213);
            this.topFailuresButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.topFailuresButton.Name = "topFailuresButton";
            this.topFailuresButton.Size = new System.Drawing.Size(86, 25);
            this.topFailuresButton.TabIndex = 0;
            this.topFailuresButton.Text = "Top błędy";
            this.topFailuresButton.UseVisualStyleBackColor = false;
            this.topFailuresButton.Click += new System.EventHandler(this.topFailuresButton_Click);
            // 
            // timer1000ms
            // 
            this.timer1000ms.Interval = 1000;
            this.timer1000ms.Tick += new System.EventHandler(this.timer1000ms_Tick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Zalogowano jako";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label4.Visible = false;
            // 
            // operatorLoginLabel
            // 
            this.operatorLoginLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.operatorLoginLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorLoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operatorLoginLabel.Location = new System.Drawing.Point(0, 0);
            this.operatorLoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.operatorLoginLabel.Name = "operatorLoginLabel";
            this.operatorLoginLabel.Size = new System.Drawing.Size(121, 20);
            this.operatorLoginLabel.TabIndex = 7;
            this.operatorLoginLabel.Text = "Operator";
            this.operatorLoginLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.operatorLoginLabel.Visible = false;
            // 
            // stationNameLabel
            // 
            this.stationNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stationNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.stationNameLabel.Location = new System.Drawing.Point(0, 0);
            this.stationNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stationNameLabel.Name = "stationNameLabel";
            this.stationNameLabel.Size = new System.Drawing.Size(217, 33);
            this.stationNameLabel.TabIndex = 6;
            this.stationNameLabel.Text = "Stacja robocza";
            this.stationNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Stacja robocza";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Tomato;
            this.panel8.Controls.Add(this.FailedQtyLabel);
            this.panel8.Location = new System.Drawing.Point(4, 212);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(105, 29);
            this.panel8.TabIndex = 14;
            // 
            // FailedQtyLabel
            // 
            this.FailedQtyLabel.BackColor = System.Drawing.Color.Tomato;
            this.FailedQtyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailedQtyLabel.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FailedQtyLabel.Location = new System.Drawing.Point(0, 0);
            this.FailedQtyLabel.Name = "FailedQtyLabel";
            this.FailedQtyLabel.Size = new System.Drawing.Size(105, 29);
            this.FailedQtyLabel.TabIndex = 0;
            this.FailedQtyLabel.Text = "0";
            this.FailedQtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel9.Controls.Add(this.YieldLabel);
            this.panel9.Location = new System.Drawing.Point(3, 244);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(106, 56);
            this.panel9.TabIndex = 15;
            // 
            // YieldLabel
            // 
            this.YieldLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.YieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YieldLabel.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YieldLabel.Location = new System.Drawing.Point(0, 0);
            this.YieldLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.YieldLabel.Name = "YieldLabel";
            this.YieldLabel.Size = new System.Drawing.Size(106, 56);
            this.YieldLabel.TabIndex = 0;
            this.YieldLabel.Text = "0";
            this.YieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailsButton
            // 
            this.detailsButton.BackColor = System.Drawing.SystemColors.Control;
            this.detailsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.detailsButton.Location = new System.Drawing.Point(125, 244);
            this.detailsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.detailsButton.Name = "detailsButton";
            this.detailsButton.Size = new System.Drawing.Size(86, 25);
            this.detailsButton.TabIndex = 0;
            this.detailsButton.Text = "Szczegóły";
            this.detailsButton.UseVisualStyleBackColor = false;
            this.detailsButton.Click += new System.EventHandler(this.detailsButton_Click);
            // 
            // breakdownButton
            // 
            this.breakdownButton.BackColor = System.Drawing.SystemColors.Control;
            this.breakdownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.breakdownButton.Location = new System.Drawing.Point(125, 275);
            this.breakdownButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.breakdownButton.Name = "breakdownButton";
            this.breakdownButton.Size = new System.Drawing.Size(86, 25);
            this.breakdownButton.TabIndex = 0;
            this.breakdownButton.Text = "Awaria";
            this.breakdownButton.UseVisualStyleBackColor = false;
            this.breakdownButton.Click += new System.EventHandler(this.breakdownButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(217, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputToolStripMenuItem,
            this.outputToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem1.Text = "Config";
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.inputToolStripMenuItem.Text = "Input";
            this.inputToolStripMenuItem.Click += new System.EventHandler(this.inputToolStripMenuItem_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.outputToolStripMenuItem.Text = "Output";
            this.outputToolStripMenuItem.Click += new System.EventHandler(this.outputToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // dataLoggingSwitchButton
            // 
            this.dataLoggingSwitchButton.BackColor = System.Drawing.Color.Green;
            this.dataLoggingSwitchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataLoggingSwitchButton.Location = new System.Drawing.Point(0, 124);
            this.dataLoggingSwitchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataLoggingSwitchButton.Name = "dataLoggingSwitchButton";
            this.dataLoggingSwitchButton.Size = new System.Drawing.Size(216, 50);
            this.dataLoggingSwitchButton.TabIndex = 0;
            this.dataLoggingSwitchButton.Text = "ON";
            this.dataLoggingSwitchButton.UseVisualStyleBackColor = false;
            this.dataLoggingSwitchButton.Click += new System.EventHandler(this.dataLoggingSwitchButton_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.BackColor = System.Drawing.SystemColors.Control;
            this.logOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logOutButton.Location = new System.Drawing.Point(125, 3);
            this.logOutButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(86, 40);
            this.logOutButton.TabIndex = 0;
            this.logOutButton.Text = "Wyloguj";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Visible = false;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.logOutButton);
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 103);
            this.panel1.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(121, 41);
            this.panel4.TabIndex = 10;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.operatorLoginLabel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 21);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(121, 20);
            this.panel6.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(121, 18);
            this.panel5.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.stationNameLabel);
            this.panel3.Location = new System.Drawing.Point(0, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(217, 33);
            this.panel3.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 18);
            this.panel2.TabIndex = 8;
            // 
            // sendOptionCombobox
            // 
            this.sendOptionCombobox.Items.AddRange(new object[] {
            "Wyślij passed",
            "Usuń wszystkie",
            "Wyślij wszystkie"});
            this.sendOptionCombobox.Location = new System.Drawing.Point(125, 180);
            this.sendOptionCombobox.Name = "sendOptionCombobox";
            this.sendOptionCombobox.Size = new System.Drawing.Size(86, 23);
            this.sendOptionCombobox.TabIndex = 21;
            this.sendOptionCombobox.Visible = false;
            // 
            // TestedQtyLabel
            // 
            this.TestedQtyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestedQtyLabel.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TestedQtyLabel.Location = new System.Drawing.Point(0, 0);
            this.TestedQtyLabel.Name = "TestedQtyLabel";
            this.TestedQtyLabel.Size = new System.Drawing.Size(105, 29);
            this.TestedQtyLabel.TabIndex = 0;
            this.TestedQtyLabel.Text = "0";
            this.TestedQtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SkyBlue;
            this.panel7.Controls.Add(this.TestedQtyLabel);
            this.panel7.Location = new System.Drawing.Point(4, 180);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(105, 29);
            this.panel7.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 304);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.sendOptionCombobox);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.breakdownButton);
            this.Controls.Add(this.dataLoggingSwitchButton);
            this.Controls.Add(this.detailsButton);
            this.Controls.Add(this.topFailuresButton);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "TestManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DowntimeForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button topFailuresButton;
        private System.Windows.Forms.Timer timer1000ms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label operatorLoginLabel;
        private System.Windows.Forms.Label stationNameLabel;
        private System.Windows.Forms.Label label1;
        private Panel panel8;
        private Label FailedQtyLabel;
        private Panel panel9;
        private Label YieldLabel;
        private Button detailsButton;
        private Button breakdownButton;
        private MenuStrip menuStrip1;
        private Button dataLoggingSwitchButton;
        private Button logOutButton;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem inputToolStripMenuItem;
        private ToolStripMenuItem outputToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ComboBox sendOptionCombobox;
        private Label TestedQtyLabel;
        private Panel panel7;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private Panel panel6;
        private Panel panel5;
    }
}

