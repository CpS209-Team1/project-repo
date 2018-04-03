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
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double x = 0;
        double y = 0;
        EnemyControl blah;
        GameController ctrl = new GameController();
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0,0,0,0,0);
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            y = 234;
            x = 159;
            Canvas.SetTop(Plr, y);
            Canvas.SetLeft(Plr, x);
            Spawn.Instance.DoSpawn(10);
            Spawning();
        }

        void Spawning()
        {
            foreach(EnemyControl i in World.Instance.CanvasEntities)
            {
                canvas.Children.Add(i);
            }
        }

        private void MovePlayer(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (y + .05 <= canvas.ActualHeight - Plr.ActualHeight)
                {
                    y += 0.05;
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                if (y - 0.05 >= 0)
                {
                    y -= 0.05;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                if (x - .05 >= 0)
                {
                    x -= 0.05;
                    Canvas.SetLeft(Plr, x);
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                if (x + .05 <= canvas.ActualWidth - Plr.ActualWidth) //275 is temp until we can figure out how to get the current viewport width
                {
                    x += 0.05;
                    Canvas.SetLeft(Plr, x);
                }
            }
            ctrl.ComputePlayerMove(x, y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point currentLocation = e.GetPosition(Plr);

            double radians = Math.Atan((currentLocation.Y / 2) /
                                        (currentLocation.X / 2));
            var angle = radians * 180 / Math.PI;

            Plr.RenderTransform = new RotateTransform(angle);
        }

        /// <summary>
        /// This method tests for player click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ctrl.ComputePlayerAttack();
        }
    }
}
