﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

class MyServer
{
    public static string GetLocalIPAddress()
    {
        var Myhost = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in Myhost.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("My Web :No network adapters with an IPv4 address in thesystem!");
    }


    public static void Main(string[] args)
    {
        Int32 MyPort = 0;
        string MyIP = null;
        Socket s = null;
        TcpListener myList = null;
        MyIP = GetLocalIPAddress();
        MyPort = 21000;
        StringBuilder st;
        try
        {
            IPAddress ipAd = IPAddress.Parse(MyIP);
            myList = new TcpListener(ipAd, MyPort);
            myList.Start();
            string str;
            while (true)
            {
                s = myList.AcceptSocket();
                byte[] b = new byte[100];
                int k = s.Receive(b);
                char[] brr = new char[k];
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(b[i]));
                    brr[i] = Convert.ToChar(b[i]);
                }
                str = new string(brr);
                string price = "5.0";
                XDocument doc = XDocument.Load(str);
                var authors = doc.Descendants("price");
                foreach (var pb in authors)
                {
                    price = pb.Value;
                }
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes(price));
                if (str.Equals("exit"))
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("My Web : Exception - " + e.StackTrace);
        }
        finally
        {
            Console.WriteLine("\nMy Web : Deallocating all resources ...");
            if (s != null)
            {
                s.Close();
            }
            if (myList != null)
            {
                myList.Stop();
            }
        }
    }
}
