using System;
using System.Windows;
using System.Windows.Controls;
using EventBus;
using UploadClient.Events;
using UploadClient.Tools;

namespace UploadApp.Views
{
    public partial class UploadView : UserControl
    {
        private string _selectedFile;

        public UploadView()
        {
            InitializeComponent();
        }

        private void FileUpload_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                if (files != null) HandleFileOpen(files[0]);
            }
        }

        private void HandleFileOpen(string filePath)
        {
            if (_selectedFile != null)
            {
                SimpleEventBus.GetDefaultEventBus().Post(new FileDeselectedEvent(_selectedFile), TimeSpan.Zero);
                _selectedFile = null;
            }

            _selectedFile = filePath;
            UploadButton.Content = FileHelper.GetFileName(filePath);
            SimpleEventBus.GetDefaultEventBus().Post(new FileSelectedEvent(filePath), TimeSpan.Zero);
        }
    }
}