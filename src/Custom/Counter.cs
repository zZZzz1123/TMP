using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Counter
{
    public static int COUNT_NVL = 0;
    public static int COUNT_ITEM_TRANG_DA_BAN = 0;
    public static int COUNT_ITEM_XANH_DA_BAN = 0;
    public static int COUNT_ITEM_TIM_DA_BAN = 0;
    public static int COUNT_BERY_BAN_ITEM = 0;
    public static int COUNT_PACKET_SKILL_SENDED = 0;
    public static int COUNT_SO_LAN_HUT_BERI_THANH_CONG = 0;

    public static int GetTotalBery()
    {
        return COUNT_BERY_BAN_ITEM;
    }
}
