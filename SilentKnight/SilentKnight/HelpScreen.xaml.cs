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
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for HelpScreen.xaml
    /// </summary>
    public partial class HelpScreen : Page
    {
        MainWindow wn;

        /// <summary>
        /// Constuctor for the Help screen
        /// </summary>
        /// <param name="t">instance of MainWindow</param>
        public HelpScreen(MainWindow t)
        {
            wn = t;
            InitializeComponent();
        }

        /// <summary>
        /// The event handler for the Back button of the Help screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            wn.Main.Content = null;
        }

        /// <summary>
        /// Handles the scrolling for the Help screen
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e"Contains the arguments passed to the event handler></param>
        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
