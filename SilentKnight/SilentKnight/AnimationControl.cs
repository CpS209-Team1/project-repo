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
    public class AnimationControl
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public Direction CurDirection { get; set;}
        public Image PlayerImage { get; set; }
        public int Pointer { get; set; }
        public bool KeyDown { get; set; }
        public bool CanAttack = true;
        public bool Channeling { get; set; }
        public int DebounceCounter { get; set; }
        public int[] curList { get; set; }
        public int[] RightList = { 1,2,3,4,5 };
        public int[] LeftList = { 6,7,8,9,10 };
        public int[] UpList = { 11,12,13,14,15 };
        public int[] DownList = { 16,17,18,19,20 };

        public void SetFrame(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/Sprites/knight_topdown_basic{0}.png",curList[Pointer]), UriKind.Relative));
        }


        public void UpdateDirection(object sender, EventArgs e)
        {
            CurDirection = Player.Instance.PlayerDirection;
        }


        public void DoSwordAttack(Image img)
        {
            Channeling = true;
            Pointer = 3;
            SetFrame(img);
            Task.Run(() => Task.Delay(100));
            Pointer = 4;
            SetFrame(img);
            Task.Run(() => Task.Delay(100));
            Channeling = false;
        }

        public void UpdateWalkPointer()
        {
            if (!Channeling)
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
                    curList = RightList;
                    break;
                case Direction.Left:
                    curList = LeftList;
                    break;
                case Direction.Up:
                    curList = UpList;
                    break;
                case Direction.Down:
                    curList = DownList;
                    break;
            }
            SetFrame(PlayerImage);
            UpdateWalkPointer();
        }







    }
}