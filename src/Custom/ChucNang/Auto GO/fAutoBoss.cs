using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fAutoBoss
{
    private static Thread t = null;
    public static void HandleSelect()
    {
        CustomConfig.isAutoHuntBoss = !CustomConfig.isAutoHuntBoss;

        if (t == null && CustomConfig.isAutoHuntBoss)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật auto săn boss ---", mFont.tahoma_7_orange);
                while (CustomConfig.isAutoHuntBoss)
                {

                    try
                    {
                        if(GameScreen.player.Hp <= 0)
                        {
                            GlobalService.gI().Player_Revice(0x01);
                            Thread.Sleep(200);
                            GlobalService.gI().Choice_Dialog_server(ReadMessenge.IdDialog, 0x00);
                            GameCanvas.end_Cur_Dialog();
                            Thread.Sleep(1000);                         
                            continue;
                        }
                        MainObject boss = Common.GetBoss();

                        if(boss != null && GameScreen.player.Hp > 0)
                        {

                            // gom quai
                            for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
                            {
                                MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(i);
                                if (mainObject2 != null)
                                {
                                    if (mainObject2 != null && mainObject2.Action != 4 && !mainObject2.isSend && mainObject2.typeObject != 10 && mainObject2.typeObject == (sbyte)eNpcType.MONSTER)
                                    {
                                        mainObject2.x = GameScreen.player.x;
                                        mainObject2.y = GameScreen.player.y;

                                    }
                                }
                            }

                            GameScreen.objFocus = boss;

                            if (GameScreen.objFocus.name != boss.name)
                            {
                                GameScreen.player.x = boss.x;
                                GameScreen.player.y = boss.y;
                                GlobalService.gI().Obj_Move((short)GameScreen.player.x, (short)GameScreen.player.y);
                            }
                            else
                            {
                                mVector VecBeFire = new mVector();
                                Object_Effect_Skill obj = new Object_Effect_Skill(boss.ID, 0x01);
                                VecBeFire.addElement(obj);

                                mVector mVec = Common.GetNearObject(boss, 500);
                                for (int j = 0; j < mVec.size(); j++)
                                {
                                    Object_Effect_Skill obj2 = new Object_Effect_Skill(((MainObject)mVec.elementAt(j)).ID, 0x01);
                                    VecBeFire.addElement(obj2);
                                }

                                VecBeFire = Common.HackDmg(VecBeFire, 20);
                                GlobalService.gI().Obj_Move((short)(boss.x), (short)(boss.y));
                                GlobalService.gI().Player_Fire(0x00, 0x1, VecBeFire);
                                Thread.Sleep(100);
                                GlobalService.gI().Player_Fire(0x07ED, 0x1, VecBeFire);
                                Thread.Sleep(100);
                            }
                            
                            if(Common.Distance(new Point(boss.x, boss.y), new Point(GameScreen.player.x,GameScreen.player.y)) > 50)
                            {
                                GameScreen.player.x = boss.x + 25;
                                GameScreen.player.y = boss.y;
                                GlobalService.gI().Obj_Move((short)GameScreen.player.x, (short)GameScreen.player.y);
                            }
                            //GlobalService.gI().Use_Potion(0x0055);
                        }
                        Thread.Sleep(100);

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isAutoHuntBoss == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}
