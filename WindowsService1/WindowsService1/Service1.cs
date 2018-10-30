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
        FileInfo fobj;
        private System.Timers.Timer timers;
        private bool m_timerTaskSuccess;
        public Service1()
        {
            InitializeComponent();
            string currentPath = @"C:\Users\shubham\Desktop\WindowsService1\WindowsService1\bin\Debug\demo";
            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);

            String Paths = Path.GetFullPath(currentPath);
            string file = Path.Combine(Paths, "demo.txt");
            fobj = new FileInfo(file);
        
        }

        protected override void OnStart(string[] args)
        {
            timers = new System.Timers.Timer();
            timers.Interval = 60000;
            timers.Elapsed += m_mainTimer_Elapsed;
            timers.AutoReset = true;
            timers.Start();
            m_timerTaskSuccess = false;
        
                    
        }

        void m_mainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (StreamWriter w = fobj.AppendText())
            {

                w.WriteLine("Marvellous Infosystem");

            }      
        }       

        protected override void OnStop()
        {
        }
    }
}
