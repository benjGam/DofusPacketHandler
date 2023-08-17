using DofusPacketManager.Network.Messages;
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
            MessageManager k = MessageManager.Instance;
            k.MessageBinder.Bind<ChatServerMessage>(ChatServerMessage_OnDeserialized, "OnDeserialized");
            nM.StartSniffing();
        }

        private void ChatServerMessage_OnDeserialized(object sender, EventArgs e)
        {
            ChatServerMessage k = (ChatServerMessage)sender;
            this.Invoke(new MethodInvoker(() => richTextBox1.Text += $"{k._content}\n"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.StopSniffing();
        }
    }
}
