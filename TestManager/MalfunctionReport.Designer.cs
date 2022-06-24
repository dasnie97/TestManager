namespace TestManager
{
    partial class MalfunctionReport
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actionTextbox = new System.Windows.Forms.TextBox();
            this.descriptionTextbox = new System.Windows.Forms.TextBox();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.actionTakenComboBox = new System.Windows.Forms.ComboBox();
            this.technicianComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.confirmButton.Location = new System.Drawing.Point(61, 296);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(94, 28);
            this.confirmButton.TabIndex = 15;
            this.confirmButton.Text = "Wyślij";
            this.confirmButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(81, 229);
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
            this.label3.Location = new System.Drawing.Point(51, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Podjęte działania";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(72, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Opis awarii";
            // 
            // actionTextbox
            // 
            this.actionTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.actionTextbox.Location = new System.Drawing.Point(25, 167);
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
            this.descriptionTextbox.Location = new System.Drawing.Point(25, 57);
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
            this.descriptionComboBox.Location = new System.Drawing.Point(25, 28);
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
            this.actionTakenComboBox.Location = new System.Drawing.Point(25, 138);
            this.actionTakenComboBox.Name = "actionTakenComboBox";
            this.actionTakenComboBox.Size = new System.Drawing.Size(166, 23);
            this.actionTakenComboBox.TabIndex = 18;
            // 
            // technicianComboBox
            // 
            this.technicianComboBox.FormattingEnabled = true;
            this.technicianComboBox.Items.AddRange(new object[] {
            "FADICH",
            "MIKUCH",
            "MAKOSI",
            "JAMALI",
            "RADABR"});
            this.technicianComboBox.Location = new System.Drawing.Point(55, 248);
            this.technicianComboBox.Name = "technicianComboBox";
            this.technicianComboBox.Size = new System.Drawing.Size(107, 23);
            this.technicianComboBox.TabIndex = 18;
            // 
            // MalfunctionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 341);
            this.Controls.Add(this.technicianComboBox);
            this.Controls.Add(this.actionTakenComboBox);
            this.Controls.Add(this.descriptionComboBox);
            this.Controls.Add(this.confirmButton);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button confirmButton;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox actionTextbox;
        private TextBox descriptionTextbox;
        private ComboBox descriptionComboBox;
        private ComboBox actionTakenComboBox;
        private ComboBox technicianComboBox;
    }
}