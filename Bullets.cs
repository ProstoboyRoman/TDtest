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
    internal class Bullets
    {
        private int BulletSpeed = 10;
        private int BulletDamage = 10;
        private int X = 10;
        private int Y = 10;

        public Ellipse BulletBody { get; private set; }
        public Bullets(double x, double y)
        {
            BulletBody = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                Fill = new SolidColorBrush(Colors.Orange)
            };

            Canvas.SetLeft(BulletBody, x - BulletBody.Width / 2);
            Canvas.SetTop(BulletBody, y - BulletBody.Height / 2);
        }
        

        public void MoveBullet()
        {

        }
        
    }
}
