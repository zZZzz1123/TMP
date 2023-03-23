using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

internal class fAutoPhoBanLienTang
{

    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isAutoPhoBanLienTang = CustomConfig.isAutoPhoBanLienTang == true ? false : true;
        if(CustomConfig.isAutoPhoBanLienTang == true)
        {
            Interface_Game.addInfoPlayerNormal($"--- Tự động đánh liên tầng ---", mFont.tahoma_7_orange);
            string notice = "Hãy bật tính năng khi tham gia phụ bản để có hiệu quả tốt nhất !";
            mVector vecCmd = new mVector();
            GameCanvas.Start_Normal_DiaLog(notice, vecCmd, true);
        }

        if (t == null && CustomConfig.isAutoPhoBanLienTang)
        {
            t = new Thread(() =>
            {


                while (CustomConfig.isAutoPhoBanLienTang)
                {

                    try
                    {
                       CountDownTicket cdt =  Interface_Game.watchMap;
                        if(cdt.timeCountDown > 0 && cdt.strInfo.ToLower().StartsWith("tầng"))
                        {
                            MainObject monster = Common.GetFisrtMonster();
                            if(monster != null)
                            {
                                GameScreen.objFocus = monster;
                                GameScreen.player.setFocus(false);
                                Interface_Game.isPaintInfoFocus = true;

                                GameScreen.player.x = monster.x;
                                GameScreen.player.y = monster.y;
                                GlobalService.gI().Obj_Move((short)monster.x, (short)monster.y);
                                Skill_Info si = (Skill_Info)Player.vecListSkill.elementAt(0);
                                GameScreen.player.beginPlayerFire(si);


                                if (Player.AutoFireCur != Player.typeAutoFireMain)
                                    Player.setStart_EndAutoFire(true);
                            }
                           
                        }
                        else
                        {                           
                            Player.setStart_EndAutoFire(false);
                        }
                    }
                    catch { }

                    Thread.Sleep(1000);  
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isAutoPhoBanLienTang == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    } 
}
