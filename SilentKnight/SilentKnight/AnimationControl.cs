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
        public int[] curList { get; set; }
        public string ResourceString = "SilentKnight.Properties.Resources[knight_topdown_basic{0}]";
        public int[] RightList = { 1,2,3,4,5 };
        public int[] LeftList = { 6,7,8,9,10 };
        public int[] UpList = { 11,12,13,14,15 };
        public int[] DownList = { 16,17,18,19,20 };

        public void WalkAnimation(Image img)
        {
            img.Source = new BitmapImage(new Uri(String.Format("/Assets/Sprites/knight_topdown_basic{0}.png",curList[Pointer]), UriKind.Relative));
        }

        public void UpdateDirection(object sender, EventArgs e)
        {
            CurDirection = Player.Instance.PlayerDirection;
        }

        public void UpdateWalkPointer()
        {
            if (Pointer == 0) ++Pointer;
            else if (Pointer == 1) --Pointer;
        }

        public void UpdateFrame(object sender, EventArgs e)
        {
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
            WalkAnimation(PlayerImage);
            UpdateWalkPointer();
        }







    }
}