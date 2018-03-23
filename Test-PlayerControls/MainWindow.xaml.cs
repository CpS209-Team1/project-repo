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
using System.Windows.Threading;

namespace Test_PlayerControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        double x = 0;
        double y = 0;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0,0,0,0,0);
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
        }

        private void MovePlayer(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.S))
            {
                y += 0.05;
                Canvas.SetTop(Plr, y);
            }
            if(Keyboard.IsKeyDown(Key.W))
            {
                y -= 0.05;
                Canvas.SetTop(Plr, y);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                x -= 0.05;
                Canvas.SetLeft(Plr, x);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                x += 0.05;
                Canvas.SetLeft(Plr, x);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point currentLocation = e.GetPosition(Plr);

            double radians = Math.Atan((currentLocation.Y / 2) /
                                        (currentLocation.X / 2));
            var angle = radians * 180 / Math.PI;

            Plr.RenderTransform = new RotateTransform(angle);
        }
    }
}
