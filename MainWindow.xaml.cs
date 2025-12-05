using System;                                             
using System.Collections.Generic;                         
using System.Runtime.InteropServices;                     
using System.Windows;                                     
using System.Windows.Controls;                            
using System.Windows.Input;                               
using System.Windows.Media;                              
using System.Windows.Shapes;                              
using System.Windows.Threading;                           

namespace TD                                              
{
    /// <summary>
    /// Hauptfenster des Tower-Defense-Spiels
    /// </summary>
    public partial class MainWindow : Window              
    {
        private readonly DispatcherTimer _timer;          // Haupttimer für das Spiel (Game Loop)

        private static int _gameSpeed = 20;               // Spielgeschwindigkeit (nicht genutzt)

        private int _gesamtGold = 0;                      // Spieler-Gold gesamt
        private int _goldCounter = 100;                   // Countdown für Goldproduktion

        private int _spawnCounter = 0;                    // Zeit bis zum nächsten Gegner-Spawn

        private int BulletZeit = 0;                       // Zeit bis zum nächsten Schuss

        // Listen aller Spielobjekte
        public List<Enemy> EnemiesList = new List<Enemy>();   // Liste der Gegner
        public List<Tower> TowerList = new List<Tower>();     // Liste der Türme
        public List<GoldMine> GoldMines = new List<GoldMine>(); // Liste der Goldminen

        private List<Ellipse> _visiblePositions = new List<Ellipse>(); // Platzierungsfelder

        private List<Bullets> _bulletsList = new List<Bullets>();      // Aktive Bullets

        public MainWindow()
        {
            InitializeComponent();                         // WPF-Elemente initialisieren

            HidePositions();                               // Platzierungs-Kreise verstecken

            _timer = new DispatcherTimer                   // Neuen Game-Timer anlegen
            {
                Interval = TimeSpan.FromMilliseconds(10)   // Timer tickt alle 10ms
            };
            _timer.Tick += OnTick;                         // Tick-Event verbinden
            _timer.Start();                                // Timer starten
        }

        /// <summary>
        /// Game Loop — läuft bei jedem Timer-Tick
        /// </summary>
        private void OnTick(object sender, EventArgs e)
        {
            _spawnCounter++;                                // Spawn-Counter erhöhen

            if (_spawnCounter >= 100)                       // Alle 100 Ticks Gegner spawnen
            {
                SpawnEnemy();                               // Gegner erzeugen
                _spawnCounter = 0;                          // Zähler zurücksetzen
            }

            ProduceGold();                                  // Goldminen arbeiten lassen

            foreach (var enemy in EnemiesList)              // Alle Gegner bewegen
            {
                enemy.MoveEnemy(GameScreen);
            }

            foreach (var tower in TowerList)                // Jeder Tower greift an
            {
                foreach (var enemy in EnemiesList)          // Gegner checken
                {
                    double towerX = Canvas.GetLeft(tower.TowerSprite) + tower.TowerSprite.Width / 2;
                    double towerY = Canvas.GetTop(tower.TowerSprite) + tower.TowerSprite.Height / 2;

                    double enemyX = Canvas.GetLeft(enemy.sprite) + enemy.sprite.Width / 2;
                    double enemyY = Canvas.GetTop(enemy.sprite) + enemy.sprite.Height / 2;

                    double distance = Math.Sqrt(Math.Pow(enemyX - towerX, 2) + Math.Pow(enemyY - towerY, 2));

                    if (distance <= 100)                    // Tower in Reichweite (hardcoded!)
                    {
                        if (BulletZeit >= 50)               // Schussverzögerung erreicht?
                        {
                            Bullets newBullet = new Bullets(towerX, towerY);  // Bullet erstellen
                            _bulletsList.Add(newBullet);                     // Bullet in Liste
                            GameScreen.Children.Add(newBullet.BulletBody);    // Grafik hinzufügen
                            BulletZeit = 0;                                   // Timer reset
                            break;                                            // Tower schießt nur 1x pro Tick
                        }
                        else
                        {
                            BulletZeit++;                                    // Nachladezeit
                        }
                    }
                }
            }

            foreach (var bullet in _bulletsList)            // Jede Kugel bewegen
            {
                bullet.MoveBullet();
            }
        }

        /// <summary>
        /// Goldproduktion der Minen
        /// </summary>
        private void ProduceGold()
        {
            if (_goldCounter <= 0)                          // Alle 100 Ticks Gold erzeugen
            {
                foreach (var mine in GoldMines)             // Für jede Mine…
                {
                    mine.AktuellesGold += mine.Production;  // Mine erzeugt eigenes Gold
                    _gesamtGold += mine.Production;         // Gesamtgold erhöhen
                }

                Gold.Text = $"Gold = {_gesamtGold}";        // UI aktualisieren

                _goldCounter = 100;                         // Reset des Timers
            }
            else
            {
                _goldCounter--;                             // Tick runterzählen
            }
        }

        /// <summary>
        /// Gegner spawnen
        /// </summary>
        private void SpawnEnemy()
        {
            if (EnemiesList.Count < 8)                      // Max. 8 Gegner gleichzeitig
            {
                Enemy newEnemy = new Enemy(0, 307);         // Gegner am Startpunkt erzeugen
                EnemiesList.Add(newEnemy);                  // Gegner zur Liste hinzufügen

                if (!GameScreen.Children.Contains(newEnemy.sprite))
                    GameScreen.Children.Add(newEnemy.sprite); // Gegner auf Canvas anzeigen
            }
        }

        #region Positionsverwaltung (Turm-/Goldminenplatzierung)

        private void HidePositions()                        // Alle Platzierungsstellen verstecken
        {
            Position1.Visibility = Visibility.Hidden;
            Position2.Visibility = Visibility.Hidden;
            Position3.Visibility = Visibility.Hidden;
            Position4.Visibility = Visibility.Hidden;
            Position5.Visibility = Visibility.Hidden;
            Position6.Visibility = Visibility.Hidden;
        }

        private void ShowPositions()                        // Alle Platzierungsstellen anzeigen
        {
            Position1.Visibility = Visibility.Visible;
            Position2.Visibility = Visibility.Visible;
            Position3.Visibility = Visibility.Visible;
            Position4.Visibility = Visibility.Visible;
            Position5.Visibility = Visibility.Visible;
            Position6.Visibility = Visibility.Visible;
        }

        private void Position_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse ellipse)
            {
                if (TowerBTN.Content.ToString() == "Cancel")
                    ellipse.Stroke = Brushes.Red;           // Turm-Platzierung sichtbar

                if (GoldMineBTN.Content.ToString() == "Cancel")
                    ellipse.Stroke = Brushes.DarkOrange;    // Goldmine-Platzierung sichtbar
            }
        }

        private void Position_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse ellipse)
                ellipse.Stroke = Brushes.Black;             // Rand normalisieren
        }

        private void Position_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Ellipse ellipse)) return;       // Safety Check

            HidePositions();                                // Plätze wieder ausblenden

            double x = Canvas.GetLeft(ellipse) + ellipse.Width / 2;  // Mittelpunkt X
            double y = Canvas.GetTop(ellipse) + ellipse.Height / 2;  // Mittelpunkt Y

            if (TowerBTN.Content.ToString() == "Cancel")    // Turm setzen
            {
                Tower newTower = new Tower(x, y);           // Tower erzeugen
                GameScreen.Children.Add(newTower.TowerSprite); // Tower anzeigen
                GameScreen.Children.Add(newTower.CircleRange);// Range anzeigen

                TowerList.Add(newTower);                    // Liste erweitern
            }
            else if (GoldMineBTN.Content.ToString() == "Cancel")
            {
                GoldMine newGoldMine = new GoldMine(x, y);  // Mine erzeugen
                GameScreen.Children.Add(newGoldMine.RangeCircle); // Range anzeigen
                GoldMines.Add(newGoldMine);                 // Liste erweitern
            }

            GoldMineBTN.Content = "Gold Mine";             // Buttons zurücksetzen
            TowerBTN.Content = "Tower";
        }

        private void TowerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TowerBTN.Content.ToString() != "Cancel")    // Tower-Platzierung starten
            {
                ShowPositions();
                TowerBTN.Content = "Cancel";
            }
            else                                            // Abbrechen
            {
                HidePositions();
                TowerBTN.Content = "Tower";
            }
        }

        private void GoldMineBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GoldMineBTN.Content.ToString() != "Cancel") // Goldmine-Platzierung starten
            {
                ShowPositions();
                GoldMineBTN.Content = "Cancel";
            }
            else
            {
                HidePositions();                           // Abbrechen
                GoldMineBTN.Content = "Gold Mine";
            }
        }

        #endregion

        private void StartBTN1_Click(object sender, RoutedEventArgs e)
        {
            // Wellenstart (noch nicht implementiert)
        }
    }
}
    