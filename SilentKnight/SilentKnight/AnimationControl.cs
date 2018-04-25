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
/// <summary>
/// Contains code for updating player and enemy sprite frames based on behaviors.
/// </summary>

namespace SilentKnight
{
    /// <summary>
    /// Contains 'PlayerAnimationControl' and 'EnemyAnimationControl'
    /// </summary>
    public class PlayerAnimationControl
    {
        public Direction CurDirection { get; set; } // Player's current direction 
        public Image PlayerImage { get; set; } // Contains a reference to the player's image
        public int Pointer { get; set; } // Pointer to player's current sprite frame
        public bool KeyDown { get; set; } // Bool indicating whether the player is moving
        public bool CanAttack = true; // Bool indicating whether the player can attack
        public bool IsAttacking { get; set; } // Bool indicating whether the player is currently attacking
        public int[] CurList { get; set; } // Current list of sprite frame indicies
        public int[] RightList = { 1,2,3,4,5 }; // List of sprite frame indices
        public int[] LeftList = { 6,7,8,9,10 }; // List of sprite frame indices
        public int[] UpList = { 11,12,13,14,15 }; // List of sprite frame indices
        public int[] DownList = { 16,17,18,19,20 }; // List of sprite frame indices

        /// <summary>
        /// Sets the player's sprite frame to an image source.
        /// </summary>
        /// <param name="img">Reference to player's image</param>
        public void SetPlayerFrame(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/player/knight_topdown_basic{0}.png",CurList[Pointer]), UriKind.Relative));
        }

        /// <summary>
        /// Updates 'CurDirection' with the user's current direction.
        /// </summary>
        public void UpdateDirection(object sender, EventArgs e)
        {
            CurDirection = Player.Instance.PlayerDirection;
        }

        /// <summary>
        /// Changes frames to play sword attack animation
        /// </summary>
        /// <param name="img">Reference to player's image</param>
        /// <param name="page">Reference to current gamescreen</param>
        public void DoSwordAttack(Image img,Page page)
        {
            IsAttacking = true;
            Pointer = 3;
            page.Dispatcher.Invoke(new Action(()=> { SetPlayerFrame(img); }));
            Task.Run(() => Task.Delay(200));
            Pointer = 4;
            page.Dispatcher.Invoke(new Action(() => { SetPlayerFrame(img); }));
            Task.Run(() => Task.Delay(100));
            IsAttacking = false;
        }

        /// <summary>
        /// Updates frame pointer based on player's 'state'
        /// </summary>
        public void UpdateWalkPointer()
        {
            if (!IsAttacking)
            {
                if (KeyDown)
                {
                    if (Pointer == 0) ++Pointer;
                    else if (Pointer == 1) --Pointer;
                    else Pointer = 0;
                }
                else
                {
                    Pointer = 2;
                }
            }
        }

        /// <summary>
        /// Sets the player's current sprite frame based on his direction.
        /// </summary>
        public void UpdateFrame(object sender, EventArgs e)
        {
            //Console.WriteLine(KeyDown);
            switch(CurDirection)
            {
                case Direction.Right:
                    CurList = RightList;
                    break;
                case Direction.Left:
                    CurList = LeftList;
                    break;
                case Direction.Up:
                    CurList = UpList;
                    break;
                case Direction.Down:
                    CurList = DownList;
                    break;
            }
            SetPlayerFrame(PlayerImage);
            UpdateWalkPointer();
        }
    }

    public class EnemyAnimationControl
    {
        public Enemy CurEnemy { get; set; } // Hold an Instance of Enemy
        public Image EnemyImage { get; set; } // Contains a reference to Enemy's Image
        public int Pointer = 3; // Pointer to enemy's animation frame
        public bool IsMoving { get; set; } // Bool indicating whether an enemy is moving
        public bool CanAttack = true; // Bool indicating whether an enemy can attack
        public bool IsAttacking { get; set; } // Bool indicating whether an enemy is currently attacking
        public DispatcherTimer animTimer = new DispatcherTimer(); // Creates an instance of dispatch timer
        public int[] RightList = { 1, 2, 3, 4, 5 }; // List of indicies for selecting sprite frames
        public int[] LeftList = { 6, 7, 8, 9, 10 }; // List of indicies for selecting sprite frames
        public int[] UpList = { 11, 12, 13, 14, 15 }; // List of indicies for selecting sprite frames
        static public int[] DownList = { 16, 17, 18, 19, 20 }; // List of indicies for selecting sprite frames
        public int[] CurList = DownList; // Contains the current direction list

        /// <summary>
        /// Sets up animation timer for enemy animations
        /// </summary>
        public void StartAnimations()
        {
            animTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            animTimer.Tick += new EventHandler(UpdateFrame);
            animTimer.Start();
        }

        /// <summary>
        /// Sets the enemy's sprite frame to an image source.
        /// </summary>
        /// <param name="img">Reference to enemy image</param>
        public void SetEnemyFrame(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", CurEnemy.GetKind(), CurEnemy.GetKind(), CurList[Pointer]), UriKind.Relative));
        }

        /// <summary>
        /// Updates enemy's frame pointer.
        /// </summary>
        public void UpdateWalkPointer()
        {
            if (!IsAttacking)
            {
                if (CurEnemy.IsMoving)
                {
                    if (Pointer == 0) ++Pointer;
                    else if (Pointer == 1) --Pointer;
                    else Pointer = 0;
                }
                else
                {
                    Pointer = 2;
                }
            }
        }

        /// <summary>
        /// Updates the enemy's sprite frame based on its direction.
        /// </summary>
        public void UpdateFrame(object sender, EventArgs e)
        {
            //Console.WriteLine(CurDirection);
            switch (CurEnemy.EnemyDirection)
            {
                case Direction.Right:
                    CurList = RightList;
                    break;
                case Direction.Left:
                    CurList = LeftList;
                    break;
                case Direction.Up:
                    CurList = UpList;
                    break;
                case Direction.Down:
                    CurList = DownList;
                    break;
            }
            SetEnemyFrame(EnemyImage);
            UpdateWalkPointer();
        }

        /// <summary>
        /// Chnages enemy sprite frames for a melee attack animation.
        /// </summary>
        /// <param name="img">Reference to enemy image</param>
        /// <param name="page">Reference to gamescreen</param>
        public void DoMeleeAttack(Image img,Page page)
        {
            IsAttacking = true;
            Pointer = 3;
            page.Dispatcher.Invoke(new Action(()=> { SetEnemyFrame(img); }));
            Task.Run(() => Task.Delay(200));
            Pointer = 4;
            page.Dispatcher.Invoke(new Action(() => { SetEnemyFrame(img); }));
            Task.Run(() => Task.Delay(100));
            IsAttacking = false;
        }
    }

}