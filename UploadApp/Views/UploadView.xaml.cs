using System;
using System.Windows;
using System.Windows.Controls;
using EventBus;
using RepozytoriumKlient.Tools;
using UploadApp.Actions;
using UploadClient.Events;
using UploadClient.Tools;
using WpfPluginBase;

namespace UploadApp.Views
{
    public partial class UploadView : UserControl
    {
        public UploadView()
        {
            InitializeComponent();
            SimpleEventBus.GetDefaultEventBus().Register(this);
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
        
        private void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            string chooseFile = FileChooser.ChooseFile();
            if (chooseFile == null) return;

            HandleFileOpen(chooseFile);
        }
        
        private void HandleFileOpen(string filePath)
        {
            SelectFileAction selectFileAction = new SelectFileAction(filePath);
            SimpleEventBus.GetDefaultEventBus().Post(new DoActionEvent(selectFileAction), TimeSpan.Zero);
        }

        [EventSubscriber]
        public void HandlePropertiesChangedEvent(ContextPropertiesChangedEvent propertiesChangedEvent)
        {
            if (!propertiesChangedEvent.Key.Equals(SelectFileAction.ActionContextKey)) return;

            string simpleFileName = FileHelper.GetFileName(propertiesChangedEvent.NewValue.ToString());
            if (simpleFileName == null || simpleFileName.Equals(""))
                simpleFileName = Properties.Resources.Text_Select_File_To_Upload;
            Application.Current.Dispatcher.Invoke(
                () => { UploadButton.Content = simpleFileName; });
            
        }

    }
}