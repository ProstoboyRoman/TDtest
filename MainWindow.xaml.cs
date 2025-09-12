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
            GameManager.Initialize();
        }

        List<Ellipse> VisilePositions = new List<Ellipse>();
        List<Tower> towerslist = new List<Tower>();
        List<Enemy> enemieslist = new List<Enemy>();
        List<GoldMine> goldminelist = new List<GoldMine>();


        private void HieddePositions()
        {
            Position1.Visibility = Visibility.Hidden;
            Position2.Visibility = Visibility.Hidden;
        }
        private void ShowPositions()
        {
            Position1.Visibility = Visibility.Visible;
            Position2.Visibility = Visibility.Visible;
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
        

        private void StartBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message");
        }

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
                //SowCurrrentPositions();
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
            //VisilePositions.Add(ellipse);
            //SowCurrrentPositions();
            

            // Mittelpunkt der Ellipse berechnen
            double x = Canvas.GetLeft(ellipse) + ellipse.Width / 2;
            double y = Canvas.GetTop(ellipse) + ellipse.Height / 2;

            if (TowerBTN.Content == "Cancel")
            {
                // Neuen Tower erstellen
                Tower newTower = new Tower("Icons/TowerIcon.png", x, y);
                GameScreen.Children.Add(newTower.RangeCircle);
                towerslist.Add(newTower);
            }
            else if (GoldMineBTN.Content == "Cancel")
            {
                GoldMine newGoldmine = new GoldMine(x, y);
                GameScreen.Children.Add(newGoldmine.RangeCircle);
                goldminelist.Add(newGoldmine);
            }
            GoldMineBTN.Content = "Gold Mine";
            TowerBTN.Content = "Tower";
        }

        
    }
}
