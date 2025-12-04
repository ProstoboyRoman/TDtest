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
        // felder 
        private int Price = 20;
        private int Damage;
        private int AttackSpeed;
        private int Range = 100;

        private double X, Y;
      
        public Image TowerSprite { get; private set; }
        public Ellipse CircleRange { get; private set; }

        public Tower(double x, double y)
        {
            this.X = x;
            this.Y = y;

            TowerSprite = new Image
            {
                Width = 50,
                Height = 50,
                Source = new BitmapImage(new Uri("pack://application:,,,/Icons/Tower.png"))
            };

            CircleRange = new Ellipse
            {
                Width = 50 + Range,
                Height = 50 + Range,
                Stroke = Brushes.Gray,
                StrokeThickness = 1
            };


            Canvas.SetLeft(TowerSprite, x - TowerSprite.Width / 2);
            Canvas.SetTop(TowerSprite, y - TowerSprite.Height / 2);

            Canvas.SetLeft(CircleRange, x - CircleRange.Width / 2);
            Canvas.SetTop(CircleRange, y - CircleRange.Height / 2);
        }

            
       
    }
}
