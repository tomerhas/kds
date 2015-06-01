using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using KdsLibrary.UI.SystemManager;
using System.IO;

namespace KdsDefinition
{
    public partial class MainForm : Form
    {
        KdsSysMan _sysMan = null;
        private BindingManagerBase _bindingManager;
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    XmlDocument xDom = new XmlDocument();
                    xDom.Load(openFileDialog1.FileName);
                    _sysMan = (KdsSysMan)KdsLibrary.Utils.KdsExtensions.DeserializeObject(
                           typeof(KdsSysMan), xDom.OuterXml);
                    if (_sysMan == null)
                    {
                        MessageBox.Show("Invalid file selected");
                        EnableMenus(false);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid file selected");
                    return;
                }
                EnableMenus(true);
                PerformBinding();
                
            }

            
        }

        private void PerformBinding()
        {
            kdsDataSourceBindingSource.PositionChanged += new EventHandler(kdsDataSourceBindingSource_PositionChanged);
            kdsDataSourceBindingSource.DataSource = _sysMan.DataSourcesList;
            BindControls();
           
            _bindingManager = this.BindingContext[kdsDataSourceBindingSource.DataSource];
        }

        void kdsDataSourceBindingSource_PositionChanged(object sender, EventArgs e)
        {
            BindSortColumnComboBox();
        }

        private void BindSortColumnComboBox()
        {
            SetSortColumnSource();
            cmbSortColumn.DataBindings.Clear();
            cmbSortColumn.DataBindings.Add("SelectedValue", grdTables.DataSource, "SortColumn");
        }

        private void BindControl(Control ctl, string property, object dataSource,
            string dataMember)
        {
            ctl.DataBindings.Clear();
            ctl.DataBindings.Add(property, dataSource, dataMember);
        }
        private void BindControls()
        {
            BindControl(txtSelect, "Text", grdTables.DataSource, "SelectMethod");
            BindControl(txtUpdate, "Text", grdTables.DataSource, "UpdateMethod");
            BindControl(txtInsert, "Text", grdTables.DataSource, "InsertMethod");
            BindControl(txtDelete, "Text", grdTables.DataSource, "DeleteMethod");
            BindControl(txtRoles, "Text", grdTables.DataSource, "Roles");
            BindControl(txtUpdateRoles, "Text", grdTables.DataSource, "UpdateRoles");
            BindSortColumnComboBox();            
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {
            EnableMenus(false);
        }

        private void grdTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    KdsDataSource selectedSource =
                        _sysMan.DataSources[grdTables[0, e.RowIndex].Value.ToString()];
                    Columns frmCols = new Columns();
                    var result = frmCols.ShowColumns(selectedSource, this);
                }
            }
        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            SelectParameters frmParams = new SelectParameters();
            KdsDataSource selectedSource = _sysMan.DataSources[grdTables[0,
                grdTables.CurrentRow.Index].Value.ToString()];
            selectedSource.SelectParametersList = frmParams.ShowParameters(selectedSource.SelectParametersList, this);
        }

        private void EnableMenus(bool enable)
        {
            saveToolStripMenuItem.Enabled = enable;
            saveAsToolStripMenuItem.Enabled = enable;
            resourcesToolStripMenuItem.Enabled = enable;
        }
        private void SaveToFile(string filename)
        {
            try
            {
                kdsDataSourceBindingSource.EndEdit();
                ValidateKdsSysMan();
                string serialized = KdsLibrary.Utils.KdsExtensions.SerializeObject(_sysMan);
                XmlDocument xDom = new XmlDocument();
                xDom.LoadXml(serialized);
                xDom.RemoveChild(xDom.FirstChild);
                string output = IndentXMLString(xDom.OuterXml);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(filename, false);
                writer.Write(output);
                writer.Close();
                MessageBox.Show(this, "File Saved", "Table Definition");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Table Definition",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string IndentXMLString(string xml)
        {
            string outXml = string.Empty;
            MemoryStream ms = new MemoryStream();
            // Create a XMLTextWriter that will send its output to a memory stream (file)
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
            XmlDocument doc = new XmlDocument();

            try
            {
                // Load the unformatted XML text string into an instance
                // of the XML Document Object Model (DOM)
                doc.LoadXml(xml);

                // Set the formatting property of the XML Text Writer to indented
                // the text writer is where the indenting will be performed
                xtw.Formatting = Formatting.Indented;

                // write dom xml to the xmltextwriter
                doc.WriteContentTo(xtw);
                // Flush the contents of the text writer
                // to the memory stream, which is simply a memory file
                xtw.Flush();

                // set to start of the memory stream (file)
                ms.Seek(0, SeekOrigin.Begin);
                // create a reader to read the contents of
                // the memory stream (file)
                StreamReader sr = new StreamReader(ms);
                // return the formatted string to caller
                return sr.ReadToEnd();
             }
             catch (Exception ex)
             {
                MessageBox.Show(ex.ToString());
                return string.Empty;
             }
        }

        private void ValidateKdsSysMan()
        {
            _sysMan.DataSourcesList.ForEach(delegate(KdsDataSource ds)
            {
                ds.ColumnsList.ForEach(delegate(KdsColumn col)
                {
                    if (col.ColumnType != KdsColumnType.DropDown && col.ListValue != null)
                        col.ListValue = null;
                        
                });
            });
        }

        private void SetSortColumnSource()
        {

            KdsDataSource selectedSource =
                        _sysMan.DataSources[grdTables[0,
                        kdsDataSourceBindingSource.Position].Value.ToString()];
            cmbSortColumn.DisplayMember = "Name";
            cmbSortColumn.ValueMember = "Name";
            KdsColumn[] cmbSource = new KdsColumn[selectedSource.ColumnsList.Count + 1];
            selectedSource.ColumnsList.CopyTo(cmbSource, 1);
            cmbSource[0] = new KdsColumn();
            cmbSource[0].Name = String.Empty;
            cmbSortColumn.DataSource = cmbSource;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(this, "Are you sure?", "Table Definition",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
                SaveToFile(openFileDialog1.FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                SaveToFile(saveFileDialog1.FileName);
            }
        }

        private void resourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resources frmResources = new Resources();
            frmResources.ShowResources(this, _sysMan.Resources.TextResourcesList);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
