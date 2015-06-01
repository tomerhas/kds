namespace KdsDefinition
{
    partial class SelectParameters
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
            this.grdParams = new System.Windows.Forms.DataGridView();
            this.controlNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kdsSelectParameterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsSelectParameterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdParams
            // 
            this.grdParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdParams.AutoGenerateColumns = false;
            this.grdParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.controlNameDataGridViewTextBoxColumn,
            this.dataTypeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.grdParams.DataSource = this.kdsSelectParameterBindingSource;
            this.grdParams.Location = new System.Drawing.Point(2, 3);
            this.grdParams.Name = "grdParams";
            this.grdParams.Size = new System.Drawing.Size(275, 150);
            this.grdParams.TabIndex = 0;
            // 
            // controlNameDataGridViewTextBoxColumn
            // 
            this.controlNameDataGridViewTextBoxColumn.DataPropertyName = "ControlName";
            this.controlNameDataGridViewTextBoxColumn.HeaderText = "ControlName";
            this.controlNameDataGridViewTextBoxColumn.Name = "controlNameDataGridViewTextBoxColumn";
            // 
            // dataTypeDataGridViewTextBoxColumn
            // 
            this.dataTypeDataGridViewTextBoxColumn.DataPropertyName = "DataType";
            this.dataTypeDataGridViewTextBoxColumn.HeaderText = "DataType";
            this.dataTypeDataGridViewTextBoxColumn.Name = "dataTypeDataGridViewTextBoxColumn";
            this.dataTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Visible = false;
            // 
            // kdsSelectParameterBindingSource
            // 
            this.kdsSelectParameterBindingSource.DataSource = typeof(KdsLibrary.UI.SystemManager.KdsSelectParameter);
            // 
            // SelectParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 170);
            this.Controls.Add(this.grdParams);
            this.Name = "SelectParameters";
            this.ShowInTaskbar = false;
            this.Text = "SelectParameters";
            this.Load += new System.EventHandler(this.SelectParameters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsSelectParameterBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdParams;
        private System.Windows.Forms.BindingSource kdsSelectParameterBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn controlNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    }
}