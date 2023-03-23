using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal class fAutoHeart
{
    public static void Handle()
    {
        new Thread(() => {
            for (int i = 0; i < 3; i++)
            {

                // yeu cau nang tim
                GlobalService.gI().Upgrade_Item(0x0F, 0xFF, 0x00);
                Thread.Sleep(1000);

                // an xac nhan
                GlobalService.gI().Choice_Dialog_server(ReadMessenge.IdDialog, 0x00);
                Thread.Sleep(100);
            }
            GameScreen.player.strChatPopup = "Đã xong !";
            GameCanvas.end_Cur_Dialog();
        })
        { IsBackground = true }.Start();
    }
}