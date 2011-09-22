using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KdsLibrary.UI.SystemManager;

namespace KdsDefinition
{
    public partial class Columns : Form
    {
        private KdsDataSource _dataSource;

        public Columns()
        {
            InitializeComponent();
        }

        public DialogResult ShowColumns(KdsDataSource dataSource,
            IWin32Window owner)
        {
            _dataSource = dataSource;
            var result = ShowDialog(owner);
            dataSource.ColumnsList.RemoveAll(delegate(KdsColumn col)
            {
                return String.IsNullOrEmpty(col.Name);
            });
            return result;
        }

        private void Columns_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn cmbCol = (DataGridViewComboBoxColumn)grdColumns.Columns[2];
            cmbCol.DataSource = Enum.GetValues(typeof(KdsColumnType));
            cmbCol = (DataGridViewComboBoxColumn)grdColumns.Columns[4];
            cmbCol.DataSource = Extensions.DataTypes;
            PerformBinding();
        }

        private void PerformBinding()
        {
            if (_dataSource.ColumnsList == null)
                _dataSource.ColumnsList = new List<KdsColumn>();
            kdsColumnBindingSource.DataSource = _dataSource.ColumnsList;
        }
       
        private void grdColumns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
                {
                    if ((KdsColumnType)Enum.Parse(typeof(KdsColumnType),
                            grdColumns[2, e.RowIndex].Value.ToString())
                        == KdsColumnType.DropDown)
                    {
                        KdsColumn selectedCol =
                            _dataSource.Columns[grdColumns[0, e.RowIndex].Value.ToString()];
                        if (selectedCol != null)
                        {
                            ListValues frmList = new ListValues();
                            selectedCol.ListValue = frmList.ShowListValues(this, selectedCol.ListValue);
                        }
                    }

                }
            }
        }

    }
}
