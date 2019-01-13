using WpfPluginBase;
using WpfPluginBase.Context;

namespace UploadApp.Actions
{
    public class SelectFileAction : IBaseAction
    {
        public static readonly string ActionContextKey = "selectedFile";
        public string FilePath { get; }

        private string _oldPath;
        public SelectFileAction(string filePath)
        {
            FilePath = filePath;
        }

        public void DoAction()
        {
            _oldPath = ContextEngine.Instance.GetStringContextObject(ActionContextKey);
            ContextEngine.Instance.SetContextObject(ActionContextKey, FilePath);
        }

        public void UndoAction()
        {
            ContextEngine.Instance.SetContextObject(ActionContextKey, _oldPath);
        }

        public void RedoAction()
        {
            throw new System.NotImplementedException();
        }
    }
}