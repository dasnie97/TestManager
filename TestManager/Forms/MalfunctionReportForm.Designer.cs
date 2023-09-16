namespace TestManager
{
    partial class DowntimeReportForm
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
            sendReportButton = new Button();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            actionTextbox = new TextBox();
            descriptionTextbox = new TextBox();
            descriptionComboBox = new ComboBox();
            actionTakenComboBox = new ComboBox();
            technicianNamesComboBox = new ComboBox();
            breakdownTimeStartedLabel = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            label1 = new Label();
            malfunctionTimeOptionalTextBox = new TextBox();
            label4 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // sendReportButton
            // 
            sendReportButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            sendReportButton.Location = new Point(63, 276);
            sendReportButton.Margin = new Padding(4, 3, 4, 3);
            sendReportButton.Name = "sendReportButton";
            sendReportButton.Size = new Size(94, 28);
            sendReportButton.TabIndex = 7;
            sendReportButton.Text = "Wyślij";
            sendReportButton.UseVisualStyleBackColor = true;
            sendReportButton.Click += sendReportButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(15, 16);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(55, 16);
            label5.TabIndex = 12;
            label5.Text = "Technik";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(27, 178);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(109, 16);
            label3.TabIndex = 13;
            label3.Text = "Akcje naprawcze";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(27, 79);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 16);
            label2.TabIndex = 14;
            label2.Text = "Opis awarii";
            // 
            // actionTextbox
            // 
            actionTextbox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            actionTextbox.Location = new Point(27, 226);
            actionTextbox.Margin = new Padding(4, 3, 4, 3);
            actionTextbox.Multiline = true;
            actionTextbox.Name = "actionTextbox";
            actionTextbox.PlaceholderText = "Dodatkowe informacje";
            actionTextbox.ScrollBars = ScrollBars.Vertical;
            actionTextbox.Size = new Size(166, 44);
            actionTextbox.TabIndex = 6;
            // 
            // descriptionTextbox
            // 
            descriptionTextbox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            descriptionTextbox.Location = new Point(27, 127);
            descriptionTextbox.Margin = new Padding(4, 3, 4, 3);
            descriptionTextbox.Multiline = true;
            descriptionTextbox.Name = "descriptionTextbox";
            descriptionTextbox.PlaceholderText = "Dodatkowe informacje";
            descriptionTextbox.ScrollBars = ScrollBars.Vertical;
            descriptionTextbox.Size = new Size(166, 44);
            descriptionTextbox.TabIndex = 4;
            // 
            // descriptionComboBox
            // 
            descriptionComboBox.FormattingEnabled = true;
            descriptionComboBox.Items.AddRange(new object[] { "Zwieszony tester", "Spady na kroku", "Brak logów", "Brak komunikacji", "Problem ze skanowaniem", "Problem z drukarką", "Inne" });
            descriptionComboBox.Location = new Point(27, 98);
            descriptionComboBox.Name = "descriptionComboBox";
            descriptionComboBox.Size = new Size(166, 23);
            descriptionComboBox.TabIndex = 3;
            // 
            // actionTakenComboBox
            // 
            actionTakenComboBox.FormattingEnabled = true;
            actionTakenComboBox.Items.AddRange(new object[] { "Restart komputera", "Restart aplikacji", "Restart urządzenia", "Rekontakt fikstury", "Czyszczenie fikstury", "Wymiana szpilek", "Wymiana przekaźników" });
            actionTakenComboBox.Location = new Point(27, 197);
            actionTakenComboBox.Name = "actionTakenComboBox";
            actionTakenComboBox.Size = new Size(166, 23);
            actionTakenComboBox.TabIndex = 5;
            // 
            // technicianNamesComboBox
            // 
            technicianNamesComboBox.FormattingEnabled = true;
            technicianNamesComboBox.Items.AddRange(new object[] { "FADICH", "MIKUCH", "MAKOSI", "JAMALI", "RADABR" });
            technicianNamesComboBox.Location = new Point(12, 34);
            technicianNamesComboBox.Name = "technicianNamesComboBox";
            technicianNamesComboBox.Size = new Size(73, 23);
            technicianNamesComboBox.TabIndex = 0;
            // 
            // breakdownTimeStartedLabel
            // 
            breakdownTimeStartedLabel.Dock = DockStyle.Fill;
            breakdownTimeStartedLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            breakdownTimeStartedLabel.Location = new Point(0, 0);
            breakdownTimeStartedLabel.Margin = new Padding(4, 0, 4, 0);
            breakdownTimeStartedLabel.Name = "breakdownTimeStartedLabel";
            breakdownTimeStartedLabel.Size = new Size(81, 22);
            breakdownTimeStartedLabel.TabIndex = 1;
            breakdownTimeStartedLabel.Text = "label1";
            breakdownTimeStartedLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // panel1
            // 
            panel1.Controls.Add(breakdownTimeStartedLabel);
            panel1.Location = new Point(134, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(81, 22);
            panel1.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(95, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(120, 16);
            label1.TabIndex = 12;
            label1.Text = "Czas trwania awarii";
            // 
            // malfunctionTimeOptionalTextBox
            // 
            malfunctionTimeOptionalTextBox.Location = new Point(134, 51);
            malfunctionTimeOptionalTextBox.Name = "malfunctionTimeOptionalTextBox";
            malfunctionTimeOptionalTextBox.PlaceholderText = "[min]";
            malfunctionTimeOptionalTextBox.Size = new Size(81, 23);
            malfunctionTimeOptionalTextBox.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
            label4.Location = new Point(152, 77);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(63, 13);
            label4.TabIndex = 12;
            label4.Text = "Opcjonalnie";
            // 
            // DowntimeReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(219, 309);
            Controls.Add(malfunctionTimeOptionalTextBox);
            Controls.Add(panel1);
            Controls.Add(technicianNamesComboBox);
            Controls.Add(actionTakenComboBox);
            Controls.Add(descriptionComboBox);
            Controls.Add(sendReportButton);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(actionTextbox);
            Controls.Add(descriptionTextbox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DowntimeReportForm";
            Text = "Awaria";
            Shown += DowntimeReport_Shown;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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