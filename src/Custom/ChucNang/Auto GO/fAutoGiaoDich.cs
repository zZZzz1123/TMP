using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static Common;

internal class fAutoGiaoDich
{
    public static bool isSend = false;
    public static bool isReceive = false;
    public static string[] SENDER = { "syshenu", "scarlet","quangduy1st", "nhantcn", "tanloik18o3" };
    public static string REVER = "zigxiv";
    //public static string REVER = "godnami";
    private static Thread t = null;

    public static void AddItem()
    {

        if (isSend)
        {
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x01, 0, 7, 1, "");
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x01, 0, 6, 0x00077A10, ""); //490.000
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x03, 0, 0, 1, "");
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x04, 0, 0, 1, "");
            TradeScreen.instance.isShowInfo = false;
        }
    }
    public static void AccpetTrade(short id ,string name)
    {
        if (isReceive)
        {
            if (SENDER.Contains(name))
            {
                Thread.Sleep(1000);
                GlobalService.gI().Trade(0x06, id, 0, 1, "");
            }
        }
    }
    public static void Lock()
    {
        if(isReceive)
        {
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x03, 0, 0, 1, "");
        }
    }

    public static void Trade()
    {
        if (isReceive)
        {
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x04, 0, 0, 1, "");
        }
       
    }

    private static void SendRequest(short id)
    {
        if (isSend)
        {
            Thread.Sleep(1000);
            GlobalService.gI().Trade(0x06, id, 0, 0, "");
        }
    }
    public static void Handle()
    {
        isSend = !isSend;
        if (t == null && isSend)
        {
            t = new Thread(() => {
                while (isSend)
                {
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
                                    if (p.name == REVER)
                                    {
                                        SendRequest(p.ID);
                                        while (TradeScreen.instance == null)
                                        {
                                            Thread.Sleep(10);
                                        }
                                        while (TradeScreen.instance.isShowInfo)
                                        {
                                            Thread.Sleep(10);
                                        }

                                    }
                                }

                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true };
            t.Start();
        }

        if (isSend == false)
        {
            t.Suspend();
            t.Abort();
            t = null;
        }
    }
}
