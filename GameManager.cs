using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TD
{
    internal static class GameManager
    {
        private static int _gameSpeed = 20;
        private static DispatcherTimer _timer;

        static public List<Enemy> enemieslist = new List<Enemy>();
        static public List<Tower> towerlist = new List<Tower>();
        static public List<GoldMine> goldMines = new List<GoldMine>();

        public static void Initialize()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnTick;
            _timer.Start(); 

        }

        private static void SpeedManager(int pDeltaSpeed)
        {   
            _timer.Interval = new TimeSpan(0,0,0,0,_gameSpeed);
        }

        private static void GameOver()
        {
            
        }

        public static void OnTick(object sernder,  EventArgs e)
        {

            // NOTE FÜR JUSTIN if(Count % 10 == 0)  timer wird 10 mal langasemer. 
            AddEnemy();

        }
        public static void AddEnemy()
        {
            Enemy newEnemy = new Enemy(0, 100);
            enemieslist.Add(newEnemy);
        }
        public static List<Enemy> DrawEnemy()
        {
            return enemieslist;
        }

    }
}
