using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirWar
{
    class SettingsLevel
    {

        public int scoreinterval;
        public float uroncomp;
        public float uronairplan;
        public float k;
        public int needscore_for_new_level;
        public float fuel_start;
        public float life_start; 
        public void SetParametrs(int level) { 
        switch (level)
        {case 1:
                scoreinterval = 150;
                uroncomp = 15f;
                uronairplan = 5f;
                k = 0.2f;
                life_start = 100f;
                fuel_start = 100f;
                needscore_for_new_level = 10000;
                break;
        case 2:
               scoreinterval = 140;
                uroncomp = 13f;
                uronairplan = 6f;
                k = 0.3f;
                life_start = 90f;
                fuel_start = 90f;
                needscore_for_new_level = 15000;
                break;
        case 3:
                 scoreinterval = 130;
                uroncomp = 11f;
                uronairplan = 7f;
                k = 0.4f;
                life_start = 80f;
                fuel_start = 80f;
                needscore_for_new_level = 20000;
                break;
        case 4:
                scoreinterval = 120;
                uroncomp = 9f;
                uronairplan = 8f;
                k = 0.5f;
                life_start = 70f;
                fuel_start = 70f;
                needscore_for_new_level = 25000;
                break;
        case 5:
               scoreinterval = 110;
                uroncomp = 7f;
                uronairplan = 6f;
                k = 0.6f;
                life_start = 60f;
                fuel_start = 60f;
                needscore_for_new_level = 30000;
                break;
        
        }
        
        }

    }
}
