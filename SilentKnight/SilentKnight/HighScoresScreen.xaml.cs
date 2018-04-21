//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   HighScoresScreen.xaml.cs
//Desc:   This file contains the code for the High Scores screen for Silent Knight
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
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for HelpScreen.xaml
    /// </summary>
    public partial class HighScoresScreen : Page
    {
        HighScore highScores;
        MainWindow wn;

        /// <summary>
        /// The constructor for the HighScoresScreen class
        /// </summary>
        /// <param name="t">An instance of the main window</param>
        /// <param name="h">The score the current player had at the end of the game</param>
        public HighScoresScreen(MainWindow t, HighScore h)
        {
            highScores = h;
            wn = t;
            InitializeComponent();
        }

        /// <summary>
        /// Displays the High Scores screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            highScores.SaveIfHighScore();
            highScores.WriteScores("HighScoresTestData.txt");
            NamesAndScores.Text = " ";
            DisplayHighScores();
        }

        /// <summary>
        /// Displays the high scoring players and their scores
        /// </summary>
        public void DisplayHighScores()
        {
            NamesAndScores.Text = "";
            if (highScores.scoreList.Count > 0)
            {
                foreach (Score score in highScores.scoreList)
                {
                    NamesAndScores.Text += score + "\n";
                }
            }
            World.Instance.ResetWorld();
            Player.Instance.ResetPlayer();
        }

        /// <summary>
        /// The event handler for the Back button of the High Scores screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
