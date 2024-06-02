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
using System.Text.Json;
using System.Xml.Serialization;
using Dictionary.MyClasses;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SerializationActions act;
        public Login()
        {
            InitializeComponent();
            act = new SerializationActions();
        }
        private void Login_Closed(object sender, EventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.Show();
                mainWindow.action.DeserializeWords();
                
            }
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            if(username == "" || password == "") 
            {
                MessageBox.Show("Nu s-au introdus datele corect");
                return;
            }
            bool isLoggedIn = false;
            act.DeserializeObject();

            foreach (var user in act.Users)
            {
                if (user.Username == username && user.Password== password) 
                { 
                    isLoggedIn=true;
                    break;
                }
            }
            if(isLoggedIn)
            {
                WordService wordService=new WordService();
                wordService.Owner = this;
                this.Hide();
                wordService.Show();
            }
            else
            {
                MessageBox.Show("Username-ul sau parola sunt gresite!");

            }
        }
    }
}
