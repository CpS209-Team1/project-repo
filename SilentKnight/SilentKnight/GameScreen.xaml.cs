//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   GameScreen.xaml.cs
//Desc:   This file contains the code for the game screen for Silent Knight.
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
        MainWindow mw; // Used for setting screen size and activating high score screen
        PlayerAnimationControl animPlayer = new PlayerAnimationControl(); // Animation instance
        public Image PlayerControl { get; set; } // Used for animation
        MediaPlayer media_player = new MediaPlayer(); // Used for sound
        SoundPlayer soundPlayer; // Used for sound
        DispatcherTimer gameTime; // Game timer
        DispatcherTimer GameEvent; // Event timer
        DispatcherTimer animTimer; // Animation timer
        public bool CanAttack = true; // Player cooldown for sound
        public string Lock = "lock"; // For lock on threads

        double x = 0; //GUI Player's x
        double y = 0; //GUI Player's y

        GameController ctrl = new GameController(); // Reference to GameController
        public GameController Controller
        {
            get { return ctrl; }
        }

        public GameScreen(MainWindow m)
        {
            mw = m;
            InitializeComponent();
        }

        /// <summary>
        /// Sets all game view variables and settings
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            while (enemyCanvas.Children.Count > 1)
            {
                enemyCanvas.Children.RemoveAt(1);
            }
            LoadTime();
            scoreNum.Text = Convert.ToString(Player.Instance.PlayerScore);
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
            levelNum.Text = Convert.ToString(World.Instance.LevelCount);
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
            animPlayer.PlayerImage = Plr;
            DoSpawn(10); //Spawns 10 enemies
            GameEvents(); //Starts EnemyEvents timer
            GameTimer();
            AnimTimer();
            soundPlayer = new SoundPlayer(SilentKnight.Properties.Resources.sword_swing);
        }

        /// <summary>
        /// Loads the game's time
        /// </summary>
        void LoadTime()
        {
            secondTxt.Text = Convert.ToString(World.Instance.Time % 60);
            minuteTxt.Text = Convert.ToString(World.Instance.Time / 60);
        }

        //TIMERS AND LOGIC LINES 74 - 149

        /// <summary>
        /// Controls the time that is displayed in game
        /// </summary>
        private void GameTimer()
        {
            InitializeComponent();
            gameTime = new DispatcherTimer();
            gameTime.Interval = new TimeSpan(0, 0, 0, 1, 0);
            gameTime.Tick += new EventHandler(AddTime);
            gameTime.Start();
        }

        /// <summary>
        /// Activates each 'animage.Tick' every 10 mls and controlls the checking of player and enemy events
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
            GameEvent.Tick += new EventHandler(animPlayer.UpdateDirection);
            GameEvent.Start();
        }

        /// <summary>
        /// Controlls animations
        /// </summary>
        void AnimTimer()
        {
            animTimer = new DispatcherTimer();
            animTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            animTimer.Tick += new EventHandler(animPlayer.UpdateFrame);
            animTimer.Start();
        }

        /// <summary>
        /// Adds a second to the timer and then updates the displayed time
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void AddTime(object sender, EventArgs e)
        {
            ctrl.AddTime();
            secondTxt.Text = (World.Instance.Time % 60).ToString("D2");
            minuteTxt.Text = (World.Instance.Time / 60).ToString("D2");
        }

        /// <summary>
        /// This method checks if the current wave of enemies is defeated. If so, then the game spawns in another wave.
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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
                    DoSpawn(10 + World.Instance.LevelCount * 2);
                    World.Instance.LevelCount += 1;
                    levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                }
            }
            else if (World.Instance.Entities.Count == 0 && World.Instance.LevelCount == 5)
            {
                levelNum.Text = Convert.ToString(Convert.ToInt32(levelNum.Text) + 1);
                DoSpawn(1);
                World.Instance.LevelCount += 1;
            }
            else if (World.Instance.Entities.Count == 0 && World.Instance.LevelCount == 6 && World.Instance.GameCompleted == false)
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

        /// <summary>
        /// Controlls player movement
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void MovePlayer(object sender, EventArgs e)
        {
            string KeyPress = "";
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (y + 5 <= World.Instance.borderBottom - 75)
                {
                    animPlayer.KeyDown = true;
                    KeyPress = "S";
                    y += 5;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                if (y - 5 >= 0 - 75)
                {
                    animPlayer.KeyDown = true;
                    KeyPress = "W";
                    y -= 5;
                    Canvas.SetTop(Plr, y);
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                if (x - 5 >= 0 - 75)
                {
                    animPlayer.KeyDown = true;
                    KeyPress = "A";
                    x -= 5;
                    Canvas.SetLeft(Plr, x);
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                if (x + 5 <= World.Instance.borderRight - 75)
                {
                    animPlayer.KeyDown = true;
                    KeyPress = "D";
                    x += 5;
                    Canvas.SetLeft(Plr, x);
                }
            }
            ctrl.ComputePlayerMove(x, y, KeyPress);
        }

        /// <summary>
        /// This method tests for player click (attack) then calls `ComputePlayerMeleeAttack` then  `KilledEnemy`
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private async void Plr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanAttack && Player.Instance.PlayerCoolDown == 0)
            {
                CanAttack = false;
                animPlayer.DoSwordAttack(PlayerControl, this);
                ctrl.ComputePlayerMeleeAttack();
                await Task.Run(() => soundPlayer.PlaySync());
                KilledEnemy();
                CanAttack = true;
            }
        }

        /// <summary>
        /// This method tests for the player right click then calls `SpawnArrow`
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        private void Plr_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpawnArrow();
        }

        /// <summary>
        /// Spawns arrow in the player's viewing direction
        /// </summary>
        void SpawnArrow()
        {
            if (Player.Instance.PlayerCoolDown == 0 && World.Instance.Load == false)
            {
                ctrl.ComputePlayerRangedAttack();
                if (Player.Instance.PlayerState.currentState is RangedState)
                {
                    var arrowControl = new ArrowControl(enemyCanvas, Player.Instance.PlayerDirection);
                    Canvas.SetTop(arrowControl, y);
                    Canvas.SetLeft(arrowControl, x);


                    // Create model object and associate with this page
                    var arrow = new Arrow(Player.Instance.PlayerLoc.X, Player.Instance.PlayerLoc.Y, Convert.ToString(Player.Instance.PlayerDirection));
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
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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
            foreach (Arrow j in World.Instance.DeadArrow)
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
                    EnemyControl enemyControl;
                    int entType = randEnt.Next(0, 2);
                    int x = rand.Next(0, (int)World.Instance.borderRight - 210);
                    int y = rand.Next(0, (int)World.Instance.borderBottom - 210);

                    if (World.Instance.LevelCount != 5)
                    {
                        switch (entType)
                        {
                            case 0:
                                enemyControl = CreateEnemyControl(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", World.Instance.EnemyTypes[entType], World.Instance.EnemyTypes[entType], 18), x, y, 250);
                                enemy = new Skeleton(enemyControl, x, y, "skeleton", (int)enemyControl.Height);
                                break;
                            case 1:
                                enemyControl = CreateEnemyControl(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", World.Instance.EnemyTypes[entType], World.Instance.EnemyTypes[entType], 18), x, y, 200);
                                enemy = new Troll(enemyControl, x, y, "troll", (int)enemyControl.Height);
                                break;
                            default:
                                enemyControl = CreateEnemyControl(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", World.Instance.EnemyTypes[entType], World.Instance.EnemyTypes[entType], 18), x, y, 250);
                                enemy = new Skeleton(enemyControl, x, y, "skeleton", (int)enemyControl.Height);
                                break;
                        }
                    }
                    else
                    {
                        enemyControl = CreateEnemyControl(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", World.Instance.EnemyTypes[entType], World.Instance.EnemyTypes[entType], 18), x, y, 500);
                        enemy = new Spider(enemyControl, x, y, "spider", (int)enemyControl.Height);
                    }
                    World.Instance.Entities.Add(enemy);
                    enemyControl.NotifySpawn(enemy);
                }
            }
            else
            {

                foreach (Enemy ent in World.Instance.Entities)
                {
                    Location loc = ent.EnemyLoc;
                    double x = loc.X;
                    double y = loc.Y;
                    var enemyControl = CreateEnemyControl(ent.Image, x, y, ent.Height);
                    ent.Observer = enemyControl;
                    enemyControl.NotifySpawn(ent);
                }

            }
            enemyNum.Text = Convert.ToString(World.Instance.Entities.Count);
        }

        /// <summary>
        /// This class c reates an enemy controll
        /// </summary>
        /// <param name="filename">image source</param>
        /// <param name="x">enemy x</param>
        /// <param name="y">enemy y</param>
        /// <param name="size">image size</param>
        /// <returns>EnemyControl</returns>
        public EnemyControl CreateEnemyControl(string filename, double x, double y, double size)
        {
            var enemyControl = new EnemyControl();
            enemyControl.Content = new Image()
            {
                Source = new BitmapImage(new Uri(filename, UriKind.Relative))
            };
           
                enemyControl.Width = size;
                enemyControl.Height = size;
           
            Canvas.SetTop(enemyControl, x);
            Canvas.SetLeft(enemyControl, y);
            enemyCanvas.Children.Add(enemyControl);
            return enemyControl;
        }

        /// <summary>
        /// This method checks the right and left borders and then calls `UpdatePosition` to update each individual position
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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

        /// <summary>
        /// Dynamically changes gamescreen size
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
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

        /// <summary>
        /// Pauses game and allows saving
        /// </summary>
        /// <param name="sender">Tells which object set off this event handler</param>
        /// <param name="e">Contains the arguments passed to the event handler</param>
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && GameEvent != null)
            {
                GameEvent.Stop();
                gameTime.Stop();
                animTimer.Stop();
				foreach(Enemy ent in World.Instance.Entities) { ent.StopEnemyControl(); }
                PauseWindow pause = new PauseWindow(ctrl);
				pause.ShowDialog();
				foreach (Enemy ent in World.Instance.Entities) { ent.StartEnemyControl(); }
				animTimer.Start();
                GameEvent.Start();
                gameTime.Start();
                return;
            }
            switch (e.Key)
            {
                case Key.W:
                    animPlayer.KeyDown = false;
                    break;
                case Key.A:
                    animPlayer.KeyDown = false;
                    break;
                case Key.S:
                    animPlayer.KeyDown = false;
                    break;
                case Key.D:
                    animPlayer.KeyDown = false;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// This class contains arrow view logic
    /// </summary>
    class ArrowControl : ContentControl
    {
        Canvas canvas; // Enemy canvas
        RotateTransform rotate; // Arrow direction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyCanvas">Reference to enemyCanvas</param>
        /// <param name="direction">Arrow's flying direction</param>
        public ArrowControl(Canvas enemyCanvas, Direction direction)
        {
            canvas = enemyCanvas;
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("/Assets/Arrow.png", UriKind.Relative))
            };

            switch (direction)
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

        /// <summary>
        /// Animates arrow
        /// </summary>
        /// <param name="sender">Arrow</param>
        /// <param name="i"></param>
        public void NotifyMoved(object sender, int i)
        {
            Arrow arrow = sender as Arrow;
            Canvas.SetTop(this, arrow.ArrowLocation.Y);
            Canvas.SetLeft(this, arrow.ArrowLocation.X);
        }

        /// <summary>
        /// Kills arrows once they hit an enemy or wall
        /// </summary>
        /// <param name="sender">Arrow</param>
        /// <param name="i"></param>
        public void NotifyDead(object sender, int i)
        {
            Arrow arrow = sender as Arrow;
            if (World.Instance.EntitiesArrow.Contains(arrow))
            {
                canvas.Children.Remove(this);

            }

        }

        /// <summary>
        /// Spawns arrow
        /// </summary>
        /// <param name="sender">Arrow</param>
        /// <param name="i"></param>
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
        public EnemyAnimationControl EnemyAnim = new EnemyAnimationControl(); //For animations
        private Canvas enemycanvas { get; set; } // Canvas where animations are done
        private Canvas gamecanvas { get; set; } // For animations
        private Page gamescreen { get; set; } // Reference to the game screen

        /// <summary>
        /// Moves the enemy
        /// </summary>
        /// <param name="enemy"></param>
        public void NotifyMoved(Enemy enemy)
        {
            EnemyAnim.UpdateWalkPointer();
            Canvas.SetTop(this, enemy.EnemyLoc.Y);
            Canvas.SetLeft(this, enemy.EnemyLoc.X);
        }

        /// <summary>
        /// Spawns the enemy and begins the animations
        /// </summary>
        /// <param name="enemy"></param>
        public void NotifySpawn(Enemy enemy)
        {
            EnemyAnim.CurEnemy = enemy;
            EnemyAnim.EnemyImage = (Image)this.Content;

            enemycanvas = (Canvas)this.Parent;
            gamecanvas = (Canvas)enemycanvas.Parent;
            gamescreen = (Page)gamecanvas.Parent;
            EnemyAnim.StartAnimations();
        }

        /// <summary>
        /// Activates the enemy's animation
        /// </summary>
        /// <param name="enemy"></param>
        public void NotifyAttack(Enemy enemy)
        {
            EnemyAnim.DoMeleeAttack((Image)this.Content, gamescreen);
        }

		public void NotifyPause(Enemy enemy)
		{
			EnemyAnim.animTimer.Stop();
		}

		public void NotifyPlay(Enemy enemy)
		{
			EnemyAnim.animTimer.Start();
		}
	}
}
