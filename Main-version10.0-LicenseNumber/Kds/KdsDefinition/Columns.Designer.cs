namespace KdsDefinition
{
    partial class Columns
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdColumns = new System.Windows.Forms.DataGridView();
            this.kdsColumnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ListValues = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.isMustDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isKeyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsLocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.showInGridDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.showInDetailsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maxLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullHeaderTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsColumnBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdColumns
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PapayaWhip;
            this.grdColumns.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdColumns.AutoGenerateColumns = false;
            this.grdColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.headerTextDataGridViewTextBoxColumn,
            this.columnTypeDataGridViewTextBoxColumn,
            this.ListValues,
            this.dataTypeDataGridViewTextBoxColumn,
            this.isMustDataGridViewCheckBoxColumn,
            this.isKeyDataGridViewCheckBoxColumn,
            this.IsLocked,
            this.showInGridDataGridViewCheckBoxColumn,
            this.showInDetailsDataGridViewCheckBoxColumn,
            this.maxLengthDataGridViewTextBoxColumn,
            this.DataFormat,
            this.listValueDataGridViewTextBoxColumn,
            this.fullHeaderTextDataGridViewTextBoxColumn,
            this.Order});
            this.grdColumns.DataSource = this.kdsColumnBindingSource;
            this.grdColumns.Location = new System.Drawing.Point(12, 12);
            this.grdColumns.Name = "grdColumns";
            this.grdColumns.Size = new System.Drawing.Size(952, 258);
            this.grdColumns.TabIndex = 0;
            this.grdColumns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdColumns_CellContentClick);
            // 
            // kdsColumnBindingSource
            // 
            this.kdsColumnBindingSource.DataSource = typeof(KdsLibrary.UI.SystemManager.KdsColumn);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // headerTextDataGridViewTextBoxColumn
            // 
            this.headerTextDataGridViewTextBoxColumn.DataPropertyName = "HeaderText";
            this.headerTextDataGridViewTextBoxColumn.HeaderText = "Title";
            this.headerTextDataGridViewTextBoxColumn.Name = "headerTextDataGridViewTextBoxColumn";
            // 
            // columnTypeDataGridViewTextBoxColumn
            // 
            this.columnTypeDataGridViewTextBoxColumn.DataPropertyName = "ColumnType";
            this.columnTypeDataGridViewTextBoxColumn.HeaderText = "Column Type";
            this.columnTypeDataGridViewTextBoxColumn.Name = "columnTypeDataGridViewTextBoxColumn";
            this.columnTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ListValues
            // 
            this.ListValues.HeaderText = "List Values";
            this.ListValues.Name = "ListValues";
            this.ListValues.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ListValues.Text = "Edit List...";
            this.ListValues.UseColumnTextForLinkValue = true;
            // 
            // dataTypeDataGridViewTextBoxColumn
            // 
            this.dataTypeDataGridViewTextBoxColumn.DataPropertyName = "DataType";
            this.dataTypeDataGridViewTextBoxColumn.HeaderText = "Data Type";
            this.dataTypeDataGridViewTextBoxColumn.Name = "dataTypeDataGridViewTextBoxColumn";
            this.dataTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isMustDataGridViewCheckBoxColumn
            // 
            this.isMustDataGridViewCheckBoxColumn.DataPropertyName = "IsMust";
            this.isMustDataGridViewCheckBoxColumn.HeaderText = "Must";
            this.isMustDataGridViewCheckBoxColumn.Name = "isMustDataGridViewCheckBoxColumn";
            // 
            // isKeyDataGridViewCheckBoxColumn
            // 
            this.isKeyDataGridViewCheckBoxColumn.DataPropertyName = "IsKey";
            this.isKeyDataGridViewCheckBoxColumn.HeaderText = "Key";
            this.isKeyDataGridViewCheckBoxColumn.Name = "isKeyDataGridViewCheckBoxColumn";
            // 
            // IsLocked
            // 
            this.IsLocked.DataPropertyName = "IsLocked";
            this.IsLocked.HeaderText = "Locked";
            this.IsLocked.Name = "IsLocked";
            // 
            // showInGridDataGridViewCheckBoxColumn
            // 
            this.showInGridDataGridViewCheckBoxColumn.DataPropertyName = "ShowInGrid";
            this.showInGridDataGridViewCheckBoxColumn.HeaderText = "Show In Grid";
            this.showInGridDataGridViewCheckBoxColumn.Name = "showInGridDataGridViewCheckBoxColumn";
            // 
            // showInDetailsDataGridViewCheckBoxColumn
            // 
            this.showInDetailsDataGridViewCheckBoxColumn.DataPropertyName = "ShowInDetails";
            this.showInDetailsDataGridViewCheckBoxColumn.HeaderText = "Show In Details";
            this.showInDetailsDataGridViewCheckBoxColumn.Name = "showInDetailsDataGridViewCheckBoxColumn";
            // 
            // maxLengthDataGridViewTextBoxColumn
            // 
            this.maxLengthDataGridViewTextBoxColumn.DataPropertyName = "MaxLength";
            this.maxLengthDataGridViewTextBoxColumn.HeaderText = "MaxLength";
            this.maxLengthDataGridViewTextBoxColumn.Name = "maxLengthDataGridViewTextBoxColumn";
            // 
            // DataFormat
            // 
            this.DataFormat.DataPropertyName = "DataFormat";
            this.DataFormat.HeaderText = "Data Format";
            this.DataFormat.Name = "DataFormat";
            // 
            // listValueDataGridViewTextBoxColumn
            // 
            this.listValueDataGridViewTextBoxColumn.DataPropertyName = "ListValue";
            this.listValueDataGridViewTextBoxColumn.HeaderText = "ListValue";
            this.listValueDataGridViewTextBoxColumn.Name = "listValueDataGridViewTextBoxColumn";
            this.listValueDataGridViewTextBoxColumn.Visible = false;
            // 
            // fullHeaderTextDataGridViewTextBoxColumn
            // 
            this.fullHeaderTextDataGridViewTextBoxColumn.DataPropertyName = "FullHeaderText";
            this.fullHeaderTextDataGridViewTextBoxColumn.HeaderText = "FullHeaderText";
            this.fullHeaderTextDataGridViewTextBoxColumn.Name = "fullHeaderTextDataGridViewTextBoxColumn";
            this.fullHeaderTextDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullHeaderTextDataGridViewTextBoxColumn.Visible = false;
            // 
            // Order
            // 
            this.Order.DataPropertyName = "Order";
            this.Order.HeaderText = "Order";
            this.Order.Name = "Order";
            // 
            // Columns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 280);
            this.Controls.Add(this.grdColumns);
            this.Name = "Columns";
            this.ShowInTaskbar = false;
            this.Text = "Columns";
            this.Load += new System.EventHandler(this.Columns_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsColumnBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdColumns;
        private System.Windows.Forms.BindingSource kdsColumnBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn ListValues;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMustDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isKeyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsLocked;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showInGridDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showInDetailsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn listValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullHeaderTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
    }
}