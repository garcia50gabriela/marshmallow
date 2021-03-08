﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class game_data
{
    public class Achievement
    {
        public string Name { get; set; }
        //public float N { get; set; }
        //public float S { get; set; }
        public bool Earned { get; set; }
    }
    private static bool MarshmallowIsPresent = false;
    private static int raccoon_counter = 0;
    private static Dictionary<int, Achievement> achievement_dict = new Dictionary<int, Achievement>() {
        {0,  new Achievement{Name="Dark Side", Earned=false } },
        {1,  new Achievement{Name="Good & Golden", Earned=false } },
        {2,  new Achievement{Name="UnRoasted", Earned=false } },
        {3,  new Achievement{Name="Completely Charred", Earned=false } },
        {5,  new Achievement{Name="Rapid Roast", Earned=false } },
        {6,  new Achievement{Name="Flambé <3", Earned=false } },
        {7,  new Achievement{Name="Smorldering Hot", Earned=false } },
        {8,  new Achievement{Name="Burn Baby Burn", Earned=false } },
        {9,  new Achievement{Name="Slow Roast", Earned=false } },
        {10,  new Achievement{Name="Free Falling", Earned=false } },
    };


    public static Dictionary<int, Achievement> AchievementDict
    {
        get
        {
            return achievement_dict;
        }
    }
    public static bool marshmallowIsPresent
    {
        get
        {
            return MarshmallowIsPresent;
        }
        set
        {
            MarshmallowIsPresent = value;
        }
    }
    public static int raccoonCounter
    {
        get
        {
            return raccoon_counter;
        }
        set
        {
            raccoon_counter = value;
        }
    }
}
