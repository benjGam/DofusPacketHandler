using DofusPacketManager.Network.Messages;
using System.Windows.Forms;

namespace DofusPacketManager.Network.Debugging
{
    public partial class PacketLoggerForm : Form
    {
        private NetworkManager networkManagerInstance;
        public PacketLoggerForm()
        {
            InitializeComponent();
            networkManagerInstance = NetworkManager.Instance;
        }

        private void startLoggingButton_Click(object sender, System.EventArgs e)
        {
            if (!networkManagerInstance.Sniffing)
            {
                MessageBox.Show("Le programme ne sniffe pas !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PacketParser.Instance.MessageInformationExtracted += PacketParser_MessageInformationExtracted;
            startLoggingButton.Enabled = false;
            stopLoggingButton.Enabled = true;
        }

        private void PacketParser_MessageInformationExtracted(object sender, Messages.CustomEventArgs.NetworkMessageInformationExtractedEventArgs e)
        {
            string messageTypeName = string.Empty;
            if (e.Message == null)
                messageTypeName = "Unrecognized";
            else
                messageTypeName = e.Message.GetType().Name;
            packetsLogsTextBox.Invoke(new MethodInvoker(() => packetsLogsTextBox.AppendText($"PacketID: {e.Informations.MessageId} : {messageTypeName} Message\n")));
        }

        private void packetsLogsTextBox_TextChanged(object sender, System.EventArgs e)
        {
            clearLogsButton.Enabled = packetsLogsTextBox.Text.Length > 0;
            copyLogsButton.Enabled = packetsLogsTextBox.Text.Length > 0;
            packetsLogsTextBox.SelectionStart = packetsLogsTextBox.Text.Length;
            packetsLogsTextBox.ScrollToCaret();
        }

        private void stopLoggingButton_Click(object sender, System.EventArgs e)
        {
            if (!networkManagerInstance.Sniffing)
            {
                MessageBox.Show("Le programme ne sniffe pas", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PacketParser.Instance.MessageInformationExtracted -= PacketParser_MessageInformationExtracted;
            startLoggingButton.Enabled = true;
            stopLoggingButton.Enabled = false;
        }

        private void clearLogsButton_Click(object sender, System.EventArgs e) => packetsLogsTextBox.Clear();

        private void copyLogsButton_Click(object sender, System.EventArgs e) => Clipboard.SetText(packetsLogsTextBox.Text);

        private void PacketLoggerForm_FormClosing(object sender, FormClosingEventArgs e) => PacketParser.Instance.MessageInformationExtracted -= PacketParser_MessageInformationExtracted;
    
    }
}
