using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TD
{
    internal class GoldMine
    {
        private int Price = 10;
        private int Production = 5;
        private int ProductionDelay = 5;


        public Ellipse RangeCircle { get; private set; }
        public GoldMine(double x, double y) 
        {
            RangeCircle = new Ellipse
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Orange,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.Yellow),

            };


            Canvas.SetLeft(RangeCircle, x - RangeCircle.Width / 2);
            Canvas.SetTop(RangeCircle, y - RangeCircle.Height / 2);
        }
    }
}
