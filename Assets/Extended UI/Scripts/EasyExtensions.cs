using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public static class EasyExtensions
    {
        public static string ToClockFormat(float time)
        {

            int seconds = (int)time % 60;
            int minutes = (int)time / 60;

            string tMinutes = minutes.ToString().PadLeft(2, '0');
            string tSeconds = seconds.ToString().PadLeft(2, '0');

            return tMinutes + ":" + tSeconds;

        }
    }
}
