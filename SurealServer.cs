using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;

public class SurealServer
{
	private static void ProcessClientRequests(object argument)
    {
        TcpClient client = (TcpClient)argument;
        try
        {
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string s = String.Empty;
            while(!((s = reader.ReadLine()).Equals("Exit")||(s == null)))
            {
                switch (s)
                {
                    case "GetList":
                    {
                            Console.WriteLine("From Client ->" + s);
                            SerializeSurrealists(client.GetStream());
                            client.GetStream().Flush();
                            break;
                    }
                    default:
                    {
                            Console.WriteLine("From Client ->" + s);
                            writer.WriteLine("From Server ->" + s);
                            writer.Flush();
                            break;
                    }
                }
            }
            reader.Close();
            writer.Close();
            client.Close();
            Console.WriteLine("Connection To The Client Closed");
        }
        catch(IOException)
        {
            Console.WriteLine("Problem When Connect To The Client");
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Incoming String Was Null");
        }
        catch(Exception e)
        {
            Console.WriteLine("Unknown Exception");
            Console.WriteLine(e);
        }
        finally
        {
            if(client != null)
            {
                client.Close();
            }
        }
    }

    private static void SerializeSurrealists(NetworkStream stream)
    {
        SurealDB db = new SurealDB();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(stream, db.GetSurealList());
    }

    private static void ShowServerNetworkConfig()
    {
        Console.ForegroundColor = ConsoleColor.White;
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        foreach(NetworkInterface adapter in adapters)
        {
            Console.WriteLine(adapter.Description);
            Console.WriteLine("\tName" + adapter.Name);
            Console.WriteLine("\tAddress" + adapter.GetPhysicalAddress());
            IPInterfaceProperties ip_prop = adapter.GetIPProperties();
            UnicastIPAddressInformationCollection addresses = ip_prop.UnicastAddresses;
            foreach(UnicastIPAddressInformation address in addresses)
            {
                Console.WriteLine("\t IP Address : " + address.Address);
            }
        }
        Console.ForegroundColor = ConsoleColor.Blue;
    } 

    public static void Main()
    {
        TcpListener listener = null;
        try
        {
            ShowServerNetworkConfig();
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            Console.WriteLine("SurrealServer Started...");
            while (true)
            {
                Console.WriteLine("Waiting For Connections");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Accepts New Client...");
                Thread t = new Thread(ProcessClientRequests);
                t.Start(client);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if(listener != null)
            {
                listener.Stop();
            }
        }
    }
}
