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
        private int Lvl = 1;

        private double X, Y;
 
        public List<Bullets> BulletsList = new List<Bullets>();

        //private List<Bullets> BulletsList = new List<Bullets>();

        public int damage
        {
            set
            {
                Damage = value;
                if (Damage < 0) 
                    Damage = 0; // z.b schaden geht nicht unter 0
            }
        }
        public int attackspeed
        {
            set 
            {
                AttackSpeed = value;
                if (AttackSpeed < 1) 
                    AttackSpeed = 1; // mindestwert muus 1
            }
                    
        }





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

            
        public Bullets Attack()
        {
            Bullets newBull = new Bullets(X,Y);
            BulletsList.Add(newBull);

            return newBull;
        }
    }
}
