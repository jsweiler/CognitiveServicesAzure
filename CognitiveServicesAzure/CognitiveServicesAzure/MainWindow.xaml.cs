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

        private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new OpenFileDialog();
            filePicker.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            var result = filePicker.ShowDialog();
            var imageFile = filePicker.SafeFileName;
            if (!File.Exists(imageFile)) return;
            var binary = GetBytesFromPath(imageFile);

            using (var httpClient = new HttpClient())
            {

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
