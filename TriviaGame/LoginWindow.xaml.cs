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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TriviaGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Width = 800;
            Height = 450; 
        }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            

            using (var context = new TriviaGameEntities())
            {
                var user = (from u in context.Users
                            where u.username == txtUsername.Text && u.password == txtPassword.Text //encodeed version of it
                            select u).FirstOrDefault();


                if (user==null)
                {
                    txtError.Text = "Username or Password Incorrect";
                }
                else
                {
                    Session.CurrentUser = user;
                    var questionWindow = new QuestionWindow();
                    questionWindow.Show();
                    this.Close();
                }
            }

        }

        private void signUpBtnClicked(object sender, RoutedEventArgs e)
        {
            var signupWindow = new SignUpWindow();
            signupWindow.Show();
            this.Close();
        }
    }
}
