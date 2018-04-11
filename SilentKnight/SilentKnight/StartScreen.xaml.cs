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
        MainWindow mw;
        GameScreen gs;
        public StartScreen(MainWindow t, GameScreen g)
        {
            gs = g;
            mw = t;
            InitializeComponent();
        }

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

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Player.Instance.PlayerName = username.Text;
            mw.Main.Content = gs;
        }
    }
}
