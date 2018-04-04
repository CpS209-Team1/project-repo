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
            y = 20;
            x = 15;
            Canvas.SetTop(Plr, y);
            Canvas.SetLeft(Plr, x);
            DoSpawn(10);
            Spawning();
        }

        void CheckLevelStatus(object sender, EventArgs e)
        {
            if (World.Instance.Entities.Count == 0)
            {
                DoSpawn(10);
            }
        }

        void Spawning()
        {
            DispatcherTimer animate = new DispatcherTimer();
            animate.Interval = new TimeSpan(0, 0, 0, 0, 10);
            animate.Tick += new EventHandler(AnimateEnemy);
            animate.Tick += new EventHandler(EnemyAttack);
            animate.Tick += new EventHandler(CheckLevelStatus);
            animate.Start();
        }

        private void AnimateEnemy(object sender, EventArgs e)
        {
            World.Instance.borderRight = canvas.ActualWidth - 50;
            World.Instance.borderBottom = canvas.ActualHeight - 50;
            foreach (Enemy i in World.Instance.Entities)
            {
                i.UpdatePosition();
            }
        }

        private void MovePlayer(object sender, EventArgs e)
        {
            string KeyPress = "";
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (y + .05 <= canvas.ActualHeight - Plr.ActualHeight)
                {
                    KeyPress = "S";
                    y += 0.05;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                if (y - 0.05 >= 0)
                {
                    KeyPress = "W";
                    y -= 0.05;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                if (x - .05 >= 0)
                {
                    KeyPress = "A";
                    x -= 0.05;
                    Canvas.SetLeft(Plr, x);
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                if (x + .05 <= canvas.ActualWidth - Plr.ActualWidth)
                {
                    KeyPress = "D";
                    x += 0.05;
                    Canvas.SetLeft(Plr, x);
                }
            }
            ctrl.ComputePlayerMove(x, y, KeyPress);
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
            KilledEnemy();
        }

        void EnemyAttack(object sender, EventArgs e)
        {
            ctrl.ComputeEnemyAttack();
        }

        void KilledEnemy()
        {
            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                canvas.Children.Remove((UIElement)i.observer);
            }
            World.Instance.DeadEnemy = new List<Enemy>();
        }



        public void DoSpawn(int enemyCount)
        {
            Random rand = new Random();
            for (int i = 1; i <= enemyCount; i++)
            {
                int x = rand.Next(0, (int)canvas.ActualWidth - 50);
                int y = rand.Next((int)canvas.ActualHeight - 52, (int)canvas.ActualHeight - 50);
                var enemyControl = new EnemyControl();
                enemyControl.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/Assets/skeleton.png", UriKind.Relative))

                };
                enemyControl.Width = 50;
                enemyControl.Height = 50;
                Canvas.SetTop(enemyControl, x);
                Canvas.SetLeft(enemyControl, y);
                canvas.Children.Add(enemyControl);
                var enemy = new Skeleton(enemyControl, x, y);
                World.Instance.Entities.Add(enemy);
            }
        }
    }
    class EnemyControl : ContentControl, IEnemyObserver
    {
        public void NotifyMoved(Enemy enemy)
        {
            Canvas.SetTop(this, enemy.EnemyLoc.Y);
            Canvas.SetLeft(this, enemy.EnemyLoc.X);
        }
    }
}
