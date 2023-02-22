using System;
using System.Windows.Forms;
using Vintasoft.WinTwain.ImageEncoders;

namespace TwainAdvancedDemo
{
    /// <summary>
    /// A form that allows to view and edit the TIFF encoder settings.
    /// </summary>
	public partial class TiffSaveSettingsForm : Form
    {

        #region Constructors

        public TiffSaveSettingsForm(bool isFileExist)
        {
            InitializeComponent();

            if (!isFileExist)
            {
                createNewDocumentaddToDocumentRadioButton.Checked = true;
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

        TwainTiffEncoderSettings _encoderSettings = new TwainTiffEncoderSettings();
        public TwainTiffEncoderSettings EncoderSettings
        {
            get { return _encoderSettings; }
        }

        #endregion



        #region Methods

        private void okButton_Click(object sender, EventArgs e)
        {
            _saveAllImages = saveAllImagesaddToDocumentRadioButton.Checked;

            try
            {
                _encoderSettings.TiffMultiPage = addToDocumentRadioButton.Checked;

                if (noneCompressionRadioButton.Checked)
                    _encoderSettings.TiffCompression = TiffCompression.None;
                else if (ccittCompressionRadioButton.Checked)
                    _encoderSettings.TiffCompression = TiffCompression.CCITGroup4;
                else if (lzwCompressionRadioButton.Checked)
                    _encoderSettings.TiffCompression = TiffCompression.LZW;
                else if (jpegCompressionRadioButton.Checked)
                {
                    _encoderSettings.TiffCompression = TiffCompression.JPEG;
                    _encoderSettings.JpegQuality = (int)jpegQualityNumericUpDown.Value;
                }
                else if (zipCompressionRadioButton.Checked)
                    _encoderSettings.TiffCompression = TiffCompression.ZIP;
                else if (autoCompressionRadioButton.Checked)
                    _encoderSettings.TiffCompression = TiffCompression.Auto;

                if (useStripsRadioButton.Checked)
                    _encoderSettings.UseTiles = false;
                else
                    _encoderSettings.UseTiles = true;
                _encoderSettings.RowsPerStrip = (int)rowsPerStripNumericUpDown.Value;
                _encoderSettings.TileSize = (int)tileSizeNumericUpDown.Value;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void useStripsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rowsPerStripLabel.Visible = true;
            rowsPerStripNumericUpDown.Visible = true;
            tileSizeLabel.Visible = false;
            tileSizeNumericUpDown.Visible = false;
        }

        private void useTilesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rowsPerStripLabel.Visible = false;
            rowsPerStripNumericUpDown.Visible = false;
            tileSizeLabel.Visible = true;
            tileSizeNumericUpDown.Visible = true;
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