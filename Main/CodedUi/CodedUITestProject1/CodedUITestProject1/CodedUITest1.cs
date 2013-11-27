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
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        public void CodedUITsestMethod1()
        {


            this.UIMap.Go_to_web_page();

            this.UIMap.כנסלמסךכרטיסיעבודהלעובד();
            this.UIMap.בחרמאוחודש();
            this.UIMap.כנסלתאריךכרטיסהעבודה();
            this.UIMap.בחרערךבשדהטכוגרף();
            this.UIMap.בחרערךבשדהלינה();
            this.UIMap.לחץעללשוניתנתוניםליוםעבודה();
            this.UIMap.Cancel_peilut();
            //this.UIMap.לחץעלביטוללאחתהפעילותבסידור();
            this.UIMap.הזןערךבשדהשעתהתחלהsh_hat();
            this.UIMap.הזןערךבשדהשעתגמר();
            this.UIMap.הזןערךבשדהשעתיציאהלאחתהפעילויות();
            this.UIMap.הזןערךבשדהמקט();
            this.UIMap.הזןערךבשדהמספררכבלפעילותהראשונהבסידורששונהממספרהרכבהקייםoto_num();
            this.UIMap.לחץעלכן();
            this.UIMap.לחץעדכןכרטיס();
            this.UIMap.לחץעלהוסףריקהחץלמעלהבפעילותהראשונהביום();
            this.UIMap.לחץעלהוסףריקהחץלמטהבפעילותהאחרונהביום();
            this.UIMap.לחץעלהוסףפעילות();
            this.UIMap.הזןערכיםתקיניםבשדות();
            this.UIMap.לחץעלעדכןכרטיס();
            this.UIMap.לחץעלהוסףסידורמיוחד();
            this.UIMap.הזןמספרסידורתקיןשעתהתחלהושעתגמר();
            this.UIMap.לחץעלהוסףפעילות1();
            this.UIMap.הזןשעתיציאהומקט();
            this.UIMap.לחץעדכןכרטיס1();
            this.UIMap.לחץעלחץשמאלההיוםהבא();
            this.UIMap.לחץעלהוסףסידורמפה();
            this.UIMap.הזןמספרסידורולחץעלהצג();
            this.UIMap.לחץעלהוסףהכל();
            this.UIMap.הזןמספררכבבפעילותהראשונה();
            this.UIMap.לחץכן();
            this.UIMap.לחץעלהוסףאתהסידורלכרטיסהעבודה();
            this.UIMap.הזןתאריךבשדהתאריךולחץעלהצג();
            this.UIMap.לחץעלדיווחהיעדרות();
            this.UIMap.בחרבקומבוהיעדרות();
            this.UIMap.הזןערכיםתקיניםבשדותשעתהתחלהושעתגמר();
            this.UIMap.לחץעלעדכוןסידורבכרטיסעבודה();
            this.UIMap.לחץעלחץימינהיוםקודם();
            this.UIMap.עדכןשעתיציאהלאחתהפעילויותבמסך();
            this.UIMap.לחץסגורכרטיס();
            this.UIMap.לחץעדכןשינוייםבכרטיס();
            this.UIMap.בחרמאוחודש1();
            this.UIMap.כנסלכרטיסעבודה();
            this.UIMap.לחץעללינקמספרסידור();
            this.UIMap.הזןערכיםתקיניםבשדות1();
            this.UIMap.לחץעלשמור();
            this.UIMap.לחץעלכפתורהשגויהבא();
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
