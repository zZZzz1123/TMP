using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class fAutoPhoBanMenu
{
    public static iCommand cmdAutoLienTang;
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();

        if (CustomConfig.isAutoPhoBanLienTang == false)
            cmdAutoLienTang = new iCommand("Bật tự động đánh liên tầng", 997, 0, GameCanvas.readMess);
        else
            cmdAutoLienTang = new iCommand("Tắt tự động đánh liên tầng", 997, 0, GameCanvas.readMess);
        mVector2.addElement(cmdAutoLienTang);

        GameCanvas.menu.startAt(mVector2, 2, "Zig Menu Auto Phó Bản " + "- V" + CustomConfig.Version);
    }


    public static void HandleSelect(int subindex)
    {
        Interface_Game.addInfoPlayerNormal(subindex.ToString(), mFont.tahoma_7_white);
        switch (subindex) 
        {
            case 0:
                fAutoPhoBanLienTang.Handle();
                break;
        }

    }
}
