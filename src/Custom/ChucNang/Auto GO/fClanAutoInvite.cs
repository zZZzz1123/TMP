using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fClanAutoInvite
{
    public static void Handle()
    {
        CustomConfig.isAutoInviteClan = !CustomConfig.isAutoInviteClan;
        List<string> inviteList = new List<string>();
        if (File.Exists("invite_clan_list.txt"))
        {
            inviteList = File.ReadAllLines("invite_clan_list.txt").ToList();
        }
        Thread t = new Thread(() =>
        {
            int count = 0;
            while (CustomConfig.isAutoInviteClan)
            {
                Thread.Sleep(1000);
                if(count == (1000 * 900 ))
                {
                    count = 0;
                    GlobalService.gI().Clan_CMD(0, "Lần đầu buff exp inbox thuyền trưởng để add tên auto mời clan khi về NH K1.", 0x00, 0x00);
                }
                for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
                {
                    MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
                    if (mainObject != null)
                    {
                        if (mainObject.typeObject == (sbyte)eNpcType.ANOTHER_PLAYER)
                        {
                            if (mainObject.GetType() != typeof(Player))
                            {
                                Other_Player p = (Other_Player)mainObject;
                                if (inviteList.Contains(p.name))
                                {
                                    GlobalService.gI().Clan_CMD(10, "", p.ID, 0x00);
                                }
                            }

                        }
                    }
                }
                count += 1000;
            }

        });
        t.IsBackground = true;
        t.Start();
    }
}
