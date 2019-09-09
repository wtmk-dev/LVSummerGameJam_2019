using System.Collections.Generic;

namespace WTMK.Command
{
    public abstract class InvokerFactory
    {
        protected Dictionary<InvokerType, IInvoker> invokers;

        protected InvokerFactory()
        {
            //get all types of type invoker
            invokers = new Dictionary<InvokerType, IInvoker>();
        }

        public virtual IInvoker Build(InvokerType type)
        {
            return invokers[type];
        }
    }
}