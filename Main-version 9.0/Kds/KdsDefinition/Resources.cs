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
    public partial class Resources : Form
    {
        private List<KdsTextResource> _resources;
        public Resources()
        {
            InitializeComponent();
        }
        public DialogResult ShowResources(IWin32Window owner,
            List<KdsTextResource> resources)
        {
            _resources = resources;
            return ShowDialog(owner);
        }

        private void Resources_Load(object sender, EventArgs e)
        {
            if (_resources == null)
                _resources = new List<KdsTextResource>();
            kdsTextResourceBindingSource.DataSource = _resources;

        }

        private void grdResources_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (grdResources[0, e.RowIndex].Value == null)
            {
                grdResources.Rows[e.RowIndex].ErrorText = "The Name cannot be empty";
                e.Cancel = true;
            }
        }
    }
}
