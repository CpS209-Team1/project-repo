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

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for the AboutPage.xaml
    /// </summary>
    public partial class AboutScreen : Page
    {
        MainWindow wn;
        public AboutScreen(MainWindow t)
        {
            wn = t;
            InitializeComponent();
        }


        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            AboutText.Text = "You open your eyes. \n" +
                             "You are disoriented and you don't remember where you are or why you're here.\n";
//Then a sound...a rustling in the shadows.
//Footsteps are approaching.You spot the familiar glint of armor emerge from the darkness.
//One, two, three, and more blades point menacingly in your direction. A blade of light manifests in your hands. Now you remember.
//You're a knight and you know no fear."
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
