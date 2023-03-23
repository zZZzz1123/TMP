using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fGomQuai
{
    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isAutoGomQuai = !CustomConfig.isAutoGomQuai;

        if (t == null && CustomConfig.isAutoGomQuai)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật auto gom quái ---", mFont.tahoma_7_orange);
                while (CustomConfig.isAutoGomQuai)
                {

                    try
                    {
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


                        Thread.Sleep(100);
                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isAutoGomQuai == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}