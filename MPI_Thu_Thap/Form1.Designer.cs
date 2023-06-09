namespace MPI_Thu_Thap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnStart = new Button();
            label1 = new Label();
            label2 = new Label();
            lblStatus = new Label();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            label3 = new Label();
            cbbLimit = new ComboBox();
            SuspendLayout();
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = Color.FromArgb(192, 0, 192);
            btnStart.Location = new Point(39, 130);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(419, 41);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start Process";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(39, 22);
            label1.Name = "label1";
            label1.Size = new Size(380, 33);
            label1.TabIndex = 1;
            label1.Text = "Hệ thống thu thập gói thầu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 189);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 2;
            label2.Text = "Trạng thái:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(84, 189);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(74, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Chưa kết nối";
            // 
            // backgroundWorker2
            // 
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 84);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 4;
            label3.Text = "Giới hạn next trang";
            // 
            // cbbLimit
            // 
            cbbLimit.FormattingEnabled = true;
            cbbLimit.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            cbbLimit.Location = new Point(153, 81);
            cbbLimit.Name = "cbbLimit";
            cbbLimit.Size = new Size(305, 23);
            cbbLimit.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 218);
            Controls.Add(cbbLimit);
            Controls.Add(label3);
            Controls.Add(lblStatus);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnStart;
        private Label label1;
        private Label label2;
        private Label lblStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Label label3;
        private ComboBox cbbLimit;
    }
}