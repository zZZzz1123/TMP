using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

internal class fDonRuong
{
    public static iCommand CmdDonRuong;
    public static iCommand cmdDonTrangBiTrang;
    public static iCommand cmdDonTrangBiXanh;
    public static iCommand cmdDonTrangBiTim;
    public static iCommand cmdMoRuongChauBau;
    private static Thread t = null;
    public static object SyncObj = new object();    
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();
        if(CustomConfig.isDonRuong)
            CmdDonRuong = new iCommand("Tắt tự động dọn rương.", 998, 0, GameCanvas.readMess);
        else
            CmdDonRuong = new iCommand("Bật tự động dọn rương.", 998, 0, GameCanvas.readMess);

        mVector2.addElement(CmdDonRuong);

        if (CustomConfig.isDonTrangBiTrang)
            cmdDonTrangBiTrang = new iCommand("Tắt tự động dọn trang bị trắng.", 998, 1, GameCanvas.readMess);
        else
            cmdDonTrangBiTrang = new iCommand("Bật tự động dọn trang bị trắng.", 998, 1, GameCanvas.readMess);

        mVector2.addElement(cmdDonTrangBiTrang);

        if (CustomConfig.isDonTrangBiXanh)
            cmdDonTrangBiXanh = new iCommand("Tắt tự động dọn trang bị xanh.", 998, 2, GameCanvas.readMess);
        else
            cmdDonTrangBiXanh = new iCommand("Bật tự động dọn trang bị xanh.", 998, 2, GameCanvas.readMess);

        mVector2.addElement(cmdDonTrangBiXanh);


        if (CustomConfig.isDonTrangBiTim)
            cmdDonTrangBiTim = new iCommand("Tắt tự động dọn trang bị tím.", 998, 3, GameCanvas.readMess);
        else
            cmdDonTrangBiTim = new iCommand("Bật tự động dọn trang bị tím.", 998, 3, GameCanvas.readMess);

        mVector2.addElement(cmdDonTrangBiTim);

        if (CustomConfig.isMoRuongChauBau)
            cmdMoRuongChauBau = new iCommand("Tắt tự động mở rương châu báu.", 998, 4, GameCanvas.readMess);
        else
            cmdMoRuongChauBau = new iCommand("Bật tự động mở rương châu báu.", 998, 4, GameCanvas.readMess);

        mVector2.addElement(cmdMoRuongChauBau);

        GameCanvas.menu.startAt(mVector2, 2, "Zig Auto Menu Rương" + "- V" + CustomConfig.Version);
    }

    public static void HandleSelect(int subindex)
    {
        Interface_Game.addInfoPlayerNormal(subindex.ToString(), mFont.tahoma_7_white);
        switch (subindex)
        {
            case 0:
                Hanlde();
                break;
            case 1:
                CustomConfig.isDonTrangBiTrang = CustomConfig.isDonTrangBiTrang == true ? false : true;
                break;
            case 2:
                CustomConfig.isDonTrangBiXanh = CustomConfig.isDonTrangBiXanh == true ? false : true;
                break;
            case 3:
                CustomConfig.isDonTrangBiTim = CustomConfig.isDonTrangBiTim == true ? false : true;
                break;
            case 4:
                CustomConfig.isMoRuongChauBau = CustomConfig.isMoRuongChauBau == true ? false : true;
                break;

        }
    }

    public static void Hanlde()
    {

        CustomConfig.isDonRuong = CustomConfig.isDonRuong == true ? false : true;

        if (t == null && CustomConfig.isDonRuong)
        {
            // THREAD BAN DO
            new Thread(() => {
                while (CustomConfig.isDonRuong)
                {
                    for (int i = 0; i < Player.vecInventory.size(); i++)
                    {
                        object tmp = Player.vecInventory.elementAt(i);
                        if (tmp.GetType() == typeof(Item))
                        {
                            Item item = (Item)tmp;
                            if (item.colorName == (sbyte)Common.eItemColor.TRANG || item.colorName == (sbyte)Common.eItemColor.XANH || item.colorName == (sbyte)Common.eItemColor.TIM)
                            {
                                GlobalService.gI().Sell_Item(0x00, item.ID, 0x03, 1);
                                Thread.Sleep(10);
                            }

                        }
                    }
                }
            }) { IsBackground = true }.Start();


            // THREAD MO RUONG
            new Thread(() => {
                for (int i = 0; i < Player.vecInventory.size(); i++)
                {
                    object tmp = Player.vecInventory.elementAt(i);
                    if (tmp.GetType() == typeof(Potion))
                    {
                        Potion p = (Potion)tmp;
                        if (p.name.ToLower().Contains("rương châu báu") || p.name.ToLower().Contains("rương huyền bí"))
                        {
                            new Thread(() => OpenChest(p)) { IsBackground = true }.Start();
                           
                        }
                    }
                }
            })
            { IsBackground = true }.Start();


        //    t = new Thread(() =>
        //    {
        //        Interface_Game.addInfoPlayerNormal("--- Bật tự động dọn rương khi về làng ---", mFont.tahoma_7_orange);
        //        List<Item> listToSell = new List<Item>();
        //        List<Potion> listRuongChauBau = new List<Potion>();
        //        while (CustomConfig.isDonRuong)
        //        {

        //            try
        //            {
        //                listToSell.Clear();
        //                listRuongChauBau.Clear();
        //                for (int i=0; i < Player.vecInventory.size(); i++)
        //                {
        //                    object tmp = Player.vecInventory.elementAt(i);
        //                    if(tmp.GetType() == typeof(Item))
        //                    {
        //                        Item item = (Item)tmp;
        //                        if (CustomConfig.isDonTrangBiTrang && item.colorName == (sbyte)Common.eItemColor.TRANG) 
        //                        {
        //                            listToSell.Add(item);
        //                        }
        //                        if (CustomConfig.isDonTrangBiXanh && item.colorName == (sbyte)Common.eItemColor.XANH)
        //                        {
        //                            listToSell.Add(item);
        //                        }
        //                        if (CustomConfig.isDonTrangBiTim && item.colorName == (sbyte)Common.eItemColor.TIM)
        //                        {
        //                            listToSell.Add(item);
        //                        }
                                
        //                    }
        //                    else if(tmp.GetType() == typeof(Potion) && CustomConfig.isMoRuongChauBau)
        //                    {
        //                        Potion p = (Potion)tmp;
        //                        if(p.name.ToLower().Contains("rương châu báu"))
        //                        {
        //                            if(p.numPotion < 100)
        //                            {
        //                                GlobalService.gI().Sell_Item(1, p.ID, 4, -100);
        //                            }
        //                            listRuongChauBau.Add(p);
        //                        }
        //                    }
        //                }

        //                foreach(Item item in listToSell)
        //                {
        //                    int priceSell = 0;
        //                    if ( item.colorName == (sbyte)Common.eItemColor.TRANG)
        //                    {
        //                        priceSell = GetPriceSell(item);
        //                    }
        //                    else if (item.colorName == (sbyte)Common.eItemColor.XANH)
        //                    {
        //                        priceSell = GetPriceSell(item);
        //                    }
        //                    else if (item.colorName == (sbyte)Common.eItemColor.TIM)
        //                    {                              
        //                        priceSell = GetPriceSell(item);
        //                    }

        //                    GlobalService.gI().Sell_Item(0x00, item.ID, 0x03, 1);
        //                    Thread.Sleep(10);


        //                }

        //                foreach (Potion p in listRuongChauBau)
        //                {
        //                    for (int i = 0; i < 1; i++)
        //                    {
        //                        GlobalService.gI().Use_Potion(p.ID);
        //                    }
        //                }

        //                GameCanvas.end_Cur_Dialog();
        //                GameCanvas.end_Dialog();

        //                Thread.Sleep(10);

        //            }
        //            catch { }

        //        }
        //    });
        //    t.IsBackground = true;
        //    t.Start();
        }

        if (CustomConfig.isDonRuong == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }

    }

    private static int GetPriceSell(Item item)
    {
        int price = 30 + (item.colorName * 2 + item.Lv_RQ / 10 + 1) * TabInventory.priceItemSell;
        if (price > TabInventory.maxPriceItemSell)
        {
            price = TabInventory.maxPriceItemSell;
        }
        return price;
    }

    private static void OpenChest(Potion chest)
    {
        while (CustomConfig.isDonRuong)
        {
            if (chest.numPotion < 300)
            {
                GlobalService.gI().Sell_Item(1, chest.ID, 4, -100);
                chest.numPotion += 100;
            }
            else
            {
                GlobalService.gI().Use_Potion(chest.ID);
                chest.numPotion--;
            }

            Thread.Sleep(50);
        }
    }
}
