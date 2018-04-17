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
    public class PlayerAnimationControl
    {
        public Direction CurDirection { get; set;}
        public Image PlayerImage { get; set; }
        public int Pointer { get; set; }
        public bool KeyDown { get; set; }
        public bool CanAttack = true;
        public bool IsAttacking { get; set; }
        public int[] CurList { get; set; }
        public int[] RightList = { 1,2,3,4,5 };
        public int[] LeftList = { 6,7,8,9,10 };
        public int[] UpList = { 11,12,13,14,15 };
        public int[] DownList = { 16,17,18,19,20 };

        public void SetPlayerFrame(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/player/knight_topdown_basic{0}.png",CurList[Pointer]), UriKind.Relative));
        }


        public void UpdateDirection(object sender, EventArgs e)
        {
            CurDirection = Player.Instance.PlayerDirection;
        }


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
        public Enemy CurEnemy { get; set; }
        public Direction CurDirection { get; set; }
        public EnemyControl EnemyImage { get; set; }
        public int Pointer { get; set; }
        public bool IsMoving { get; set; }
        public bool CanAttack = true;
        public bool IsAttacking { get; set; }

        public int[] CurList { get; set; }
        public int[] RightList = { 1, 2, 3, 4, 5 };
        public int[] LeftList = { 6, 7, 8, 9, 10 };
        public int[] UpList = { 11, 12, 13, 14, 15 };
        public int[] DownList = { 16, 17, 18, 19, 20 };

        public void Set(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/{0}/{1}_topdown_basic{2}.png", CurEnemy.GetType(), CurEnemy.GetType(), CurList[Pointer]), UriKind.Relative));
        }





    }

}