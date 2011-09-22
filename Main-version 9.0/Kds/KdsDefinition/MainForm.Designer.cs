namespace KdsDefinition
{
    partial class MainForm
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grdTables = new System.Windows.Forms.DataGridView();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReadOnlyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allowInsertDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allowEditDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Columns = new System.Windows.Forms.DataGridViewLinkColumn();
            this.selectMethodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.insertMethodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateMethodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rolesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectParametersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kdsDataSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSortColumn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUpdateRoles = new System.Windows.Forms.TextBox();
            this.btnParams = new System.Windows.Forms.Button();
            this.txtRoles = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInsert = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUpdate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDelete = new System.Windows.Forms.TextBox();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsDataSourceBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(886, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resourcesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // resourcesToolStripMenuItem
            // 
            this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
            this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.resourcesToolStripMenuItem.Text = "Resources";
            this.resourcesToolStripMenuItem.Click += new System.EventHandler(this.resourcesToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            // 
            // grdTables
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PapayaWhip;
            this.grdTables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTables.AutoGenerateColumns = false;
            this.grdTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TableName,
            this.Title,
            this.isReadOnlyDataGridViewCheckBoxColumn,
            this.allowInsertDataGridViewCheckBoxColumn,
            this.allowEditDataGridViewCheckBoxColumn,
            this.Columns,
            this.selectMethodDataGridViewTextBoxColumn,
            this.insertMethodDataGridViewTextBoxColumn,
            this.updateMethodDataGridViewTextBoxColumn,
            this.rolesDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.columnsDataGridViewTextBoxColumn,
            this.selectParametersDataGridViewTextBoxColumn});
            this.grdTables.DataSource = this.kdsDataSourceBindingSource;
            this.grdTables.Location = new System.Drawing.Point(0, 42);
            this.grdTables.Name = "grdTables";
            this.grdTables.Size = new System.Drawing.Size(859, 263);
            this.grdTables.TabIndex = 1;
            this.grdTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTables_CellContentClick);
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "Name";
            this.TableName.HeaderText = "Name";
            this.TableName.Name = "TableName";
            this.TableName.Width = 200;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 200;
            // 
            // isReadOnlyDataGridViewCheckBoxColumn
            // 
            this.isReadOnlyDataGridViewCheckBoxColumn.DataPropertyName = "IsReadOnly";
            this.isReadOnlyDataGridViewCheckBoxColumn.HeaderText = "Read Only";
            this.isReadOnlyDataGridViewCheckBoxColumn.Name = "isReadOnlyDataGridViewCheckBoxColumn";
            // 
            // allowInsertDataGridViewCheckBoxColumn
            // 
            this.allowInsertDataGridViewCheckBoxColumn.DataPropertyName = "AllowInsert";
            this.allowInsertDataGridViewCheckBoxColumn.HeaderText = "Allow Insert";
            this.allowInsertDataGridViewCheckBoxColumn.Name = "allowInsertDataGridViewCheckBoxColumn";
            this.allowInsertDataGridViewCheckBoxColumn.ReadOnly = true;
            this.allowInsertDataGridViewCheckBoxColumn.Visible = false;
            // 
            // allowEditDataGridViewCheckBoxColumn
            // 
            this.allowEditDataGridViewCheckBoxColumn.DataPropertyName = "AllowEdit";
            this.allowEditDataGridViewCheckBoxColumn.HeaderText = "Allow Edit";
            this.allowEditDataGridViewCheckBoxColumn.Name = "allowEditDataGridViewCheckBoxColumn";
            this.allowEditDataGridViewCheckBoxColumn.ReadOnly = true;
            this.allowEditDataGridViewCheckBoxColumn.Visible = false;
            // 
            // Columns
            // 
            this.Columns.HeaderText = "Columns";
            this.Columns.Name = "Columns";
            this.Columns.ReadOnly = true;
            this.Columns.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Columns.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Columns.Text = "Edit Columns...";
            this.Columns.UseColumnTextForLinkValue = true;
            // 
            // selectMethodDataGridViewTextBoxColumn
            // 
            this.selectMethodDataGridViewTextBoxColumn.DataPropertyName = "SelectMethod";
            this.selectMethodDataGridViewTextBoxColumn.HeaderText = "SelectMethod";
            this.selectMethodDataGridViewTextBoxColumn.Name = "selectMethodDataGridViewTextBoxColumn";
            this.selectMethodDataGridViewTextBoxColumn.Visible = false;
            // 
            // insertMethodDataGridViewTextBoxColumn
            // 
            this.insertMethodDataGridViewTextBoxColumn.DataPropertyName = "InsertMethod";
            this.insertMethodDataGridViewTextBoxColumn.HeaderText = "InsertMethod";
            this.insertMethodDataGridViewTextBoxColumn.Name = "insertMethodDataGridViewTextBoxColumn";
            this.insertMethodDataGridViewTextBoxColumn.Visible = false;
            // 
            // updateMethodDataGridViewTextBoxColumn
            // 
            this.updateMethodDataGridViewTextBoxColumn.DataPropertyName = "UpdateMethod";
            this.updateMethodDataGridViewTextBoxColumn.HeaderText = "UpdateMethod";
            this.updateMethodDataGridViewTextBoxColumn.Name = "updateMethodDataGridViewTextBoxColumn";
            this.updateMethodDataGridViewTextBoxColumn.Visible = false;
            // 
            // rolesDataGridViewTextBoxColumn
            // 
            this.rolesDataGridViewTextBoxColumn.DataPropertyName = "Roles";
            this.rolesDataGridViewTextBoxColumn.HeaderText = "Roles";
            this.rolesDataGridViewTextBoxColumn.Name = "rolesDataGridViewTextBoxColumn";
            this.rolesDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Visible = false;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.Visible = false;
            // 
            // columnsDataGridViewTextBoxColumn
            // 
            this.columnsDataGridViewTextBoxColumn.DataPropertyName = "Columns";
            this.columnsDataGridViewTextBoxColumn.HeaderText = "Columns";
            this.columnsDataGridViewTextBoxColumn.Name = "columnsDataGridViewTextBoxColumn";
            this.columnsDataGridViewTextBoxColumn.ReadOnly = true;
            this.columnsDataGridViewTextBoxColumn.Visible = false;
            // 
            // selectParametersDataGridViewTextBoxColumn
            // 
            this.selectParametersDataGridViewTextBoxColumn.DataPropertyName = "SelectParameters";
            this.selectParametersDataGridViewTextBoxColumn.HeaderText = "SelectParameters";
            this.selectParametersDataGridViewTextBoxColumn.Name = "selectParametersDataGridViewTextBoxColumn";
            this.selectParametersDataGridViewTextBoxColumn.ReadOnly = true;
            this.selectParametersDataGridViewTextBoxColumn.Visible = false;
            // 
            // kdsDataSourceBindingSource
            // 
            this.kdsDataSourceBindingSource.DataSource = typeof(KdsLibrary.UI.SystemManager.KdsDataSource);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtDelete);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbSortColumn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtUpdateRoles);
            this.panel1.Controls.Add(this.btnParams);
            this.panel1.Controls.Add(this.txtRoles);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtInsert);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUpdate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSelect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 311);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(859, 136);
            this.panel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(513, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Sort By ";
            // 
            // cmbSortColumn
            // 
            this.cmbSortColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortColumn.FormattingEnabled = true;
            this.cmbSortColumn.Location = new System.Drawing.Point(563, 44);
            this.cmbSortColumn.Name = "cmbSortColumn";
            this.cmbSortColumn.Size = new System.Drawing.Size(84, 21);
            this.cmbSortColumn.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(614, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Update Roles";
            // 
            // txtUpdateRoles
            // 
            this.txtUpdateRoles.Location = new System.Drawing.Point(695, 68);
            this.txtUpdateRoles.Multiline = true;
            this.txtUpdateRoles.Name = "txtUpdateRoles";
            this.txtUpdateRoles.Size = new System.Drawing.Size(164, 34);
            this.txtUpdateRoles.TabIndex = 11;
            // 
            // btnParams
            // 
            this.btnParams.Location = new System.Drawing.Point(513, 13);
            this.btnParams.Name = "btnParams";
            this.btnParams.Size = new System.Drawing.Size(57, 19);
            this.btnParams.TabIndex = 10;
            this.btnParams.Text = "Params";
            this.btnParams.UseVisualStyleBackColor = true;
            this.btnParams.Click += new System.EventHandler(this.btnParams_Click);
            // 
            // txtRoles
            // 
            this.txtRoles.Location = new System.Drawing.Point(692, 12);
            this.txtRoles.Multiline = true;
            this.txtRoles.Name = "txtRoles";
            this.txtRoles.Size = new System.Drawing.Size(164, 50);
            this.txtRoles.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(652, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Roles";
            // 
            // txtInsert
            // 
            this.txtInsert.Location = new System.Drawing.Point(86, 80);
            this.txtInsert.Name = "txtInsert";
            this.txtInsert.Size = new System.Drawing.Size(421, 20);
            this.txtInsert.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Insert Method";
            // 
            // txtUpdate
            // 
            this.txtUpdate.Location = new System.Drawing.Point(86, 46);
            this.txtUpdate.Name = "txtUpdate";
            this.txtUpdate.Size = new System.Drawing.Size(421, 20);
            this.txtUpdate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Update Method";
            // 
            // txtSelect
            // 
            this.txtSelect.Location = new System.Drawing.Point(86, 13);
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(421, 20);
            this.txtSelect.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Method";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML files (*.xml)|*.xml";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Delete Method";
            // 
            // txtDelete
            // 
            this.txtDelete.Location = new System.Drawing.Point(86, 108);
            this.txtDelete.Name = "txtDelete";
            this.txtDelete.Size = new System.Drawing.Size(421, 20);
            this.txtDelete.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 451);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdTables);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "MainForm";
            this.Text = "Table Definiton";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdsDataSourceBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView grdTables;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtInsert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRoles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnParams;
        private System.Windows.Forms.BindingSource kdsDataSourceBindingSource;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isReadOnlyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowInsertDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowEditDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn Columns;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectMethodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn insertMethodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updateMethodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rolesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectParametersDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtUpdateRoles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSortColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDelete;
        private System.Windows.Forms.Label label7;
    }
}

