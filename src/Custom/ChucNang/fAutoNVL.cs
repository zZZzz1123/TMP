using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

internal class fAutoNVL
{
    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isAutoNVL = CustomConfig.isAutoNVL == true ? false : true;
        
        if(t == null  && CustomConfig.isAutoNVL)
        {
            t = new Thread(() => 
            {

                Interface_Game.addInfoPlayerNormal("--- Bật tự động làm nhiệm vụ lặp ---", mFont.tahoma_7_orange);
                while (CustomConfig.isAutoNVL)
                {

                    try
                    {
                        GameCanvas.end_Cur_Dialog();
                        MainQuest nvlDaNhan = GetNvlDaNhan();
                        MainQuest nvlDaHoanThanh = GetNvlDaHoanThanh(); 
                        MainQuest nvlChuaNhan = null;
                        // chua co nhiem vu => nhan nhiem vu
                        if (nvlDaNhan == null && nvlDaHoanThanh == null)
                        {

                            nvlChuaNhan = GetNvlChuaNhan();
                            if(nvlChuaNhan != null)
                            {
                                if (Player.ticket < 3) 
                                {
                                    int timeWait = (3 - Player.ticket) * 15 * 60000; 
                                    Thread.Sleep(timeWait);
                                }
                                NPC npcNvl = getNpcNVL();
                                if(npcNvl != null)
                                {
                                    // gui tin fake toi server rang minh dang ke ben ban nvl
                                    GlobalService.gI().Obj_Move((short)npcNvl.x, (short)npcNvl.y);
                                    Thread.Sleep(100);

                                    // gui yeu cau nhan nhiem vu
                                    GlobalService.gI().quest(1, nvlChuaNhan.ID);
                                    Thread.Sleep(100);


                                    // nhan nhiem vu
                                    GlobalService.gI().Choice_Dialog_server(ReadMessenge.IdDialog, 0);
                                    
                                }
                            }
                            
                        }
                        else if(nvlDaHoanThanh != null)
                        {
                            // hoan thanh => tra nhiem vu
                            GlobalService.gI().quest(4, nvlDaHoanThanh.ID);
                            Counter.COUNT_NVL++;
                        }
                        Thread.Sleep(1000);
                        
                    }
                    catch { }
                    
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        
        if(CustomConfig.isAutoNVL == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }

       
    }

    private static NPC getNpcByName(string name)
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.typeObject == (sbyte)Common.eNpcType.NPC && mainObject.name.ToLower().Equals(name))
                {
                    return (NPC)mainObject;
                }
            }
        }
        return null;
    }
    private static NPC getNpcNVL()
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.typeObject == (sbyte)Common.eNpcType.NPC && mainObject.name.ToLower().Equals("nhiệm vụ"))
                {
                    return (NPC)mainObject;
                }
            }
        }
        return null;
    }

    private static MainQuest GetNvlChuaNhan()
    {
        for (int m = 0; m < Player.vecQuest.size(); m++)
        {
            MainQuest quest = (MainQuest)Player.vecQuest.elementAt(m);
            if (quest.typeMainSub == 2 && quest.statusQuest == 0x00)
            {
                return quest;
            }

        }
        return null;
    }

    private static MainQuest GetNvlDaHoanThanh()
    {
        for (int m = 0; m < Player.vecQuest.size(); m++)
        {
            MainQuest quest = (MainQuest)Player.vecQuest.elementAt(m);
            if (quest.typeMainSub == 2 && quest.statusQuest == 0x02)
            {
                return quest;
            }

        }
        return null;
    }
    private static MainQuest GetNvlDaNhan()
    {
        for (int m = 0; m < Player.vecQuest.size(); m++)
        {
            MainQuest quest = (MainQuest)Player.vecQuest.elementAt(m);
            if(quest.typeMainSub == 2 && quest.statusQuest == 0x01)
            {
                return quest;
            }

        }
        return null;
    }
}