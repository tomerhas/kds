namespace KdsDefinition
{
    partial class Resources
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
            this.grdResources = new System.Windows.Forms.DataGridView();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kdsTextResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdResources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsTextResourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdResources
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PapayaWhip;
            this.grdResources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdResources.AutoGenerateColumns = false;
            this.grdResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.grdResources.DataSource = this.kdsTextResourceBindingSource;
            this.grdResources.Location = new System.Drawing.Point(12, 12);
            this.grdResources.Name = "grdResources";
            this.grdResources.Size = new System.Drawing.Size(268, 150);
            this.grdResources.TabIndex = 0;
            this.grdResources.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grdResources_RowValidating);
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Visible = false;
            // 
            // kdsTextResourceBindingSource
            // 
            this.kdsTextResourceBindingSource.DataSource = typeof(KdsLibrary.UI.SystemManager.KdsTextResource);
            // 
            // Resources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 177);
            this.Controls.Add(this.grdResources);
            this.Name = "Resources";
            this.ShowInTaskbar = false;
            this.Text = "Resources";
            this.Load += new System.EventHandler(this.Resources_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdResources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsTextResourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdResources;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource kdsTextResourceBindingSource;
    }
}