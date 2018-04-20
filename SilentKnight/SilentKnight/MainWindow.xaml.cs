//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   MainWindow.xaml.cs
//Desc:   This file contains the code for the Main Window for Silent Knight
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
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
        GameScreen gs;
        HelpScreen hs;
        HighScoresScreen highscoresscreen;
        AboutScreen aboutscreen;
        StartScreen startScreen;

        public MainWindow()
        {
            gs = new GameScreen(this);
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
            World.Instance.MenuBorderRight = mainWindowCanvas.ActualWidth;
            World.Instance.MenuBorderBottom = mainWindowCanvas.ActualHeight;

            Main.Width = World.Instance.MenuBorderRight;
            Main.Height = World.Instance.MenuBorderBottom;

            gs.gameScreen.Width = World.Instance.MenuBorderRight;
            gs.gameScreen.Height = World.Instance.MenuBorderBottom;
            gs.enemyCanvas.Width = .78 * World.Instance.MenuBorderRight;
            gs.enemyCanvas.Height = .7 * World.Instance.MenuBorderBottom;
           
            Thickness margin = gs.enemyCanvas.Margin;
            margin.Left = (157/1400) * World.Instance.MenuBorderRight;
            margin.Top = .12 * World.Instance.MenuBorderBottom;

            Main.VerticalContentAlignment = (VerticalAlignment)Stretch.Fill;

            Menu.Width = World.Instance.MenuBorderRight;
            Menu.Height = World.Instance.MenuBorderBottom;
            World.Instance.borderRight = gs.enemyCanvas.Width;
            World.Instance.borderBottom = gs.enemyCanvas.Height;
        }

        private void Button_Click_LoadScreen(object sender, RoutedEventArgs e)
        {
            LoadWindow lw = new LoadWindow(gs.Controller, this, gs);
            lw.Show();
        }

        public void ShowHighScoreScreen()
        {
            Main.Content = highscoresscreen;
        }
    }
}
