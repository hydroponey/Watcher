using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Watcher
{
    /// <summary>
    /// Logique d'interaction pour AddressWindow.xaml
    /// </summary>
    public partial class AddressWindow : Window
    {
        private string _address;
        public AddressWindow(string address)
        {
            InitializeComponent();
            _address = address; Loaded += AddressWindow_Loaded;
        }
        private async void AddressWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();

            string url;
            if (IsEthereumAddress(_address))
            {
                url = $"https://etherscan.io/address/{_address}";
            }
            else if (IsSolanaAddress(_address))
            {
                url = $"https://explorer.solana.com/address/{_address}";
            }
            else
            {
                MessageBox.Show("Invalid address format.");
                Close();
                return;
            }

            webView.CoreWebView2.Navigate(url);
        }

        private bool IsEthereumAddress(string address)
        {
            return address.StartsWith("0x") && address.Length == 42;
        }

        private bool IsSolanaAddress(string address)
        {
            return address.Length == 44;
        }
    }
}
