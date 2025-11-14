using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TD
{
    public class Enemy
    {
        public Ellipse elipse;
        public int currentPoint = 0; // Index des nächsten Wegpunkts
        private double SX, SY;

        public Enemy(double startX, double startY)
        {
            SX = startX;
            SY = startY;

            elipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(elipse, SX);
            Canvas.SetTop(elipse, SY);
        }

        public void MoveEnemy()
        {
            SX += 10;
        }
    }
}