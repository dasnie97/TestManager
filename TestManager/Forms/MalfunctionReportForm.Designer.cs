namespace TestManager
{
    partial class MalfunctionReportForm
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
            this.sendReportButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actionTextbox = new System.Windows.Forms.TextBox();
            this.descriptionTextbox = new System.Windows.Forms.TextBox();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.actionTakenComboBox = new System.Windows.Forms.ComboBox();
            this.technicianNamesComboBox = new System.Windows.Forms.ComboBox();
            this.breakdownTimeStartedLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.malfunctionTimeOptionalTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendReportButton
            // 
            this.sendReportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sendReportButton.Location = new System.Drawing.Point(63, 276);
            this.sendReportButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sendReportButton.Name = "sendReportButton";
            this.sendReportButton.Size = new System.Drawing.Size(94, 28);
            this.sendReportButton.TabIndex = 15;
            this.sendReportButton.Text = "Wyślij";
            this.sendReportButton.UseVisualStyleBackColor = true;
            this.sendReportButton.Click += new System.EventHandler(this.sendReportButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(13, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Technik";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(27, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Akcje naprawcze";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(27, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Opis awarii";
            // 
            // actionTextbox
            // 
            this.actionTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.actionTextbox.Location = new System.Drawing.Point(27, 226);
            this.actionTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.actionTextbox.Multiline = true;
            this.actionTextbox.Name = "actionTextbox";
            this.actionTextbox.PlaceholderText = "Dodatkowe informacje";
            this.actionTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.actionTextbox.Size = new System.Drawing.Size(166, 44);
            this.actionTextbox.TabIndex = 10;
            // 
            // descriptionTextbox
            // 
            this.descriptionTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.descriptionTextbox.Location = new System.Drawing.Point(27, 127);
            this.descriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.descriptionTextbox.Multiline = true;
            this.descriptionTextbox.Name = "descriptionTextbox";
            this.descriptionTextbox.PlaceholderText = "Dodatkowe informacje";
            this.descriptionTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextbox.Size = new System.Drawing.Size(166, 44);
            this.descriptionTextbox.TabIndex = 11;
            // 
            // descriptionComboBox
            // 
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Items.AddRange(new object[] {
            "Zwieszony tester",
            "Spady na kroku",
            "Brak logów",
            "Brak komunikacji",
            "Problem ze skanowaniem",
            "Problem z drukarką",
            "Inne"});
            this.descriptionComboBox.Location = new System.Drawing.Point(27, 98);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(166, 23);
            this.descriptionComboBox.TabIndex = 17;
            // 
            // actionTakenComboBox
            // 
            this.actionTakenComboBox.FormattingEnabled = true;
            this.actionTakenComboBox.Items.AddRange(new object[] {
            "Restart komputera",
            "Restart aplikacji",
            "Restart urządzenia",
            "Rekontakt fikstury",
            "Czyszczenie fikstury",
            "Wymiana szpilek",
            "Wymiana przekaźników"});
            this.actionTakenComboBox.Location = new System.Drawing.Point(27, 197);
            this.actionTakenComboBox.Name = "actionTakenComboBox";
            this.actionTakenComboBox.Size = new System.Drawing.Size(166, 23);
            this.actionTakenComboBox.TabIndex = 18;
            // 
            // technicianNamesComboBox
            // 
            this.technicianNamesComboBox.FormattingEnabled = true;
            this.technicianNamesComboBox.Items.AddRange(new object[] {
            "FADICH",
            "MIKUCH",
            "MAKOSI",
            "JAMALI",
            "RADABR"});
            this.technicianNamesComboBox.Location = new System.Drawing.Point(12, 28);
            this.technicianNamesComboBox.Name = "technicianNamesComboBox";
            this.technicianNamesComboBox.Size = new System.Drawing.Size(73, 23);
            this.technicianNamesComboBox.TabIndex = 18;
            // 
            // breakdownTimeStartedLabel
            // 
            this.breakdownTimeStartedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breakdownTimeStartedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.breakdownTimeStartedLabel.Location = new System.Drawing.Point(0, 0);
            this.breakdownTimeStartedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.breakdownTimeStartedLabel.Name = "breakdownTimeStartedLabel";
            this.breakdownTimeStartedLabel.Size = new System.Drawing.Size(81, 22);
            this.breakdownTimeStartedLabel.TabIndex = 1;
            this.breakdownTimeStartedLabel.Text = "label1";
            this.breakdownTimeStartedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.breakdownTimeStartedLabel);
            this.panel1.Location = new System.Drawing.Point(134, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 22);
            this.panel1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(95, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Czas trwania awarii";
            // 
            // malfunctionTimeOptionalTextBox
            // 
            this.malfunctionTimeOptionalTextBox.Location = new System.Drawing.Point(134, 51);
            this.malfunctionTimeOptionalTextBox.Name = "malfunctionTimeOptionalTextBox";
            this.malfunctionTimeOptionalTextBox.PlaceholderText = "[min]";
            this.malfunctionTimeOptionalTextBox.Size = new System.Drawing.Size(81, 23);
            this.malfunctionTimeOptionalTextBox.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(152, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Opcjonalnie";
            // 
            // MalfunctionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 309);
            this.Controls.Add(this.malfunctionTimeOptionalTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.technicianNamesComboBox);
            this.Controls.Add(this.actionTakenComboBox);
            this.Controls.Add(this.descriptionComboBox);
            this.Controls.Add(this.sendReportButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.actionTextbox);
            this.Controls.Add(this.descriptionTextbox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MalfunctionReport";
            this.Text = "MalfunctionReport";
            this.Shown += new System.EventHandler(this.MalfunctionReport_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button sendReportButton;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox actionTextbox;
        private TextBox descriptionTextbox;
        private ComboBox descriptionComboBox;
        private ComboBox actionTakenComboBox;
        private ComboBox technicianNamesComboBox;
        private Label breakdownTimeStartedLabel;
        private System.Windows.Forms.Timer timer1;
        private Panel panel1;
        private Label label1;
        private TextBox malfunctionTimeOptionalTextBox;
        private Label label4;
    }
}