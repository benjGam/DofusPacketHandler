using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Types
{
    public abstract class NetworkType
    {
        public abstract ushort TypeID { get; }
        protected abstract void DeserializeType(IDataReader Reader);
    }
}
