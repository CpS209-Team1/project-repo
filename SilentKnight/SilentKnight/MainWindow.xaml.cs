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
        GameScreen gameScreen;
        HelpScreen helpScreen;
        HighScoresScreen highScoreScreen;
        AboutScreen aboutscreen;
        StartScreen startScreen;
        bool loaded = false;
        public MainWindow()
        {
            gameScreen = new GameScreen(this);
            InitializeComponent();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            topScore = new HighScore();
            startScreen = new StartScreen(this, gameScreen);
            highScoreScreen = new HighScoresScreen(this, topScore);
            helpScreen = new HelpScreen(this);
            aboutscreen = new AboutScreen(this);
            topScore.LoadScores("HighScoresTestData.txt");
            foreach(Score i in topScore.scoreList)
            {
                Console.WriteLine(i.Name);
            }
            loaded = true;
        }

        private void Button_Click_StartScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = startScreen;
        }

        private void Button_Click_HelpScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = helpScreen;
        }

        private void Button_Click_AboutScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = aboutscreen;
        }

        private void Button_Click_HighScoresScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = highScoreScreen;
        }

        private void btnKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e);
            gameScreen.OnKeyUp(sender, e);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            World.Instance.MenuBorderRight = mainWindow.ActualWidth;
            World.Instance.MenuBorderBottom = mainWindow.ActualHeight;

            Main.Width = World.Instance.MenuBorderRight;
            Main.Height = World.Instance.MenuBorderBottom;

            gameScreen.gameScreen.Width = World.Instance.MenuBorderRight;
            gameScreen.gameScreen.Height = World.Instance.MenuBorderBottom;

            if(loaded)
            {
                aboutscreen.aboutPage.Width = World.Instance.MenuBorderBottom;
                aboutscreen.aboutPage.Height = World.Instance.MenuBorderBottom;
                aboutscreen.aboutStack.Width = World.Instance.MenuBorderRight;
                aboutscreen.aboutStack.Height = World.Instance.MenuBorderBottom;

                helpScreen.helpPage.Width = World.Instance.MenuBorderRight;
                helpScreen.helpPage.Height = World.Instance.MenuBorderBottom;
                helpScreen.helpStack.Width = World.Instance.MenuBorderRight;
                helpScreen.helpStack.Height = World.Instance.MenuBorderBottom;

                startScreen.startPage.Width = World.Instance.MenuBorderRight;
                startScreen.startPage.Height = World.Instance.MenuBorderBottom;
                startScreen.startStack.Width = World.Instance.MenuBorderRight;
                startScreen.startStack.Height = World.Instance.MenuBorderBottom;

                highScoreScreen.highPage.Width = World.Instance.MenuBorderRight;
                highScoreScreen.highPage.Height = World.Instance.MenuBorderBottom;
                highScoreScreen.highStack.Width = World.Instance.MenuBorderRight;
                highScoreScreen.highStack.Height = World.Instance.MenuBorderBottom;
            }

            gameScreen.enemyCanvas.Width = .78 * World.Instance.MenuBorderRight;
            gameScreen.enemyCanvas.Height = .63 * World.Instance.MenuBorderBottom;
           
            Thickness margin = gameScreen.enemyCanvas.Margin;
            margin.Left = (157/1400) * World.Instance.MenuBorderRight;
            margin.Top = .12 * World.Instance.MenuBorderBottom;

            Main.VerticalContentAlignment = (VerticalAlignment)Stretch.Fill;

            Menu.Width = World.Instance.MenuBorderRight;
            Menu.Height = World.Instance.MenuBorderBottom;
            World.Instance.borderRight = gameScreen.enemyCanvas.Width;
            World.Instance.borderBottom = gameScreen.enemyCanvas.Height;
        }

        private void Button_Click_LoadScreen(object sender, RoutedEventArgs e)
        {
            LoadWindow lw = new LoadWindow(gameScreen.Controller, this, gameScreen);
            lw.Show();
        }

        public void ShowHighScoreScreen()
        {
            Main.Content = highScoreScreen;
        }
    }
}
