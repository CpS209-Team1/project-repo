//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   LoadMenu.xaml.cs
//Desc:   This file contains the code for the Load menu for Silent Knight.
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
using System.Windows.Shapes;
using System.IO;
using Model;
/// <summary>
/// Contains logic to load the game
/// </summary>

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        MainWindow mw; // Reference to main window
        
        GameScreen gs; // Reference to the game screen
        GameController ctrl; // Reference to game controller

        /// <summary>
        /// Sets variables when window is loaded
        /// </summary>
        /// <param name="c">Game controller</param>
        /// <param name="m">Main window</param>
        /// <param name="g">Game screen</param>
        public LoadWindow(GameController c, MainWindow m, GameScreen g)
        {
            mw = m;
            gs = g;
            InitializeComponent();
            ctrl = c;
        }

        /// <summary>
        /// Loads the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadClick(object sender, RoutedEventArgs e)
        {
            World.Instance.Load = true;
            Console.WriteLine("Loading..");
            string name = txtName.Text;
            if (ctrl.ValidateUser(name, "data.txt"))
            {
                Player.Instance.Login(name);
                ctrl.Load("data.txt");
                ctrl.Print();
                mw.Main.Content = gs;
                Close();
                return;
            }
            txtStatus.Text = String.Format("The user {0} does not exist.",name);
        }
    }
}
