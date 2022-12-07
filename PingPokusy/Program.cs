using System.Net;
using System.Net.NetworkInformation;

namespace PingPokusy
{
    internal class Program
    {
        static IPAddress[] GetAddresses() => Dns.GetHostAddresses(Dns.GetHostName());

        static async Task PingujVse(IPAddress[] ips)
        {
            Console.WriteLine("Získané IP adresy:");
            foreach (IPAddress address in ips)
            {
                Console.WriteLine(address.ToString());
            }
            Console.WriteLine();

            foreach (IPAddress address in GetAddresses())
            {
                using (Ping P = new Ping())
                {
                    PingReply PR = P.Send(address);
                    if (PR.Status == IPStatus.Success)
                    {
                        Console.WriteLine(Dns.GetHostByAddress(address).HostName);
                        Console.WriteLine($"IP Adresa: {PR.Address}");
                        Console.WriteLine($"Doba {PR.RoundtripTime} ms");

                    }
                }
            }
        }


        static async Task PingujAdresu(string addrs)
        {
            string ip = addrs.Trim();
            using (Ping P = new Ping())
            {
                Console.WriteLine("Posílám ping na " + Dns.GetHostByAddress(ip).HostName);
                PingReply PR = P.Send(ip);
                if (PR.Status == IPStatus.Success)
                {
                    Console.WriteLine(Dns.GetHostByAddress(ip).HostName);
                    Console.WriteLine($"IP Adresa: {PR.Address}");
                    Console.WriteLine($"Doba {PR.RoundtripTime} ms");
                }
            }
        }


        static async Task Main(string[] args)
        {
            await PingujVse(GetAddresses());

            //await PingujAdresu("192.168.51.119");

        }


    }

}
