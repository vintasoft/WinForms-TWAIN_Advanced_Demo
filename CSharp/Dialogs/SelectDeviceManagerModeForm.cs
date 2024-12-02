using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace TwainAdvancedDemo
{
    /// <summary>
    /// A form that allows to view and edit mode of 64-bit TWAIN2 device manager.
    /// </summary>
    public partial class SelectDeviceManagerModeForm : Form
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectDeviceManagerModeForm"/> class.
        /// </summary>
        public SelectDeviceManagerModeForm()
        {
            InitializeComponent();

            if (IntPtr.Size == 4)
                use32BitDevicesRadioButton.Checked = true;
            else
                use64BitDevicesRadioButton.Checked = true;
        }



        /// <summary>
        /// Gets or sets a value indicating whether the 64-bit TWAIN2 device manager must use 32-bit devices.
        /// </summary>
        /// <value>
        /// <b>true</b> if 64-bit TWAIN2 device manager must use 32-bit devices; otherwise, <b>false</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Use32BitDevices
        {
            get
            {
                return use32BitDevicesRadioButton.Checked;
            }
            set
            {
                use32BitDevicesRadioButton.Checked = value;
            }
        }

    }
}
