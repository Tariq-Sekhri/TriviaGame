using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for QuestionWIndow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private DateTime questionFetchedAt;
        private string rightOrLeft = "";
        public QuestionWindow()
        {
            InitializeComponent();
            btnRight.IsEnabled = false;
            btnLeft.IsEnabled = false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Width = 800;
            Height = 450;
            fetchUserInfo();
            GetTriviaQuestion();
            btnRight.IsEnabled = true;
            btnLeft.IsEnabled = true;
        }

        private void fetchUserInfo()
        {
            using (var context = new TriviaGameEntities())
            {
                var user = (from u in context.Users
                            where u.username == Session.CurrentUser.username
                            select u).FirstOrDefault();
                Session.CurrentUser = user;
                txtUsername.Text = Session.CurrentUser.username;
                txtScore.Text = $"Score:{Session.CurrentUser.score}";
            }
        }

        private async void GetTriviaQuestion()
        {

            var result = JObject.Parse(await new HttpClient().GetStringAsync("https://opentdb.com/api.php?amount=1&type=multiple"))["results"][0];
            questionFetchedAt = DateTime.Now;


            txtQuestion.Text = WebUtility.HtmlDecode(result["question"].ToString());
            //txtAwnser.Text = WebUtility.HtmlDecode(result["correct_answer"].ToString());

            var correct = WebUtility.HtmlDecode(result["correct_answer"].ToString());
            var wrong = WebUtility.HtmlDecode(result["incorrect_answers"][0].ToString());
            var rnd = new Random().Next(2);

            btnLeft.Content = rnd == 0 ? correct : wrong;
            btnRight.Content = rnd == 0 ? wrong : correct;
            this.rightOrLeft = rnd == 0 ? "left" : "right";

        }

        private async void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            bool isCorrect = rightOrLeft == "left";
            btnLeft.Background = isCorrect ? Brushes.Green : Brushes.Red;

            using (var context = new TriviaGameEntities())
            {
                var user = context.Users.Find(Session.CurrentUser.id);
                if (user != null)
                {
                    user.score = (user.score ?? 0) + (isCorrect ? 1 : -1);
                    user.attempted++;
                    user.correct += (isCorrect ? 1 : 0);
                    user.incorrect += (!isCorrect ? 1 : 0);
                    txtScoreChange.Text = (isCorrect ? "+1" : "-1");

                    context.SaveChanges();

                }
            }
            btnRight.IsEnabled = false;
            await WaitIfNeeded();
            btnNext.Visibility = Visibility.Visible;
        }


        private async void btnRight_Click(object sender, RoutedEventArgs e)
        {
            bool isCorrect = rightOrLeft == "right";
            btnRight.Background = isCorrect ? Brushes.Green : Brushes.Red;

            using (var context = new TriviaGameEntities())
            {
                var user = context.Users.Find(Session.CurrentUser.id);
                if (user != null)
                {
                    user.score = (user.score ?? 0) + (isCorrect ? 1 : -1);
                    user.attempted++;
                    user.correct += (isCorrect ? 1 : 0);
                    user.incorrect += (!isCorrect ? 1 : 0);
                    txtScoreChange.Text = (isCorrect ? "+1" : "-1");
                    context.SaveChanges();

                }
            }
            btnLeft.IsEnabled = false;
            await WaitIfNeeded();
            btnNext.Visibility = Visibility.Visible;

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var questionWindow = new QuestionWindow();
            questionWindow.Show();
            this.Close();
        }

        private async Task WaitIfNeeded()
        {
            var elapsed = (DateTime.Now - questionFetchedAt).TotalMilliseconds;
            int delay = Math.Max(0, 5000 - (int)elapsed);
            txtTimeLeft.Text = delay > 0 ? $"API Cooldown: timeleft: {delay / 1000}" : "";
            await Task.Delay(delay);
        }


        private void ImgTrivia_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var userInfoWindow = new UserInfoWindow();
            userInfoWindow.Show();
            this.Close();
        }


    }
}
