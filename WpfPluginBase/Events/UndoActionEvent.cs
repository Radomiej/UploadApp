using WpfPluginBase;

namespace UploadClient.Events
{
    public class UndoActionEvent
    {
        public IBaseAction ActionToUndo { get; }

        public UndoActionEvent(IBaseAction actionToUndo)
        {
            ActionToUndo = actionToUndo;
        }
    }
}