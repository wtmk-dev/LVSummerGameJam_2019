namespace WTMK.Command
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }
}