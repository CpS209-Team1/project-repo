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
using System.Media;
using Model;

namespace SilentKnight
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {
        MainWindow mw;
        MediaPlayer media_player = new MediaPlayer();
        SoundPlayer soundPlayer;
        DispatcherTimer gameTime;
        DispatcherTimer animate;
        DispatcherTimer timer;
        double x = 0; //GUI Player's x
        double y = 0; //GUI Player's y
        GameController ctrl = new GameController();
        public GameController Controller
        {
            get { return ctrl; }
        }

        public GameScreen()
        {
            InitializeComponent();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            mw = (MainWindow)Application.Current.MainWindow;
            gameScreen.Width = World.Instance.borderRight;
            Console.WriteLine(World.Instance.borderRight);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
            y = 20;
            x = 15;
            Canvas.SetTop(Plr, y);
            Canvas.SetLeft(Plr, x);
            DoSpawn(10); //Spawns 10 enemies
            EnemyEvents(); //Starts EnemyEvents timer
            GameTimer();
            soundPlayer = new SoundPlayer(SilentKnight.Properties.Resources.sword_swing);
        }

        private void GameTimer()
        {
            InitializeComponent();
            gameTime = new DispatcherTimer();
            gameTime.Interval = new TimeSpan(0, 0, 0, 1, 0);
            gameTime.Tick += new EventHandler(AddTime);
            gameTime.Start();
        }

        private void AddTime(object sender, EventArgs e)
        {
            int time = ctrl.AddTime();
            if (time < 60 && time > 0)
            {
                secondTxt.Text = time.ToString("D2");
            }
            else
            {
                int minute = Convert.ToInt32(minuteTxt.Text) + 1;
                minuteTxt.Text = minute.ToString("D2");
                secondTxt.Text = time.ToString("D2");
            }
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
                    levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                    DoSpawn(10);
                    World.Instance.LevelCount += 1;
                }
                else
                {
                    DoSpawn(10);
                    World.Instance.LevelCount += 1;
                    levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                }
            }
            else if (World.Instance.Entities.Count == 9 && World.Instance.LevelCount == 1 && World.Instance.GameCompleted == false)
            {
                gameTime.Stop();
                World.Instance.GameCompleted = true;
                ctrl.CalculateScore();
                scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);
                mw.ShowHighScoreScreen();
            }
        }

        /// <summary>
        /// Activates each 'animage.Tick' ever 10 mls
        /// </summary>
        void EnemyEvents()
        {
            animate = new DispatcherTimer();
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
            World.Instance.borderRight = gameScreen.ActualWidth - 50; //50 is the picture width
            World.Instance.borderBottom = gameScreen.ActualHeight - 50;
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
                if (y + .05 <= gameScreenCanvas.ActualHeight - Plr.ActualHeight)
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
                if (x + .05 <= gameScreenCanvas.ActualWidth - Plr.ActualWidth)
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

            Console.WriteLine("Swinging sword!");

            Task.Run(()=> soundPlayer.PlaySync());

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
            int playerHits = ctrl.ComputeEnemyAttack();

            if (playerHits > 0 && Player.Instance.Health > 0)
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
                    if (Player.Instance.HealthLevel == 6) //This number is the amount of sprites per row on the sprite sheet
                    {

                        Player.Instance.HealthLevel = 1;
                        HealthSheet.X = 0;
                        HealthSheet.Y -= 67;
                    }
                    HealthSheet.X -= 264;
                    Player.Instance.HealthLevel += 1;
                }
            }
            else
            {
                HealthSheet.X -= 264;
                Player.Instance.HealthLevel += 1;
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
                gameScreenCanvas.Children.Remove((UIElement)i.Observer);
            }
            World.Instance.DeadEnemy = new List<Enemy>();
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
            scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);
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
                int x = rand.Next(0, (int)gameScreenCanvas.ActualWidth - 50);
                int y = rand.Next((int)gameScreenCanvas.ActualHeight - 52, (int)gameScreenCanvas.ActualHeight - 50);
                var enemyControl = new EnemyControl();
                enemyControl.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/Assets/skeleton.png", UriKind.Relative))

                };
                enemyControl.Width = 50;
                enemyControl.Height = 50;
                Canvas.SetTop(enemyControl, x);
                Canvas.SetLeft(enemyControl, y);
                gameScreenCanvas.Children.Add(enemyControl);
                var enemy = new Skeleton(enemyControl, x, y, "skeleton");
                World.Instance.Entities.Add(enemy);
            }
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Thickness margin = levelTxt.Margin;
            margin.Left = World.Instance.borderRight - 220;
            levelTxt.Margin = margin;

            margin = enemyTxt.Margin;
            margin.Left = World.Instance.borderRight - 220;
            enemyTxt.Margin = margin;

            margin = timeTxt.Margin;
            margin.Left = World.Instance.borderRight / 2 - 150;
            timeTxt.Margin = margin;

            margin = scoreTxt.Margin;
            margin.Left = World.Instance.borderRight / 2 - 150;
            scoreTxt.Margin = margin;
            Console.WriteLine(World.Instance.borderRight);
            ctrl.KeepEnemyInBounds();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && timer != null)
            {
                Console.WriteLine("Pressed Escape!");
                timer.Stop();
                animate.Stop();
                gameTime.Stop();
                PauseWindow pause = new PauseWindow(ctrl);
                pause.ShowDialog();
                timer.Start();
                animate.Start();
                gameTime.Start();
            }
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
        public void NotifySpawn(int x, int y)
        {

        }
    }
}
