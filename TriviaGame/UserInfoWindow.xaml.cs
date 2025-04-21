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
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Width = 800;
            Height = 450;

            var user = Session.CurrentUser;

            txtScore.Text = $"Score: {user.score ?? 0}";
            txtQuestions.Text = $"Attempted: {user.attempted ?? 0}";
            txtCorrect.Text = $"Correct: {user.correct ?? 0}";
            txtIncorrect.Text = $"Incorrect: {user.incorrect ?? 0}";

            int attempted = user.attempted ?? 0;
            int correct = user.correct ?? 0;
            string percentage = attempted == 0 ? "0%" : $"{(correct * 100 / attempted)}%";
            txtcorrectPercentage.Text = $"Accuracy: {percentage}";
        }


        private void logOutBtnClicked(object sender, RoutedEventArgs e)
        {
            Session.CurrentUser = null;
            var loginWindow= new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void backBtnClicked(object sender, RoutedEventArgs e)
        {
            var questionWindow = new QuestionWindow();
            questionWindow.Show();
            this.Close();
        }
    }
}
