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
    public class Tower
    {
        private int Price = 20;
        private int Damage = 1;
        private int AttackSpeed = 1;
        private int Range = 100;
        private int Lvl = 1;

        private double X, Y;

        public List<Bullets> BulletsList = new List<Bullets>();


        
        public Ellipse CircleBody { get; private set; }
        public Ellipse CircleRange { get; private set; }

        public Tower(double x, double y)
        {
            this.X = x;
            this.Y = y;

            CircleBody = new Ellipse
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Blue,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.Black)

            };

            CircleRange = new Ellipse
            {
                Width = 50 + Range,
                Height = 50 + Range,
                Stroke = Brushes.Gray,
                StrokeThickness = 1
            };


            Canvas.SetLeft(CircleBody, x - CircleBody.Width / 2);
            Canvas.SetTop(CircleBody, y - CircleBody.Height / 2);

            Canvas.SetLeft(CircleRange, x - CircleRange.Width / 2);
            Canvas.SetTop(CircleRange, y - CircleRange.Height / 2);
        }


        public Bullets Attack()
        {
            Bullets newBull = new Bullets(X,Y);
            BulletsList.Add(newBull);

            return newBull;
        }
    }
}
