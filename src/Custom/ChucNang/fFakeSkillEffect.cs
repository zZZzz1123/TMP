using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal class fFakeSkillEffect
{

    public static iCommand cmdEffectLv5;
    public static iCommand cmdEffectLv10;
    public static iCommand cmdEffectLv15;
    public static iCommand cmdEffectLv20;
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();


        cmdEffectLv5 = new iCommand("Bật hiệu ứng Skill LV 5", 995, 0, GameCanvas.readMess);
        mVector2.addElement(cmdEffectLv5);

        cmdEffectLv10 = new iCommand("Bật hiệu ứng Skill LV 10", 995, 1, GameCanvas.readMess);
        mVector2.addElement(cmdEffectLv10);

        cmdEffectLv15 = new iCommand("Bật hiệu ứng Skill LV 15", 995, 2, GameCanvas.readMess);
        mVector2.addElement(cmdEffectLv15);

        cmdEffectLv20 = new iCommand("Bật hiệu ứng Skill LV 20", 995, 3, GameCanvas.readMess);
        mVector2.addElement(cmdEffectLv20);


        GameCanvas.menu.startAt(mVector2, 2, "Zig Menu Set Effect " + "- V" + CustomConfig.Version);
    }

    public static void HandleSelect(int subindex)
    {
        sbyte aaa = GameScreen.player.clazz;

        switch (subindex) 
        {
            case 0:
                Interface_Game.addInfoPlayerNormal($"--- Bật hiệu ứng LV 5 ---", mFont.tahoma_7_orange);
                GameCanvas.readMess.commandPointer(16, GameScreen.player.clazz - 1);
                break;
            case 1:
                Interface_Game.addInfoPlayerNormal($"--- Bật hiệu ứng LV 10 ---", mFont.tahoma_7_orange);
                GameCanvas.readMess.commandPointer(17, GameScreen.player.clazz - 1);
                break;
            case 2:
                Interface_Game.addInfoPlayerNormal($"--- Bật hiệu ứng LV 15 ---", mFont.tahoma_7_orange);
                GameCanvas.readMess.commandPointer(8, GameScreen.player.clazz - 1);
                break;
            case 3:
                Interface_Game.addInfoPlayerNormal($"--- Bật hiệu ứng LV 20 ---", mFont.tahoma_7_orange);
                GameCanvas.readMess.commandPointer(10, GameScreen.player.clazz - 1);
                break;
        }

    }
}
