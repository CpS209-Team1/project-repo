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
    public partial class PauseWindow : Window
    {
        GameController ctrl;
        public PauseWindow(GameController c)
        {
            InitializeComponent();
            ctrl = c;
        }

        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            Player.Instance.Login(name,"data.txt", ctrl);
            ctrl.Save("data.txt");
            ctrl.Print();
        }
    }
}
