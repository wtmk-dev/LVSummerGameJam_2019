using System.Collections.Generic;

namespace WTMK.Command
{
    public abstract class CommandFactory
    {
        protected Dictionary<CommandType, ICommand> commands;

        protected CommandFactory()
        {
            //get all types of type command
            commands = new Dictionary<CommandType, ICommand>();
        }

        public virtual ICommand Build(CommandType type)
        {
            return commands[type];
        }
    }
}