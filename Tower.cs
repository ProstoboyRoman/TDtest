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
        // Felder
        private int Price = 20;                        // Kosten des Towers
        private int Damage;                            // Schaden, den der Tower verursacht
        private int AttackSpeed;                       // Geschwindigkeit der Angriffe
        private int Range = 100;                       // Reichweite des Towers in Pixeln

        private double X, Y;                           // Position des Towers im Canvas

        public Image TowerSprite { get; private set; } // Das Bild des Towers, nur lesbar für außen
        public Ellipse CircleRange { get; private set; } // Kreis, der die Reichweite anzeigt

        public Tower(double x, double y)
        {
            this.X = x;                                // X-Position speichern
            this.Y = y;                                // Y-Position speichern

            TowerSprite = new Image                    // Neues Tower-Image erstellen
            {
                Width = 50,                            // Breite des Turms
                Height = 50,                           // Höhe des Turms
                Source = new BitmapImage(              // Grafik laden
                    new Uri("pack://application:,,,/Icons/Tower.png"))
            };

            CircleRange = new Ellipse                  // Reichweitenkreis erzeugen
            {
                Width = 50 + Range,                    // Breite = Spritedurchmesser + Reichweite
                Height = 50 + Range,                   // Höhe = Spritedurchmesser + Reichweite
                Stroke = Brushes.Gray,                 // Farbe des Kreises
                StrokeThickness = 1                    // Linienstärke
            };

            // Tower zentriert platzieren
            Canvas.SetLeft(TowerSprite, x - TowerSprite.Width / 2);
            Canvas.SetTop(TowerSprite, y - TowerSprite.Height / 2);

            // Reichweitenkreis ebenfalls zentrieren
            Canvas.SetLeft(CircleRange, x - CircleRange.Width / 2);
            Canvas.SetTop(CircleRange, y - CircleRange.Height / 2);
        }
    }
}
