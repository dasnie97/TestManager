namespace TestManager
{
    partial class LogInForm
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
            label1 = new Label();
            operatorLoginTextbox = new TextBox();
            acceptButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 18);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(287, 31);
            label1.TabIndex = 0;
            label1.Text = "Logowanie operatora";
            // 
            // operatorLoginTextbox
            // 
            operatorLoginTextbox.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            operatorLoginTextbox.Location = new Point(88, 64);
            operatorLoginTextbox.Margin = new Padding(4, 3, 4, 3);
            operatorLoginTextbox.Name = "operatorLoginTextbox";
            operatorLoginTextbox.Size = new Size(135, 31);
            operatorLoginTextbox.TabIndex = 1;
            operatorLoginTextbox.KeyPress += operatorLoginTextbox_KeyPress;
            // 
            // acceptButton
            // 
            acceptButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            acceptButton.Location = new Point(82, 111);
            acceptButton.Margin = new Padding(4, 3, 4, 3);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(147, 37);
            acceptButton.TabIndex = 4;
            acceptButton.Text = "Zatwierdź";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 174);
            ControlBox = false;
            Controls.Add(acceptButton);
            Controls.Add(operatorLoginTextbox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            Name = "LogInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Logowanie";
            TopMost = true;
            KeyDown += LoginForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox operatorLoginTextbox;
        private Button acceptButton;
    }
}