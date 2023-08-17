using DofusPacketManager.Utils;
using System.Windows.Forms;

namespace DofusPacketManager.Networking.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        public MessageManager()
        {
            _messageInitializer = new MessageInitializer();
        }
    }
}
