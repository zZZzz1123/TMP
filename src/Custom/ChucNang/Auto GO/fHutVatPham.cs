using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal class fHutVatPham
{

    private static Thread t = null;
    public static void Handle()
    {
        CustomConfig.isHutVatPham = CustomConfig.isHutVatPham == true ? false : true;

        if (t == null && CustomConfig.isHutVatPham)
        {
            t = new Thread(() =>
            {

                Interface_Game.addInfoPlayerNormal("--- Bật hút vật phẩm ---", mFont.tahoma_7_orange);
                while (CustomConfig.isHutVatPham)
                {

                    try
                    {
                        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
                        {
                            object obj = GameScreen.vecPlayers.elementAt(i);
                            if (obj.GetType() == typeof(Item_Drop))
                            {
                                Item_Drop item = (Item_Drop)obj;
                                GlobalService.gI().Get_Item_Map(item.ID, item.typeObject);
                                if(item.ID == 2 && item.typeObject == 4)
                                {
                                    Counter.COUNT_SO_LAN_HUT_BERI_THANH_CONG++;
                                }
                            }

                            Thread.Sleep(250);
                        }
                        Thread.Sleep(1000);

                    }
                    catch { }

                }
            });
            t.IsBackground = true;
            t.Start();
        }

        if (CustomConfig.isHutVatPham == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}
