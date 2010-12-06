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
    public partial class SelectParameters : Form
    {
        private List<KdsSelectParameter> _selectParams;
        public SelectParameters()
        {
            InitializeComponent();
        }
        public List<KdsSelectParameter> ShowParameters(List<KdsSelectParameter> selectParams,
            IWin32Window owner)
        {
            _selectParams = selectParams;
            var result = ShowDialog(owner);
            _selectParams.RemoveAll(delegate(KdsSelectParameter param)
            {
                return String.IsNullOrEmpty(param.ControlName) ||
                    String.IsNullOrEmpty(param.DataType);
            });
            return _selectParams;
        }

        private void SelectParameters_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            cmbCol.DataSource = Extensions.DataTypes;
            cmbCol.DataPropertyName = "DataType";
            cmbCol.HeaderText = "Data Type";
            grdParams.Columns.Add(cmbCol);

            PerformBinding();

        }

        private void PerformBinding()
        {
            if (_selectParams == null) _selectParams = new List<KdsSelectParameter>();
            kdsSelectParameterBindingSource.DataSource = _selectParams;
        }

    }
}
