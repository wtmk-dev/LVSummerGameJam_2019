using System.Collections.Generic;
namespace WTMK.Command
{
    public interface IInvoker
    {
        void SetCommand(ICommand command);
        ICommand GetCommand(CommandType commandType);
    }
}