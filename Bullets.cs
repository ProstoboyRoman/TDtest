using System.Windows.Controls;          
using System.Windows.Media;             
using System.Windows.Shapes;            

public class Bullets
{
    private int BulletSpeed = 10;        // Geschwindigkeit der Kugel pro Tick
    private int BulletDamage = 10;       // Schaden, den die Kugel verursacht
    private int X = 10;                  // Aktuelle X-Position der Kugel
    private int Y = 10;                  // Aktuelle Y-Position der Kugel

    public Ellipse BulletBody { get; private set; } // Öffentliche Ellipse der Kugel, aber nur lesbar von außen
        
    // Konstruktor – erzeugt die Kugel und setzt ihre Startposition
    public Bullets(double x, double y)
    {
        BulletBody = new Ellipse            // Neue Ellipse erzeugen
        {
            Width = 10,                     // Breite der Kugel in Pixeln
            Height = 10,                    // Höhe der Kugel in Pixeln
            Stroke = Brushes.Gray,          // Randfarbe der Kugel
            StrokeThickness = 1,            // Dicke des Rands
            Fill = new SolidColorBrush(Colors.Orange) // Füllfarbe der Kugel
        };

        Canvas.SetLeft(BulletBody, x - BulletBody.Width / 2); // Kugel horizontal zentriert setzen
        Canvas.SetTop(BulletBody, y - BulletBody.Height / 2); // Kugel vertikal zentriert setzen

        X = (int)(x - BulletBody.Width / 2); // Start-X speichern, damit MoveBullet richtig funktioniert
        Y = (int)(y - BulletBody.Height / 2); // Start-Y speichern (wird aktuell noch nicht bewegt)
    }

    public void MoveBullet()
    {
        X += BulletSpeed;                    // X-Position um die Geschwindigkeit erhöhen
        Canvas.SetLeft(BulletBody, X);       // Neue Position im Canvas setzen
                                             // Y wird nicht verändert → Kugel bewegt sich nur horizontal
    }
}
