namespace TwainAdvancedDemo
{
    partial class UploadForm
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
            this.httpUrlTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.httpStatusLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ftpFileNameTextBox = new System.Windows.Forms.TextBox();
            this.ftpServerTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.httpUploadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ftpPathTextBox = new System.Windows.Forms.TextBox();
            this.ftpPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ftpUserTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ftpUploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.flagPassMode = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ftpServerPortTextBox = new System.Windows.Forms.TextBox();
            this.ftpUploadButton = new System.Windows.Forms.Button();
            this.ftpUploadCancelButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ftpStatusLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // httpUrlTextBox
            // 
            this.httpUrlTextBox.Location = new System.Drawing.Point(48, 24);
            this.httpUrlTextBox.Name = "httpUrlTextBox";
            this.httpUrlTextBox.Size = new System.Drawing.Size(248, 20);
            this.httpUrlTextBox.TabIndex = 0;
            this.httpUrlTextBox.Text = "https://demos.vintasoft.com/AspNetCoreTwainScanningDemo/api/ImageUploadApi";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "URL:";
            // 
            // httpStatusLabel
            // 
            this.httpStatusLabel.Location = new System.Drawing.Point(56, 184);
            this.httpStatusLabel.Name = "httpStatusLabel";
            this.httpStatusLabel.Size = new System.Drawing.Size(240, 24);
            this.httpStatusLabel.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 16);
            this.label14.TabIndex = 16;
            this.label14.Text = "File name:";
            // 
            // ftpFileNameTextBox
            // 
            this.ftpFileNameTextBox.Location = new System.Drawing.Point(72, 120);
            this.ftpFileNameTextBox.Name = "ftpFileNameTextBox";
            this.ftpFileNameTextBox.Size = new System.Drawing.Size(184, 20);
            this.ftpFileNameTextBox.TabIndex = 17;
            this.ftpFileNameTextBox.Text = "demo.jpg";
            // 
            // ftpServerTextBox
            // 
            this.ftpServerTextBox.Location = new System.Drawing.Point(72, 24);
            this.ftpServerTextBox.Name = "ftpServerTextBox";
            this.ftpServerTextBox.Size = new System.Drawing.Size(184, 20);
            this.ftpServerTextBox.TabIndex = 0;
            this.ftpServerTextBox.Text = "ftp.test.com";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Path:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(144, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // httpUploadButton
            // 
            this.httpUploadButton.Location = new System.Drawing.Point(108, 216);
            this.httpUploadButton.Name = "httpUploadButton";
            this.httpUploadButton.Size = new System.Drawing.Size(75, 23);
            this.httpUploadButton.TabIndex = 5;
            this.httpUploadButton.Text = "Upload";
            this.httpUploadButton.Click += new System.EventHandler(this.httpUploadButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "User:";
            // 
            // ftpPathTextBox
            // 
            this.ftpPathTextBox.Location = new System.Drawing.Point(72, 96);
            this.ftpPathTextBox.Name = "ftpPathTextBox";
            this.ftpPathTextBox.Size = new System.Drawing.Size(184, 20);
            this.ftpPathTextBox.TabIndex = 5;
            this.ftpPathTextBox.Text = "/imgs/";
            // 
            // ftpPasswordTextBox
            // 
            this.ftpPasswordTextBox.Location = new System.Drawing.Point(200, 72);
            this.ftpPasswordTextBox.Name = "ftpPasswordTextBox";
            this.ftpPasswordTextBox.PasswordChar = '*';
            this.ftpPasswordTextBox.Size = new System.Drawing.Size(56, 20);
            this.ftpPasswordTextBox.TabIndex = 4;
            this.ftpPasswordTextBox.Text = "guest";
            // 
            // ftpUserTextBox
            // 
            this.ftpUserTextBox.Location = new System.Drawing.Point(72, 72);
            this.ftpUserTextBox.Name = "ftpUserTextBox";
            this.ftpUserTextBox.Size = new System.Drawing.Size(64, 20);
            this.ftpUserTextBox.TabIndex = 3;
            this.ftpUserTextBox.Text = "guest";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // ftpUploadProgressBar
            // 
            this.ftpUploadProgressBar.Location = new System.Drawing.Point(8, 152);
            this.ftpUploadProgressBar.Name = "ftpUploadProgressBar";
            this.ftpUploadProgressBar.Size = new System.Drawing.Size(248, 23);
            this.ftpUploadProgressBar.TabIndex = 13;
            // 
            // flagPassMode
            // 
            this.flagPassMode.Location = new System.Drawing.Point(152, 48);
            this.flagPassMode.Name = "flagPassMode";
            this.flagPassMode.Size = new System.Drawing.Size(104, 16);
            this.flagPassMode.TabIndex = 2;
            this.flagPassMode.Text = "Passive mode";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Port:";
            // 
            // ftpServerPortTextBox
            // 
            this.ftpServerPortTextBox.Location = new System.Drawing.Point(72, 48);
            this.ftpServerPortTextBox.Name = "ftpServerPortTextBox";
            this.ftpServerPortTextBox.Size = new System.Drawing.Size(48, 20);
            this.ftpServerPortTextBox.TabIndex = 1;
            this.ftpServerPortTextBox.Text = "21";
            this.ftpServerPortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ftpUploadButton
            // 
            this.ftpUploadButton.Location = new System.Drawing.Point(48, 216);
            this.ftpUploadButton.Name = "ftpUploadButton";
            this.ftpUploadButton.Size = new System.Drawing.Size(75, 23);
            this.ftpUploadButton.TabIndex = 6;
            this.ftpUploadButton.Text = "Upload";
            this.ftpUploadButton.Click += new System.EventHandler(this.ftpUploadButton_Click);
            // 
            // ftpUploadCancelButton
            // 
            this.ftpUploadCancelButton.Enabled = false;
            this.ftpUploadCancelButton.Location = new System.Drawing.Point(139, 216);
            this.ftpUploadCancelButton.Name = "ftpUploadCancelButton";
            this.ftpUploadCancelButton.Size = new System.Drawing.Size(75, 23);
            this.ftpUploadCancelButton.TabIndex = 9;
            this.ftpUploadCancelButton.Text = "Cancel";
            this.ftpUploadCancelButton.Click += new System.EventHandler(this.ftpUploadCancelButton_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Status:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.httpUrlTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.httpStatusLabel);
            this.groupBox2.Controls.Add(this.httpUploadButton);
            this.groupBox2.Location = new System.Drawing.Point(278, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 248);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HTTP";
            // 
            // ftpStatusLabel
            // 
            this.ftpStatusLabel.Location = new System.Drawing.Point(56, 184);
            this.ftpStatusLabel.Name = "ftpStatusLabel";
            this.ftpStatusLabel.Size = new System.Drawing.Size(200, 24);
            this.ftpStatusLabel.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ftpFileNameTextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.ftpPathTextBox);
            this.groupBox1.Controls.Add(this.ftpPasswordTextBox);
            this.groupBox1.Controls.Add(this.ftpUserTextBox);
            this.groupBox1.Controls.Add(this.ftpServerTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ftpStatusLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ftpUploadProgressBar);
            this.groupBox1.Controls.Add(this.flagPassMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ftpServerPortTextBox);
            this.groupBox1.Controls.Add(this.ftpUploadButton);
            this.groupBox1.Controls.Add(this.ftpUploadCancelButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 248);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTP";
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(238, 261);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 19;
            this.exitButton.Text = "Exit";
            // 
            // UploadForm
            // 
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(591, 293);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.exitButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload image onto FTP or HTTP server";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox httpUrlTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label httpStatusLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox ftpFileNameTextBox;
        private System.Windows.Forms.TextBox ftpServerTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button httpUploadButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ftpPathTextBox;
        private System.Windows.Forms.TextBox ftpPasswordTextBox;
        private System.Windows.Forms.TextBox ftpUserTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar ftpUploadProgressBar;
        private System.Windows.Forms.CheckBox flagPassMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ftpServerPortTextBox;
        private System.Windows.Forms.Button ftpUploadButton;
        private System.Windows.Forms.Button ftpUploadCancelButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ftpStatusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button exitButton;
    }
}