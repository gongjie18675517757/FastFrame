using FastFrame.Infrastructure.MessageBus;

namespace FastFrame.Service.Events
{
    public class DoMainMessage<T> : Message<T>
    {
        public DoMainMessage(string typeName, MsgType category, T content)
            : base(category, content)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }
}
