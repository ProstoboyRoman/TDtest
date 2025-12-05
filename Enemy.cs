using System;                                      
using System.Collections.Generic;                  
using System.Windows;                              
using System.Windows.Controls;                     
using System.Windows.Media.Imaging;                

public class Enemy
{  
    
    public Image sprite;                            // Das Bild, das den Gegner darstellt
    private double SX, SY;                          // Aktuelle X- und Y-Position des Gegners
    private double speed = 1;                       // Bewegungsgeschwindigkeit pro Frame
    private List<Point> path;                       // Liste der Wegpunkte, die der Gegner abläuft
    private int currentTarget = 0;                  // Index des momentan angepeilten Zielpunkts
    private int _lebensenergie = 100; // internes Feld
    public bool lebt = true;          // Status, ob der Gegner noch lebt

    // Property mit Getter und Setter
    public int Lebensenergie
    {
        get { return _lebensenergie; } // Gibt aktuellen Wert zurück
        set
        {
            _lebensenergie = value;     // Wert setzen
            if (_lebensenergie <= 0)    // Prüfen, ob Gegner gestorben ist
                lebt = false;           // Status ändern
        }
    }
  

    public Enemy(double startX, double startY)
    {
        SX = startX;                                // Startposition X setzen
        SY = startY;                                // Startposition Y setzen

        sprite = new Image                          // Neues Image-Objekt für den Gegner erstellen
        {
            Width = 40,                             // Breite des Gegners
            Height = 40,                            // Höhe des Gegners
            Source = new BitmapImage(               // Grafikdatei laden
                new Uri("pack://application:,,,/Icons/kindpng_7297780.png"))
        };

        Canvas.SetLeft(sprite, SX);                 // Gegner-Bild auf X-Position setzen
        Canvas.SetTop(sprite, SY);                  // Gegner-Bild auf Y-Position setzen

        // Pfadpunkte definieren, die der Gegner nacheinander abläuft
        path = new List<Point>
        {
            new Point(50, 300),
            new Point(230, 300),
            new Point(230, 200),
            new Point(350, 200),
            new Point(350, 100),
            new Point(140, 100),
            new Point(140, 50),
            new Point(500, 50)
        };
    }

    public void MoveEnemy(Canvas gameCanvas)
    {
        if (currentTarget >= path.Count)            // Wenn alle Zielpunkte erreicht sind …
            return;                                 // … keine Bewegung mehr

        Point target = path[currentTarget];         // Aktuelles Ziel aus der Liste holen

        double dx = target.X - SX;                  // Abstand X zum Ziel
        double dy = target.Y - SY;                  // Abstand Y zum Ziel
        double distance = Math.Sqrt(dx * dx + dy * dy); // Entfernung mittels Pythagoras berechnen

        if (distance < speed)
        {
            SX = target.X;                          // Wenn sehr nah am Ziel → direkt setzen
            SY = target.Y;
            currentTarget++;                        // Nächsten Zielpunkt ansteuern
        }
        else
        {
            // Normaler Schritt Richtung Ziel, normalisiert auf 1 * speed
            SX += dx / distance * speed;            // X-Position um Schritt weiter bewegen
            SY += dy / distance * speed;            // Y-Position um Schritt weiter bewegen
        }

        Canvas.SetLeft(sprite, SX);                 // Neue X-Position ans Canvas übergeben
        Canvas.SetTop(sprite, SY);                  // Neue Y-Position ans Canvas übergeben

        if (!gameCanvas.Children.Contains(sprite))  // Wenn das Sprite noch nicht im Canvas ist …
            gameCanvas.Children.Add(sprite);        // … dann hinzufügen
    }
}
