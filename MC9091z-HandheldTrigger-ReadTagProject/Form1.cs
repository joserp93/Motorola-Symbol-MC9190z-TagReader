using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MC9091z_HandheldTrigger_ReadTagProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetupRFIDDevice();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading RFID Tag:{0}", ex.Message);
            }
        }

        private void SetupRFIDDevice()
        {
            if (!RFIDWrapper.RFIDReaderConnected())
            {
                MessageBox.Show("This device will not scan an RFID code.");
            }
            else
            {
                //Supply callback for handling RFID tag reads
                RFIDWrapper.HandleRFIDScan(this.Instance_RFIDTagIdReceived);
            }
        }

        private void Instance_RFIDTagIdReceived(string tagId)
        {
            //Process all windows messages currently in the message queue
            System.Windows.Forms.Application.DoEvents();

            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(Instance_RFIDTagIdReceived), tagId);
                }
                else
	            {
                    tboxRFIDTag.Text = tagId;
	            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            if (RFIDWrapper.Instance != null)
            {
                RFIDWrapper.Disconnect();               
            }
        }
    }
}