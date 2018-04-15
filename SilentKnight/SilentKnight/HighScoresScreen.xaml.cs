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
        public HighScoresScreen(MainWindow t, HighScore h)
        {
            highScores = h;
            wn = t;
            InitializeComponent();
       }
        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
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
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
