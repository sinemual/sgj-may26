using System.Collections.Generic;
using Client.DevTools.MyTools;
using UnityEngine;

namespace Client.Data
{
    public static class Sounds
    {
        public static readonly string MusicGameplaySound = "ost";
        public static readonly string NatureSound = "nature";
        public static readonly string UiClickSound = "ui_click";
        public static readonly string FlushSound = "flush";
        public static readonly string SleepSound = "sleep";
        public static readonly string PopSound = "pop";
        public static readonly string WinSound = "win";
        public static readonly string LoseSound = "lose";
        public static readonly string LureSound = "lure";   
        public static readonly string BulkSound = "bulk";   
        public static readonly string ChawSound = "chaw";   

        
        /*private static readonly List<string> CarHitSounds = new List<string>()
        {
            "car_hit_1",
            "car_hit_2"
        };
        
        public static string GetRandomCarHit() => CarHitSounds[Random.Range(0, CarHitSounds.Count)];*/
    }
}