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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("HCKJHHJSJNCJSNC");
            foreach (Score i in highScores.scoreList)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine();
            highScores.SaveIfHighScore();
            foreach (Score i in highScores.scoreList)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine();
            highScores.WriteScores("HighScoresTestData.txt");
            foreach (Score i in highScores.scoreList)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine();
            NamesAndScores.Text = " ";
            DisplayHighScores();
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
