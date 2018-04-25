//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   StartScreen.xaml.cs
//Desc:   This file contains the code for the Start screen for Silent Knight
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
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Page
    {
        MainWindow mw; // Main Window
        GameScreen gs; // Game Screen

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t">Main Window</param>
        /// <param name="g">Game Screen</param>
        public StartScreen(MainWindow t, GameScreen g)
        {
            gs = g;
            mw = t;
            InitializeComponent();
        }

        /// <summary>
        /// Changes difficulty
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Difficulty_Click(object sender, RoutedEventArgs e)
        {
            string gameDifficulty = (string)difficulty.Content;
            switch(gameDifficulty)
            {
                case "Easy":
                    World.Instance.Difficulty = 2;
                    difficulty.Content = "Normal";
                    break;
                case "Normal":
                    World.Instance.Difficulty = 3;
                    difficulty.Content = "Hard";
                    break;
                case "Hard":
                    World.Instance.Difficulty = 1;
                    difficulty.Content = "Easy";
                    break;
            }
        }

        /// <summary>
        /// Changes Cheat Mode
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Cheat_Click(object sender, RoutedEventArgs e)
        {
            string gameCheat = (string)cheat.Content;
            switch(gameCheat)
            {
                case "Cheat Off":
                    World.Instance.CheatMode = true;
                    cheat.Content = "Cheat On";
                    break;
                case "Cheat On":
                    World.Instance.CheatMode = false;
                    cheat.Content = "Cheat Off";
                    break;
            }
        }

        /// <summary>
        /// Launches Game
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text != "")
            {
                Player.Instance.PlayerName = username.Text;
                mw.Main.Content = gs;
            }
        }

        /// <summary>
        /// Goes back to main menu
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mw.Main.Content = null;
        }
    }
}
