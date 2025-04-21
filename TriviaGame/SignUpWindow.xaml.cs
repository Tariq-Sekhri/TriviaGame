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

namespace TriviaGame
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Width = 800;
            Height = 450;
        }

        private void backBtnClicked(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            
            this.Close();
        }

        private void signUpBtnClicked(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text == "") { txtError.Text = "username cant be blank"; return; }
            if(txtPassword.Text == "") { txtError.Text = "password cant be blank"; return; }
            if (txtPassword.Text != txtPasswordConfirm.Text) { txtError.Text = "passwords dont match"; return; }

            using (var context = new TriviaGameEntities())
            {
                var existingUser = (from u in context.Users
                            where u.username == txtUsername.Text
                            select u).FirstOrDefault();
                if (existingUser != null) {txtError.Text = "username unavaliable"; return; }
                User newUser = new User();
                newUser.username =txtUsername.Text;
                newUser.password =txtPassword.Text;
                newUser.score= 0;
                newUser.attempted= 0;
                newUser.correct = 0;
                newUser.incorrect = 0;
                context.Users.Add(newUser);
                context.SaveChanges();
                Session.CurrentUser = newUser;
                txtError.Text = "";
                var questionWindow = new QuestionWindow();
                questionWindow.Show();
                this.Close();
            }


        }
    }
}
