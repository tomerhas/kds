namespace CodedUITestProject1
{
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Input;
    using System.CodeDom.Compiler;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;


    public partial class UIMap
    {

        /// <summary>
        /// Cancel_Activity - Use 'Cancel_ActivityParams' to pass parameters into this method.
        /// </summary>
        public void Cancel_Activity()
        {
            #region Variable Declarations
            HtmlInputButton uISD000ctl03SD_000_ctlButton = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UISD000ctl03SD_000_ctlButton;
            HtmlInputButton uISDimgAddPeilut0Button = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UISDimgAddPeilut0Button;
            HtmlCustom uISD_000_ctl03_lblKnisCustom = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UISD_000_ctl03_lblKnisCustom;
            HtmlCell uIItemCell = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UISD_000Table.UIItemCell;
            HtmlCell uIרמתשלמהגשאולדכביש4Cell = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UISD_000Table1.UIרמתשלמהגשאולדכביש4Cell;
            HtmlEdit uIתאריךשעתהיציאההוא010Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UIתאריךשעתהיציאההוא010Edit;
            HtmlEdit uICtl07MakatNumberEdit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UICtl07MakatNumberEdit;
            HtmlInputButton uIעדכןכרטיסButton = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument3.UIעדכןכרטיסButton;
            #endregion

            // Click 'SD$000$ctl03$SD_000_ctl03CancelPeilut' button
            Mouse.Click(uISD000ctl03SD_000_ctlButton, new Point(10, 13));

            // Click 'SD$imgAddPeilut0' button
            Mouse.Click(uISDimgAddPeilut0Button, new Point(14, 13));

            // Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
            Playback.PlaybackSettings.ContinueOnError = true;

            // Mouse hover 'SD_000_ctl03_lblKnisa030762100' custom control at (52, 1)
            Mouse.Hover(uISD_000_ctl03_lblKnisCustom, new Point(52, 1));

            // Mouse hover  cell at (136, 10)
            Mouse.Hover(uIItemCell, new Point(136, 10));

            // Mouse hover 'רמת שלמה-ג.שאול ד.כביש 4' cell at (57, 23)
            Mouse.Hover(uIרמתשלמהגשאולדכביש4Cell, new Point(57, 23));

            // Reset flag to ensure that play back stops if there is an error.
            Playback.PlaybackSettings.ContinueOnError = false;

            // Type '07:21' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
            //uIתאריךשעתהיציאההוא010Edit.Text = this.Cancel_ActivityParams.UIתאריךשעתהיציאההוא010EditText;
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא010Edit, "0721");

            // Type '{Tab}' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא010Edit, this.Cancel_ActivityParams.UIתאריךשעתהיציאההוא010EditSendKeys, ModifierKeys.None);

            // Type '30762100' in 'ctl07MakatNumber' text box
            uICtl07MakatNumberEdit.Text = this.Cancel_ActivityParams.UICtl07MakatNumberEditText;

            // Type '{Tab}' in 'ctl07MakatNumber' text box
            Keyboard.SendKeys(uICtl07MakatNumberEdit, this.Cancel_ActivityParams.UICtl07MakatNumberEditSendKeys, ModifierKeys.None);

            // Click 'עדכן כרטיס' button
            Mouse.Click(uIעדכןכרטיסButton, new Point(63, 26));
        }

        public virtual Cancel_ActivityParams Cancel_ActivityParams
        {
            get
            {
                if ((this.mCancel_ActivityParams == null))
                {
                    this.mCancel_ActivityParams = new Cancel_ActivityParams();
                }
                return this.mCancel_ActivityParams;
            }
        }

        private Cancel_ActivityParams mCancel_ActivityParams;

        /// <summary>
        /// הזןערךבשדהשעתהתחלהsh_hat - Test Case 729 - Use 'הזןערךבשדהשעתהתחלהsh_hatParams' to pass parameters into this method.
        /// </summary>
        public void הזןערךבשדהשעתהתחלהsh_hat()
        {
            #region Variable Declarations
            HtmlEdit uIתאריךתחילתהסידורהוא1Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךתחילתהסידורהוא1Edit;
            #endregion

            // Type '07:11' in 'תאריך תחילת הסידור הוא: 17/11/2013' text box
            //uIתאריךתחילתהסידורהוא1Edit.Text = this.הזןערךבשדהשעתהתחלהsh_hatParams.UIתאריךתחילתהסידורהוא1EditText;
            Keyboard.SendKeys(uIתאריךתחילתהסידורהוא1Edit, "0711");
        }

        public virtual הזןערךבשדהשעתהתחלהsh_hatParams הזןערךבשדהשעתהתחלהsh_hatParams
        {
            get
            {
                if ((this.mהזןערךבשדהשעתהתחלהsh_hatParams == null))
                {
                    this.mהזןערךבשדהשעתהתחלהsh_hatParams = new הזןערךבשדהשעתהתחלהsh_hatParams();
                }
                return this.mהזןערךבשדהשעתהתחלהsh_hatParams;
            }
        }

        private הזןערךבשדהשעתהתחלהsh_hatParams mהזןערךבשדהשעתהתחלהsh_hatParams;

        /// <summary>
        /// הזןערךבשדהשעתגמר - Test Case 729 - Use 'הזןערךבשדהשעתגמרParams' to pass parameters into this method.
        /// </summary>
        public void הזןערךבשדהשעתגמר()
        {
            #region Variable Declarations
            HtmlEdit uIתאריךגמרהסידורהוא171Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךגמרהסידורהוא171Edit;
            #endregion

            // Type '14:04' in 'תאריך גמר הסידור הוא: 17/11/2013' text box
            //uIתאריךגמרהסידורהוא171Edit.Text = this.הזןערךבשדהשעתגמרParams.UIתאריךגמרהסידורהוא171EditText;
            Keyboard.SendKeys(uIתאריךגמרהסידורהוא171Edit, "1404");
        }

        public virtual הזןערךבשדהשעתגמרParams הזןערךבשדהשעתגמרParams
        {
            get
            {
                if ((this.mהזןערךבשדהשעתגמרParams == null))
                {
                    this.mהזןערךבשדהשעתגמרParams = new הזןערךבשדהשעתגמרParams();
                }
                return this.mהזןערךבשדהשעתגמרParams;
            }
        }

        private הזןערךבשדהשעתגמרParams mהזןערךבשדהשעתגמרParams;

        /// <summary>
        /// הזןמספרסידורתקיןשעתהתחלהושעתגמר - Test Case 729 - Use 'הזןמספרסידורתקיןשעתהתחלהושעתגמרParams' to pass parameters into this method.
        /// </summary>
        public void הזןמספרסידורתקיןשעתהתחלהושעתגמר()
        {
            #region Variable Declarations
            HtmlEdit uISDlblSidur1Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UISDlblSidur1Edit;
            HtmlEdit uIתאריךתחילתהסידורהוא0Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךתחילתהסידורהוא0Edit;
            HtmlEdit uIתאריךגמרהסידורהוא010Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךגמרהסידורהוא010Edit;
            #endregion

            // Type '99300' in 'SD$lblSidur1' text box
            uISDlblSidur1Edit.Text = this.הזןמספרסידורתקיןשעתהתחלהושעתגמרParams.UISDlblSidur1EditText;

            // Type '15:00' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
            //uIתאריךתחילתהסידורהוא0Edit.Text = this.הזןמספרסידורתקיןשעתהתחלהושעתגמרParams.UIתאריךתחילתהסידורהוא0EditText;

            Keyboard.SendKeys(uIתאריךתחילתהסידורהוא0Edit, "1500");


            // Type '{Add}' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
            Keyboard.SendKeys(uIתאריךתחילתהסידורהוא0Edit, this.הזןמספרסידורתקיןשעתהתחלהושעתגמרParams.UIתאריךתחילתהסידורהוא0EditSendKeys, ModifierKeys.None);

            // Type '16:00' in 'תאריך גמר הסידור הוא: 01/01/0001' text box
            //uIתאריךגמרהסידורהוא010Edit.Text = this.הזןמספרסידורתקיןשעתהתחלהושעתגמרParams.UIתאריךגמרהסידורהוא010EditText;
            Keyboard.SendKeys(uIתאריךגמרהסידורהוא010Edit, "1600");
        }

        public virtual הזןמספרסידורתקיןשעתהתחלהושעתגמרParams הזןמספרסידורתקיןשעתהתחלהושעתגמרParams
        {
            get
            {
                if ((this.mהזןמספרסידורתקיןשעתהתחלהושעתגמרParams == null))
                {
                    this.mהזןמספרסידורתקיןשעתהתחלהושעתגמרParams = new הזןמספרסידורתקיןשעתהתחלהושעתגמרParams();
                }
                return this.mהזןמספרסידורתקיןשעתהתחלהושעתגמרParams;
            }
        }

        private הזןמספרסידורתקיןשעתהתחלהושעתגמרParams mהזןמספרסידורתקיןשעתהתחלהושעתגמרParams;

        /// <summary>
        /// הזןערךבשדהשעתיציאהלאחתהפעילויות - Test Case 729 - Use 'הזןערךבשדהשעתיציאהלאחתהפעילויותParams' to pass parameters into this method.
        /// </summary>
        public void הזןערךבשדהשעתיציאהלאחתהפעילויות()
        {
            #region Variable Declarations
            HtmlEdit uIתאריךשעתהיציאההוא171Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךשעתהיציאההוא171Edit;
            #endregion

            // Type '11:51' in 'תאריך שעת היציאה הוא: 17/11/2013' text box
            //uIתאריךשעתהיציאההוא171Edit.Text = this.הזןערךבשדהשעתיציאהלאחתהפעילויותParams.UIתאריךשעתהיציאההוא171EditText;
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא171Edit, "1151");
        }

        public virtual הזןערךבשדהשעתיציאהלאחתהפעילויותParams הזןערךבשדהשעתיציאהלאחתהפעילויותParams
        {
            get
            {
                if ((this.mהזןערךבשדהשעתיציאהלאחתהפעילויותParams == null))
                {
                    this.mהזןערךבשדהשעתיציאהלאחתהפעילויותParams = new הזןערךבשדהשעתיציאהלאחתהפעילויותParams();
                }
                return this.mהזןערךבשדהשעתיציאהלאחתהפעילויותParams;
            }
        }

        private הזןערךבשדהשעתיציאהלאחתהפעילויותParams mהזןערךבשדהשעתיציאהלאחתהפעילויותParams;

        /// <summary>
        /// Add_activity - Use 'Add_activityParams' to pass parameters into this method.
        /// </summary>
        public void Add_activity()
        {
            #region Variable Declarations
            HtmlInputButton uISDimgAddPeilut0Button = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument5.UISDimgAddPeilut0Button;
            HtmlInputButton uISDimgAddPeilut0Button1 = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument5.UISDimgAddPeilut0Button1;
            HtmlEdit uIתאריךשעתהיציאההוא010Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument5.UIתאריךשעתהיציאההוא010Edit;
            HtmlEdit uICtl07MakatNumberEdit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument5.UICtl07MakatNumberEdit;
            HtmlInputButton uISD000ctl07SD_000_ctlButton = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument5.UISD000ctl07SD_000_ctlButton;
            #endregion

            // Click 'SD$imgAddPeilut0' button
            Mouse.Click(uISDimgAddPeilut0Button, new Point(9, 13));

            // Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
            Playback.PlaybackSettings.ContinueOnError = true;

            // Mouse hover 'SD$imgAddPeilut0' button at (9, 13)
            Mouse.Hover(uISDimgAddPeilut0Button, new Point(9, 13));

            // Mouse hover 'SD$imgAddPeilut0' button at (9, 13)
            Mouse.Hover(uISDimgAddPeilut0Button1, new Point(9, 13));

            // Reset flag to ensure that play back stops if there is an error.
            Playback.PlaybackSettings.ContinueOnError = false;

            // Type '11:30' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
            //uIתאריךשעתהיציאההוא010Edit.Text = this.Add_activityParams.UIתאריךשעתהיציאההוא010EditText;
            //uIתאריךשעתהיציאההוא010Edit.WaitForControlEnabled();

            Keyboard.SendKeys(uIתאריךשעתהיציאההוא010Edit, "1130");

            // Type '{Add}' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא010Edit, this.Add_activityParams.UIתאריךשעתהיציאההוא010EditSendKeys, ModifierKeys.None);

            // Type '79100500' in 'ctl07MakatNumber' text box
            uICtl07MakatNumberEdit.Text = this.Add_activityParams.UICtl07MakatNumberEditText;

            // Type '{Add}' in 'ctl07MakatNumber' text box
            Keyboard.SendKeys(uICtl07MakatNumberEdit, this.Add_activityParams.UICtl07MakatNumberEditSendKeys, ModifierKeys.None);

            // Click 'SD$000$ctl07$SD_000_ctl07CancelPeilut' button
            Mouse.Click(uISD000ctl07SD_000_ctlButton, new Point(8, 10));
        }

        public virtual Add_activityParams Add_activityParams
        {
            get
            {
                if ((this.mAdd_activityParams == null))
                {
                    this.mAdd_activityParams = new Add_activityParams();
                }
                return this.mAdd_activityParams;
            }
        }

        private Add_activityParams mAdd_activityParams;

        /// <summary>
        /// Add_sidur_meyuhad - Use 'Add_sidur_meyuhadParams' to pass parameters into this method.
        /// </summary>
        public void Add_sidur_meyuhad()
        {
            #region Variable Declarations
            HtmlInputButton uIהוסףסידורמיוחדButton = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument6.UIהוסףסידורמיוחדButton;
            HtmlEdit uISDlblSidur1Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument6.UISDlblSidur1Edit;
            HtmlEdit uIתאריךתחילתהסידורהוא0Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument6.UIתאריךתחילתהסידורהוא0Edit;
            HtmlEdit uIתאריךגמרהסידורהוא010Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument6.UIתאריךגמרהסידורהוא010Edit;
            #endregion

            // Click 'הוסף סידור מיוחד' button
            Mouse.Click(uIהוסףסידורמיוחדButton, new Point(56, 19));

            // Type '99300' in 'SD$lblSidur1' text box
            uISDlblSidur1Edit.Text = this.Add_sidur_meyuhadParams.UISDlblSidur1EditText;

            // Type '{Enter}{Add}' in 'SD$lblSidur1' text box
            Keyboard.SendKeys(uISDlblSidur1Edit, this.Add_sidur_meyuhadParams.UISDlblSidur1EditSendKeys, ModifierKeys.None);

            // Type '15:00' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
            //uIתאריךתחילתהסידורהוא0Edit.Text = this.Add_sidur_meyuhadParams.UIתאריךתחילתהסידורהוא0EditText;
            Keyboard.SendKeys(uIתאריךתחילתהסידורהוא0Edit, "1500");

            // Type '{Add}' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
            Keyboard.SendKeys(uIתאריךתחילתהסידורהוא0Edit, this.Add_sidur_meyuhadParams.UIתאריךתחילתהסידורהוא0EditSendKeys, ModifierKeys.None);

            // Type '16:00' in 'תאריך גמר הסידור הוא: 01/01/0001' text box
            //uIתאריךגמרהסידורהוא010Edit.Text = this.Add_sidur_meyuhadParams.UIתאריךגמרהסידורהוא010EditText;
            Keyboard.SendKeys(uIתאריךגמרהסידורהוא010Edit, "1600");
        }

        public virtual Add_sidur_meyuhadParams Add_sidur_meyuhadParams
        {
            get
            {
                if ((this.mAdd_sidur_meyuhadParams == null))
                {
                    this.mAdd_sidur_meyuhadParams = new Add_sidur_meyuhadParams();
                }
                return this.mAdd_sidur_meyuhadParams;
            }
        }

        private Add_sidur_meyuhadParams mAdd_sidur_meyuhadParams;

        /// <summary>
        /// הזןשעתיציאהומקט - Test Case 729 - Use 'הזןשעתיציאהומקטParams' to pass parameters into this method.
        /// </summary>
        public void הזןשעתיציאהומקט()
        {
            #region Variable Declarations
            HtmlEdit uIתאריךשעתהיציאההוא010Edit1 = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךשעתהיציאההוא010Edit1;
            HtmlEdit uICtl02MakatNumberEdit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UICtl02MakatNumberEdit;
            #endregion

            // Type '15:00' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
            //uIתאריךשעתהיציאההוא010Edit1.Text = this.הזןשעתיציאהומקטParams.UIתאריךשעתהיציאההוא010Edit1Text;
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא010Edit1, "1500");

            // Type '49036200' in 'ctl02MakatNumber' text box
            uICtl02MakatNumberEdit.Text = this.הזןשעתיציאהומקטParams.UICtl02MakatNumberEditText;

            // Type '{Add}' in 'ctl02MakatNumber' text box
            Keyboard.SendKeys(uICtl02MakatNumberEdit, this.הזןשעתיציאהומקטParams.UICtl02MakatNumberEditSendKeys, ModifierKeys.None);
        }

        public virtual הזןשעתיציאהומקטParams הזןשעתיציאהומקטParams
        {
            get
            {
                if ((this.mהזןשעתיציאהומקטParams == null))
                {
                    this.mהזןשעתיציאהומקטParams = new הזןשעתיציאהומקטParams();
                }
                return this.mהזןשעתיציאהומקטParams;
            }
        }

        private הזןשעתיציאהומקטParams mהזןשעתיציאהומקטParams;

        /// <summary>
        /// Type_date - Use 'Type_dateParams' to pass parameters into this method.
        /// </summary>
        public void Type_date()
        {
            #region Variable Declarations
            HtmlEdit uIClnDateEdit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument7.UIClnDateEdit;
            HtmlInputButton uIהצגButton = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument7.UIהצגButton;
            #endregion

            // Type '20/11/2013' in 'clnDate' text box
            //uIClnDateEdit.Text = this.Type_dateParams.UIClnDateEditText;
            Keyboard.SendKeys(uIClnDateEdit, "20112013");

            // Type '{Add}' in 'clnDate' text box
            Keyboard.SendKeys(uIClnDateEdit, this.Type_dateParams.UIClnDateEditSendKeys, ModifierKeys.None);

            // Click 'הצג' button
            Mouse.Click(uIהצגButton, new Point(18, 14));
        }

        public virtual Type_dateParams Type_dateParams
        {
            get
            {
                if ((this.mType_dateParams == null))
                {
                    this.mType_dateParams = new Type_dateParams();
                }
                return this.mType_dateParams;
            }
        }

        private Type_dateParams mType_dateParams;

        /// <summary>
        /// הזןערכיםתקיניםבשדותשעתהתחלהושעתגמר - Test Case 729 - Use 'הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams' to pass parameters into this method.
        /// </summary>
        public void הזןערכיםתקיניםבשדותשעתהתחלהושעתגמר()
        {
            #region Variable Declarations
            HtmlEdit uITxtStartTimeEdit = this.UIמערכתקדםשכרWebpageDiWindow1.UIמערכתקדםשכרDocument2.UITxtStartTimeEdit;
            HtmlEdit uITxtEndTimeEdit = this.UIמערכתקדםשכרWebpageDiWindow1.UIמערכתקדםשכרDocument2.UITxtEndTimeEdit;
            #endregion

            // Type '05:00' in 'txtStartTime' text box
            //uITxtStartTimeEdit.Text = this.הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams.UITxtStartTimeEditText;
            Keyboard.SendKeys(uITxtStartTimeEdit, "0500");

            // Type '{Add}' in 'txtStartTime' text box
            Keyboard.SendKeys(uITxtStartTimeEdit, this.הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams.UITxtStartTimeEditSendKeys, ModifierKeys.None);

            // Type '23:59' in 'txtEndTime' text box
            //uITxtEndTimeEdit.Text = this.הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams.UITxtEndTimeEditText;
            Keyboard.SendKeys(uITxtEndTimeEdit, "2359");
        }

        public virtual הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams
        {
            get
            {
                if ((this.mהזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams == null))
                {
                    this.mהזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams = new הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams();
                }
                return this.mהזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams;
            }
        }

        private הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams mהזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams;

        /// <summary>
        /// עדכןשעתיציאהלאחתהפעילויותבמסך - Test Case 729 - Use 'עדכןשעתיציאהלאחתהפעילויותבמסךParams' to pass parameters into this method.
        /// </summary>
        public void עדכןשעתיציאהלאחתהפעילויותבמסך()
        {
            #region Variable Declarations
            HtmlEdit uIתאריךשעתהיציאההוא191Edit = this.UIמערכתקדםשכרWebpageDiWindow.UIמערכתקדםשכרDocument.UIתאריךשעתהיציאההוא191Edit;
            #endregion

            // Type '09:12' in 'תאריך שעת היציאה הוא: 19/11/2013' text box
            //uIתאריךשעתהיציאההוא191Edit.Text = this.עדכןשעתיציאהלאחתהפעילויותבמסךParams.UIתאריךשעתהיציאההוא191EditText;
            Keyboard.SendKeys(uIתאריךשעתהיציאההוא191Edit, "0912");
        }

        public virtual עדכןשעתיציאהלאחתהפעילויותבמסךParams עדכןשעתיציאהלאחתהפעילויותבמסךParams
        {
            get
            {
                if ((this.mעדכןשעתיציאהלאחתהפעילויותבמסךParams == null))
                {
                    this.mעדכןשעתיציאהלאחתהפעילויותבמסךParams = new עדכןשעתיציאהלאחתהפעילויותבמסךParams();
                }
                return this.mעדכןשעתיציאהלאחתהפעילויותבמסךParams;
            }
        }

        private עדכןשעתיציאהלאחתהפעילויותבמסךParams mעדכןשעתיציאהלאחתהפעילויותבמסךParams;
    }
    /// <summary>
    /// Parameters to be passed into 'Cancel_Activity'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class Cancel_ActivityParams
    {

        #region Fields
        /// <summary>
        /// Type '07:21' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא010EditText = "07:21";

        /// <summary>
        /// Type '{Tab}' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא010EditSendKeys = "{Tab}";

        /// <summary>
        /// Type '30762100' in 'ctl07MakatNumber' text box
        /// </summary>
        public string UICtl07MakatNumberEditText = "30762100";

        /// <summary>
        /// Type '{Tab}' in 'ctl07MakatNumber' text box
        /// </summary>
        public string UICtl07MakatNumberEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןערךבשדהשעתהתחלהsh_hat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןערךבשדהשעתהתחלהsh_hatParams
    {

        #region Fields
        /// <summary>
        /// Type '07:11' in 'תאריך תחילת הסידור הוא: 17/11/2013' text box
        /// </summary>
        public string UIתאריךתחילתהסידורהוא1EditText = "07:11";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןערךבשדהשעתגמר'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןערךבשדהשעתגמרParams
    {

        #region Fields
        /// <summary>
        /// Type '14:04' in 'תאריך גמר הסידור הוא: 17/11/2013' text box
        /// </summary>
        public string UIתאריךגמרהסידורהוא171EditText = "14:04";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןמספרסידורתקיןשעתהתחלהושעתגמר'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןמספרסידורתקיןשעתהתחלהושעתגמרParams
    {

        #region Fields
        /// <summary>
        /// Type '99300' in 'SD$lblSidur1' text box
        /// </summary>
        public string UISDlblSidur1EditText = "99300";

        /// <summary>
        /// Type '15:00' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךתחילתהסידורהוא0EditText = "15:00";

        /// <summary>
        /// Type '{Add}' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךתחילתהסידורהוא0EditSendKeys = "{Add}";

        /// <summary>
        /// Type '16:00' in 'תאריך גמר הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךגמרהסידורהוא010EditText = "16:00";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןערךבשדהשעתיציאהלאחתהפעילויות'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןערךבשדהשעתיציאהלאחתהפעילויותParams
    {

        #region Fields
        /// <summary>
        /// Type '11:51' in 'תאריך שעת היציאה הוא: 17/11/2013' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא171EditText = "11:51";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Add_activity'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class Add_activityParams
    {

        #region Fields
        /// <summary>
        /// Type '11:30' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא010EditText = "11:30";

        /// <summary>
        /// Type '{Add}' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא010EditSendKeys = "{Add}";

        /// <summary>
        /// Type '79100500' in 'ctl07MakatNumber' text box
        /// </summary>
        public string UICtl07MakatNumberEditText = "79100500";

        /// <summary>
        /// Type '{Add}' in 'ctl07MakatNumber' text box
        /// </summary>
        public string UICtl07MakatNumberEditSendKeys = "{Add}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Add_sidur_meyuhad'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class Add_sidur_meyuhadParams
    {

        #region Fields
        /// <summary>
        /// Type '99300' in 'SD$lblSidur1' text box
        /// </summary>
        public string UISDlblSidur1EditText = "99300";

        /// <summary>
        /// Type '{Enter}{Add}' in 'SD$lblSidur1' text box
        /// </summary>
        public string UISDlblSidur1EditSendKeys = "{Enter}{Add}";

        /// <summary>
        /// Type '15:00' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךתחילתהסידורהוא0EditText = "15:00";

        /// <summary>
        /// Type '{Add}' in 'תאריך תחילת הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךתחילתהסידורהוא0EditSendKeys = "{Add}";

        /// <summary>
        /// Type '16:00' in 'תאריך גמר הסידור הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךגמרהסידורהוא010EditText = "16:00";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןשעתיציאהומקט'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןשעתיציאהומקטParams
    {

        #region Fields
        /// <summary>
        /// Type '15:00' in 'תאריך שעת היציאה הוא: 01/01/0001' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא010Edit1Text = "15:00";

        /// <summary>
        /// Type '49036200' in 'ctl02MakatNumber' text box
        /// </summary>
        public string UICtl02MakatNumberEditText = "49036200";

        /// <summary>
        /// Type '{Add}' in 'ctl02MakatNumber' text box
        /// </summary>
        public string UICtl02MakatNumberEditSendKeys = "{Add}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Type_date'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class Type_dateParams
    {

        #region Fields
        /// <summary>
        /// Type '20/11/2013' in 'clnDate' text box
        /// </summary>
        public string UIClnDateEditText = "20/11/2013";

        /// <summary>
        /// Type '{Add}' in 'clnDate' text box
        /// </summary>
        public string UIClnDateEditSendKeys = "{Add}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'הזןערכיםתקיניםבשדותשעתהתחלהושעתגמר'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class הזןערכיםתקיניםבשדותשעתהתחלהושעתגמרParams
    {

        #region Fields
        /// <summary>
        /// Type '05:00' in 'txtStartTime' text box
        /// </summary>
        public string UITxtStartTimeEditText = "05:00";

        /// <summary>
        /// Type '{Add}' in 'txtStartTime' text box
        /// </summary>
        public string UITxtStartTimeEditSendKeys = "{Add}";

        /// <summary>
        /// Type '23:59' in 'txtEndTime' text box
        /// </summary>
        public string UITxtEndTimeEditText = "23:59";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'עדכןשעתיציאהלאחתהפעילויותבמסך'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class עדכןשעתיציאהלאחתהפעילויותבמסךParams
    {

        #region Fields
        /// <summary>
        /// Type '09:12' in 'תאריך שעת היציאה הוא: 19/11/2013' text box
        /// </summary>
        public string UIתאריךשעתהיציאההוא191EditText = "09:12";
        #endregion
}
}
