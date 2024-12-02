using System;
using System.Windows.Forms;
using Vintasoft.WinTwain.ImageEncoders;

namespace TwainAdvancedDemo
{
    /// <summary>
    /// A form that allows to view and edit the PDF encoder settings.
    /// </summary>
	public partial class PdfSaveSettingsForm : Form
    {

        #region Constructors

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



        #region Methods

        private void okButton_Click(object sender, EventArgs e)
        {
            _saveAllImages = saveAllImagesRadioButton.Checked;

            _encoderSettings.PdfMultiPage = addToDocumentRadioButton.Checked;
            _encoderSettings.PdfACompatible = pdfACompatibleCheckBox.Checked;
            _encoderSettings.PdfDocumentInfo.Author = pdfAuthorTextBox.Text;
            _encoderSettings.PdfDocumentInfo.Title = pdfTitleTextBox.Text;

            if (noneCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.None;
            else if (ccittCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.CcittFax;
            else if (lzwCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.LZW;
            else if (jpegCompressionRadioButton.Checked)
            {
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.JPEG;
                _encoderSettings.JpegQuality = (int)jpegQualityNumericUpDown.Value;
            }
            else if (zipCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.ZIP;
            else if (autoCompressionRadioButton.Checked)
                _encoderSettings.PdfImageCompression = TwainPdfImageCompression.Auto;

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