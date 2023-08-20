using System.Windows.Forms;

namespace DofusPacketManager.Network
{
    public partial class PacketLoggerForm : Form
    {
        private NetworkManager networkManagerInstance;
        private bool isLogging = false;
        public PacketLoggerForm()
        {
            InitializeComponent();
            networkManagerInstance = NetworkManager.Instance;
        }

        private void startLoggingButton_Click(object sender, System.EventArgs e)
        {
            if (!networkManagerInstance.Sniffing)
            {
                MessageBox.Show("Le programme ne sniffe pas", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //networkManagerInstance.MessageManager.PacketParser.
        }

        private void packetsLogsTextBox_TextChanged(object sender, System.EventArgs e)
        {
            clearLogsButton.Enabled = packetsLogsTextBox.Text.Length > 0;
            copyLogsButton.Enabled = packetsLogsTextBox.Text.Length > 0;
        }
    }
}
