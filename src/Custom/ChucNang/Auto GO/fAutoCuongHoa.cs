using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;

internal class fAutoCuongHoa : AvMain
{
    public static iCommand cmd6;
    public static iCommand cmd10;
    public static iCommand cmd11;
    public static iCommand cmd12;
    public static iCommand cmd13;
    public static iCommand cmd14;
    public static iCommand cmd15;
    public static fAutoCuongHoa hanlder = new fAutoCuongHoa();
    private static int maxLevel = 0;
    public static int[] notUseShield = { 0, 3, 6, 9 };
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();

        cmd6 = new iCommand("Cường hóa +6 (Auto 20)", 0, 6, hanlder);
        mVector2.addElement(cmd6);

        cmd10 = new iCommand("Cường hóa +10 (Auto 20)", 0, 10, hanlder);
        mVector2.addElement(cmd10);

        cmd11 = new iCommand("Cường hóa +11 (Auto 20)", 0, 11, hanlder);
        mVector2.addElement(cmd11);

        cmd12 = new iCommand("Cường hóa +12 (Auto 20)", 0, 12, hanlder);
        mVector2.addElement(cmd12);

        cmd13 = new iCommand("Cường hóa +13 (Auto 20)", 0, 13, hanlder);
        mVector2.addElement(cmd13);

        cmd14 = new iCommand("Cường hóa +14 (Auto 20)", 0, 14, hanlder);
        mVector2.addElement(cmd14);

        cmd15 = new iCommand("Cường hóa +15 (Auto 20)", 0, 15, hanlder);
        mVector2.addElement(cmd15);
        GameCanvas.menu.startAt(mVector2, 2, "Zig Auto Server GO Menu " + "- V" + CustomConfig.Version);
    }
    public override void commandPointer(int index, int subindex) 
    {
        switch (index)
        {
            case 0:
                maxLevel = subindex;
                ShowItemCanUp(0, subindex);
                break;

            case 10:
                StartUpgrade(subindex);
                break;

            case 11:
                StartSuperUpgrade(subindex);
                break;

        }
    }
    
    public static void StartUpgrade(int itemId)
    {
        
        new Thread(() => {
            for(int i =0;i < 20; i++)
            {
                Item equip = Common.GetEquipById(itemId);
                if(equip == null)
                {
                    GameCanvas.Start_Normal_DiaLog("Không tìm thấy vật phẩm !", new mVector(), true);
                    return;
                }
                // bo item vo
                GlobalService.gI().Upgrade_Item(0x04, (short)itemId, 0);
                Thread.Sleep(100);

                // bo sao may man
                GlobalService.gI().Upgrade_Item(0x05, 0x000B, 0x01);
                Thread.Sleep(100);

                // bo mai rua
                if (!notUseShield.Contains(equip.LvUpgrade))
                {

                    GlobalService.gI().Upgrade_Item(0x06, 0x0006, 0x01);
                    Thread.Sleep(100);
                }

                // yeu cau
                GlobalService.gI().Upgrade_Item(0x01, 0x00, 0x00);
                Thread.Sleep(100);
                GameCanvas.end_Cur_Dialog();

                // cuong hoa

                GlobalService.gI().Upgrade_Item(0x02, 0x0, 0x02);
                Thread.Sleep(500);

                equip = Common.GetEquipById(itemId);
                if(equip.LvUpgrade >= maxLevel)
                {
                    return;
                }

            }
        }) { IsBackground = true }.Start();
    }


    public static void StartSuperUpgrade(int itemId)
    {
        new Thread(() => {
            for (int i = 0; i < 200000; i++)
            {
                Item equip = Common.GetEquipById(itemId);
                if (equip == null)
                {
                    GameCanvas.Start_Normal_DiaLog("Không tìm thấy vật phẩm !", new mVector(), true);
                    return;
                }

                // bo item vo
                GlobalService.gI().Upgrade_Super_Item(0x04, (short)itemId, 0,0);
                Thread.Sleep(10);

                // bo sao may man
                GlobalService.gI().Upgrade_Super_Item(0x05, 0x000B, 0x01, 0x01);
                Thread.Sleep(10);


                // bo mai rua
                GlobalService.gI().Upgrade_Super_Item(0x06, 0x0006, 0x01, 0x05);
                Thread.Sleep(10);

                // yeu cau
                GlobalService.gI().Upgrade_Super_Item(0x01, 0x0000, 0x00, 0x00);
                Thread.Sleep(10);

                // nang cap
                GlobalService.gI().Upgrade_Super_Item(0x02, 0x0000, 0x02, 0x00);
                Thread.Sleep(10);

                equip = Common.GetEquipById(itemId);
                if (equip.LvUpgrade >= maxLevel)
                {
                    return;
                }

            }
        })
        { IsBackground = true }.Start();
    }
    public static void ShowItemCanUp(int minLevel, int maxLevel)
    {
        mVector mVector2 = new mVector();
        for (int i = 0; i < Player.vecInventory.size(); i++) 
        {
            if(Player.vecInventory.elementAt(i).GetType() == typeof(Item))
            {
                Item item = (Item)Player.vecInventory.elementAt(i);
                if(item.typeObject == 3 && item.LvUpgrade < maxLevel && item.LvUpgrade >= minLevel)
                {
                    if(item.LvUpgrade >= 10)
                    {
                        iCommand cmd = new iCommand(item.name, 11, item.ID, hanlder);
                        mVector2.addElement(cmd);
                    }
                    else
                    {

                        iCommand cmd = new iCommand(item.name, 10, item.ID, hanlder);
                        mVector2.addElement(cmd);
                    } 
                }
            }
        }
        GameCanvas.menu.startAt(mVector2, 2, "Zig Auto Server GO Menu " + "- V" + CustomConfig.Version);
    }

}