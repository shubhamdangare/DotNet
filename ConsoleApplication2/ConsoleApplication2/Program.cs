using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


/*
 try
            {
                Process[] localByName = Process.GetProcessesByName("Notepad");
               

                    for (int ii = 0; ii < localByName.Length; ii++)
                    {
                        int cnt = localByName[ii].Threads.Count;
                        Console.WriteLine(cnt);
                        Console.WriteLine(localByName[ii].ProcessName);
                        foreach (ProcessModule module in localByName[ii].Modules)
                        {
                            Console.WriteLine(module.FileName);
                        }
                    }

               
            }
 
 */
namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string dll = "Kernel32.dll";
            while (true)
            {

                try
                {
                    Process[] processlist = Process.GetProcesses();

                    foreach (Process theprocess in processlist)
                    {
                        if (theprocess.ProcessName.Equals("svchost"))
                        {
                            continue;
                        }
                        Process[] localByName = Process.GetProcessesByName(theprocess.ProcessName);
                        for (int ii = 0; ii < localByName.Length; ii++)
                        {

                            foreach (ProcessModule module in localByName[ii].Modules)
                            {
                                if (dll.Equals(module.FileName))
                                {

                                    Console.WriteLine(theprocess.ProcessName);

                                }


                            }
                        }


                    }

                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            }
        }
    }
}
