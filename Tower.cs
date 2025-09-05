using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TD
{
    internal class Tower
    {
        private int Price = 20;
        private int Damage = 1;
        private int AttackSpeed = 1;
        private int Range = 10;
        private int Lvl = 1;


        
        public Ellipse RangeCircle { get; private set; }

        public Tower(string imagePath, double x, double y)
        {
            RangeCircle = new Ellipse
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Blue,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.Black),

            };


            Canvas.SetLeft(RangeCircle, x - RangeCircle.Width / 2);
            Canvas.SetTop(RangeCircle, y - RangeCircle.Height / 2);
        }
    }
}
