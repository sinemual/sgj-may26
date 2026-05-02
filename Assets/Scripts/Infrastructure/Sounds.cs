using System.Collections.Generic;
using Client.DevTools.MyTools;
using UnityEngine;

namespace Client.Data
{
    public static class Sounds
    {
        public static readonly string MusicGameplaySound = "ost";
        public static readonly string UiClickSound = "ui_click";
        public static readonly string FlushSound = "flush";
        public static readonly string SleepSound = "sleep";
        public static readonly string FeedSound = "feed";
        public static readonly string AddIngredientSound = "ingredient";
        public static readonly string ChangeJarSound = "change_jar";

        
        /*private static readonly List<string> CarHitSounds = new List<string>()
        {
            "car_hit_1",
            "car_hit_2"
        };
        
        public static string GetRandomCarHit() => CarHitSounds[Random.Range(0, CarHitSounds.Count)];*/
    }
}