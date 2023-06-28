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
            components = new System.ComponentModel.Container();
            topFailuresButton = new Button();
            label4 = new Label();
            operatorLoginLabel = new Label();
            stationNameLabel = new Label();
            label1 = new Label();
            detailsButton = new Button();
            breakdownButton = new Button();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            iodirsToolStripMenuItem = new ToolStripMenuItem();
            inputToolStripMenuItem = new ToolStripMenuItem();
            outputToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            mesToolStripMenuItem = new ToolStripMenuItem();
            verify3510ToolStripMenuItem = new ToolStripMenuItem();
            httpToolStripMenuItem = new ToolStripMenuItem();
            ftpToolStripMenuItem = new ToolStripMenuItem();
            testReportTransferSwitchButton = new Button();
            logOutButton = new Button();
            panel1 = new Panel();
            panel4 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            transferOptionCombobox = new ComboBox();
            statisticsControl = new StatisticsControl();
            timer3000ms = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // topFailuresButton
            // 
            topFailuresButton.BackColor = SystemColors.Control;
            topFailuresButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            topFailuresButton.Location = new Point(125, 213);
            topFailuresButton.Margin = new Padding(4, 3, 4, 3);
            topFailuresButton.Name = "topFailuresButton";
            topFailuresButton.Size = new Size(86, 25);
            topFailuresButton.TabIndex = 0;
            topFailuresButton.Text = "Top błędy";
            topFailuresButton.UseVisualStyleBackColor = false;
            topFailuresButton.Click += topFailuresButton_Click;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.ControlLight;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(0, 0);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(121, 18);
            label4.TabIndex = 6;
            label4.Text = "Zalogowano jako";
            label4.TextAlign = ContentAlignment.BottomCenter;
            label4.Visible = false;
            // 
            // operatorLoginLabel
            // 
            operatorLoginLabel.BackColor = SystemColors.ControlLight;
            operatorLoginLabel.Dock = DockStyle.Fill;
            operatorLoginLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            operatorLoginLabel.Location = new Point(0, 0);
            operatorLoginLabel.Margin = new Padding(4, 0, 4, 0);
            operatorLoginLabel.Name = "operatorLoginLabel";
            operatorLoginLabel.Size = new Size(121, 20);
            operatorLoginLabel.TabIndex = 7;
            operatorLoginLabel.Text = "Operator";
            operatorLoginLabel.TextAlign = ContentAlignment.TopCenter;
            operatorLoginLabel.Visible = false;
            // 
            // stationNameLabel
            // 
            stationNameLabel.Dock = DockStyle.Fill;
            stationNameLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            stationNameLabel.Location = new Point(0, 0);
            stationNameLabel.Margin = new Padding(4, 0, 4, 0);
            stationNameLabel.Name = "stationNameLabel";
            stationNameLabel.Size = new Size(217, 33);
            stationNameLabel.TabIndex = 6;
            stationNameLabel.Text = "Stacja robocza";
            stationNameLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(216, 18);
            label1.TabIndex = 6;
            label1.Text = "Stacja robocza";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // detailsButton
            // 
            detailsButton.BackColor = SystemColors.Control;
            detailsButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            detailsButton.Location = new Point(125, 244);
            detailsButton.Margin = new Padding(4, 3, 4, 3);
            detailsButton.Name = "detailsButton";
            detailsButton.Size = new Size(86, 25);
            detailsButton.TabIndex = 0;
            detailsButton.Text = "Szczegóły";
            detailsButton.UseVisualStyleBackColor = false;
            detailsButton.Click += detailsButton_Click;
            // 
            // breakdownButton
            // 
            breakdownButton.BackColor = SystemColors.Control;
            breakdownButton.Enabled = false;
            breakdownButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            breakdownButton.Location = new Point(125, 275);
            breakdownButton.Margin = new Padding(4, 3, 4, 3);
            breakdownButton.Name = "breakdownButton";
            breakdownButton.Size = new Size(86, 25);
            breakdownButton.TabIndex = 0;
            breakdownButton.Text = "Awaria";
            breakdownButton.UseVisualStyleBackColor = false;
            breakdownButton.Click += breakdownButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(217, 24);
            menuStrip1.TabIndex = 16;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor = SystemColors.Menu;
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { iodirsToolStripMenuItem, mesToolStripMenuItem, verify3510ToolStripMenuItem, httpToolStripMenuItem, ftpToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(55, 20);
            toolStripMenuItem1.Text = "Config";
            // 
            // iodirsToolStripMenuItem
            // 
            iodirsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { inputToolStripMenuItem, outputToolStripMenuItem, copyToolStripMenuItem });
            iodirsToolStripMenuItem.Name = "iodirsToolStripMenuItem";
            iodirsToolStripMenuItem.Size = new Size(159, 22);
            iodirsToolStripMenuItem.Text = "I/O Directories";
            // 
            // inputToolStripMenuItem
            // 
            inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            inputToolStripMenuItem.Size = new Size(112, 22);
            inputToolStripMenuItem.Text = "Input";
            inputToolStripMenuItem.Click += inputToolStripMenuItem_Click;
            // 
            // outputToolStripMenuItem
            // 
            outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            outputToolStripMenuItem.Size = new Size(112, 22);
            outputToolStripMenuItem.Text = "Output";
            outputToolStripMenuItem.Click += outputToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(112, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // mesToolStripMenuItem
            // 
            mesToolStripMenuItem.CheckOnClick = true;
            mesToolStripMenuItem.Enabled = false;
            mesToolStripMenuItem.Name = "mesToolStripMenuItem";
            mesToolStripMenuItem.Size = new Size(159, 22);
            mesToolStripMenuItem.Text = "Verify MES";
            mesToolStripMenuItem.Click += mesToolStripMenuItem_Click;
            // 
            // verify3510ToolStripMenuItem
            // 
            verify3510ToolStripMenuItem.CheckOnClick = true;
            verify3510ToolStripMenuItem.Enabled = false;
            verify3510ToolStripMenuItem.Name = "verify3510ToolStripMenuItem";
            verify3510ToolStripMenuItem.Size = new Size(159, 22);
            verify3510ToolStripMenuItem.Text = "Verify 3-5-10";
            verify3510ToolStripMenuItem.Click += verify3510ToolStripMenuItem_Click;
            // 
            // httpToolStripMenuItem
            // 
            httpToolStripMenuItem.CheckOnClick = true;
            httpToolStripMenuItem.Enabled = false;
            httpToolStripMenuItem.Name = "httpToolStripMenuItem";
            httpToolStripMenuItem.Size = new Size(159, 22);
            httpToolStripMenuItem.Text = "Send to WebAPI";
            httpToolStripMenuItem.Click += httpToolStripMenuItem_Click;
            // 
            // ftpToolStripMenuItem
            // 
            ftpToolStripMenuItem.CheckOnClick = true;
            ftpToolStripMenuItem.Name = "ftpToolStripMenuItem";
            ftpToolStripMenuItem.Size = new Size(159, 22);
            ftpToolStripMenuItem.Text = "Send to FTP";
            ftpToolStripMenuItem.Click += ftpToolStripMenuItem_Click;
            // 
            // testReportTransferSwitchButton
            // 
            testReportTransferSwitchButton.BackColor = Color.Green;
            testReportTransferSwitchButton.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            testReportTransferSwitchButton.Location = new Point(0, 124);
            testReportTransferSwitchButton.Margin = new Padding(4, 3, 4, 3);
            testReportTransferSwitchButton.Name = "testReportTransferSwitchButton";
            testReportTransferSwitchButton.Size = new Size(216, 50);
            testReportTransferSwitchButton.TabIndex = 0;
            testReportTransferSwitchButton.Text = "ON";
            testReportTransferSwitchButton.UseVisualStyleBackColor = false;
            testReportTransferSwitchButton.Click += testReportTransferSwitchButton_Click;
            // 
            // logOutButton
            // 
            logOutButton.BackColor = SystemColors.Control;
            logOutButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            logOutButton.Location = new Point(125, 3);
            logOutButton.Margin = new Padding(4, 3, 4, 3);
            logOutButton.Name = "logOutButton";
            logOutButton.Size = new Size(86, 40);
            logOutButton.TabIndex = 0;
            logOutButton.Text = "Wyloguj";
            logOutButton.UseVisualStyleBackColor = false;
            logOutButton.Visible = false;
            logOutButton.Click += logOutButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(logOutButton);
            panel1.Location = new Point(0, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(216, 103);
            panel1.TabIndex = 18;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(121, 41);
            panel4.TabIndex = 10;
            // 
            // panel6
            // 
            panel6.Controls.Add(operatorLoginLabel);
            panel6.Dock = DockStyle.Bottom;
            panel6.Location = new Point(0, 21);
            panel6.Name = "panel6";
            panel6.Size = new Size(121, 20);
            panel6.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(label4);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(121, 18);
            panel5.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(stationNameLabel);
            panel3.Location = new Point(0, 67);
            panel3.Name = "panel3";
            panel3.Size = new Size(217, 33);
            panel3.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Location = new Point(0, 46);
            panel2.Name = "panel2";
            panel2.Size = new Size(216, 18);
            panel2.TabIndex = 8;
            // 
            // transferOptionCombobox
            // 
            transferOptionCombobox.Items.AddRange(new object[] { "Wyślij passed", "Usuń wszystkie", "Wyślij wszystkie" });
            transferOptionCombobox.Location = new Point(125, 184);
            transferOptionCombobox.Name = "transferOptionCombobox";
            transferOptionCombobox.Size = new Size(86, 23);
            transferOptionCombobox.TabIndex = 21;
            transferOptionCombobox.Visible = false;
            // 
            // statisticsControl
            // 
            statisticsControl.Location = new Point(3, 175);
            statisticsControl.Name = "statisticsControl";
            statisticsControl.Size = new Size(111, 125);
            statisticsControl.Statistics = null;
            statisticsControl.TabIndex = 22;
            // 
            // timer3000ms
            // 
            timer3000ms.Enabled = true;
            timer3000ms.Interval = 3000;
            timer3000ms.Tick += timer3000ms_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(217, 304);
            Controls.Add(statisticsControl);
            Controls.Add(transferOptionCombobox);
            Controls.Add(breakdownButton);
            Controls.Add(testReportTransferSwitchButton);
            Controls.Add(detailsButton);
            Controls.Add(topFailuresButton);
            Controls.Add(menuStrip1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button topFailuresButton;
        private Label label4;
        private Label operatorLoginLabel;
        private Label stationNameLabel;
        private Label label1;
        private Button detailsButton;
        private Button breakdownButton;
        private MenuStrip menuStrip1;
        private Button testReportTransferSwitchButton;
        private Button logOutButton;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem iodirsToolStripMenuItem;
        private ToolStripMenuItem mesToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem verify3510ToolStripMenuItem;
        private ComboBox transferOptionCombobox;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private Panel panel6;
        private Panel panel5;
        private ToolStripMenuItem inputToolStripMenuItem;
        private ToolStripMenuItem outputToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem httpToolStripMenuItem;
        private ToolStripMenuItem ftpToolStripMenuItem;
        private StatisticsControl statisticsControl;
        private System.Windows.Forms.Timer timer3000ms;
    }
}

