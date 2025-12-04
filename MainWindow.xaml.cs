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
    /// Hauptfenster für das Tower-Defense-Spiel
    /// </summary>
    public partial class MainWindow : Window
    {
        // Timer für das Spiel
        private readonly DispatcherTimer _timer;

        // Spielgeschwindigkeit
        private static int _gameSpeed = 20;

        // Gold
        private int _gesamtGold = 0;
        private int _goldCounter = 100;

        // Spawn-Counter für Gegner
        private int _spawnCounter = 0;

        private int BulletZeit = 0;

        // Listen für Gegner, Türme un  d Goldminen
        public List<Enemy> EnemiesList= new List<Enemy>();
        public List<Tower> TowerList = new List<Tower>();
        public List<GoldMine> GoldMines = new List<GoldMine>();

        // Sichtbare Positionen für Turm-/Goldplatzierung
        private List<Ellipse> _visiblePositions = new List<Ellipse>();

        private List<Bullets> _bulletsList = new List<Bullets>();

        public MainWindow()
        {
            InitializeComponent();

            HidePositions();
                
            // Timer initialisieren
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10) // Tick alle 10ms
            };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        /// <summary>
        /// Spiel-Loop: Gegner bewegen, Gold produzieren, Gegner spawnen
        /// </summary>
        private void OnTick(object sender, EventArgs e)
        {
            _spawnCounter++;

            // Gegner spawnen alle 50 Ticks
            if (_spawnCounter >= 100)
            {
                SpawnEnemy();
                _spawnCounter = 0;
            }

            ProduceGold();

            // Gegner bewegen
            foreach (var enemy in EnemiesList)
            {
                enemy.MoveEnemy(GameScreen);
            }

            // Tower schießen, wenn Gegner in Reichweite
            foreach (var tower in TowerList)
            {    

                foreach (var enemy in EnemiesList)
                {
                    // Abstand Mittelpunkt Tower zu Gegner
                    double towerX = Canvas.GetLeft(tower.TowerSprite) + tower.TowerSprite.Width / 2;
                    double towerY = Canvas.GetTop(tower.TowerSprite) + tower.TowerSprite.Height / 2;

                    double enemyX = Canvas.GetLeft(enemy.sprite) + enemy.sprite.Width / 2;
                    double enemyY = Canvas.GetTop(enemy.sprite) + enemy.sprite.Height / 2;

                    double distance = Math.Sqrt(Math.Pow(enemyX - towerX, 2) + Math.Pow(enemyY - towerY, 2));

                    if (distance <= 100) // Tower.Range (hardcoded hier)
                    {
                        if(BulletZeit >= 50)
                        {
                          // Gegner in Range -> Bullet erzeugen
                          Bullets newBullet = new Bullets(towerX, towerY);
                         _bulletsList.Add(newBullet);
                         GameScreen.Children.Add(newBullet.BulletBody);
                            BulletZeit = 0;
                          break; // nur eine Bullet pro Tick pro Tower
                        }
                        else
                        {
                            BulletZeit++;
                        }
                             
                        
                        
                    }
                }
            }

            // Alle Bullets bewegen
            foreach (var bullet in _bulletsList)
            {
                bullet.MoveBullet();
            }
        }
            // TODO: Tower schießen lassen (Bullets erzeugen und bewegen)
            // TODO: Kollisionsabfrage Bullets -> Gegner
        

        /// <summary>
        /// Goldproduktion für Goldminen
        /// </summary>
        private void ProduceGold()
        {
            if (_goldCounter <= 0)
            {
                foreach (var mine in GoldMines)
                {
                    mine.AktuellesGold += mine.Production; // GoldMine eigenes Gold erhöhen
                    _gesamtGold += mine.Production;        // Gesamtgold erhöhen
                }

                // TextBlock aktualisieren
                Gold.Text = $"Gold = {_gesamtGold}";

                _goldCounter = 100;
            }
            else
            {
                _goldCounter--;
            }
        }

        /// <summary>
        /// Gegner spawnen
        /// </summary>
        private void SpawnEnemy()
        {
            if (EnemiesList.Count < 8)
            {
                Enemy newEnemy = new Enemy(0, 307);
                EnemiesList.Add(newEnemy);

                if (!GameScreen.Children.Contains(newEnemy.sprite))
                    GameScreen.Children.Add(newEnemy.sprite);
            }
        }

        #region Positionsverwaltung (Turm-/Goldminenplatzierung)

        private void HidePositions()
        {
            Position1.Visibility = Visibility.Hidden;
            Position2.Visibility = Visibility.Hidden;
            Position3.Visibility = Visibility.Hidden;
            Position4.Visibility = Visibility.Hidden;
            Position5.Visibility = Visibility.Hidden;
            Position6.Visibility = Visibility.Hidden;
        }

        private void ShowPositions()
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
                    ellipse.Stroke = Brushes.Red;

                if (GoldMineBTN.Content.ToString() == "Cancel")
                    ellipse.Stroke = Brushes.DarkOrange;
            }
        }

        private void Position_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse ellipse)
                ellipse.Stroke = Brushes.Black;
        }

        private void Position_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Ellipse ellipse)) return;

            HidePositions();

            // Mittelpunkt der Ellipse berechnen
            double x = Canvas.GetLeft(ellipse) + ellipse.Width / 2;
            double y = Canvas.GetTop(ellipse) + ellipse.Height / 2;

            if (TowerBTN.Content.ToString() == "Cancel")
            {
                // Neuen Turm erstellen
                Tower newTower = new Tower(x, y);
                GameScreen.Children.Add(newTower.TowerSprite);
                GameScreen.Children.Add(newTower.CircleRange);

                TowerList.Add(newTower);

                // TODO: Tower Angriff/Funktionalität hinzufügen
            }
            else if (GoldMineBTN.Content.ToString() == "Cancel")
            {
                GoldMine newGoldMine = new GoldMine(x, y);
                GameScreen.Children.Add(newGoldMine.RangeCircle);
                GoldMines.Add(newGoldMine);
            }

            GoldMineBTN.Content = "Gold Mine";
            TowerBTN.Content = "Tower";
        }

        private void TowerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TowerBTN.Content.ToString() != "Cancel")
            {
                ShowPositions();
                TowerBTN.Content = "Cancel";
            }
            else
            {
                HidePositions();
                TowerBTN.Content = "Tower";
            }
        }

        private void GoldMineBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GoldMineBTN.Content.ToString() != "Cancel")
            {
                ShowPositions();
                GoldMineBTN.Content = "Cancel";
            }
            else
            {
                HidePositions();
                GoldMineBTN.Content = "Gold Mine";
            }
        }

        #endregion

        private void StartBTN1_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Start/Wellen-Mechanik
        }
    }
}
