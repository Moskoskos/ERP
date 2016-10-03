namespace ERP
{
    partial class frmPrint
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPrinterList = new System.Windows.Forms.ComboBox();
            this.cmbLocalPrinters = new System.Windows.Forms.ComboBox();
            this.btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbNetworkPrinters = new System.Windows.Forms.ComboBox();
            this.btnNetwork = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Discover all installed printers";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPrinterList
            // 
            this.cmbPrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterList.FormattingEnabled = true;
            this.cmbPrinterList.Location = new System.Drawing.Point(12, 52);
            this.cmbPrinterList.Name = "cmbPrinterList";
            this.cmbPrinterList.Size = new System.Drawing.Size(121, 21);
            this.cmbPrinterList.TabIndex = 6;
            // 
            // cmbLocalPrinters
            // 
            this.cmbLocalPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalPrinters.FormattingEnabled = true;
            this.cmbLocalPrinters.Location = new System.Drawing.Point(12, 206);
            this.cmbLocalPrinters.Name = "cmbLocalPrinters";
            this.cmbLocalPrinters.Size = new System.Drawing.Size(121, 21);
            this.cmbLocalPrinters.TabIndex = 9;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(12, 155);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(283, 23);
            this.btn.TabIndex = 8;
            this.btn.Text = "Computer Installed Local and Network";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Local";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Network";
            // 
            // cmbNetworkPrinters
            // 
            this.cmbNetworkPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetworkPrinters.FormattingEnabled = true;
            this.cmbNetworkPrinters.Location = new System.Drawing.Point(12, 271);
            this.cmbNetworkPrinters.Name = "cmbNetworkPrinters";
            this.cmbNetworkPrinters.Size = new System.Drawing.Size(121, 21);
            this.cmbNetworkPrinters.TabIndex = 13;
            // 
            // btnNetwork
            // 
            this.btnNetwork.Location = new System.Drawing.Point(12, 320);
            this.btnNetwork.Name = "btnNetwork";
            this.btnNetwork.Size = new System.Drawing.Size(283, 23);
            this.btnNetwork.TabIndex = 14;
            this.btnNetwork.Text = "Network Discover";
            this.btnNetwork.UseVisualStyleBackColor = true;
            this.btnNetwork.Click += new System.EventHandler(this.btnNetwork_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(16, 358);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 15;
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 489);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnNetwork);
            this.Controls.Add(this.cmbNetworkPrinters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLocalPrinters);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.cmbPrinterList);
            this.Controls.Add(this.button1);
            this.Name = "frmPrint";
            this.Text = "frmPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPrinterList;
        private System.Windows.Forms.ComboBox cmbLocalPrinters;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbNetworkPrinters;
        private System.Windows.Forms.Button btnNetwork;
        private System.Windows.Forms.ListBox listBox1;
    }
}