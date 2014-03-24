using System;
using System.Collections.Generic;
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
            HostFactory.Run(x =>                                
            {
                x.Service<AppInit>(s =>                        
                {
                    s.ConstructUsing(name => new AppInit());     
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                          

                x.SetDescription("KDS For Development");       
                x.SetDisplayName("KDSDev");                       
                x.SetServiceName("KDSDev");                       
            });       


//#if  DEBUG
//            var service = new Listener();
//            service.StartListener();
//            System.Windows.Forms.Application.Run(new System.Windows.Forms.Form());
//            service.StopListener();
//            return;
//#endif
//            ServiceBase[] ServicesToRun;
//            ServicesToRun = new ServiceBase[] 
//            { 
//                new Listener() 
//            };
//            ServiceBase.Run(ServicesToRun);
        }
    }
}
