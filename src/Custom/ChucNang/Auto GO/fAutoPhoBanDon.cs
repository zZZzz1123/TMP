using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fAutoPhoBanDon
{
    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isAutoPhoBanDon = !CustomConfig.isAutoPhoBanDon;
        if (t == null && CustomConfig.isAutoPhoBanDon)
        {
            t = new Thread(() => {
                while (CustomConfig.isAutoPhoBanDon)
                {
                    KillAll();
                }
            }) { IsBackground = true };
            t.Start();
        }

        if (CustomConfig.isAutoPhoBanDon == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }


    public static void KillAll()
    {
        try
        {
            object tmpObj = Player.vecListSkill.elementAt(0);
            if (tmpObj != null)
            {


                Skill_Info si = (Skill_Info)tmpObj;
                MainObject monster = Common.GetRandomMonster();
                mVector VecBeFire = new mVector();

                if (monster == null)
                {
                    return;
                }

                while (Common.GetMonserByID(monster.ID) != null)
                {

                    // them monster dau tien
                    Object_Effect_Skill obj = new Object_Effect_Skill(monster.ID, 0x01);
                    VecBeFire.addElement(obj);


                    if (GameScreen.player.Mp > si.manaLost)
                    {
                        GlobalService.gI().Obj_Move((short)monster.x, (short)monster.y);
                        GlobalService.gI().Player_Fire((short)CustomConfig.idSKillAutoFarm, 0x1, VecBeFire);
                    }

                    Thread.Sleep(si.timeDelay);

                }

            }
        }
        catch { }
    }

}
