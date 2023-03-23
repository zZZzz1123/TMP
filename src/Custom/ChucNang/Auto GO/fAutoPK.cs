using System.Linq;
using System.Threading;

internal class fAutoPK
{
    private static Thread t = null;
    private static int[] flagType = { 3, 4, 5, 6, 7, 8 };
    public static void Handle()
    {
        CustomConfig.isAutoPK = !CustomConfig.isAutoPK;

        if (t == null && CustomConfig.isAutoPK)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật tự động PK ---", mFont.tahoma_7_orange);
                while (CustomConfig.isAutoPK)
                {
                    try
                    {
                        mVector VecBeFire = new mVector();

                        //for (int i =0; i < GameScreen.vecPlayers.size(); i++)
                        //{
                        //    object obj = GameScreen.vecPlayers.elementAt(i);

                        //    if(obj.GetType() == typeof(Other_Player))
                        //    {
                        //        Other_Player enemy = (Other_Player)obj; 
                        //        if(enemy.typePK != -1 && enemy.Hp > 0)
                        //        {
                        //            if (enemy.typePK == 0) // do sat
                        //            {
                        //                Object_Effect_Skill oes = new Object_Effect_Skill(enemy.ID, 0x00);
                        //                VecBeFire.addElement(oes);
                        //            }
                        //            else if(flagType.Contains(enemy.typePK) && flagType.Contains(GameScreen.player.typePK)) // bat co
                        //            {
                        //                if(enemy.typePK != GameScreen.player.typePK)
                        //                {
                        //                    Object_Effect_Skill oes = new Object_Effect_Skill(enemy.ID, 0x00);
                        //                    VecBeFire.addElement(oes);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        // danh 

                        if(GameScreen.objFocus != null)
                        {
                            if(GameScreen.objFocus.GetType() == typeof(Other_Player))
                            {
                                Other_Player enemy = (Other_Player)GameScreen.objFocus;
                                if(enemy.Hp > 0)
                                {
                                    Object_Effect_Skill oes = new Object_Effect_Skill(enemy.ID, 0x00);
                                    VecBeFire.addElement(oes);
                                }
                            }
                        }
                        if(VecBeFire.size() > 0)
                        {
                            object tmpObj = Common.GetOtherPlayerById(((Object_Effect_Skill)VecBeFire.elementAt(0)).ID);
                            if(tmpObj != null)
                            {
                                Other_Player enemy = (Other_Player)tmpObj;
                                GameScreen.objFocus = enemy;
                                GameScreen.player.x = enemy.x;
                                GameScreen.player.y = enemy.y;

                                VecBeFire = Common.HackDmg(VecBeFire, 1);

                                GlobalService.gI().Obj_Move((short)enemy.x, (short)enemy.y);
                                GlobalService.gI().Player_Fire(0, 0x0, VecBeFire);
                            }
                            Thread.Sleep(900);
                        }
                        Thread.Sleep(100);

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isAutoPK == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}