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

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        MainWindow mw;
        
        GameScreen gs;
        GameController ctrl;
        public LoadWindow(GameController c, MainWindow m, GameScreen g)
        {
            mw = m;
            gs = g;
            InitializeComponent();
            ctrl = c;
        }

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
