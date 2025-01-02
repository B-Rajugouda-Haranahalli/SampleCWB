using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CWB.App
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string localIpAddress = GetSpecificIpAddress();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex, "CWB APP startup failed");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static string GetSpecificIpAddress()
        {
            //string _hostname = Dns.GetHostName();
            //IPAddress[] iPAddresses = Dns.GetHostEntry(_hostname).AddressList;
            //foreach (var item in iPAddresses)
            //{
            //    string ip = item.ToString();
            //    return ip;
            //}
            //return string.Empty;

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces().Where(n=>n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback))
            {
                foreach (UnicastIPAddressInformation item in networkInterface.GetIPProperties().UnicastAddresses.Where(ip => !ip.Address.IsIPv6LinkLocal))
                {
                    Console.WriteLine("Ip :"+ item.Address.ToString());
                }
            }
            return string.Empty;
        }

    }
}
