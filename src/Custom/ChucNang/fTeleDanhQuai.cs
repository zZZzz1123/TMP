using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fTeleDanhQuai
{
    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isTeleDanhQuai = CustomConfig.isTeleDanhQuai == true ? false : true;

        if (t == null && CustomConfig.isTeleDanhQuai)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật tự động đánh kèm teleport ---", mFont.tahoma_7_orange);
                while (CustomConfig.isTeleDanhQuai)
                {

                    try
                    {
                        MainObject mainObject = GameScreen.objFocus;
                        int auto = Player.AutoFireCur;
                        if (Player.AutoFireCur != 0 && mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 && mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                        {
                            GameScreen.player.x = mainObject.x + 10;
                            GameScreen.player.y = mainObject.y + 10;
                            GlobalService.gI().Obj_Move((short)mainObject.x, (short)mainObject.y);
                        }
                        else
                        {
                            GlobalService.gI().Obj_Move((short)GameScreen.player.x, (short)GameScreen.player.y);
                        }
                        Thread.Sleep(1000);

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isTeleDanhQuai == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }


    }
}