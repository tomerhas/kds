using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace KdsLibrary.Controls
{
    public abstract class KdsPanel : Panel
    {
        protected Table _Table;
        protected TableRow _Tr;
        protected TableCell _Td;
        protected ScriptManager _scriptManager;
        protected string _IdName; 


        private const int WidthPanel = 100;

        public KdsPanel(ScriptManager scriptManager, string IdName)
        {
            _IdName = IdName.Replace("_", "");
            _scriptManager = scriptManager;
            _Table = new Table();
            _Table.Width = Unit.Percentage(100);
            
        }
        public void FillControls()
        {
            try
            {
                this.Controls.Add(_Table);
                BuildPanel();
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        protected abstract void BuildPanel();

        protected new int Width
        {
            get { return clGeneral.GetIntegerValue(_Table.Width.ToString()); }
            set { _Table.Width = Unit.Pixel(value); }

        }
        protected bool GridLine
        {
            set
            {
                _Table.BorderWidth = Unit.Pixel(1);
                _Table.BorderStyle = value ? BorderStyle.Solid : BorderStyle.None;
            }
        }

    }

}
