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
        private static int _gameSpeed = 2000;
        private static DispatcherTimer _timer;

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
           
        }

    }
}
