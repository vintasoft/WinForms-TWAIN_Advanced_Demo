﻿using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Vintasoft.WinTwain;
using Vintasoft.WinTwain.ImageEncoders;
using Vintasoft.WinTwain.ImageUploading.Ftp;
using Vintasoft.WinTwain.ImageUploading.Http;

namespace TwainAdvancedDemo
{
    public partial class UploadForm : Form
    {

        #region Fields

        /// <summary>
        /// Acquired image to upload.
        /// </summary>
        AcquiredImage _acquiredImageToUpload;

        /// <summary>
        /// FTP uploader.
        /// </summary>
        FtpUpload _ftpUpload = null;
        /// <summary>
        /// HTTP uploader.
        /// </summary>
        HttpUpload _httpUpload = null;

        #endregion



        #region Constructors

        public UploadForm(AcquiredImage acquiredImageToUpload)
        {
            InitializeComponent();

            _acquiredImageToUpload = acquiredImageToUpload;
        }

        #endregion



        #region Methdos

        #region FTP upload

        /// <summary>
        /// Start image uploading process to FTP server.
        /// </summary>
        private void ftpUploadButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm;
            mainForm = (Owner as MainForm);
            ftpUploadButton.Enabled = false;
            ftpUploadCancelButton.Enabled = true;
            ftpUploadProgressBar.Value = 0;

            try
            {
                _ftpUpload = new FtpUpload(this);
                _ftpUpload.StatusChanged += new EventHandler<Vintasoft.WinTwain.ImageUploading.Ftp.StatusChangedEventArgs>(_ftpUpload_StatusChanged);
                _ftpUpload.ProgressChanged += new EventHandler<Vintasoft.WinTwain.ImageUploading.Ftp.ProgressChangedEventArgs>(_ftpUpload_ProgressChanged);
                _ftpUpload.Completed += new EventHandler<Vintasoft.WinTwain.ImageUploading.Ftp.CompletedEventArgs>(_ftpUpload_Completed);

                _ftpUpload.Host = ftpServerTextBox.Text;

                int ftpServerPort = 21;
                try
                {
                    ftpServerPort = int.Parse(ftpServerPortTextBox.Text);
                }
                catch
                {
                }
                _ftpUpload.Port = ftpServerPort;

                _ftpUpload.User = ftpUserTextBox.Text;
                _ftpUpload.Password = ftpPasswordTextBox.Text;
                _ftpUpload.PassiveMode = flagPassMode.Checked;
                _ftpUpload.Timeout = 5000;
                _ftpUpload.Path = ftpPathTextBox.Text;
                _ftpUpload.AddFile(ftpFileNameTextBox.Text, _acquiredImageToUpload.GetAsStream(GetImageFileFormat(ftpFileNameTextBox.Text)));
                _ftpUpload.PostData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ftpUploadButton.Enabled = true;
                ftpUploadCancelButton.Enabled = false;
            }
            finally
            {
                ftpUploadProgressBar.Maximum = _ftpUpload.BytesTotal;
            }
        }

        /// <summary>
        /// Cancel image uploading process.
        /// </summary>
        private void ftpUploadCancelButton_Click(object sender, EventArgs e)
        {
            _ftpUpload.Abort();
        }

        /// <summary>
        /// Status of uploading process is changed.
        /// </summary>
        private void _ftpUpload_StatusChanged(object sender, Vintasoft.WinTwain.ImageUploading.Ftp.StatusChangedEventArgs e)
        {
            ftpStatusLabel.Text = e.StatusString;
        }

        /// <summary>
        /// Progress of uploading process is changed.
        /// </summary>
        private void _ftpUpload_ProgressChanged(object sender, Vintasoft.WinTwain.ImageUploading.Ftp.ProgressChangedEventArgs e)
        {
            ftpUploadProgressBar.Value = e.BytesUploaded;
            if (e.StatusCode == Vintasoft.WinTwain.ImageUploading.Ftp.StatusCode.SendingData)
                ftpStatusLabel.Text = string.Format("{0}{1} Uploaded {2} bytes from {3} bytes.", e.StatusString, Environment.NewLine, e.BytesUploaded, e.BytesTotal);
        }

        /// <summary>
        /// Uploading process is completed.
        /// </summary>
        private void _ftpUpload_Completed(object sender, Vintasoft.WinTwain.ImageUploading.Ftp.CompletedEventArgs e)
        {
            if (e.ErrorCode == Vintasoft.WinTwain.ImageUploading.Ftp.ErrorCode.None)
                MessageBox.Show("FTP: Image is uploaded successfully!", "FTP");
            else
                MessageBox.Show(e.ErrorString, "FTP error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ftpUploadButton.Enabled = true;
            ftpUploadCancelButton.Enabled = false;
        }

        #endregion


        #region HTTP upload

        /// <summary>
        /// Start image uploading process to FTP server.
        /// </summary>
        private void httpUploadButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm;
            mainForm = (Owner as MainForm);
            httpUploadButton.Enabled = false;
            httpUploadCancelButton.Enabled = true;
            httpUploadProgressBar.Value = 0;

            System.Net.ServicePointManager.Expect100Continue = false;

            try
            {
                _httpUpload = new HttpUpload(this);
                _httpUpload.StatusChanged += new EventHandler<Vintasoft.WinTwain.ImageUploading.Http.StatusChangedEventArgs>(_httpUpload_StatusChanged);
                _httpUpload.ProgressChanged += new EventHandler<Vintasoft.WinTwain.ImageUploading.Http.ProgressChangedEventArgs>(_httpUpload_ProgressChanged);
                _httpUpload.Completed += new EventHandler<Vintasoft.WinTwain.ImageUploading.Http.CompletedEventArgs>(_httpUpload_Completed);

                _httpUpload.Url = httpUrlTextBox.Text;
                _httpUpload.Timeout = 5000;
                _httpUpload.UseDefaultCredentials = true;
                _httpUpload.AddTextField(httpTextField1TextBox.Text, httpTextField1ValueTextBox.Text);
                _httpUpload.AddTextField(httpTextField2TextBox.Text, httpTextField2ValueTextBox.Text);
                _httpUpload.AddFileField(httpFileFieldTextBox.Text, httpFileFieldValueTextBox.Text, _acquiredImageToUpload.GetAsStream(GetImageFileFormat(httpFileFieldValueTextBox.Text)));
                _httpUpload.PostData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HTTP error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                httpUploadButton.Enabled = true;
                httpUploadCancelButton.Enabled = false;
            }
            finally
            {
                httpUploadProgressBar.Maximum = _httpUpload.BytesTotal;
            }
        }

        /// <summary>
        /// Cancel image uploading process.
        /// </summary>
        private void httpUploadCancelButton_Click(object sender, EventArgs e)
        {
            _httpUpload.Abort();
        }

        /// <summary>
        /// Status of uploading process is changed.
        /// </summary>
        private void _httpUpload_StatusChanged(object sender, Vintasoft.WinTwain.ImageUploading.Http.StatusChangedEventArgs e)
        {
            httpStatusLabel.Text = e.StatusString;
        }

        /// <summary>
        /// Progress of uploading process is changed.
        /// </summary>
        private void _httpUpload_ProgressChanged(object sender, Vintasoft.WinTwain.ImageUploading.Http.ProgressChangedEventArgs e)
        {
            httpUploadProgressBar.Value = e.BytesUploaded;
            if (e.StatusCode == Vintasoft.WinTwain.ImageUploading.Http.StatusCode.Sending)
                httpStatusLabel.Text = string.Format("{0}{3} Uploaded {1}  bytes from {2} bytes", e.StatusString, e.BytesUploaded, e.BytesTotal, Environment.NewLine);
        }

        /// <summary>
        /// Uploading process is completed.
        /// </summary>
        private void _httpUpload_Completed(object sender, Vintasoft.WinTwain.ImageUploading.Http.CompletedEventArgs e)
        {
            if (e.ErrorCode == 0)
            {
                if (e.ResponseCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("HTTP: Image is uploaded successfully!", "HTTP");
                    MessageBox.Show("Response content: " + e.ResponseContent, "HTTP");
                }
                else if (e.ResponseCode == HttpStatusCode.NoContent)
                {
                    MessageBox.Show("HTTP: Image is uploaded successfully!", "HTTP");
                }
                else
                {
                    MessageBox.Show("Response code: " + e.ResponseCode, "HTTP");
                    MessageBox.Show("Response string: " + e.ResponseString, "HTTP");
                }
            }
            else
            {
                MessageBox.Show(e.ErrorString, "HTTP error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            httpUploadButton.Enabled = true;
            httpUploadCancelButton.Enabled = false;
        }

        #endregion


        private TwainImageEncoderSettings GetImageFileFormat(string filename)
        {
            string filenameExt = Path.GetExtension(filename);
            switch (filenameExt)
            {
                case ".bmp":
                    return new TwainBmpEncoderSettings();

                case ".pdf":
                    return new TwainPdfEncoderSettings();

                case ".png":
                    return new TwainPngEncoderSettings();

                case ".tif":
                case ".tiff":
                    return new TwainTiffEncoderSettings();
            }

            return new TwainJpegEncoderSettings();
        }

        #endregion

    }
}