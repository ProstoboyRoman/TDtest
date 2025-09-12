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
        public Ellipse Sprite { get; private set; }

        public Enemy(double x, double y)
        {
            Sprite = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(Sprite, x);
            Canvas.SetTop(Sprite, y);
        }
    }
}