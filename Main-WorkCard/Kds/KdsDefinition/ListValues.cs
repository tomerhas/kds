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
    public partial class ListValues : Form
    {
        KdsListValue _listValue;
        KdsListValue _oldListValue;
        public ListValues()
        {
            InitializeComponent();
        }

        public KdsListValue ShowListValues(IWin32Window owner,
            KdsListValue listValue)
        {
            _listValue = listValue;
            if (listValue != null)
                _oldListValue = listValue.Clone();
            else _oldListValue = null;
            var result = ShowDialog(owner);
            if (result == DialogResult.OK) return _listValue;
            else return _oldListValue;
        }

        private void ListValues_Load(object sender, EventArgs e)
        {
            if (_listValue == null) _listValue = new KdsListValue();
            txtSelect.DataBindings.Clear();
            txtTextField.DataBindings.Clear();
            txtValueField.DataBindings.Clear();
            txtSelect.DataBindings.Add("Text", _listValue, "SelectMethod");
            txtTextField.DataBindings.Add("Text", _listValue, "TextField");
            txtValueField.DataBindings.Add("Text", _listValue, "ValueField");
            if (_listValue.UnboundValues == null)
                _listValue.UnboundValues = new List<KdsUnboundValue>();
            kdsUnboundValueBindingSource.DataSource = _listValue.UnboundValues;
        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            SelectParameters frmParams = new SelectParameters();
            _listValue.SelectParametersList = frmParams.ShowParameters(_listValue.SelectParametersList, this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidText(txtSelect))
                errProvider.SetError(txtSelect, "The value cannot be empty");
            else if (!IsValidText(txtTextField))
                errProvider.SetError(txtTextField, "The value cannot be empty");
            else if (!IsValidText(txtValueField))
                errProvider.SetError(txtValueField, "The value cannot be empty");
            else
            {
                errProvider.SetError(txtSelect, String.Empty);
                this.DialogResult = DialogResult.OK;
            }
        }
        private bool IsValidText(TextBox txt)
        {
            return !String.IsNullOrEmpty(txt.Text);
        }
    }
}
