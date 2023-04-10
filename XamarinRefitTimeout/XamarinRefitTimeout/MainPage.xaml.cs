using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Polly;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinRefitTimeout
{
    public partial class MainPage : ContentPage
    {
        public int conn = 5;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnClicked(object sender, EventArgs e)
        {
            try
            {
                var policy = Policy.Handle<NoNetworkException>()
                    .WaitAndRetryAsync(5, sleepDurationProvider => 
                                             TimeSpan.FromSeconds(5));

                await policy.ExecuteAsync(GetAllUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }

        private async Task GetAllUser()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new NoNetworkException("Connection to internet isn't available");
            }

            string baseUrl = "http://192.168.100.5:5272";
            var apiClient = RestService.For<ICallApi>(baseUrl);
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));
            var response = await apiClient.GetAll(cts.Token);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(content);
        }
    }
}
