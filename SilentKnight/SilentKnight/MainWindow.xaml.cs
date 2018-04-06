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
        int healthLevel = 1;
        double x = 0; //GUI Player's x
        double y = 0; //GUI Player's y
        GameController ctrl = new GameController();

        public MainWindow()
        {
            //Timer for player movement
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            y = 20;
            x = 15;
            Canvas.SetTop(Plr, y);
            Canvas.SetLeft(Plr, x);
            DoSpawn(10); //Spawns 10 enemies
            EnemyEvents(); //Starts EnemyEvents timer
        }

        /// <summary>
        /// This method checks if the current wave of enemies is defeated. If so, then the game spawns in another wave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckLevelStatus(object sender, EventArgs e)
        {

            if (World.Instance.Entities.Count == 0 && World.Instance.LevelCount < 5)
            {
                if (World.Instance.LevelCount == 1)
                {
                    levelNumber.Width = 23;
                    levelSheet.X -= 73;
                    DoSpawn(10);
                    World.Instance.LevelCount += 1;
                }
                else
                {
                    DoSpawn(10);
                    World.Instance.LevelCount += 1;
                    levelSheet.X -= 131;
                }
            }
        }

        /// <summary>
        /// Activates each 'animage.Tick' ever 10 mls
        /// </summary>
        void EnemyEvents()
        {
            DispatcherTimer animate = new DispatcherTimer();
            animate.Interval = new TimeSpan(0, 0, 0, 0, 10);
            animate.Tick += new EventHandler(AnimateEnemy); //Adds AnimateEnemy to the timer
            animate.Tick += new EventHandler(EnemyAttack); //Adds EnemyAttack to the timer
            animate.Tick += new EventHandler(CheckLevelStatus); //Adds CheckLevelStatus to the timer
            animate.Start();
        }

        /// <summary>
        /// This method checks the right and left borders and then calls `UpdatePosition` to update each individual position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimateEnemy(object sender, EventArgs e)
        {
            World.Instance.borderRight = canvas.ActualWidth - 50; //50 is the picture width
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
        /// This method tests for player click (attack) then calls `ComputePlayerAttack` then  `KilledEnemy`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ctrl.ComputePlayerAttack();
            KilledEnemy();
        }

        /// <summary>
        /// Enemy Attack calls `ComputeEnemyAttack`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EnemyAttack(object sender, EventArgs e)
        {
            bool playerHit = ctrl.ComputeEnemyAttack();

            if (playerHit && Player.Instance.Health > 0)
            {
                PlayerHealth();
            }
        }

        /// <summary>
        /// This method is used to display the player's health
        /// </summary>
        void PlayerHealth()
        {
            if (Player.Instance.Health > 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (healthLevel == 6) //This number is the amount of sprites per row on the sprite sheet
                    {

                        healthLevel = 1;
                        HealthSheet.X = 0;
                        HealthSheet.Y -= 67;
                    }
                    HealthSheet.X -= 264;
                    healthLevel += 1;
                }
            }
            else
            {
                HealthSheet.X -= 264;
                healthLevel += 1;
            }

        }

        /// <summary>
        /// This method loops through DeadEnemy and removes each instance from the canvas
        /// DeadEnemy is reset to empty after all of the current dead enemies are removed
        /// </summary>
        void KilledEnemy()
        {
            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                canvas.Children.Remove((UIElement)i.observer);
            }
            World.Instance.DeadEnemy = new List<Enemy>();
        }

        /// <summary>
        /// This method creates new enemies
        /// </summary>
        /// <param name="enemyCount"></param>
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

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            World.Instance.borderBottom = canvas.ActualHeight;
            World.Instance.borderRight = canvas.ActualWidth;

            Thickness margin = levelNumber.Margin;
            margin.Left = World.Instance.borderRight - 75;
            levelNumber.Margin = margin;
            margin = levelTxt.Margin;
            margin.Left = World.Instance.borderRight - 190;
            levelTxt.Margin = margin;

        }
    }

    /// <summary>
    /// This class updates the enemy's position
    /// </summary>
    class EnemyControl : ContentControl, IEnemyObserver
    {
        public void NotifyMoved(Enemy enemy)
        {
            Canvas.SetTop(this, enemy.EnemyLoc.Y);
            Canvas.SetLeft(this, enemy.EnemyLoc.X);
        }
    }
}
