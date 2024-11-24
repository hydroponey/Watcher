using Fleck;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Watcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartWebSocketServer();
        }
        private void StartWebSocketServer()
        {
            Task.Run(() =>
            {
                var server = new WebSocketServer("ws://0.0.0.0:19111");
                server.Start(socket =>
                {
                    socket.OnOpen = () => Console.WriteLine("Client connected!");
                    socket.OnClose = () => Console.WriteLine("Client disconnected!");
                    socket.OnMessage = message =>
                    {
                        Console.WriteLine("Received: " + message);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ShowAddressWindow(message);
                        });
                    };
                });
            });
        }
        private void ShowAddressWindow(string address)
        {
            var window = new AddressWindow(address);
            window.Show();
        }
    }

}
