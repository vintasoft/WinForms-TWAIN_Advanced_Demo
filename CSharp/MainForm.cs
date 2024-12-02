﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using Vintasoft.WinTwain;
using Vintasoft.WinTwain.ImageEncoders;

namespace TwainAdvancedDemo
{
    public partial class MainForm : Form
    {

        #region Fields

        /// <summary>
        /// TWAIN device manager.
        /// </summary>
        DeviceManager _deviceManager;

        /// <summary>
        /// Current device.
        /// </summary>
        Device _currentDevice;

        /// <summary>
        /// Indicates that device is acquiring image(s).
        /// </summary>
        bool _isImageAcquiring;

        /// <summary>
        /// Acquired image collection.
        /// </summary>
        AcquiredImageCollection _images = new AcquiredImageCollection();

        /// <summary>
        /// Current image index in acquired image collection.
        /// </summary>
        int _imageIndex = -1;

        /// <summary>
        /// Determines that image acquistion must be canceled because application's form is closing.
        /// </summary>
        bool _cancelTransferBecauseFormIsClosing;

        #endregion



        #region Constructors

        public MainForm()
        {
            // register the evaluation license for VintaSoft TWAIN .NET SDK
            Vintasoft.Twain.TwainGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");

            InitializeComponent();

            this.Text = string.Format("VintaSoft TWAIN Advanced Demo v{0}", TwainGlobalSettings.ProductVersion);

            transferModeComboBox.SelectedIndex = 1;
            pixelTypeComboBox.SelectedIndex = 1;
            resolutionComboBox.SelectedIndex = 1;

            // get country and language for TWAIN device manager
            CountryCode country;
            LanguageType language;
            GetCountryAndLanguage(out country, out language);

            // create TWAIN device manager
            _deviceManager = new DeviceManager(this, this.Handle, country, language);

            UpdateUI();
        }
        #endregion



        #region Methods

        #region Device manager

        /// <summary>
        /// Opens the TWAIN device manager.
        /// </summary>
        private void openDeviceManagerButton_Click(object sender, EventArgs e)
        {
            // clear list of devices
            devicesComboBox.Items.Clear();

            // if TWAIN device manager is open - close the TWAIN device manager
            if (_deviceManager.State == DeviceManagerState.Opened)
            {
                // close the TWAIN device manager
                _deviceManager.Close();

                // change text on this button
                openDeviceManagerButton.Text = "Open device manager";
            }

            // if TWAIN device manager is closed - open the TWAIN device manager
            else
            {
                try
                {
                    // try to find the TWAIN device manager specified by user
                    _deviceManager.IsTwain2Compatible = twain2CompatibleCheckBox.Checked;
                }
                catch (Exception ex)
                {
                    // show dialog with error message
                    MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // open a HTML page with article describing how to solve the problem
                    OpenBrowser("https://www.vintasoft.com/docs/vstwain-dotnet/Programming-Twain-Device_Manager.html");

                    return;
                }

                // TWAIN device manager is found

                // if check box value should be updated
                if (twain2CompatibleCheckBox.Checked != _deviceManager.IsTwain2Compatible)
                    // update check box value 
                    twain2CompatibleCheckBox.Checked = _deviceManager.IsTwain2Compatible;

                // if TWAIN device manager 2.x must be used
                if (twain2CompatibleCheckBox.Checked)
                {
                    // get path to the TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK
                    string twainDsmDllCustomPath = GetTwainDsmCustomPath(IntPtr.Size == 4);
                    // if file exist
                    if (twainDsmDllCustomPath != null)
                        // specify that SDK should use TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK
                        _deviceManager.TwainDllPath = twainDsmDllCustomPath;
                }

                // if TWAIN device manager 2.x (64-bit) is used
                if (IntPtr.Size == 8 && _deviceManager.IsTwain2Compatible)
                {
                    if (!InitTwain2DeviceManagerMode())
                        return;
                }

                try
                {
                    // open the TWAIN device manager
                    _deviceManager.Open();
                }
                catch (TwainDeviceManagerException ex)
                {
                    if (_deviceManager.State != DeviceManagerState.Closed)
                    {
                        // close the TWAIN device manager
                        _deviceManager.Close();
                    }

                    // show dialog with error message
                    MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // open a HTML page with article describing how to solve the problem
                    OpenBrowser("https://www.vintasoft.com/docs/vstwain-dotnet/Programming-Twain-Device_Manager.html");

                    return;
                }

                DeviceCollection devices = _deviceManager.Devices;
                // for each available TWAIN device
                for (int i = 0; i < devices.Count; i++)
                {
                    // add the TWAIN device name to a combo box
                    devicesComboBox.Items.Add(devices[i].Info.ProductName);

                    // if TWAIN device is default device
                    if (devices[i] == _deviceManager.DefaultDevice)
                        // select TWAIN device in a combo box
                        devicesComboBox.SelectedIndex = i;
                }

                // change text on this button
                openDeviceManagerButton.Text = "Close device manager";
            }

            UpdateUI();
        }

        /// <summary>
        /// Initializes the device manager mode.
        /// </summary>
        private bool InitTwain2DeviceManagerMode()
        {
            // create a form that allows to view and edit mode of 64-bit TWAIN2 device manager
            using (SelectDeviceManagerModeForm form = new SelectDeviceManagerModeForm())
            {
                // initialize form
                form.StartPosition = FormStartPosition.CenterParent;
                form.Owner = this;
                form.Use32BitDevices = _deviceManager.Are32BitDevicesUsed;

                // show dialog
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // get path to the TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK
                    string twainDsmDllCustomPath = GetTwainDsmCustomPath(form.Use32BitDevices);
                    // if file exist
                    if (twainDsmDllCustomPath != null)
                        // specify that SDK should use TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK
                        _deviceManager.TwainDllPath = twainDsmDllCustomPath;

                    // if device manager mode is changed
                    if (form.Use32BitDevices != _deviceManager.Are32BitDevicesUsed)
                    {
                        try
                        {
                            // if 32-bit devices must be used
                            if (form.Use32BitDevices)
                                _deviceManager.Use32BitDevices();
                            else
                                _deviceManager.Use64BitDevices();
                        }
                        catch (TwainDeviceManagerException ex)
                        {
                            // show dialog with error message
                            MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        #endregion


        #region Devices

        /// <summary>
        /// Selects the default device.
        /// </summary>
        private void selectDefaultDeviceButton_Click(object sender, EventArgs e)
        {
            // show the TWAIN device selection dialog
            if (_deviceManager.ShowDefaultDeviceSelectionDialog())
            {
                DeviceCollection devices = _deviceManager.Devices;
                // for each device
                for (int i = 0; i < devices.Count; i++)
                {
                    // if device is default device
                    if (devices[i] == _deviceManager.DefaultDevice)
                        // select device in a combo box
                        devicesComboBox.SelectedIndex = i;
                }
            }
        }

        #endregion


        #region Device

        /// <summary>
        /// Gets information about device and device capabilities.
        /// </summary>
        private void getDeviceInfoButton_Click(object sender, EventArgs e)
        {
            try
            {
                // find a device by device name
                Device device = _deviceManager.Devices.Find((string)devicesComboBox.SelectedItem);

                // show dialog with information about device and device capabilities
                DevCapsForm deviceCapabilitiesForm = new DevCapsForm(device);
                deviceCapabilitiesForm.ShowDialog();
            }
            catch (TwainDeviceException ex)
            {
                MessageBox.Show(GetFullExceptionMessage(ex), "Device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TwainDeviceCapabilityException ex)
            {
                MessageBox.Show(GetFullExceptionMessage(ex), "Device capability error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enables/disables User Interface of device.
        /// </summary>
        private void showUICheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modalUICheckBox.Enabled = showUICheckBox.Checked;
            disableAfterScanCheckBox.Enabled = showUICheckBox.Checked;

            adfGroupBox.Enabled = !showUICheckBox.Checked;
        }

        /// <summary>
        /// Transfer mode is changed.
        /// </summary>
        private void transferModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (transferModeComboBox.SelectedIndex == 0)
                imageAcquisitionProgressBar.Visible = false;
            else
                imageAcquisitionProgressBar.Visible = true;
        }

        /// <summary>
        /// Enables/disables the automatic document feeder.
        /// </summary>
        private void useAdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useDuplexCheckBox.Enabled = useAdfCheckBox.Checked;
            imagesToAcquireNumericUpDown.Enabled = useAdfCheckBox.Checked;
        }

        #endregion


        #region Image acquisition

        /// <summary>
        /// Starts the image acquisition.
        /// </summary>
        private void acquireImageButton_Click(object sender, EventArgs e)
        {
            // specify that image acquisition is started
            _isImageAcquiring = true;
            // update UI
            UpdateUI();

            try
            {
                if (_currentDevice != null)
                    // unsubscribe from the device events
                    UnsubscribeFromDeviceEvents(_currentDevice);

                // find the device by device name
                string deviceName = (string)devicesComboBox.SelectedItem;
                Device device = _deviceManager.Devices.Find(deviceName);
                if (device == null)
                {
                    // specify that image acquisition is finished
                    _isImageAcquiring = false;

                    MessageBox.Show(string.Format("Device '{0}' is not found.", deviceName), "TWAIN device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentDevice = device;
                // subscribe to the device events
                SubscribeToDeviceEvents(_currentDevice);

                // set the image acquisition parameters
                device.ShowUI = showUICheckBox.Checked;
                device.ModalUI = modalUICheckBox.Checked;
                device.ShowIndicators = showIndicatorsCheckBox.Checked;
                device.DisableAfterAcquire = disableAfterScanCheckBox.Checked;

                // trasfer mode
                if (transferModeComboBox.SelectedIndex == 0)
                    device.TransferMode = TransferMode.Native;
                else
                    device.TransferMode = TransferMode.Memory;

                try
                {
                    // open the device
                    device.Open();
                }
                catch (TwainException ex)
                {
                    // specify that image acquisition is finished
                    _isImageAcquiring = false;

                    MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // set device capabilities

                // pixel type
                PixelType pixelType = PixelType.BW;
                try
                {
                    if (pixelTypeComboBox.SelectedIndex == 1)
                        pixelType = PixelType.Gray;
                    else if (pixelTypeComboBox.SelectedIndex == 2)
                        pixelType = PixelType.RGB;

                    if (device.PixelType != pixelType)
                        device.PixelType = pixelType;
                }
                catch (TwainException)
                {
                    MessageBox.Show(string.Format("Pixel type '{0}' is not supported.", pixelType), "TWAIN device");
                }

                // unit of measure
                try
                {
                    if (device.UnitOfMeasure != UnitOfMeasure.Inches)
                        device.UnitOfMeasure = UnitOfMeasure.Inches;
                }
                catch (TwainException)
                {
                    MessageBox.Show("Unit of measure 'Inches' is not supported.", "TWAIN device");
                }

                // resolution
                Resolution resolution = new Resolution(100, 100);
                try
                {
                    if (resolutionComboBox.SelectedIndex == 1)
                        resolution = new Resolution(150, 150);
                    else if (resolutionComboBox.SelectedIndex == 2)
                        resolution = new Resolution(200, 200);
                    else if (resolutionComboBox.SelectedIndex == 3)
                        resolution = new Resolution(300, 300);
                    else if (resolutionComboBox.SelectedIndex == 4)
                        resolution = new Resolution(600, 600);

                    if (device.Resolution.GetXResolutionInDpi() != resolution.GetXResolutionInDpi() ||
                        device.Resolution.GetYResolutionInDpi() != resolution.GetYResolutionInDpi())
                        device.Resolution = resolution;
                }
                catch (TwainException)
                {
                }

                // if device is Fujitsu scanner
                if (device.Info.ProductName.ToUpper().StartsWith("FUJITSU"))
                {
                    DeviceCapability undefinedImageSizeCap = device.Capabilities.Find(DeviceCapabilityId.IUndefinedImageSize);
                    // if undefined image size is supported
                    if (undefinedImageSizeCap != null)
                    {
                        try
                        {
                            // enable undefined image size feature
                            undefinedImageSizeCap.SetValue(true);
                        }
                        catch (TwainDeviceCapabilityException)
                        {
                        }
                    }
                }

                try
                {
                    // if ADF present
                    if (!device.Info.IsWIA && device.HasFeeder)
                    {
                        // enable/disable ADF if necessary
                        try
                        {
                            if (device.DocumentFeeder.Enabled != useAdfCheckBox.Checked)
                                device.DocumentFeeder.Enabled = useAdfCheckBox.Checked;
                        }
                        catch (TwainDeviceCapabilityException)
                        {
                        }

                        // enable/disable duplex if necessary
                        try
                        {
                            if (device.DocumentFeeder.DuplexEnabled != useDuplexCheckBox.Checked)
                                device.DocumentFeeder.DuplexEnabled = useDuplexCheckBox.Checked;
                        }
                        catch (TwainDeviceCapabilityException)
                        {
                        }
                    }

                    if (!acquireAllImagesRadioButton.Checked)
                    {
                        Int16 xferCount = (Int16)imagesToAcquireNumericUpDown.Value;

                        // patch for "bug" in Kodak drivers
                        if (device.Info.ProductName.ToUpper().StartsWith("KODAK"))
                        {
                            if (device.ShowUI && xferCount == 1)
                                xferCount = -1;
                        }

                        device.XferCount = xferCount;
                    }
                }
                catch (TwainException ex)
                {
                    MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device");
                }

                // if device supports asynchronous events
                if (device.IsAsyncEventsSupported)
                {
                    try
                    {
                        // enable all asynchronous events supported by device
                        device.AsyncEvents = device.GetSupportedAsyncEvents();
                    }
                    catch
                    {
                    }
                }


                try
                {
                    // start image acquition process
                    device.Acquire();
                }
                catch (TwainException ex)
                {
                    // specify that image acquisition is finished
                    _isImageAcquiring = false;

                    MessageBox.Show(GetFullExceptionMessage(ex), "TWAIN device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            finally
            {
                // update UI
                UpdateUI();
            }
        }

        /// <summary>
        /// Subscribe to the device events.
        /// </summary>
        private void SubscribeToDeviceEvents(Device device)
        {
            device.ImageAcquiringProgress += new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired += new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanFailed += new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            device.AsyncEvent += new EventHandler<DeviceAsyncEventArgs>(device_AsyncEvent);
            device.ScanFinished += new EventHandler(device_ScanFinished);
        }

        /// <summary>
        /// Unsubscribe from the device events.
        /// </summary>
        private void UnsubscribeFromDeviceEvents(Device device)
        {
            device.ImageAcquiringProgress -= new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            device.AsyncEvent -= new EventHandler<DeviceAsyncEventArgs>(device_AsyncEvent);
            device.ScanFinished -= new EventHandler(device_ScanFinished);
        }

        /// <summary>
        /// Image acquiring progress is changed.
        /// </summary>
        private void device_ImageAcquiringProgress(object sender, ImageAcquiringProgressEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                // cancel image acquisition
                _currentDevice.CancelTransfer();
                return;
            }

            imageAcquisitionProgressBar.Value = (int)e.Progress;

            if (imageAcquisitionProgressBar.Value == 100)
            {
                imageAcquisitionProgressBar.Value = 0;
            }
        }

        /// <summary>
        /// Image is acquired.
        /// </summary>
        private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                // cancel image acquisition
                _currentDevice.CancelTransfer();
                return;
            }

            _images.Add(e.Image);

            SetCurrentImage(_images.Count - 1);
        }

        /// <summary>
        /// Scan is failed.
        /// </summary>
        private void device_ScanFailed(object sender, ScanFailedEventArgs e)
        {
            // show error message
            MessageBox.Show(e.ErrorString, "Scan is failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// An asynchronous event was generated by device.
        /// </summary>
        private void device_AsyncEvent(object sender, DeviceAsyncEventArgs e)
        {
            switch (e.DeviceEvent)
            {
                case DeviceEventId.PaperJam:
                    MessageBox.Show("Paper is jammed.");
                    break;

                case DeviceEventId.CheckDeviceOnline:
                    MessageBox.Show("Check that device is online.");
                    break;

                case DeviceEventId.CheckBattery:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, BatteryMinutes={2}, BatteryPercentage={3}",
                        e.DeviceName, e.DeviceEvent, e.BatteryMinutes, e.BatteryPercentage));
                    break;

                case DeviceEventId.CheckPowerSupply:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, PowerSupply={2}",
                        e.DeviceName, e.DeviceEvent, e.PowerSupply));
                    break;

                case DeviceEventId.CheckResolution:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, Resolution={2}",
                        e.DeviceName, e.DeviceEvent, e.Resolution));
                    break;

                case DeviceEventId.CheckFlash:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, FlashUsed={2}",
                        e.DeviceName, e.DeviceEvent, e.FlashUsed));
                    break;

                case DeviceEventId.CheckAutomaticCapture:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, AutomaticCapture={2}, TimeBeforeFirstCapture={3}, TimeBetweenCaptures={4}",
                        e.DeviceName, e.DeviceEvent, e.AutomaticCapture, e.TimeBeforeFirstCapture, e.TimeBetweenCaptures));
                    break;

                default:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}",
                        e.DeviceName, e.DeviceEvent));
                    break;
            }

            // if device is enabled or transferring images
            if (_currentDevice.State >= DeviceState.Enabled)
                return;

            // close the device
            _currentDevice.Close();
        }

        /// <summary>
        /// Scan is finished.
        /// </summary>
        private void device_ScanFinished(object sender, EventArgs e)
        {
            // close the device
            _currentDevice.Close();

            // specify that image acquisition is finished
            _isImageAcquiring = false;
            // update UI
            UpdateUI();
        }

        #endregion


        #region Acquired images

        #region Navigate

        /// <summary>
        /// Shows previous acquired image.
        /// </summary>
        private void previousImageButton_Click(object sender, EventArgs e)
        {
            SetCurrentImage(_imageIndex - 1);

            UpdateUI();
        }

        /// <summary>
        /// Shows next acquired image.
        /// </summary>
        private void nextImageButton_Click(object sender, EventArgs e)
        {
            SetCurrentImage(_imageIndex + 1);

            UpdateUI();
        }

        #endregion


        #region Preview

        /// <summary>
        /// Gets the information about current image.
        /// </summary>
        private string GetCurrentImageInfo(int index, AcquiredImage acquiredImage)
        {
            ImageInfo imageInfo = acquiredImage.ImageInfo;
            return string.Format("Image {0} from {1} ({2} x {3}, {4} bpp, {5})", index + 1, _images.Count, imageInfo.Width, imageInfo.Height, imageInfo.BitCount, imageInfo.Resolution);
        }

        /// <summary>
        /// Sets the current image.
        /// </summary>
        private void SetCurrentImage(int index)
        {
            lock (this)
            {
                // dispose previous image if necessary
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pictureBox1.Size = new Size(0, 0);

                // get the image from the internal buffer of the device if image is present
                if (index >= 0)
                {
                    AcquiredImage acquiredImage = _images[index];

                    pictureBox1.Image = acquiredImage.GetAsBitmap(true);
                    SetImageScrolls();

                    imageInfoLabel.Text = GetCurrentImageInfo(index, acquiredImage);

                    _imageIndex = index;
                }
                // show "No images" text
                else
                {
                    imageInfoLabel.Text = "No images";

                    _imageIndex = -1;
                }

                //
                UpdateUI();
            }
        }

        /// <summary>
        /// Sets image scrolls.
        /// </summary>
        private void SetImageScrolls()
        {
            if (stretchImageCheckBox.Checked)
            {
                pictureBox1.Size = new Size(pictureBoxPanel.Size.Width - 2, pictureBoxPanel.Size.Height - 2);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (pictureBox1.Image != null)
            {
                pictureBox1.Size = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Changes preview mode of current image.
        /// </summary>
        private void stretchImageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetImageScrolls();
        }

        /// <summary>
        /// Form of application is resized.
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            SetImageScrolls();
        }

        #endregion


        #region Process

        /// <summary>
        /// Processes acquired image.
        /// </summary>
        private void rotateImageButton_Click(object sender, EventArgs e)
        {
            // get reference to current image
            AcquiredImage currentImage = _images[_imageIndex];

            // process current image
            ImageProcessingForm form1 = new ImageProcessingForm(currentImage);
            form1.ShowDialog();

            // update current image
            SetCurrentImage(_imageIndex);
        }

        #endregion


        #region Save

        /// <summary>
        /// Saves acquired image.
        /// </summary>
        private void saveImageButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            bool isFileExist = File.Exists(saveFileDialog1.FileName);
            bool saveAllImages = false;
            try
            {
                TwainImageEncoderSettings encoderSettings = null;

                switch (saveFileDialog1.FilterIndex)
                {
                    case 2:	// JPEG
                        using (JpegSaveSettingsForm dlg = new JpegSaveSettingsForm())
                        {
                            if (dlg.ShowDialog() != DialogResult.OK)
                                return;

                            encoderSettings = dlg.EncoderSettings;
                        }
                        break;

                    case 4: // TIFF
                        using (TiffSaveSettingsForm dlg = new TiffSaveSettingsForm(isFileExist))
                        {
                            if (dlg.ShowDialog() != DialogResult.OK)
                                return;

                            saveAllImages = dlg.SaveAllImages;
                            encoderSettings = dlg.EncoderSettings;
                        }
                        break;

                    case 5: // PDF
                        using (PdfSaveSettingsForm dlg = new PdfSaveSettingsForm(isFileExist))
                        {
                            if (dlg.ShowDialog() != DialogResult.OK)
                                return;

                            saveAllImages = dlg.SaveAllImages;
                            encoderSettings = dlg.EncoderSettings;
                        }
                        break;
                }

                Cursor = Cursors.WaitCursor;

                string filename = saveFileDialog1.FileName;
                // save all images to specified file
                if (saveAllImages)
                {
                    // save first image
                    _images[0].Save(filename, encoderSettings);

                    // enable multipage support if necessary
                    if (saveFileDialog1.FilterIndex == 4)
                        ((TwainTiffEncoderSettings)encoderSettings).TiffMultiPage = true;
                    else if (saveFileDialog1.FilterIndex == 5)
                        ((TwainPdfEncoderSettings)encoderSettings).PdfMultiPage = true;

                    // save second and next images
                    for (int i = 1; i < _images.Count; i++)
                        _images[i].Save(filename, encoderSettings);
                }
                // save only current image to specified file
                else
                    _images[_imageIndex].Save(filename, encoderSettings);

                Cursor = Cursors.Default;

                MessageBox.Show("Image(s) saved successfully!");
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(GetFullExceptionMessage(ex), "Saving error");
            }
        }

        #endregion


        #region Upload

        /// <summary>
        /// Uploads acquired image.
        /// </summary>
        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            UploadForm uploadForm = new UploadForm(_images[_imageIndex]);
            uploadForm.ShowDialog();
        }

        #endregion


        #region Delete, clear

        /// <summary>
        /// Removes image from acquired image collection.
        /// </summary>
        private void deleteImageButton_Click(object sender, EventArgs e)
        {
            // dispose the image
            _images[_imageIndex].Dispose();

            // remove image from image collection
            _images.RemoveAt(_imageIndex);

            if (_imageIndex >= (_images.Count - 1))
                _imageIndex = _images.Count - 1;

            SetCurrentImage(_imageIndex);
        }

        /// <summary>
        /// Clears acquired image collection.
        /// </summary>
        private void clearImagesButton_Click(object sender, EventArgs e)
        {
            // dispose all images from image collection and clear the image collection
            _images.ClearAndDisposeItems();

            _imageIndex = -1;

            SetCurrentImage(_imageIndex);
        }

        #endregion

        #endregion


        /// <summary>
        /// Returns country and language for TWAIN device manager.
        /// </summary>
        /// <remarks>
        /// Unfortunately only KODAK scanners allow to set country and language.
        /// </remarks>
        private void GetCountryAndLanguage(out CountryCode country, out LanguageType language)
        {
            country = CountryCode.Usa;
            language = LanguageType.EnglishUsa;

            switch (CultureInfo.CurrentUICulture.Parent.IetfLanguageTag)
            {
                case "de":
                    country = CountryCode.Germany;
                    language = LanguageType.German;
                    break;

                case "es":
                    country = CountryCode.Spain;
                    language = LanguageType.Spanish;
                    break;

                case "fr":
                    country = CountryCode.France;
                    language = LanguageType.French;
                    break;

                case "it":
                    country = CountryCode.Italy;
                    language = LanguageType.Italian;
                    break;

                case "pt":
                    country = CountryCode.Portugal;
                    language = LanguageType.Portuguese;
                    break;

                case "ru":
                    country = CountryCode.Russia;
                    language = LanguageType.Russian;
                    break;
            }
        }


        /// <summary>
        /// Update UI.
        /// </summary>
        private void UpdateUI()
        {
            bool isDeviceManagerOpened = _deviceManager.State == DeviceManagerState.Opened;
            bool hasDevices = false;
            if (isDeviceManagerOpened)
            {
                if (_deviceManager.Devices.Count > 0)
                    hasDevices = true;
            }

            twain2CompatibleCheckBox.Enabled = _deviceManager.State == DeviceManagerState.Closed;
            openDeviceManagerButton.Enabled = !_isImageAcquiring;
            selectDefaultDeviceButton.Enabled = isDeviceManagerOpened && !_isImageAcquiring;

            acquireImageButton.Enabled = isDeviceManagerOpened && hasDevices && !_isImageAcquiring;

            devicesComboBox.Enabled = isDeviceManagerOpened && !_isImageAcquiring;
            getDeviceInfoButton.Enabled = isDeviceManagerOpened && hasDevices && !_isImageAcquiring;

            imageGroupBox.Enabled = isDeviceManagerOpened && hasDevices && !_isImageAcquiring;
            userInterfaceGroupBox.Enabled = isDeviceManagerOpened && hasDevices && !_isImageAcquiring;
            adfGroupBox.Enabled = isDeviceManagerOpened && hasDevices && !_isImageAcquiring;


            // image navigation/processing

            if (_imageIndex > 0)
                previousImageButton.Enabled = true;
            else
                previousImageButton.Enabled = false;

            if (_imageIndex < (_images.Count - 1))
                nextImageButton.Enabled = true;
            else
                nextImageButton.Enabled = false;

            processImageButton.Enabled = _images.Count > 0;
            saveImageButton.Enabled = _images.Count > 0;
            uploadImageButton.Enabled = _images.Count > 0;
            deleteImageButton.Enabled = _images.Count > 0;
            clearImagesButton.Enabled = _images.Count > 0;

            stretchImageCheckBox.Enabled = _images.Count > 0;
        }

        /// <summary>
        /// Application's form is closing.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currentDevice != null)
            {
                // if image is acquiring
                if (_currentDevice.State > DeviceState.Enabled)
                {
                    // cancel image acquisition
                    _currentDevice.CancelTransfer();
                    // specify that form must be closed when image acquisition is canceled
                    _cancelTransferBecauseFormIsClosing = true;
                    // cancel form closing
                    e.Cancel = true;
                    return;
                }

                // unsubscribe from device events
                UnsubscribeFromDeviceEvents(_currentDevice);
                // close the device
                _currentDevice.Close();
                _currentDevice = null;
            }

            // close the device manager
            _deviceManager.Close();
            // dispose the device manager
            _deviceManager.Dispose();

            // dispose all images from image collection and clear the image collection
            _images.ClearAndDisposeItems();
        }

        /// <summary>
        /// Returns the message of exception and inner exceptions.
        /// </summary>
        private string GetFullExceptionMessage(Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(ex.Message);

            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                if (ex.Message != innerException.Message)
                    sb.AppendLine(string.Format("Inner exception: {0}", innerException.Message));
                innerException = innerException.InnerException;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Opens the browser with specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        private void OpenBrowser(string url)
        {
            ProcessStartInfo pi = new ProcessStartInfo("cmd", string.Format("/c start {0}", url));
            pi.CreateNoWindow = true;
            pi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(pi);
        }

        /// <summary>
        /// Returns path to the TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK.
        /// </summary>
        /// <param name="use32BitDevice">The value indicating whether the 32-bit TWAIN device must be used.</param>
        /// <returns>The path to the TWAIN device manager 2.x from installation of VintaSoft TWAIN .NET SDK.</returns>
        private string GetTwainDsmCustomPath(bool use32BitDevice)
        {
            string twainFolderName = "TWAINDSM64";
            if (use32BitDevice)
                twainFolderName = "TWAINDSM32";

            string[] binFolderPaths = { @"..\..\Bin", @"..\..\..\..\..\Bin", @"..\..\..\..\..\..\Bin" };
            string binFolderPath = null;
            for (int i = 0; i < binFolderPaths.Length; i++)
            {
                if (Directory.Exists(Path.Combine(binFolderPaths[i], twainFolderName)))
                {
                    binFolderPath = binFolderPaths[i];
                    break;
                }
            }

            if (binFolderPath != null)
            {
                if (use32BitDevice)
                    // get path to the TWAIN device manager 2.x (32-bit) from installation of VintaSoft TWAIN .NET SDK
                    return Path.Combine(binFolderPath, "TWAINDSM32", "TWAINDSM.DLL");
                else
                    // get path to the TWAIN device manager 2.x (64-bit) from installation of VintaSoft TWAIN .NET SDK
                    return Path.Combine(binFolderPath, "TWAINDSM64", "TWAINDSM.DLL");
            }

            return null;
        }

        #endregion

    }
}
