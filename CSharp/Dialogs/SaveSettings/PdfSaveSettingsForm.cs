using System;
using System.Windows.Forms;
using Vintasoft.Twain;
using Vintasoft.Twain.ImageEncoders;

namespace TwainAdvancedDemo
{
	public partial class PdfSaveSettingsForm : Form
    {

        #region Properties

        bool _saveAllImages = false;
        public bool SaveAllImages
        {
            get { return _saveAllImages; }
        }

        TwainPdfEncoderSettings _encoderSettings = new TwainPdfEncoderSettings();
        public TwainPdfEncoderSettings EncoderSettings
		{
            get { return _encoderSettings; }
		}

        #endregion


        #region Constructor

        public PdfSaveSettingsForm(bool isFileExist)
		{
			InitializeComponent();

			if (!isFileExist)
			{
				createNewDocumentRadioButton.Checked = true;
				addToDocumentRadioButton.Enabled = false;
			}
        }

        #endregion


        #region Methods

        private void okButton_Click(object sender, EventArgs e)
		{
            _saveAllImages = saveAllImagesRadioButton.Checked;

            _encoderSettings.PdfMultiPage = addToDocumentRadioButton.Checked;
            _encoderSettings.PdfACompatible = pdfACompatibleCheckBox.Checked;
            _encoderSettings.PdfDocumentInfo.Author = pdfAuthorTextBox.Text;
            _encoderSettings.PdfDocumentInfo.Title = pdfTitleTextBox.Text;

			if (noneCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = PdfImageCompression.None;
			else if (ccittCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = PdfImageCompression.CcittFax;
			else if (lzwCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = PdfImageCompression.LZW;
			else if (jpegCompressionRadioButton.Checked)
			{
                _encoderSettings.PdfImageCompression = PdfImageCompression.JPEG;
                _encoderSettings.JpegQuality = (int)jpegQualityNumericUpDown.Value;
			}
			else if (zipCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = PdfImageCompression.ZIP;
			else if (autoCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = PdfImageCompression.Auto;

			DialogResult = DialogResult.OK;
		}

		private void EnableJpegCompressionQuality(object sender, EventArgs e)
		{
			gbJpegCompression.Enabled = true;
		}

		private void DisableJpegCompressionQuality(object sender, EventArgs e)
		{
			gbJpegCompression.Enabled = false;
        }

        #endregion

    }
}