namespace MC9091z_HandheldTrigger_ReadTagProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.tboxRFIDTag = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 22);
            this.label1.Text = "RFID Tag";
            // 
            // tboxRFIDTag
            // 
            this.tboxRFIDTag.BackColor = System.Drawing.SystemColors.Info;
            this.tboxRFIDTag.Location = new System.Drawing.Point(0, 51);
            this.tboxRFIDTag.Name = "tboxRFIDTag";
            this.tboxRFIDTag.ReadOnly = true;
            this.tboxRFIDTag.Size = new System.Drawing.Size(329, 22);
            this.tboxRFIDTag.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 173);
            this.Controls.Add(this.tboxRFIDTag);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Scan with Handheld trigger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tboxRFIDTag;
    }
}

