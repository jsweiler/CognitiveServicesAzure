using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CognitiveServicesAzure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            btnAnalyze.IsEnabled = false;
            try
            {
                var filePicker = new OpenFileDialog();
                filePicker.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                var result = filePicker.ShowDialog();
                var imageFile = filePicker.FileName;
                if (!File.Exists(imageFile)) return;
                var binary = GetBytesFromPath(imageFile);

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "yourAPIKey");
                    var requestParameters = "visualFeatures=Description&language=en";
                    var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + requestParameters;
                    using (var content = new ByteArrayContent(binary))
                    {
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        var response = await httpClient.PostAsync(uri, content);

                        var json = await response.Content.ReadAsStringAsync();
                        txtResults.Text = json;
                    }
                }
            }
            catch (Exception)
            {
                txtResults.Text = "An error has occurred. Make sure you put your api key where it says yourAPIKey";
            }
            finally
            {
                btnAnalyze.IsEnabled = true;
            }
        }

        byte[] GetBytesFromPath(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            using(var br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);//TODO only works if the file isnt too big
            }
        }
    }
}
