﻿namespace ERP
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.batchIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberOfCupsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchCompletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.timeDateOrderedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDateCompletedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchOrdreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.batchOrderDataSet = new ERP.BatchOrderDataSet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFillTran = new System.Windows.Forms.TextBox();
            this.txtFillTall = new System.Windows.Forms.TextBox();
            this.txtFillRed = new System.Windows.Forms.TextBox();
            this.txtTran = new System.Windows.Forms.TextBox();
            this.txtTall = new System.Windows.Forms.TextBox();
            this.txtRed = new System.Windows.Forms.TextBox();
            this.txtBlack = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchOrdreTableAdapter = new ERP.BatchOrderDataSetTableAdapters.BatchOrdreTableAdapter();
            this.btnReconnect = new System.Windows.Forms.Button();
            this.txtFillBlack = new System.Windows.Forms.TextBox();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSelectedOrder = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.cupOrdreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cupOrderDataSet = new ERP.CupOrderDataSet();
            this.cupOrdreTableAdapter = new ERP.CupOrderDataSetTableAdapters.CupOrdreTableAdapter();
            this.cupIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeOfCupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderedWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rFIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completedApprovedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.completedDiscardDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchOrdreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchOrderDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cupOrdreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cupOrderDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red Cup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fill [g]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fill [g]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Black Cup";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fill [g]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tall Cup";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Fill [g]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Transparent Cup";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(10, 224);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(197, 21);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit Order";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.batchIDDataGridViewTextBoxColumn,
            this.numberOfCupsDataGridViewTextBoxColumn,
            this.batchCompletedDataGridViewCheckBoxColumn,
            this.timeDateOrderedDataGridViewTextBoxColumn,
            this.timeDateCompletedDataGridViewTextBoxColumn,
            this.batchTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.batchOrdreBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(14, 19);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(751, 250);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged_1);
            // 
            // batchIDDataGridViewTextBoxColumn
            // 
            this.batchIDDataGridViewTextBoxColumn.DataPropertyName = "BatchID";
            this.batchIDDataGridViewTextBoxColumn.HeaderText = "Batch Number";
            this.batchIDDataGridViewTextBoxColumn.Name = "batchIDDataGridViewTextBoxColumn";
            this.batchIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.batchIDDataGridViewTextBoxColumn.Width = 120;
            // 
            // numberOfCupsDataGridViewTextBoxColumn
            // 
            this.numberOfCupsDataGridViewTextBoxColumn.DataPropertyName = "NumberOfCups";
            this.numberOfCupsDataGridViewTextBoxColumn.HeaderText = "Total Cups";
            this.numberOfCupsDataGridViewTextBoxColumn.Name = "numberOfCupsDataGridViewTextBoxColumn";
            this.numberOfCupsDataGridViewTextBoxColumn.ReadOnly = true;
            this.numberOfCupsDataGridViewTextBoxColumn.Width = 120;
            // 
            // batchCompletedDataGridViewCheckBoxColumn
            // 
            this.batchCompletedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.batchCompletedDataGridViewCheckBoxColumn.DataPropertyName = "BatchCompleted";
            this.batchCompletedDataGridViewCheckBoxColumn.HeaderText = "Completed";
            this.batchCompletedDataGridViewCheckBoxColumn.Name = "batchCompletedDataGridViewCheckBoxColumn";
            this.batchCompletedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // timeDateOrderedDataGridViewTextBoxColumn
            // 
            this.timeDateOrderedDataGridViewTextBoxColumn.DataPropertyName = "TimeDateOrdered";
            this.timeDateOrderedDataGridViewTextBoxColumn.HeaderText = "Date Ordered";
            this.timeDateOrderedDataGridViewTextBoxColumn.Name = "timeDateOrderedDataGridViewTextBoxColumn";
            this.timeDateOrderedDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeDateOrderedDataGridViewTextBoxColumn.Width = 120;
            // 
            // timeDateCompletedDataGridViewTextBoxColumn
            // 
            this.timeDateCompletedDataGridViewTextBoxColumn.DataPropertyName = "TimeDateCompleted";
            this.timeDateCompletedDataGridViewTextBoxColumn.HeaderText = "Date Completed";
            this.timeDateCompletedDataGridViewTextBoxColumn.Name = "timeDateCompletedDataGridViewTextBoxColumn";
            this.timeDateCompletedDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeDateCompletedDataGridViewTextBoxColumn.Width = 120;
            // 
            // batchTimeDataGridViewTextBoxColumn
            // 
            this.batchTimeDataGridViewTextBoxColumn.DataPropertyName = "BatchTime";
            this.batchTimeDataGridViewTextBoxColumn.HeaderText = "Production Time";
            this.batchTimeDataGridViewTextBoxColumn.Name = "batchTimeDataGridViewTextBoxColumn";
            this.batchTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.batchTimeDataGridViewTextBoxColumn.Width = 120;
            // 
            // batchOrdreBindingSource
            // 
            this.batchOrdreBindingSource.DataMember = "BatchOrdre";
            this.batchOrdreBindingSource.DataSource = this.batchOrderDataSet;
            // 
            // batchOrderDataSet
            // 
            this.batchOrderDataSet.DataSetName = "BatchOrderDataSet";
            this.batchOrderDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(249, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 573);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Previous Batch Orders";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Batch Order Contents";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(598, 275);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(684, 275);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 25);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(484, 278);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(108, 20);
            this.txtSearch.TabIndex = 16;
            this.txtSearch.Text = "Batch Number";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cupIDDataGridViewTextBoxColumn,
            this.typeOfCupDataGridViewTextBoxColumn,
            this.OrderedWeight,
            this.ActualWeight,
            this.rFIDDataGridViewTextBoxColumn,
            this.completedApprovedDataGridViewCheckBoxColumn,
            this.completedDiscardDataGridViewCheckBoxColumn});
            this.dataGridView2.DataSource = this.cupOrdreBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(13, 317);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(751, 250);
            this.dataGridView2.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFillTran);
            this.groupBox2.Controls.Add(this.txtFillTall);
            this.groupBox2.Controls.Add(this.txtFillRed);
            this.groupBox2.Controls.Add(this.txtTran);
            this.groupBox2.Controls.Add(this.txtTall);
            this.groupBox2.Controls.Add(this.txtRed);
            this.groupBox2.Controls.Add(this.txtBlack);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSubmit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 269);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Batch Order";
            // 
            // txtFillTran
            // 
            this.txtFillTran.Location = new System.Drawing.Point(137, 185);
            this.txtFillTran.Name = "txtFillTran";
            this.txtFillTran.Size = new System.Drawing.Size(70, 20);
            this.txtFillTran.TabIndex = 8;
            this.txtFillTran.Text = "0";
            this.txtFillTran.MouseHover += new System.EventHandler(this.txtFillTran_MouseHover);
            // 
            // txtFillTall
            // 
            this.txtFillTall.Location = new System.Drawing.Point(137, 134);
            this.txtFillTall.Name = "txtFillTall";
            this.txtFillTall.Size = new System.Drawing.Size(70, 20);
            this.txtFillTall.TabIndex = 6;
            this.txtFillTall.Text = "0";
            this.txtFillTall.MouseHover += new System.EventHandler(this.txtFillTall_MouseHover);
            // 
            // txtFillRed
            // 
            this.txtFillRed.Location = new System.Drawing.Point(137, 84);
            this.txtFillRed.Name = "txtFillRed";
            this.txtFillRed.Size = new System.Drawing.Size(70, 20);
            this.txtFillRed.TabIndex = 4;
            this.txtFillRed.Text = "0";
            this.txtFillRed.MouseHover += new System.EventHandler(this.txtFillRed_MouseHover);
            // 
            // txtTran
            // 
            this.txtTran.Location = new System.Drawing.Point(10, 185);
            this.txtTran.Name = "txtTran";
            this.txtTran.Size = new System.Drawing.Size(121, 20);
            this.txtTran.TabIndex = 7;
            this.txtTran.Text = "0";
            // 
            // txtTall
            // 
            this.txtTall.Location = new System.Drawing.Point(10, 135);
            this.txtTall.Name = "txtTall";
            this.txtTall.Size = new System.Drawing.Size(121, 20);
            this.txtTall.TabIndex = 5;
            this.txtTall.Text = "0";
            // 
            // txtRed
            // 
            this.txtRed.Location = new System.Drawing.Point(10, 84);
            this.txtRed.Name = "txtRed";
            this.txtRed.Size = new System.Drawing.Size(121, 20);
            this.txtRed.TabIndex = 3;
            this.txtRed.Text = "0";
            // 
            // txtBlack
            // 
            this.txtBlack.Location = new System.Drawing.Point(10, 35);
            this.txtBlack.Name = "txtBlack";
            this.txtBlack.Size = new System.Drawing.Size(121, 20);
            this.txtBlack.TabIndex = 1;
            this.txtBlack.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // batchOrdreTableAdapter
            // 
            this.batchOrdreTableAdapter.ClearBeforeFill = true;
            // 
            // btnReconnect
            // 
            this.btnReconnect.Location = new System.Drawing.Point(22, 381);
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.Size = new System.Drawing.Size(197, 21);
            this.btnReconnect.TabIndex = 22;
            this.btnReconnect.Text = "Reconnect To Database";
            this.btnReconnect.UseVisualStyleBackColor = true;
            this.btnReconnect.Click += new System.EventHandler(this.btnReconnect_Click);
            // 
            // txtFillBlack
            // 
            this.txtFillBlack.Location = new System.Drawing.Point(149, 61);
            this.txtFillBlack.Name = "txtFillBlack";
            this.txtFillBlack.Size = new System.Drawing.Size(70, 20);
            this.txtFillBlack.TabIndex = 2;
            this.txtFillBlack.Text = "0";
            this.txtFillBlack.MouseHover += new System.EventHandler(this.txtFillBlack_MouseHover);
            // 
            // btnInvoice
            // 
            this.btnInvoice.Location = new System.Drawing.Point(10, 131);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(197, 21);
            this.btnInvoice.TabIndex = 16;
            this.btnInvoice.Text = "Print Selected Batch Order";
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(10, 34);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(197, 21);
            this.cmbPrinters.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Selected printer";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtSelectedOrder);
            this.groupBox3.Controls.Add(this.btnInvoice);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbPrinters);
            this.groupBox3.Location = new System.Drawing.Point(12, 421);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 179);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Print Batch Orderr";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Selected Order";
            // 
            // txtSelectedOrder
            // 
            this.txtSelectedOrder.Location = new System.Drawing.Point(13, 88);
            this.txtSelectedOrder.Name = "txtSelectedOrder";
            this.txtSelectedOrder.ReadOnly = true;
            this.txtSelectedOrder.Size = new System.Drawing.Size(194, 20);
            this.txtSelectedOrder.TabIndex = 16;
            this.txtSelectedOrder.Text = "0";
            // 
            // cupOrdreBindingSource
            // 
            this.cupOrdreBindingSource.DataMember = "CupOrdre";
            this.cupOrdreBindingSource.DataSource = this.cupOrderDataSet;
            // 
            // cupOrderDataSet
            // 
            this.cupOrderDataSet.DataSetName = "CupOrderDataSet";
            this.cupOrderDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cupOrdreTableAdapter
            // 
            this.cupOrdreTableAdapter.ClearBeforeFill = true;
            // 
            // cupIDDataGridViewTextBoxColumn
            // 
            this.cupIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cupIDDataGridViewTextBoxColumn.DataPropertyName = "CupID";
            this.cupIDDataGridViewTextBoxColumn.HeaderText = "Cup Number";
            this.cupIDDataGridViewTextBoxColumn.Name = "cupIDDataGridViewTextBoxColumn";
            this.cupIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeOfCupDataGridViewTextBoxColumn
            // 
            this.typeOfCupDataGridViewTextBoxColumn.DataPropertyName = "TypeOfCup";
            this.typeOfCupDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeOfCupDataGridViewTextBoxColumn.Name = "typeOfCupDataGridViewTextBoxColumn";
            this.typeOfCupDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // OrderedWeight
            // 
            this.OrderedWeight.DataPropertyName = "OrderedWeight";
            this.OrderedWeight.HeaderText = "Ordered [g]";
            this.OrderedWeight.Name = "OrderedWeight";
            this.OrderedWeight.ReadOnly = true;
            // 
            // ActualWeight
            // 
            this.ActualWeight.DataPropertyName = "ActualWeight";
            this.ActualWeight.HeaderText = "Measured [g]";
            this.ActualWeight.Name = "ActualWeight";
            this.ActualWeight.ReadOnly = true;
            // 
            // rFIDDataGridViewTextBoxColumn
            // 
            this.rFIDDataGridViewTextBoxColumn.DataPropertyName = "RFID";
            this.rFIDDataGridViewTextBoxColumn.HeaderText = "Tag";
            this.rFIDDataGridViewTextBoxColumn.Name = "rFIDDataGridViewTextBoxColumn";
            this.rFIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.rFIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // completedApprovedDataGridViewCheckBoxColumn
            // 
            this.completedApprovedDataGridViewCheckBoxColumn.DataPropertyName = "CompletedApproved";
            this.completedApprovedDataGridViewCheckBoxColumn.HeaderText = "Approved";
            this.completedApprovedDataGridViewCheckBoxColumn.Name = "completedApprovedDataGridViewCheckBoxColumn";
            this.completedApprovedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // completedDiscardDataGridViewCheckBoxColumn
            // 
            this.completedDiscardDataGridViewCheckBoxColumn.DataPropertyName = "CompletedDiscard";
            this.completedDiscardDataGridViewCheckBoxColumn.HeaderText = "Discarded";
            this.completedDiscardDataGridViewCheckBoxColumn.Name = "completedDiscardDataGridViewCheckBoxColumn";
            this.completedDiscardDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 620);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtFillBlack);
            this.Controls.Add(this.btnReconnect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP 1.3.4";
            this.Load += new System.EventHandler(this.ERP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchOrdreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchOrderDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cupOrdreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cupOrderDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView2;
        private BatchOrderDataSet batchOrderDataSet;
        private System.Windows.Forms.BindingSource batchOrdreBindingSource;
        private BatchOrderDataSetTableAdapters.BatchOrdreTableAdapter batchOrdreTableAdapter;
        private CupOrderDataSet cupOrderDataSet;
        private System.Windows.Forms.BindingSource cupOrdreBindingSource;
        private CupOrderDataSetTableAdapters.CupOrdreTableAdapter cupOrdreTableAdapter;
        private System.Windows.Forms.Button btnReconnect;
        //private System.Windows.Forms.DataGridViewTextBoxColumn orderedWeightDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn actualWeightDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtTran;
        private System.Windows.Forms.TextBox txtTall;
        private System.Windows.Forms.TextBox txtRed;
        private System.Windows.Forms.TextBox txtBlack;
        private System.Windows.Forms.TextBox txtFillTran;
        private System.Windows.Forms.TextBox txtFillTall;
        private System.Windows.Forms.TextBox txtFillRed;
        private System.Windows.Forms.TextBox txtFillBlack;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSelectedOrder;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberOfCupsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn batchCompletedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDateOrderedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDateCompletedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cupIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeOfCupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderedWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn rFIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn completedApprovedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn completedDiscardDataGridViewCheckBoxColumn;
    }
}

