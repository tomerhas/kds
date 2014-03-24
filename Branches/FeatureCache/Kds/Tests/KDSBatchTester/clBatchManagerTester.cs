using System;
using KdsBatch;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KDSBatchTester
{
    [TestClass]
    public class clBatchManagerTester
    {
        private static IUnityContainer _container;

        [ClassInitialize]
        public static void Initialize(TestContext t)
        {
            Bootstraper b = new Bootstraper();
            _container =  b.Init();
        }

        [TestMethod]
        public void MainOvedErrorsNew_InsertValues_Succeeded()
        {
            clBatchManager bManager = new clBatchManager();
            DateTime date = new DateTime(2014, 3,23);
            var result = bManager.MainOvedErrorsNew(75757, date); ;
            Assert.IsTrue(result.IsSuccess, "Result returned false");
        }

        [TestMethod]
        public void MainInputDataNew_InsertValues_Succeeded()
        {
            clBatchManager bManager = new clBatchManager();
            DateTime date = new DateTime(2014, 3, 23);
            var result = bManager.MainInputDataNew(75757, date); ;
            Assert.IsTrue(result.IsSuccess, "Result returned false");
        }
    }
}
