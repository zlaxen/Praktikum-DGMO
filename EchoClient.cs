using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class EchoClient
{
    static List<PlayerData> DeserializeSurreal(NetworkStream stream)
    {
        BinaryFormatter bf = new BinaryFormatter();
        return (List<PlayerData>)bf.Deserialize(stream);
    }

    static void WriteSurrealData(List<PlayerData> surrealists)
    {
        foreach(PlayerData p in surrealists)
        {
            Console.WriteLine(p);
        }
    }

    public static void main(String[] args)
    {
        IPAddress ip_address = IPAddress.Parse("127.0.0.1");
        int port = 8080;
        try
        {
            if(args.Length >= 1)
            {
                ip_address = IPAddress.Parse(args[0]);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Ip Address Entered. The Ip Address in your device is : " + ip_address.ToString());
        }

        try
        {
            Console.WriteLine("Attempt TO Connect", ip_address.ToString(), port);
            TcpClient client = new TcpClient(ip_address.ToString(), port);
            Console.WriteLine("Connection Success");
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());
            string s = String.Empty;
            while(!s.Equals("Exit"))
            {
                Console.WriteLine("\"GetList\"to retrive list from server: ");
                s = Console.ReadLine();
                Console.WriteLine();
                switch (s)
                {
                    case "GetList":
                        {
                            writer.WriteLine(s);
                            writer.Flush();
                            WriteSurrealData(DeserializeSurreal(client.GetStream()));
                            Console.WriteLine();
                            break;
                        }
                    case "Exit":
                        {
                            writer.WriteLine(s);
                            writer.Flush();
                            break;
                        }
                    default:
                        {
                            writer.WriteLine(s);
                            writer.Flush();
                            string server_string = reader.ReadLine();
                            Console.WriteLine(server_string);
                            Console.WriteLine();
                            break;
                        }
                }
            }
            reader.Close();
            writer.Close();
            client.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}