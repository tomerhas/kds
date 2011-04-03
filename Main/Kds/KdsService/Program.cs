using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace KdsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            var service = new Listener();
            service.StartListener();
            System.Windows.Forms.Application.Run(new System.Windows.Forms.Form());
            service.StopListener();
            return;
#endif
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Listener() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
