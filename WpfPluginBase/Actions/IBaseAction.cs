namespace WpfPluginBase
{
    public interface IBaseAction
    {
        void DoAction();
        void UndoAction();
        void RedoAction();
    }
}