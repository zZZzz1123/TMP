using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

internal class fBugExp
{
    private static Thread t = null;

    private static List<string> listTown = new List<string>();
    private static Dictionary<string,int> listLand = new Dictionary<string, int>();
    private static int areaBugged = -1;
    public static void Handle()
    {
        CustomConfig.isBugExp = CustomConfig.isBugExp == true ? false : true;

        if (t == null && CustomConfig.isBugExp)
        {
            listTown.Clear();
            listTown.Add("Thị trấn Orang");
            listTown.Add("Làng Sirup");
            listTown.Add("Nhà hàng Barati");
            listTown.Add("Làng hạt dẻ");

            listLand.Clear();
            listLand.Add("Đảo Orange", 65516);
            listLand.Add("Bến tàu Orang", 65516);

            listLand.Add("Đảo Sirup", 65508);
            listLand.Add("Bến tàu Sirup", 65508);

            listLand.Add("Tàu Baratie", 65500);
            listLand.Add("Bến tàu Barati", 65500);

            listLand.Add("Đảo hạt dẻ", 65492);
            listLand.Add("Bến tàu Cocoya", 65492);

            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật farm bug exp ---", mFont.tahoma_7_orange);
                while (CustomConfig.isBugExp)
                {

                    try
                    {

                        // new o map DAU TIEN, TOWN, BEN TAU thi bay ve map thi town
                        if(GameScreen.player.Hp <= 0)
                        {
                            GlobalService.gI().Player_Revice(0);
                        }
                        Thread.Sleep(3000);
                        bool isBug = false;

                        foreach (var land in listLand)
                        {
                            if (land.Key.Equals(LoadMap.mNameMap[GameCanvas.loadmap.idMap]))
                            {
                                GlobalService.gI().Dynamic_Menu((short)land.Value, 0, 0);
                                Thread.Sleep(500);
                                GlobalService.gI().Dynamic_Menu((short)land.Value, 0x01, 2);
                                Thread.Sleep(1000);
                                isBug = true;
                                break;
                            }
                        }
                        if(isBug == true)
                        {
                            continue;
                        }

                        Thread.Sleep(500);

                        if (listTown.Contains(LoadMap.mNameMap[GameCanvas.loadmap.idMap]) )
                        {
                            NPC npcTele = GetNpcTeleport();
                            if (npcTele == null)
                            {
                                continue;
                            }
                            // lay bang dich chuyen o town
                            GlobalService.gI().Dynamic_Menu(npcTele.ID, 0, 0);
                            Thread.Sleep(500);
                            int currentMapBug = -1;
                            for (int i = 2; i < GameCanvas.menu.menuItems.size() - 1; i++)
                            {
                                iCommand cmd = (iCommand)GameCanvas.menu.menuItems.elementAt(i);
                                string name = cmd.caption.ToLower();
                                currentMapBug = i; // bug từ map cuối đi lên
                            }

                            // dich chuyen toi map cuoi
                            GlobalService.gI().Dynamic_Menu(npcTele.ID, 0x01, (sbyte)currentMapBug);
                            Thread.Sleep(3000);
                            continue;
                            
                        }
                        if (areaBugged == -1 || areaBugged == 14)
                            areaBugged = 0;
                        
                        
                        GlobalService.gI().Select_Area(0, (sbyte)areaBugged++);
                        

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isBugExp == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }


    public static NPC GetNpcTeleport()
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.typeObject == (sbyte)Common.eNpcType.NPC && mainObject.nameGiaotiep.ToLower().Equals("bản đồ"))
                {
                    return (NPC)mainObject;
                }
            }
        }
        return null;
    }

    public static NPC GetNpcChangeArea()
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.typeObject == (sbyte)Common.eNpcType.NPC && mainObject.nameGiaotiep.ToLower().Equals("chuyển khu"))
                {
                    return (NPC)mainObject;
                }
            }
        }
        return null;
    }
}