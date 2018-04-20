//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   HelpScreen.xaml.cs
//Desc:   This file contains the code for the Help screen for Silent Knight
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

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for HelpScreen.xaml
    /// </summary>
    public partial class HelpScreen : Page
    {
        MainWindow wn;
        public HelpScreen(MainWindow t)
        {
            wn = t;
            InitializeComponent();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }

        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
