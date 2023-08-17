using DofusPacketManager.Network.Messages.game.chat;
using DofusPacketManager.Networking;
using DofusPacketManager.Networking.Messages;
using System;
using System.Windows.Forms;

namespace DofusPacketManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkManager nM = new NetworkManager();
            PacketParser.Instance.OnMessageRecieved += PacketParser_OnMessageRecieved;
            nM.StartSniffing();
        }

        private void PacketParser_OnMessageRecieved(object sender, Network.Messages.NetworkMessageReceivedEventArgs e)
        {
            e.RecievedMessage.OnDeserialized += RecievedMessage_OnDeserialized;
        }

        private void RecievedMessage_OnDeserialized(object sender, EventArgs e)
        {
            ChatServerMessage d = (ChatServerMessage)sender;
            this.Invoke(new MethodInvoker(() => richTextBox1.Text += $"Content : {d._content}\n"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.StopSniffing();
        }
    }
}
