using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using Xamarin.Forms;

namespace XamarinRefitTimeout
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnClicked(object sender, EventArgs e)
        {
            try
            {
                string baseUrl = "XXXX";
                var apiClient = RestService.For<ICallApi>(baseUrl);
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(10));
                var response = await apiClient.GetAll(cts.Token);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(content);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }
    }
}
