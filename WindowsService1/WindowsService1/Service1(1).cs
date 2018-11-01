using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Management;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        FileInfo fobj;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            String username = wp.Identity.Name;
            string userName1 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username1 = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];

            string currentPath = @"C:\Users\sdangare\Desktop";
            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);
            String Paths = Path.GetFullPath(currentPath);
            string file = Path.Combine(Paths, "demo.txt");
            fobj = new FileInfo(file);

            using (StreamWriter w = fobj.AppendText())
            {
               
                w.WriteLine(username1);
                w.WriteLine(DateTime.Now);


            }
        }

        protected override void OnStop()
        {
            using (StreamWriter w = fobj.AppendText())
            {

                w.WriteLine("Logged out");
                w.WriteLine(DateTime.Now);


            }


        }
        
    }
}
