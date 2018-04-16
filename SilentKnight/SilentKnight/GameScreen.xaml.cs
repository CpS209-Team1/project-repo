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
        AnimationControl anim = new AnimationControl();
        public Image PlayerControl { get; set; }
        MediaPlayer media_player = new MediaPlayer();
        SoundPlayer soundPlayer;
        DispatcherTimer gameTime;
        DispatcherTimer GameEvent;
        DispatcherTimer animTimer;
        double x = 0; //GUI Player's x
        double y = 0; //GUI Player's y

        GameController ctrl = new GameController();
        public GameController Controller
        {
            get { return ctrl; }
        }

        public GameScreen(MainWindow m)
        {
            mw = m;
            InitializeComponent();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
          foreach(Image i in enemyCanvas.Children)
            {
                if(i.Name != "Plr")
                {
                    enemyCanvas.Children.Remove(i);
                }
            }
            minuteTxt.Text = "00";
            secondTxt.Text = "00";
            scoreNum.Text = "0";
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
            levelNum.Text = "1";
            PlayerHealth();
            gameScreen.Width = mw.ActualWidth;
            if (World.Instance.Load == true)
            {
                SpawnArrow();
            }
            PlayerControl = Plr;
            y = Player.Instance.PlayerLoc.Y;
            x = Player.Instance.PlayerLoc.X;
            Canvas.SetTop(Plr, y);
            Canvas.SetLeft(Plr, x);
            anim.PlayerImage = Plr;
            DoSpawn(10); //Spawns 10 enemies
            GameEvents(); //Starts EnemyEvents timer
            GameTimer();
            AnimTimer();
            soundPlayer = new SoundPlayer(SilentKnight.Properties.Resources.sword_swing);
        }



        //TIMERS AND LOGIC LINES 74 - 149

        private void GameTimer()
        {
            InitializeComponent();
            gameTime = new DispatcherTimer();
            gameTime.Interval = new TimeSpan(0, 0, 0, 1, 0);
            gameTime.Tick += new EventHandler(AddTime);
            gameTime.Start();
        }

        /// <summary>
        /// Activates each 'animage.Tick' ever 10 mls
        /// </summary>
        void GameEvents()
        {
            GameEvent = new DispatcherTimer();
            Console.WriteLine(GameEvent);
            GameEvent.Interval = new TimeSpan(0, 0, 0, 0, 10);
            GameEvent.Tick += new EventHandler(AnimateEntity); //Adds AnimateEnemy to the timer
            GameEvent.Tick += new EventHandler(EntityAttack); //Adds EnemyAttack to the timer
            GameEvent.Tick += new EventHandler(CheckLevelStatus); //Adds CheckLevelStatus to the timer
            GameEvent.Tick += new EventHandler(MovePlayer);
            GameEvent.Tick += new EventHandler(anim.UpdateDirection);
            GameEvent.Start();
        }

        void AnimTimer()
        {
            animTimer = new DispatcherTimer();
            animTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            animTimer.Tick += new EventHandler(anim.UpdateFrame);
            animTimer.Start();
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
            if (Player.Instance.PlayerIsDead == true)
            {
                GameEvent.Stop();
                gameTime.Stop();
                World.Instance.GameCompleted = true;
               
                scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);
                mw.ShowHighScoreScreen();
            }
            else if (World.Instance.Entities.Count == 0 && World.Instance.LevelCount < 5)
            {
                
                if (World.Instance.LevelCount == 1)
                {
                    levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                    DoSpawn(10 + World.Instance.LevelCount);
                    World.Instance.LevelCount += 1;
                }
                else
                {
                    DoSpawn(10 + World.Instance.LevelCount);
                    World.Instance.LevelCount += 1;
                    levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                }
            }
            else if (World.Instance.Entities.Count == 0 && World.Instance.LevelCount == 5 && World.Instance.GameCompleted == false)
            {

                GameEvent.Stop();
                gameTime.Stop();
                World.Instance.GameCompleted = true;
                ctrl.CalculateScore();
                
                scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);

                mw.ShowHighScoreScreen();
            }
            if (Player.Instance.PlayerCoolDown != 0)
            {
                Player.Instance.PlayerCoolDown -= 1;
            }
        }


        //KEY PRESS AND PLAYER MOVEMENT

        private void MovePlayer(object sender, EventArgs e)
        {
            string KeyPress = "";
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (y + 5 <= World.Instance.borderBottom - 75)
                {
                    KeyPress = "S";
                    y += 5;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                if (y - 5 >= 0 - 75)
                {
                    KeyPress = "W";
                    y -= 5;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                if (x - 5 >= 0 - 75)
                {
                    KeyPress = "A";
                    x -= 5;
                    Canvas.SetLeft(Plr, x);
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                if (x + 5 <= World.Instance.borderRight - 75)
                {
                    KeyPress = "D";
                    x += 5;
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

            //Console.WriteLine("Swinging sword!");

            Task.Run(() => soundPlayer.PlaySync());

            ctrl.ComputePlayerMeleeAttack();
            KilledEnemy();
        }

        private void Plr_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpawnArrow();
        }

        void SpawnArrow()
        {
            if (Player.Instance.PlayerCoolDown == 0 && World.Instance.Load == false)
            {
                ctrl.ComputePlayerRangedAttack();
                if (Player.Instance.PlayerState.currentState is RangedState)
                {
                    var arrowControl = new ArrowControl(enemyCanvas,Player.Instance.PlayerDirection);
                    Canvas.SetTop(arrowControl, y);
                    Canvas.SetLeft(arrowControl, x);


                    // Create model object and associate with this page
                    var arrow = new Arrow(Player.Instance.PlayerLoc.X + (Plr.Width/2), Player.Instance.PlayerLoc.Y + (Plr.Height/2), Convert.ToString(Player.Instance.PlayerDirection));
                    arrow.ArrowMovedEvent += arrowControl.NotifyMoved;
                    arrow.ArrowKilledEvent += arrowControl.NotifyDead;
                    arrow.ArrowSpawnEvent += arrowControl.NotifySpawn;
                    World.Instance.AddEntityArrow(arrow);
                    arrow.Spawn();

                }
                foreach (UIElement i in enemyCanvas.Children)
                {
                    Console.WriteLine(i);
                }
            }
            else if (World.Instance.Load == true)
            {
                foreach (Arrow i in World.Instance.EntitiesArrow)
                {
                    var arrowControl = new ArrowControl(enemyCanvas, i.ArrowDirection);
                    Canvas.SetTop(arrowControl, i.ArrowLocation.Y);
                    Canvas.SetLeft(arrowControl, i.ArrowLocation.X);

                    i.ArrowMovedEvent += arrowControl.NotifyMoved;
                    i.ArrowKilledEvent += arrowControl.NotifyDead;
                    i.ArrowSpawnEvent += arrowControl.NotifySpawn;
                    i.Spawn();
                }
                World.Instance.Load = false;
            }
        }


        //ATTACK AND HEALTH LOGIC

        /// <summary>
        /// Enemy Attack calls `ComputeEnemyAttack`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EntityAttack(object sender, EventArgs e)
        {
            ctrl.ComputeEnemyAttack();
            ctrl.ComputeArrowAttack();
            PlayerHealth();
            KilledEnemy();
        }

        /// <summary>
        /// This method is used to display the player's health
        /// </summary>
        void PlayerHealth()
        {
            if (Player.Instance.Health > 0)
            {
                HealthSheet.Y = -(Player.Instance.Health * 375);
            }
            else
            {
                HealthSheet.Y = 0;
                Player.Instance.PlayerIsDead = true;
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
                enemyCanvas.Children.Remove((UIElement)i.Observer);
            }
            foreach(Arrow j in World.Instance.DeadArrow)
            {
                j.Killed();
                j.KillArrow(j);
            }
            World.Instance.DeadEnemy = new List<Enemy>();
            World.Instance.DeadArrow = new List<Arrow>();
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
            scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);
        }



        //ENEMY LOGIC LINES 276 - 333

        /// <summary>
        /// This method creates new enemies
        /// </summary>
        /// <param name="enemyCount"></param>
        public void DoSpawn(int enemyCount)
        {
            Enemy enemy;
            Random randEnt = new Random();
           
            if (World.Instance.Entities.Count == 0 && World.Instance.Load == false)
            {
                Random rand = new Random();
                for (int i = 1; i <= enemyCount; i++)
                {
                    int entType = randEnt.Next(0, 2);
                    int x = rand.Next(0, (int)World.Instance.borderRight);
                    int y = rand.Next(0, (int)World.Instance.borderBottom);
                    var enemyControl = CreateEnemyControl("/Assets/" + World.Instance.EnemyTypes[entType] + ".png", x, y);
                    switch (entType)
                    {
                        case 0:
                            enemy = new Skeleton(enemyControl, x, y, "skeleton", (int)enemyControl.Height);
                            break;
                        case 1:
                            enemy = new Troll(enemyControl, x, y, "troll", (int)enemyControl.Height);
                            break;
                        default:
                            enemy = new Skeleton(enemyControl, x, y, "skeleton", (int)enemyControl.Height);
                            break;
                    }
                  
                    World.Instance.Entities.Add(enemy);
                }
            }
            else
            {
               
                foreach (Enemy ent in World.Instance.Entities)
                {
                    Location loc = ent.EnemyLoc;
                    double x = loc.X;
                    double y = loc.Y;
                    var enemyControl = CreateEnemyControl("/Assets/skeleton.png", x, y);
                    ent.Observer = enemyControl;
                }

            }
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
        }

        public EnemyControl CreateEnemyControl(string filename, double x, double y)
        {
            var enemyControl = new EnemyControl();
            enemyControl.Content = new Image()
            {
                Source = new BitmapImage(new Uri(filename, UriKind.Relative))
            };
            enemyControl.Width = 75;
            enemyControl.Height = 75;
            Canvas.SetTop(enemyControl, x);
            Canvas.SetLeft(enemyControl, y);
            enemyCanvas.Children.Add(enemyControl);
            return enemyControl;
        }

        /// <summary>
        /// This method checks the right and left borders and then calls `UpdatePosition` to update each individual position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimateEntity(object sender, EventArgs e)
        {

            foreach (Enemy i in World.Instance.Entities)
            {
                i.UpdatePosition();
            }
            foreach (Arrow i in World.Instance.EntitiesArrow)
            {
                i.Update();
            }
        }


        //DYNAMIC MATH LINES 338 - 357

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Thickness margin = levelTxt.Margin;
            margin.Left = World.Instance.MenuBorderRight - 220;
            levelTxt.Margin = margin;

            margin = enemyTxt.Margin;
            margin.Left = World.Instance.MenuBorderRight - 220;
            enemyTxt.Margin = margin;

            margin = timeTxt.Margin;
            margin.Left = World.Instance.MenuBorderRight / 2 - 150;
            timeTxt.Margin = margin;

            margin = scoreTxt.Margin;
            margin.Left = World.Instance.MenuBorderRight / 2 - 150;
            scoreTxt.Margin = margin;
           
            ctrl.KeepEnemyInBounds();
        }


        // PAUSE MENU

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && GameEvent != null)
            {
                GameEvent.Stop();
                gameTime.Stop();
                PauseWindow pause = new PauseWindow(ctrl);
                pause.ShowDialog();
                GameEvent.Start();
                gameTime.Start();
            }
        }
    }

    class ArrowControl : ContentControl
    {
        Canvas canvas;
        RotateTransform rotate;
        public ArrowControl(Canvas enemyCanvas, Direction direction)
        {
            canvas = enemyCanvas;
           Image image = new Image()
            {
                Source = new BitmapImage(new Uri("/Assets/Arrow.png", UriKind.Relative))
            };
         
            switch(direction)
            {
                case Direction.Up:
                    rotate = new RotateTransform(0);
                        break;
                case Direction.Right:
                    rotate = new RotateTransform(90);
                    break;
                case Direction.Down:
                    rotate = new RotateTransform(180);
                    break;
                case Direction.Left:
                    rotate = new RotateTransform(270);
                    break;
            }
            image.RenderTransform = rotate;
            Content = image;
        }

        public void NotifyMoved(object sender, int i)
        {
            Arrow arrow = sender as Arrow;
            Canvas.SetTop(this, arrow.ArrowLocation.Y);
            Canvas.SetLeft(this, arrow.ArrowLocation.X);
        }

        public void NotifyDead(object sender, int i)
        {
            Arrow arrow = sender as Arrow;
            if (World.Instance.EntitiesArrow.Contains(arrow))
            {
                canvas.Children.Remove(this);

            }

        }

        public void NotifySpawn(object sender, int i)
        {
            this.Width = 20;
            canvas.Children.Add(this);
        }
    }

    /// <summary>
    /// This class updates the enemy's position
    /// </summary>
    public class EnemyControl : ContentControl, IEnemyObserver
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
