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
/// <summary>
/// This file contains logic for controlling the menu
/// </summary>

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This class controlls the menus
    /// </summary>
    public partial class MainWindow : Window
    {
        HighScore topScore; // High score
        GameScreen gameScreen; // Game screen
        HelpScreen helpScreen; // Help Screen
        HighScoresScreen highScoreScreen; // High Scores Screen
        AboutScreen aboutscreen; // About Screen
        StartScreen startScreen; // Start Screen
        bool loaded = false; // Is game loaded

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            gameScreen = new GameScreen(this);
            InitializeComponent();
        }

        /// <summary>
        /// Sets variables when window is loaded
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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

        /// <summary>
        /// Changes to Start Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Button_Click_StartScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = startScreen;
        }
        /// <summary>
        /// Changes to Help Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>

        private void Button_Click_HelpScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = helpScreen;
        }

        /// <summary>
        /// Changes to About Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Button_Click_AboutScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = aboutscreen;
        }

        /// <summary>
        /// Changes to High Scores Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Button_Click_HighScoresScreen(object sender, RoutedEventArgs e)
        {
            Main.Content = highScoreScreen;
        }

        /// <summary>
        /// Changes to Game Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void btnKeyUp(object sender, KeyEventArgs e)
        {
            gameScreen.OnKeyUp(sender, e);
        }

        /// <summary>
        /// Calculates dynamic measurements
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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

        /// <summary>
        /// Changes to Load Screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Button_Click_LoadScreen(object sender, RoutedEventArgs e)
        {
            LoadWindow lw = new LoadWindow(gameScreen.Controller, this, gameScreen);
            lw.Show();
        }

        /// <summary>
        /// Changes to High Score Screen
        /// </summary>
        public void ShowHighScoreScreen()
        {
            Main.Content = highScoreScreen;
        }
    }
}
