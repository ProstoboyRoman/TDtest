    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Channels;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    namespace TD
    {
        /// <summary>
        /// Interaktionslogik für MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
                HieddePositions();
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(50); // alle 5 Sekunden

            _timer.Tick += OnTick;
                _timer.Start();
            }

            private static DispatcherTimer _timer;
            private static int _gameSpeed = 20;

             public List<Enemy> enemieslist = new List<Enemy>();
             public List<Tower> towerlist = new List<Tower>();
             public List<GoldMine> goldMines = new List<GoldMine>();
             private int spawnCounter = 0;

        Enemy enemy = new Enemy(100,100);
            private static void SpeedManager(int pDeltaSpeed)
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, _gameSpeed);
            }
       

            public void OnTick(object sernder, EventArgs e)
            {
            spawnCounter++;
            if (spawnCounter >= 20) // Alle 20 Ticks
            {
                EnemySpawn();
                spawnCounter = 0;
            }

            foreach (var enemy in enemieslist)
             {
                enemy.MoveEnemy(GameScreen); // Canvas übergeben
            }
             // NOTE FÜR JUSTIN if(Count % 10 == 0)  timer wird 10 mal langasemer. 
        }

            List<Ellipse> VisilePositions = new List<Ellipse>();


            public void EnemySpawn()
            {
            // Maximal 3 Enemies gleichzeitig
            if (enemieslist.Count < 3)
            {
                Enemy newEnemy = new Enemy(0, 307);
                enemieslist.Add(newEnemy);
                GameScreen.Children.Add(newEnemy.sprite);
            }
        }// Startposition (x=0, y=100)
            
        
            private void HieddePositions()
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
            


            private void SowCurrrentPositions()
            {
                foreach (var Pos in VisilePositions)
                {
                    Pos.Visibility = Visibility;
                }
            }

            //
            // --------------- EVENTS --------------------
            //
        


            private void TowerBTN_Click(object sender, RoutedEventArgs e)
            {
                if(TowerBTN.Content != "Cancel")
                {
                    ShowPositions();
                    TowerBTN.Content = "Cancel";
                }
                else
                {
                    HieddePositions();
                    TowerBTN.Content = "Tower";
                }
                
            }

            private void GoldMineBTN_Click(object sender, RoutedEventArgs e)
            {
                if(GoldMineBTN.Content != "Cancel")
                {
                    ShowPositions();
                    GoldMineBTN.Content = "Cancel";
                }
                else
                {
                    HieddePositions();
                    GoldMineBTN.Content = "Gold Mine";
                }
            
            }

            //
            // ------------------ Position -----------------------
            //

            private void Position_MouseEnter(object sender, MouseEventArgs e)
            {
                Ellipse ellipse = sender as Ellipse;
                if (TowerBTN.Content == "Cancel")
                {
                
                    ellipse.Stroke = new SolidColorBrush(Colors.Red);
                }

                if (GoldMineBTN.Content == "Cancel")
                {
                    ellipse.Stroke = new SolidColorBrush(Colors.DarkOrange);
                }

            }

            private void Position_MouseLeave(object sender, MouseEventArgs e)
            {
                Ellipse ellipse = sender as Ellipse;
                ellipse.Stroke = new SolidColorBrush(Colors.Black);
            }

            private void Position_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Ellipse ellipse = sender as Ellipse;


                HieddePositions();

            

                // Mittelpunkt der Ellipse berechnen
                double x = Canvas.GetLeft(ellipse) + ellipse.Width / 2;
                double y = Canvas.GetTop(ellipse) + ellipse.Height / 2;

                if (TowerBTN.Content == "Cancel")
                {
                    // Neuen Tower erstellen
                    Tower newTower = new Tower(x, y);
                    GameScreen.Children.Add(newTower.TowerSprite); // PNG statt Ellipse
                    GameScreen.Children.Add(newTower.CircleRange);

                   //GameScreen.Children.Add(newTower.Attack().BulletBody);

                    towerlist.Add(newTower);
                }
                else if (GoldMineBTN.Content == "Cancel")
                {
                    GoldMine newGoldmine = new GoldMine(x, y);
                    GameScreen.Children.Add(newGoldmine.RangeCircle);
                    goldMines.Add(newGoldmine);
                }
                GoldMineBTN.Content = "Gold Mine";
                TowerBTN.Content = "Tower";
            }
            private void StartBTN1_Click(object sender, RoutedEventArgs e)
            {
                // Zum Test: Einen Enemy hinzufügen
                //Enemy newEnemy = new Enemy(0, 100); // Startposition (x=0, y=100)

           

            }
        }
    }
