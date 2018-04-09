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
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        public PauseWindow()
        {
            InitializeComponent();
        }

        static GameScreen main = (GameScreen)Application.Current.MainWindow;
        GameController ctrl = main.Controller;

        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            if (name == "Enter name here") return;
            Player.Instance.Login(name, "data.txt", ctrl);
            ctrl.Save("data.txt");
            ctrl.Print();
        }
    }
}
