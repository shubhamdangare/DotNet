using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace DWalker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string Pro = textBox1.Text;
            {

                Process[] localByName = Process.GetProcessesByName(Pro);


                for (int ii = 0; ii < localByName.Length; ii++)
                {
                   textBox3.Text = Convert.ToString(localByName[ii].Threads.Count);
                    
                    textBox2.Text =  localByName[ii].ProcessName;
                    foreach (ProcessModule module in localByName[ii].Modules)
                    {
                        textBox4.Text = module.FileName + "\n" + textBox4.Text;
                      
                    }
                }
            
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;

            string dll = textBox5.Text;// "kernel32.dll";
            var wmiQueryString = string.Format("select * from CIM_ProcessExecutable");
            Dictionary<int, ProcInfo> procsMods = new Dictionary<int, ProcInfo>();
            using (var searcher = new ManagementObjectSearcher(string.Format(wmiQueryString)))
            {
                    using (var results = searcher.Get())
                    {
                        foreach (var item in results.Cast<ManagementObject>())
                        {
                            try
                            {
                                var antecedent = new ManagementObject((string)item["Antecedent"]);
                                var dependent = new ManagementObject((string)item["Dependent"]);
                                int procHandleInt = Convert.ToInt32(dependent["Handle"]);
                                ProcInfo pI = new ProcInfo { Handle = procHandleInt, FileProc = new FileInfo((string)dependent["Name"]) };
                                if (!procsMods.ContainsKey(procHandleInt))
                                {
                                    procsMods.Add(procHandleInt, pI);
                                }
                                procsMods[procHandleInt].Modules.Add(new ModInfo { FileMod = new FileInfo((string)antecedent["Name"]) });
                            }
                            catch (System.Management.ManagementException ex)
                            { 
                            }
                        }
                    }
            }

            foreach (var item in procsMods)
            {
                    //Console.WriteLine(string.Format("{0} ({1}):", item.Value.FileProc.Name, item.Key));
                    foreach (var mod in item.Value.Modules)
                    {
                        if (dll.Equals(mod.FileMod.Name))
                            textBox6.Text = textBox6.Text + "\n" + item.Value.FileProc.Name;
                            
                            //Console.WriteLine(string.Format("{0} ({1}):", item.Value.FileProc.Name, item.Key));
                    }
            }
        }

        
    }
    class ProcInfo
    {
        public FileInfo FileProc { get; set; }
        public int Handle { get; set; }
        public List<ModInfo> Modules { get; set; }

        public ProcInfo()
        {
            Modules = new List<ModInfo>();
        }
    }
    class ModInfo
    {
        public FileInfo FileMod { get; set; }
    }
}
