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
        HighScore highScores = new HighScore();
        MainWindow wn;
        public HighScoresScreen(MainWindow t)
        {
            wn = t;
            InitializeComponent();
       }
        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            NamesAndScores.Text = " ";
            DisplayHighScores();
        }

        public void DisplayHighScores()
        {
            highScores.LoadScores("HighScoresTestData.txt");
            if (highScores.scoreList.Count > 0)
            {
                foreach (Score score in highScores.scoreList)
                {
                    NamesAndScores.Text += score + "\n";
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
