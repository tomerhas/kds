using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using Topshelf;

namespace KdsService
{
    static class Program
    {

        public class AppInit
        {
           
            public void Start() 
            {
                Bootstrapper b = new Bootstrapper();
                b.Init();
                Console.WriteLine("Opening service...");
                var serviceHost = new ServiceHost(typeof(BatchService));
                // Open the ServiceHost to start listening for messages.
                serviceHost.Open();

              
            }
            public void Stop() 
            {
            
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var serviceName = ConfigurationManager.AppSettings["ServiceName"];
            if(string.IsNullOrWhiteSpace(serviceName))
                throw new Exception("The service name property is empty or does not exist in app settings. Please add it");
            HostFactory.Run(x =>                                
            {
                x.Service<AppInit>(s =>                        
                {
                    s.ConstructUsing(name => new AppInit());     
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                          

                x.SetDescription(serviceName);       
                x.SetDisplayName(serviceName);                       
                x.SetServiceName(serviceName);                       
            });       

        }
    }
}
