using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;

namespace KdsService
{
    public partial class Listener : ServiceBase
    {
        private ServiceHost _serviceHost;
        public Listener()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartListener();
        }

        protected override void OnStop()
        {
            StopListener();
        }

        internal void StartListener()
        {
            _serviceHost = new ServiceHost(typeof(BatchService));
            try
            {
                // Open the ServiceHost to start listening for messages.
                _serviceHost.Open();

               
            }
            catch (System.ServiceProcess.TimeoutException timeProblem)
            {
                
            }
            catch (CommunicationException commProblem)
            {
                
            }
            

        }

        internal void StopListener()
        {
            if (_serviceHost != null)
            {
                try
                {
                    _serviceHost.Close();
                }
                catch (Exception)
                { }
            }
        }
    }
}
