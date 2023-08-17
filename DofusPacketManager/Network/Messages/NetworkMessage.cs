using System.Data;

namespace DofusPacketManager.Networking.Messages
{
    public abstract class NetworkMessage
    {
        public abstract ushort MessageID { get; }
        public abstract bool InstanceID { get; }
        public abstract void Deserialize(IDataReader Reader);
    }
}
