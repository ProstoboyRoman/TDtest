using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace TD
{
    public class Enemy
    {
        public Image sprite; // Bild des Gegners
        private double SX, SY; // aktuelle Position
        private double speed = 5; // Geschwindigkeit
        private List<Point> path; // Pfadpunkte
        private int currentTarget = 0; // aktueller Zielpunkt

        public Enemy(double startX, double startY)
        {
            SX = startX;
            SY = startY;

            sprite = new Image
            {
                Width = 40,
                Height = 40,
                Source = new BitmapImage(new Uri("pack://application:,,,/Icons/kindpng_7297780.png"))
            };

            Canvas.SetLeft(sprite, SX);
            Canvas.SetTop(sprite, SY);

            // Pfad definieren
            path = new List<Point>
            {
                new Point(50, 300),
                new Point(230, 300),
                new Point(230, 200),
                new Point(350, 200),
                new Point(350, 100),
                new Point(140, 100),
                new Point(140, 50),
                new Point(500,50)
            };
        }

        public void MoveEnemy(Canvas gameCanvas)
        {
            if (currentTarget >= path.Count)
                return; // Ziel erreicht

            Point target = path[currentTarget];

            double dx = target.X - SX;
            double dy = target.Y - SY;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < speed)
            {
                // Punkt erreicht
                SX = target.X;
                SY = target.Y;
                currentTarget++;
            }
            else
            {
                // Schrittweise Bewegung
                SX += dx / distance * speed;
                SY += dy / distance * speed;
            }

            // Position aktualisieren
            Canvas.SetLeft(sprite, SX);
            Canvas.SetTop(sprite, SY);

            // Wenn Sprite noch nicht im Canvas ist, hinzufügen
            if (!gameCanvas.Children.Contains(sprite))
                gameCanvas.Children.Add(sprite);
        }
    }
}
