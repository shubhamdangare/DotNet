using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;



namespace WindowsService1
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {

      private readonly ServiceProcessInstaller processInstaller;
      private readonly System.ServiceProcess.ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.ServiceName = "AAAAAAAAMYSERVICE";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
            serviceInstaller.AfterInstall += ServiceInstaller_AfterInstall;  
        }
        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController sc = new ServiceController("AAAAAAAAMYSERVICE");
            sc.Start();
        }
    }
}
