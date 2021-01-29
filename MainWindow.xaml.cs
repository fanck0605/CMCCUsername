using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CMCCUsername
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

        private static readonly Regex PhoneNumberPattern = new Regex("^\\d{11}$");

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var phoneNumber = PhoneNumberBox.Text.Trim();

            if (!PhoneNumberPattern.Match(phoneNumber).Success)
            {
                MessageBox.Show("请输入正确的手机号！");
                return;
            }

            UsernameBox.Text = MakeEncodedUsername(phoneNumber);
        }

        private static string MakeEncodedUsername(string phoneNumber)
        {
            string uuidString = Guid.NewGuid().ToString().Replace("-", "");
            byte[] uuidBytes = Encoding.Default.GetBytes(uuidString);

            byte[] phongNumberBytes = Encoding.Default.GetBytes(phoneNumber);

            int magicNumber = uuidBytes[0] + uuidBytes[1] + uuidBytes[2] + phongNumberBytes[0] + phongNumberBytes[1] + phongNumberBytes[2];

            int magicNumber2 = (177 * magicNumber + 5166) % 10000;

            return $"{uuidString}{magicNumber2:0000}01{phoneNumber}";
        }
    }
}
