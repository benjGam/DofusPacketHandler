namespace DofusPacketManager.Network
{
    partial class PacketLoggerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clearLogsButton = new System.Windows.Forms.Button();
            this.packetsLogsTextBox = new System.Windows.Forms.RichTextBox();
            this.copyLogsButton = new System.Windows.Forms.Button();
            this.startLoggingButton = new System.Windows.Forms.Button();
            this.stopLoggingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clearLogsButton
            // 
            this.clearLogsButton.Location = new System.Drawing.Point(314, 299);
            this.clearLogsButton.Name = "clearLogsButton";
            this.clearLogsButton.Size = new System.Drawing.Size(275, 23);
            this.clearLogsButton.TabIndex = 0;
            this.clearLogsButton.Text = "Clear Logs";
            this.clearLogsButton.UseVisualStyleBackColor = true;
            // 
            // packetsLogsTextBox
            // 
            this.packetsLogsTextBox.Location = new System.Drawing.Point(12, 12);
            this.packetsLogsTextBox.Name = "packetsLogsTextBox";
            this.packetsLogsTextBox.ReadOnly = true;
            this.packetsLogsTextBox.Size = new System.Drawing.Size(577, 275);
            this.packetsLogsTextBox.TabIndex = 1;
            this.packetsLogsTextBox.Text = "";
            this.packetsLogsTextBox.TextChanged += new System.EventHandler(this.packetsLogsTextBox_TextChanged);
            // 
            // copyLogsButton
            // 
            this.copyLogsButton.Location = new System.Drawing.Point(314, 328);
            this.copyLogsButton.Name = "copyLogsButton";
            this.copyLogsButton.Size = new System.Drawing.Size(275, 23);
            this.copyLogsButton.TabIndex = 2;
            this.copyLogsButton.Text = "Copy logs";
            this.copyLogsButton.UseVisualStyleBackColor = true;
            // 
            // startLoggingButton
            // 
            this.startLoggingButton.Location = new System.Drawing.Point(12, 299);
            this.startLoggingButton.Name = "startLoggingButton";
            this.startLoggingButton.Size = new System.Drawing.Size(296, 23);
            this.startLoggingButton.TabIndex = 3;
            this.startLoggingButton.Text = "Start logging";
            this.startLoggingButton.UseVisualStyleBackColor = true;
            this.startLoggingButton.Click += new System.EventHandler(this.startLoggingButton_Click);
            // 
            // stopLoggingButton
            // 
            this.stopLoggingButton.Enabled = false;
            this.stopLoggingButton.Location = new System.Drawing.Point(12, 328);
            this.stopLoggingButton.Name = "stopLoggingButton";
            this.stopLoggingButton.Size = new System.Drawing.Size(296, 23);
            this.stopLoggingButton.TabIndex = 4;
            this.stopLoggingButton.Text = "Stop logging";
            this.stopLoggingButton.UseVisualStyleBackColor = true;
            // 
            // PacketLoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 358);
            this.Controls.Add(this.stopLoggingButton);
            this.Controls.Add(this.startLoggingButton);
            this.Controls.Add(this.copyLogsButton);
            this.Controls.Add(this.packetsLogsTextBox);
            this.Controls.Add(this.clearLogsButton);
            this.Name = "PacketLoggerForm";
            this.Text = "PacketLoggerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clearLogsButton;
        private System.Windows.Forms.RichTextBox packetsLogsTextBox;
        private System.Windows.Forms.Button copyLogsButton;
        private System.Windows.Forms.Button startLoggingButton;
        private System.Windows.Forms.Button stopLoggingButton;
    }
}