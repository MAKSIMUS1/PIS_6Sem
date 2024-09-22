using System;
using System.Collections.Generic;
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

namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var url = "http://localhost:5205/calculate/sum";
                var data = new Dictionary<string, string>
                {
                    { "parmX", "null" }, 
                    { "parmY", "null" }  
                };

                data["parmX"] = Convert.ToString(parameterX.Text);
                data["parmY"] = Convert.ToString(parameterY.Text);

                var content = new FormUrlEncodedContent(data);
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(responseContent, "Успешный ответ от сервера");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при отправке запроса", "Ошибка");
                }
            }
        }
    }
}
