//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   AboutScreen.xaml.cs
//Desc:   This file contains the code for the About screen for Silent Knight
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
    /// Interaction logic for the AboutPage.xaml
    /// </summary>
    public partial class AboutScreen : Page
    {
        MainWindow wn;

        /// <summary>
        /// The constructor for the About screen
        /// </summary>
        /// <param name="t">An instance of the main window</param>
        public AboutScreen(MainWindow t)
        {
            wn = t;
            InitializeComponent();
        }

        /// <summary>
        /// Loads the About screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// The event handler for the Back button of the About screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }
    }
}
