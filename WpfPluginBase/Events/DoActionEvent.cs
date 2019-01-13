using WpfPluginBase;

namespace UploadClient.Events
{
    public class DoActionEvent
    {
        public IBaseAction ActionToDo { get; }

        public DoActionEvent(IBaseAction actionToDo)
        {
            ActionToDo = actionToDo;
        }
    }
}