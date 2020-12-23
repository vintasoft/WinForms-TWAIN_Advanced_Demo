namespace TwainAdvancedDemo
{
    partial class SelectDeviceManagerModeForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.use32BitDevicesRadioButton = new System.Windows.Forms.RadioButton();
            this.use64BitDevicesRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(84, 42);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(165, 42);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // use32BitDevicesRadioButton
            // 
            this.use32BitDevicesRadioButton.AutoSize = true;
            this.use32BitDevicesRadioButton.Location = new System.Drawing.Point(12, 12);
            this.use32BitDevicesRadioButton.Name = "use32BitDevicesRadioButton";
            this.use32BitDevicesRadioButton.Size = new System.Drawing.Size(113, 17);
            this.use32BitDevicesRadioButton.TabIndex = 2;
            this.use32BitDevicesRadioButton.TabStop = true;
            this.use32BitDevicesRadioButton.Text = "Use 32-bit devices";
            this.use32BitDevicesRadioButton.UseVisualStyleBackColor = true;
            // 
            // use64BitDevicesRadioButton
            // 
            this.use64BitDevicesRadioButton.AutoSize = true;
            this.use64BitDevicesRadioButton.Location = new System.Drawing.Point(131, 12);
            this.use64BitDevicesRadioButton.Name = "use64BitDevicesRadioButton";
            this.use64BitDevicesRadioButton.Size = new System.Drawing.Size(113, 17);
            this.use64BitDevicesRadioButton.TabIndex = 3;
            this.use64BitDevicesRadioButton.TabStop = true;
            this.use64BitDevicesRadioButton.Text = "Use 64-bit devices";
            this.use64BitDevicesRadioButton.UseVisualStyleBackColor = true;
            // 
            // SelectDeviceManagerModeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(252, 77);
            this.Controls.Add(this.use64BitDevicesRadioButton);
            this.Controls.Add(this.use32BitDevicesRadioButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "SelectDeviceManagerModeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Device Manager Mode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton use32BitDevicesRadioButton;
        private System.Windows.Forms.RadioButton use64BitDevicesRadioButton;
    }
}