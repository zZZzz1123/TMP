using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal class fAutoBuff
{
    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isAutoBuff = CustomConfig.isAutoBuff == true ? false : true;

        if (t == null && CustomConfig.isAutoBuff)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật tự động buff ---", mFont.tahoma_7_orange);
                while (CustomConfig.isAutoBuff)
                {

                    try
                    {
                        for(int i = 0; i < Player.vecListSkill.size(); i++)
                        {
                            Skill_Info si = (Skill_Info)Player.vecListSkill.elementAt(i);
                            if(si.typeBuff == 1 || si.typeBuff == 2)
                            {

                                mVector beBuff = new mVector();
                                Object_Effect_Skill obj2 = new Object_Effect_Skill(GameScreen.player.ID, 0x00);
                                beBuff.addElement(obj2);

                                GlobalService.gI().Buff(si.ID, 0x00, beBuff);
                                Thread.Sleep(1000);
                            }
                           
                        }

                    }
                    catch { }
                    Thread.Sleep(10000);

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isAutoBuff == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}