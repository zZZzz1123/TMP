using hqx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using static Common;
using static System.Collections.Specialized.BitVector32;

internal class fCongCu
{
    public static iCommand cmdCurrentMap;
    public static iCommand cmdQuaiInMap;
    public static iCommand cmdNpcInMap;
    public static iCommand cmdInfoObjectFocus;
    public static iCommand cmdGetChangePointInMap;

    public static iCommand cmdTest;
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();

        cmdCurrentMap = new iCommand("Map hiện tại", 996, 0, GameCanvas.readMess);
        mVector2.addElement(cmdCurrentMap);

        cmdGetChangePointInMap = new iCommand("ChangePoint hiện tại", 996, 5, GameCanvas.readMess);
        mVector2.addElement(cmdGetChangePointInMap);

        cmdQuaiInMap = new iCommand("Thông tin quái trong map", 996, 1, GameCanvas.readMess);
        mVector2.addElement(cmdQuaiInMap);

        cmdNpcInMap = new iCommand("Thông tin NPC trong map", 996, 3, GameCanvas.readMess);
        mVector2.addElement(cmdNpcInMap);


        cmdInfoObjectFocus = new iCommand("Thông tin Player/Npc/Monster", 996, 4, GameCanvas.readMess);
        mVector2.addElement(cmdInfoObjectFocus);

        cmdTest = new iCommand("Test CMD", 996, 2, GameCanvas.readMess);
        mVector2.addElement(cmdTest);

        GameCanvas.menu.startAt(mVector2, 2, "Zig Menu Công Cụ " + "- V" + CustomConfig.Version);
    }


    public static void HandleSelect(int subindex)
    {
        switch (subindex)
        {
            case 0:
                MapHienTai();
                break;
            case 1:
                MonsterInMap();
                break;
            case 2:
                TestCmd();
                break;
            case 3:
                NpcInMap();
                break;
            case 4:
                IdNpcHienTai();
                break;
            case 5:
                ChangePointHienTai();
                break;


        }

    }

    public static void IdNpcHienTai()
    {
        
        MainObject npc = GameScreen.objFocus;
        string info = $"---- {npc.name}({npc.Lv}) ----\n" +
            $"ID: 0x{npc.ID.ToString("X")} ({int.Parse(npc.ID.ToString("X"), System.Globalization.NumberStyles.HexNumber)})\n" +
            $"NameGiaoTiep: {npc.nameGiaotiep}\n" +
            $"TypeObject: {npc.typeObject}\n" +
            $"TypeSpecMonster: {npc.typeSpecMonSter}\n" +
            $"TypePK: {npc.typePK}\n" +
            $"TypeQuest: {npc.typeQuest}\n" +
            $"-------------\n\n";
        
        GameCanvas.chatTabScr.addNewChat("NPC", "", info, 1, false);
    }

    public static void ChangePointHienTai()
    {
        string info = "";
        for(int i =0; i < LoadMap.vecPointChange.size();i++)
        {
            Point p = (Point)LoadMap.vecPointChange.elementAt(i);
            info +=
                $"Name: {p.name}\n" +
                $"X: {p.x}\n" +
                $"Y: {p.y}\n";
        }
        info += $"TOTAL: {LoadMap.vecPointChange.size()}\n" +
            $"----------\n";
        GameCanvas.chatTabScr.addNewChat("CHANGE-POINT", "", info, 1, false);
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
    private static void TestCmd()
    {
        // int a = 65436;
        //Thread t = new Thread(() =>
        //{
        //    for (int i = 0; i < 200; i++)
        //    {
        //        //GlobalService.gI().Dynamic_Menu((short)a, 0, 0);
        //        GlobalService.gI().Use_Potion(0x009E);
        //        Thread.Sleep(10);
        //    }

        //});
        //t.IsBackground = true;
        //t.Start();
        // AUTO UP DEVIL
        //Thread t = new Thread(() =>
        //{
        //    for (int i = 0; i < 200; i++)
        //    {
        //        GlobalService.gI().Devil_Upgrade(0x09, 0x07EE, 0x68, 0x0000);
        //        Thread.Sleep(300);

        //        GlobalService.gI().Devil_Upgrade(0x0C, 0x07EE, 0x68, 0x0000);
        //        Thread.Sleep(300);

        //        GlobalService.gI().Choice_Dialog_server(ReadMessenge.IdDialog, 0x01);
        //        Thread.Sleep(300);
        //    }

        //});
        //t.IsBackground = true;
        //t.Start();


        // AUTO MO DO
        List<Item> listToSell = new List<Item>();
        List<Potion> listRuongChauBau = new List<Potion>();
        Thread t = new Thread(() =>
        {
            while (true)
            {
                listToSell.Clear();
                listRuongChauBau.Clear();
                for (int i = 0; i < Player.vecInventory.size(); i++)
                {
                    object tmp = Player.vecInventory.elementAt(i);
                    if (tmp.GetType() == typeof(Item))
                    {
                        Item item = (Item)tmp;
                        if (item.colorName == (sbyte)Common.eItemColor.TRANG ||
                        item.colorName == (sbyte)Common.eItemColor.XANH ||
                        item.colorName == (sbyte)Common.eItemColor.TIM ||
                        item.colorName == (sbyte)Common.eItemColor.CAM
                        )
                        {
                            listToSell.Add(item);
                        }

                    }
                    else if (tmp.GetType() == typeof(Potion))
                    {
                        Potion p = (Potion)tmp;
                        if (p.name.ToLower().Contains("rương huyền bí lv60"))
                        {
                            listRuongChauBau.Add(p);
                        }
                    }
                }
                List<Item> listToSellFinal = new List<Item>();
                List<Item> listToKeep = new List<Item>();
                for (int i = 0; i < listToSell.Count; i++)
                {
                    Item item = listToSell.ElementAt(i);
                    //if(item.typeEquip == 0 && item.numLoKham == 2) // VK
                    //if(item.typeEquip == 3 && item.numLoKham == 2) // AO
                    //if(item.typeEquip == 5 && item.numLoKham == 2) // QUAN
                    //if(item.typeEquip == 1 && item.numLoKham == 2) // NON
                    //if(item.typeEquip == 2 && item.numLoKham == 2) // DAY CHUYEN
                    //if (item.typeEquip == 4 && item.numLoKham == 2) // NHAN
                    //{
                    for (int j = 0; j < item.vecInfo.size(); j++)
                    {
                        if (item.colorName == (sbyte)Common.eItemColor.CAM)
                        {
                            infoShow info = (infoShow)item.vecInfo.elementAt(j);
                            if ((info.value > 87 && info.value <= 89) ||
                                (info.value > 8 && info.value <= 9))
                            {
                                listToKeep.Add(item);
                            }
                            else
                            {
                                listToSellFinal.Add(item);
                            }
                        }
                        else
                        {
                            listToSellFinal.Add(item);
                        }


                        //if(info.id == 11) //STCM
                        //if(info.id == 12) //NE TRANH
                        //if(info.id == 5) //TNSM
                        //if(info.id == 10) //CHI MANG
                        //if(info.id == 7) //TNTL
                        //if (info.id == 6) //TNPT
                        //{
                        //    if(info.value > 90 && info.value <= 100)// CM
                        //    //if(info.value > 400 && info.value <= 409) // STCM
                        //    //if(info.value > 76 && info.value <= 79) // NE TRANH
                        //    //if (info.value > 7 && info.value <= 8) // TNSM
                        //    {
                        //        listToKeep.Add(item);
                        //    }
                        //}
                    }
                    //}
                    //else
                    //{
                    //    listToSellFinal.Add(item);
                    //}
                }

                for (int i = 0; i < listToSell.Count(); i++)
                {
                    Item item = listToSell.ElementAt(i);
                    if (!listToKeep.Contains(item))
                    {
                        GlobalService.gI().Sell_Item(0x00, item.ID, 0x03, 1);
                        Thread.Sleep(100);
                        GlobalService.gI().Choice_Dialog_server(ReadMessenge.IdDialog, 0);
                    }
                }

                foreach (Potion p in listRuongChauBau)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GlobalService.gI().Use_Potion(p.ID);
                    }
                }

                GameCanvas.end_Cur_Dialog();
                if (listToKeep.Count >= 37) { return; }
                Thread.Sleep(100);
            }

        });
        t.IsBackground = true;
        t.Start();





    }
    private static void MapHienTai()
    {
        string area = (LoadMap.Area +1).ToString();
        string idMap = GameCanvas.loadmap.idMap.ToString();
        string name = LoadMap.mNameMap[GameCanvas.loadmap.idMap];

        string result = $"Bạn đang ở {name} ({idMap}) - khu {area}";
        mVector vecCmd = new mVector();
        GameCanvas.Start_Normal_DiaLog(result, vecCmd, true);
    }

    private static void NpcInMap()
    {
        string content = "ID | Name\r\n";
        int count = 0;
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.typeObject == (sbyte)eNpcType.NPC)
                {
                    string name = string.IsNullOrEmpty(mainObject.name) ? $"({mainObject.nameGiaotiep})" : mainObject.name;
                    string id = $"0x{mainObject.ID.ToString("X")} ({int.Parse(mainObject.ID.ToString("X"), System.Globalization.NumberStyles.HexNumber)})";
                    content += id + " | " + name + "\r\n";
                    count++;
                }
            }
        }
        content += "Totol: " + count + "\r\n------";
        GameCanvas.chatTabScr.addNewChat("Zig-Monster", "", content, 1, false);
    }
    private static void MonsterInMap()
    {
        string content = "ID | Name\r\n";
        int count = 0;
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 && mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                {
                    string name = string.IsNullOrEmpty(mainObject.name) ? $"({mainObject.nameGiaotiep})" : mainObject.name;
                    string id = $"0x{mainObject.ID.ToString("X")} ({int.Parse(mainObject.ID.ToString("X"), System.Globalization.NumberStyles.HexNumber)})";
                    content += id + " | " + name + "\r\n";
                    count++;
                }
            }
        }
        content += "Totol: " + count + "\r\n------";
        GameCanvas.chatTabScr.addNewChat("Zig-Monster", "", content, 1, false);

    }
}