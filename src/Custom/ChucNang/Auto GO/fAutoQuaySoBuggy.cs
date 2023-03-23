using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal class fAutoQuaySoBuggy
{
    public static void Handle()
    {
        new Thread(() => {
            for(int i =0; i < 100; i++)
            {
                GlobalService.gI().Quayso(2);
                Thread.Sleep(10);
            }
        }) { IsBackground = true }.Start();
    }
}