using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager oManager = new Manager();
            if (oManager.HasSomethingToDo)
                oManager.Run();
        }
    }
}
