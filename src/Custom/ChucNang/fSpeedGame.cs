using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
internal class fSpeedGame
{

    public static void Handle()
    {
        CustomConfig.isX2Speed = CustomConfig.isX2Speed == true ? false : true;
        if (CustomConfig.isX2Speed)
        {
            Time.timeScale = 1.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
        Interface_Game.addInfoPlayerNormal($"--- Tốc độ game x{Time.timeScale} ---", mFont.tahoma_7_orange);
    }

}
