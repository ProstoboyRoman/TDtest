using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TD
{
    public class Enemy
    {

        public Ellipse elipse { get; private set; }

        public Enemy(double x, double y)
        {
            elipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(elipse, x);
            Canvas.SetTop(elipse, y);
        }

    }
}