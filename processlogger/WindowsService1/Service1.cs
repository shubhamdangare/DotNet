using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            

            
            string currentPath = @"C:\Users\shubham\Desktop\WindowsService1\WindowsService1\bin\Debug\demo";
            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);

            String Paths = Path.GetFullPath(currentPath);
            string file = Path.Combine(Paths, "demo.txt");
            FileInfo fobj = new FileInfo(file);
            Process[] processCollection = Process.GetProcesses();
            
            using (StreamWriter w = fobj.AppendText())
            {
                  
                foreach (Process p in processCollection)
                {
           
                    w.WriteLine(p.ProcessName);
                }
            }                    
        }

        protected override void OnStop()
        {
        }
    }
}
