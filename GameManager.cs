using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TD
{
    internal static class GameManager
    {


    }
}

            if (!_gameScreen.Children.Contains(newEnemy.elipse))
            {
                _gameScreen.Children.Add(newEnemy.elipse);
            }
        }
        public static List<Enemy> DrawEnemy()//gibt die enemy list zurück
        {
            return enemieslist;
        }

