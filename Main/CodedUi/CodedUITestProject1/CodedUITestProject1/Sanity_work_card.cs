using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITestProject1
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Sanity_work_card
    {
        public Sanity_work_card()
        {
        }

        [TestMethod]
        public void CodedUITsestMethod1()
        {
            this.UIMap.Go_to_web_page();
            this.UIMap.כנסלמסךכרטיסיעבודהלעובד();
            this.UIMap.Select_driverid_and_month();
            this.UIMap.כנסלתאריךכרטיסהעבודה();
            this.UIMap.בחרערךבשדהטכוגרף();
            this.UIMap.בחרערךבשדהלינה();
            this.UIMap.לחץעללשוניתנתוניםליוםעבודה();
            //this.UIMap.Cancel_Activity();
            this.UIMap.הזןערךבשדהשעתהתחלהsh_hat();
            this.UIMap.הזןערךבשדהשעתגמר();
            //this.UIMap.הזןערךבשדהשעתיציאהלאחתהפעילויות();
            this.UIMap.הזןערךבשדהמספררכבלפעילותהראשונהבסידורששונהממספרהרכבהקייםoto_num();
            this.UIMap.לחץעלכן();
            this.UIMap.לחץעדכןכרטיס();
            this.UIMap.Add_first_rika();
            this.UIMap.Update_work_card();
            this.UIMap.Add_last_reka();
            this.UIMap.לחץעלעדכןכרטיס();
            this.UIMap.Add_activity();
            //this.UIMap.הזןמספרסידורתקיןשעתהתחלהושעתגמר();
            this.UIMap.Add_sidur_meyuhad();
            this.UIMap.לחץעלהוסףפעילות1();
            this.UIMap.הזןשעתיציאהומקט();
            this.UIMap.Cancel_sidur();
            this.UIMap.לחץעדכןכרטיס1();
            this.UIMap.Click_next_card();
            this.UIMap.לחץעלהוסףסידורמפה();
            this.UIMap.הזןמספרסידורולחץעלהצג();
            this.UIMap.לחץעלהוסףהכל();
            this.UIMap.הזןמספררכבבפעילותהראשונה();
            this.UIMap.לחץכן();
            this.UIMap.לחץעלהוסףאתהסידורלכרטיסהעבודה();
            this.UIMap.Update_work_card();
            this.UIMap.Close_work_card();
            this.UIMap.Enter_work_card();
            //this.UIMap.Type_date();
            //this.UIMap.Type_date2();
            this.UIMap.עדכןשעתיציאהלאחתהפעילויותבמסך();
            this.UIMap.Close_and_update_workcard_popup1();
            this.UIMap.בחרמאוחודש1();
            this.UIMap.כנסלכרטיסעבודה();
            this.UIMap.לחץעללינקמספרסידור();
            this.UIMap.הזןערכיםתקיניםבשדות1();
            this.UIMap.לחץעלשמור();
            this.UIMap.Enter_driver_id();
            this.UIMap.Type_date();
            this.UIMap.Select_Headrut();
            this.UIMap.הזןערכיםתקיניםבשדותשעתהתחלהושעתגמר();
            this.UIMap.לחץעלעדכוןסידורבכרטיסעבודה();
            this.UIMap.Cancel_headrut();
            this.UIMap.לחץעלכפתורהשגויהבא();
            this.UIMap.Close_work_card();
            this.UIMap.Close_explorer();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
