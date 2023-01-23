namespace TestManager
{
    partial class StatisticsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel8 = new System.Windows.Forms.Panel();
            this.FailedQtyLabel = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.YieldLabel = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.TestedQtyLabel = new System.Windows.Forms.Label();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Tomato;
            this.panel8.Controls.Add(this.FailedQtyLabel);
            this.panel8.Location = new System.Drawing.Point(3, 35);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(105, 29);
            this.panel8.TabIndex = 17;
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
            this.panel9.Location = new System.Drawing.Point(2, 67);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(106, 56);
            this.panel9.TabIndex = 18;
            // 
            // YieldLabel
            // 
            this.YieldLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.YieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YieldLabel.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YieldLabel.Location = new System.Drawing.Point(0, 0);
            this.YieldLabel.Margin = new System.Windows.Forms.Padding(0);
            this.YieldLabel.Name = "YieldLabel";
            this.YieldLabel.Size = new System.Drawing.Size(106, 56);
            this.YieldLabel.TabIndex = 0;
            this.YieldLabel.Text = "0";
            this.YieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SkyBlue;
            this.panel7.Controls.Add(this.TestedQtyLabel);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(105, 29);
            this.panel7.TabIndex = 16;
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
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel7);
            this.Name = "Statistics";
            this.Size = new System.Drawing.Size(111, 125);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel8;
        private Label FailedQtyLabel;
        private Panel panel9;
        private Label YieldLabel;
        private Panel panel7;
        private Label TestedQtyLabel;
    }
}
