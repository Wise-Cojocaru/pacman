using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Core;
using System.Threading;
using Windows.UI.Xaml;
using PacManNamespace.Models;
namespace PacManNamespace
{
    public class Server
    {

        public TcpListener listener {get; set;}
        public TcpClient client { get; set; }
        DispatcherTimer timer;
        Pacman Pacman;

        public Server()
        {
            IPAddress ipAddress = IPAddress.Loopback;
            listener = new TcpListener(ipAddress, 5000);
            listener.Start();
        }

        private async void Timer_Tick(object sender, object e)
        {
            try
            {
                if (listener.Pending())
                {
                    timer.Stop();
                    client = await listener.AcceptTcpClientAsync();
                    Task hndClient = Task.Run(() => HandleClient());
                }
            }
            catch (Exception ex)
            {

            }

        }

        void HandleClient()
        {
         
            

        }
    }
}
