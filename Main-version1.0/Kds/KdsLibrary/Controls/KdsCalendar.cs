using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace KdsLibrary.Controls
{
    public class KdsCalendar : WebControl 
    {
        TextBox _calendar;
        CalendarExtender CalEx;
        ImageButton imgCal;
        public delegate void TextChangeEventHandler(object sender, EventArgs e);
        public event TextChangeEventHandler TextChanged;


        private RegularExpressionValidator _RegularExpressionValidator;
        private MaskedEditExtender _MaskedEditExtender;
        private ValidatorCalloutExtender _ValidatorCalloutExtender;
        private string _LabelName = string.Empty;

        public KdsCalendar()
        {
            _calendar = new TextBox();
            CalEx = new CalendarExtender();
            imgCal = new ImageButton();
            imgCal.CausesValidation = false;            
            _RegularExpressionValidator = new RegularExpressionValidator();
            _MaskedEditExtender = new MaskedEditExtender();
            _ValidatorCalloutExtender = new ValidatorCalloutExtender();
            _calendar.TextChanged += new EventHandler(_calendar_TextChanged);
            
        }

        private void _calendar_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(sender, e);
        }
        public string LabelName
        {
            set { _LabelName = value; }
        }
        public string Text
        {
            set{   _calendar.Text = value; }
            get{ return _calendar.Text; }
        }
        public string TextBoxCssClass
        {
            set { _calendar.CssClass = value; }
            get { return _calendar.Text; }
        }
        public string ImageUrl
        {
            set { imgCal.ImageUrl = value; }        
        }

        public short ImgTabIndex
        {
            set { imgCal.TabIndex = value; }
            get { return imgCal.TabIndex;  }
        }
        public short CalenderTabIndex
        {
            set { _calendar.TabIndex = value; }
            get { return _calendar.TabIndex; }
        }
        public bool AutoPostBack
        {
            set { _calendar.AutoPostBack = value; }
        }
        public bool CausesValidation
        {
            set { _calendar.CausesValidation = value; }
        }
        public ValidatorCalloutPosition PopupPositionCallOut
        {
            set { _ValidatorCalloutExtender.PopupPosition = value; }
        } 

        public string OnChangeCalScript
        {
            set { _calendar.Attributes.Add("onchange", value);} 
        }      
        public string OnBlurCalFunction
        {
            set { _calendar.Attributes.Add("onblur", value); }
        }
        public string OnClientDateSelectionChanged
        {
            get {return CalEx.OnClientDateSelectionChanged; }
        }
        public new bool Enabled
        {
            set  { _calendar.Enabled = value;
                   CalEx.EnabledOnClient = value;}
            get { return this.Enabled; }
        }
        public bool CalloutMessageDisplayed
        {
            set
            {
                _ValidatorCalloutExtender.Enabled = value;
            }
        }

        public System.Web.UI.WebControls.Unit TextBoxWidth
        {
            set { _calendar.Width = value; }
            get { return _calendar.Width; }
        }
       
        public string CalendarId
        {
            get
            {
                return "objCal_" + this.ID;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _calendar.ID = this.ID;
            this.ID = CalendarId;
            if (_calendar.Width.IsEmpty)
            {
                _calendar.Width = Unit.Parse("90px");
            }


            int LengthToRemove = Page.Request.Url.AbsoluteUri.IndexOf(Page.Request.Url.AbsolutePath);
            string urlAbs = Page.Request.Url.AbsoluteUri.Remove(LengthToRemove);
            urlAbs += Page.Request.ApplicationPath;

            
            imgCal.ID = "Img" + _calendar.ID;
            imgCal.ImageUrl = urlAbs + "Images/B_calander.png";
            imgCal.Style.Add("padding-right", "5px");
            imgCal.ImageAlign = ImageAlign.Middle;

            CalEx.ID = "Ex_" + _calendar.ID;
            CalEx.Format = "dd/MM/yyyy";
            CalEx.TargetControlID = _calendar.ID;
            CalEx.PopupButtonID = imgCal.ID;
            CalEx.PopupPosition = CalendarPosition.BottomRight; 
            CalEx.CssClass = "custom-calendar";

            this.Controls.Add(_calendar);
            this.Controls.Add(CalEx);
            //this.Controls.Add(imgSpace);
            this.Controls.Add(imgCal);
            this.Attributes.Add("dir", "rtl");

            setOnClickImg();
            setFocusImg();
            SetMaskedEditExtender();
            SetRegularExpressionValidator();
            SetValidatorCalloutExtender();
        }

        private void setOnClickImg()
        {
            string sScript = "function OnClick_ImgCalendar() { ";
            sScript += " if (document.getElementById('" + _calendar.ClientID + "').disabled == true) ";
            sScript += " $find('" + CalEx.ClientID + "')._enabled = false; ";
            sScript += " else $find('" + CalEx.ClientID + "')._enabled = true; }";

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DisabledCalendar", sScript, true);
            imgCal.Attributes.Add("onclick", "OnClick_ImgCalendar();");
        }

        private void setFocusImg()
        {
           // _calendar.Attributes.Add("onfocusin","document.getElementById('" + _calendar.ClientID + "').select();" );
            imgCal.Attributes.Add("onfocusin", "this.style.border ='1px solid black';");
            imgCal.Attributes.Add("onfocusout", "this.style.border ='none';");
        }
        private void SetRegularExpressionValidator()
        {
            _RegularExpressionValidator.ControlToValidate = _calendar.ID;
            _RegularExpressionValidator.ErrorMessage = (_LabelName == string.Empty )? "תאריך לא תקין" : "שדה '" + _LabelName + "' לא תקין";
            _RegularExpressionValidator.ID = "vldCalDate_" + _calendar.ID;
            _RegularExpressionValidator.Display = ValidatorDisplay.None;
            _RegularExpressionValidator.ValidationExpression = "^(((0[1-9]|[12]\\d|3[01])\\/(0[13578]|1[02])\\/((1[6-9]|[2-9]\\d)\\d{2}))|((0[1-9]|[12]\\d|30)\\/(0[13456789]|1[012])\\/((1[6-9]|[2-9]\\d)\\d{2}))|((0[1-9]|1\\d|2[0-8])\\/02\\/((1[6-9]|[2-9]\\d)\\d{2}))|(29\\/02\\/((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$";
            _RegularExpressionValidator.SetFocusOnError = true;
            _RegularExpressionValidator.EnableClientScript = true;
            this.Controls.Add(_RegularExpressionValidator);
        }

        private void SetMaskedEditExtender()
        {
           

            _MaskedEditExtender.ID = "MaskCalDate_" + _calendar.ID;
            _MaskedEditExtender.TargetControlID = _calendar.ID;   
            _MaskedEditExtender.MaskType = MaskedEditType.Date;
            _MaskedEditExtender.Mask = "99/99/9999";
            _MaskedEditExtender.ClearMaskOnLostFocus = true;
            this.Controls.Add(_MaskedEditExtender);
         }
        private void SetValidatorCalloutExtender()
        {
           

            _ValidatorCalloutExtender.ID = "CalloutExCalDate_" + _calendar.ID;
            _ValidatorCalloutExtender.TargetControlID = _RegularExpressionValidator.ID;
            _ValidatorCalloutExtender.Width = Unit.Parse("200px");
           // _ValidatorCalloutExtender.PopupPosition= ValidatorCalloutPosition.Left;

            this.Controls.Add(_ValidatorCalloutExtender);
        }  
    }
}
