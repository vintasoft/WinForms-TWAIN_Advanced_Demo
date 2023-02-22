using System;
using System.Windows.Forms;
using Vintasoft.WinTwain.ImageEncoders;

namespace TwainAdvancedDemo
{
    /// <summary>
    /// A form that allows to view and edit the JPEG encoder settings.
    /// </summary>
    public partial class JpegSaveSettingsForm : Form
    {

        public JpegSaveSettingsForm()
        {
            InitializeComponent();
        }



        TwainJpegEncoderSettings _encoderSettings = new TwainJpegEncoderSettings();
        public TwainJpegEncoderSettings EncoderSettings
        {
            get { return _encoderSettings; }
        }



        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                _encoderSettings.JpegQuality = (int)qualityNumericUpDown.Value;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}