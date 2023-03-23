using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal class fStandFarmSkill
{

    public static iCommand cmdStart;
    public static iCommand cmdSkill1;
    public static iCommand cmdSkill2;
    public static iCommand cmdSkill3;
    public static iCommand cmdSkill4;

    private static Thread t = null;
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();

        if (CustomConfig.isFarmSkillTaiCho == false)
            cmdStart = new iCommand("Bật tự động farm skill tại chỗ", 994, 0, GameCanvas.readMess);
        else
            cmdStart = new iCommand("Tắt tự động farm skill tại chỗ", 994, 0, GameCanvas.readMess);
        mVector2.addElement(cmdStart);

       
        cmdSkill1 = new iCommand("Chọn skill 1", 994, 1, GameCanvas.readMess);
        mVector2.addElement(cmdSkill1);

        cmdSkill2 = new iCommand("Chọn skill 2", 994, 2, GameCanvas.readMess);
        mVector2.addElement(cmdSkill2);

        cmdSkill3 = new iCommand("Chọn skill 3", 994, 3, GameCanvas.readMess);
        mVector2.addElement(cmdSkill3);

        cmdSkill4 = new iCommand("Chọn skill 4", 994, 4, GameCanvas.readMess);
        mVector2.addElement(cmdSkill4);

        GameCanvas.menu.startAt(mVector2, 2, "Zig Menu Up Skill " + "- V" + CustomConfig.Version);
    }

    public static void HandleSelect(int subindex)
    {
        Interface_Game.addInfoPlayerNormal(subindex.ToString(), mFont.tahoma_7_white);
        switch (subindex)
        {
            case 0:
                Handle();
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                CustomConfig.idSKillAutoFarm = subindex - 1;
                break;


        }
    }
    public static void Handle()
    {
        CustomConfig.isFarmSkillTaiCho = CustomConfig.isFarmSkillTaiCho == true ? false : true;
        Point oringinPoint = new Point(GameScreen.player.x, GameScreen.player.y);
        //fGomQuai.Handle();
        if (t == null && CustomConfig.isFarmSkillTaiCho)
        {
            t = new Thread(() =>
            {
                Interface_Game.addInfoPlayerNormal("--- Bật tự động up skill ---", mFont.tahoma_7_orange);

                while (CustomConfig.isFarmSkillTaiCho)
                {

                    try
                    {
                        object tmpObj =  Player.vecListSkill.elementAt(CustomConfig.idSKillAutoFarm);
                        if(tmpObj != null)
                        {
                            Skill_Info si = (Skill_Info)tmpObj;
                            MainObject monster = Common.GetRandomMonster();
                            mVector VecBeFire = new mVector();

                            //while (true)
                            //{
                            //    Thread.Sleep(10);
                            //    for(int i = 0; i < LoadMap.vecPointChange.size(); i++)
                            //    {
                            //        monster = Common.GetRandomMonster();
                            //        Point pointChangeMap  = (Point)LoadMap.vecPointChange.elementAt(i);
                            //        int dis = Common.Distance(pointChangeMap, new Point() { x = monster.x, y = monster.y });
                            //        if(dis < 100)
                            //        {
                            //            continue;
                            //        }
                            //        else
                            //        {
                            //            break;
                            //        }
                            //    }
                            //    break;
                            //}
                            
                           

                            // them monster dau tien
                            Object_Effect_Skill obj = new Object_Effect_Skill(monster.ID, 0x01);
                            VecBeFire.addElement(obj);


                            // lay het monster gan do
                            //mVector mVec = Common.GetNearObject(monster, 5000);
                            //for (int j = 0; j < mVec.size(); j++)
                            //{
                            //    Object_Effect_Skill obj2 = new Object_Effect_Skill(((MainObject)mVec.elementAt(j)).ID, 0x01);
                            //    VecBeFire.addElement(obj2);
                            //}

                            if (GameScreen.player.Mp > si.manaLost)
                            {
                                GlobalService.gI().Obj_Move((short)monster.x, (short)monster.y);
                                GlobalService.gI().Player_Fire((short)CustomConfig.idSKillAutoFarm, 0x1, VecBeFire);
                            }

                            GlobalService.gI().Obj_Move((short)oringinPoint.x, (short)oringinPoint.y);
                            Thread.Sleep(si.timeDelay);
                            continue;

                        }

                        Thread.Sleep(10);

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isFarmSkillTaiCho == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}
