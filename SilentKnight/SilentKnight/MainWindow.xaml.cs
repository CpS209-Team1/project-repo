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
using System.Windows.Threading;
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HighScore topScore;
        GameScreen gs = new GameScreen();
        HelpScreen hs;
        HighScoresScreen highscoresscreen;
        AboutScreen aboutscreen;
        StartScreen startScreen;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            topScore = new HighScore();
            startScreen = new StartScreen(this, gs);
            highscoresscreen = new HighScoresScreen(this, topScore);
            hs = new HelpScreen(this);
            aboutscreen = new AboutScreen(this);
            topScore.LoadScores("HighScoresTestData.txt");
            foreach(Score i in topScore.scoreList)
            {
                Console.WriteLine(i.Name);
            }
        }

        private void Button_Click_GameScreen(object sender, RoutedEventArgs e)
        {

            Main.Content = startScreen;
        }

        private void Button_Click_HelpScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = hs;
        }

        private void Button_Click_AboutScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = aboutscreen;
        }

        private void Button_Click_HighScoresScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = highscoresscreen;
        }

        private void btnKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e);
            gs.OnKeyUp(sender, e);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            World.Instance.borderRight = mainWindowCanvas.ActualWidth;
            World.Instance.borderBottom = mainWindowCanvas.ActualHeight;

            Main.Width = World.Instance.borderRight;
            Main.Height = World.Instance.borderBottom;

            gs.gameScreen.Width = World.Instance.borderRight;
            gs.gameScreen.Height = World.Instance.borderBottom;

            Main.VerticalContentAlignment = (VerticalAlignment)Stretch.Fill;

            Menu.Width = World.Instance.borderRight;
            Menu.Height = World.Instance.borderBottom;
        }

        private void Button_Click_LoadScreen(object sender, RoutedEventArgs e)
        {
            LoadWindow lw = new LoadWindow(gs.Controller);
            Console.WriteLine("Is it null?");
            lw.Show();
            Console.WriteLine("Maybe it is...");
        }

        public void ShowHighScoreScreen()
        {
            Main.Content = highscoresscreen;
        }
    }
}
