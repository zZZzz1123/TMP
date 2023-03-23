using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

internal class CustomMenu
{

    public static iCommand cmdTongKet;
    public static iCommand AutoNVL;
    public static iCommand X2Speed;
    public static iCommand DonRuong;
    public static iCommand cmdAutoPhoBan;
    public static iCommand cmdCongCu;
    public static iCommand cmdAuthor;
    public static iCommand cmdTeleDanhQuai;
    public static iCommand cmdFakeSkillEffect;
    public static iCommand cmdFarmSkillTaiCho;
    public static iCommand cmdCustomGoServer;
    public static void StartMenuChest()
    {
       
        mVector mVector2 = new mVector();




        cmdCustomGoServer = new iCommand("Mod Server GO", 999, 10, GameCanvas.readMess);
        mVector2.addElement(cmdCustomGoServer);

        cmdFarmSkillTaiCho = new iCommand("Auto up skill tại chỗ", 999, 9, GameCanvas.readMess);
        mVector2.addElement(cmdFarmSkillTaiCho);

        if (CustomConfig.isAutoNVL == false)
            AutoNVL = new iCommand("Bật tự động nhiệm vụ lặp map hiện tại", 999, 1, GameCanvas.readMess);
        else
            AutoNVL = new iCommand("Tắt tự động nhiệm vụ lặp map hiện tại", 999, 1, GameCanvas.readMess);
        mVector2.addElement(AutoNVL);

        if (CustomConfig.isX2Speed == false)
            X2Speed = new iCommand("Bật x1.5 tốc độ game", 999, 2, GameCanvas.readMess);
        else
            X2Speed = new iCommand("Tắt x1.5 tốc độ game", 999, 2, GameCanvas.readMess);
        mVector2.addElement(X2Speed);

        DonRuong = new iCommand("Tự động dọn rương khi về làng", 999, 3, GameCanvas.readMess);
        mVector2.addElement(DonRuong);

        cmdAutoPhoBan = new iCommand("Auto phó bản", 999, 4, GameCanvas.readMess);
        mVector2.addElement(cmdAutoPhoBan);

        if (CustomConfig.isTeleDanhQuai == false)
            cmdTeleDanhQuai = new iCommand("Bật teleport đánh quái", 999, 7, GameCanvas.readMess);
        else
            cmdTeleDanhQuai = new iCommand("Tắt teleport đánh quái", 999, 7, GameCanvas.readMess);
        mVector2.addElement(cmdTeleDanhQuai);

        cmdCongCu = new iCommand("Công cụ", 999, 5, GameCanvas.readMess);
        mVector2.addElement(cmdCongCu);

        cmdFakeSkillEffect = new iCommand("Hiệu ứng kĩ năng", 999, 8, GameCanvas.readMess);
        mVector2.addElement(cmdFakeSkillEffect);

        cmdTongKet = new iCommand("Thống kê", 999, 0, GameCanvas.readMess);
        mVector2.addElement(cmdTongKet);

        cmdAuthor = new iCommand("Tác giả", 999, 6, GameCanvas.readMess);
        mVector2.addElement(cmdAuthor);


        GameCanvas.menu.startAt(mVector2, 2, "Zig Auto Menu " + "- V" + CustomConfig.Version );
    }

    public static void HandleSelect(int subindex)
    {
        Interface_Game.addInfoPlayerNormal(subindex.ToString(), mFont.tahoma_7_white);
        switch (subindex)
        {

            case 0:
                Analyze();
                break;
            case 1:
                fAutoNVL.Handle();
                break;
            case 2:
                fSpeedGame.Handle();
                break;
            case 3: 
                fDonRuong.StartMenuChest();
                break;
            case 4:
                fAutoPhoBanMenu.StartMenuChest();
                break;
            case 5:
                fCongCu.StartMenuChest();
                break;
            case 6:
                mVector vecCmd = new mVector();
                GameCanvas.Start_Normal_DiaLog(CustomConfig.BanQuyen, vecCmd, true);
                break;
            case 7:
                fTeleDanhQuai.Handle();
                break;
            case 8:
                fFakeSkillEffect.StartMenuChest();
                break;
            case 9:
                fStandFarmSkill.StartMenuChest();
                break;
            case 10:
                fCustomGoMenu.StartMenuChest();
                break;
        }
    }
    public static void Analyze()
    {
        string tonghop = "Tổng hợp:\r\n" +
             $"[-] Số t.bị bán: {Counter.COUNT_ITEM_TIM_DA_BAN}\r\n" +
             $"[-] Beri thu về: {Counter.COUNT_BERY_BAN_ITEM.ToString("#,##")}\r\n" +
             $"[-] Packet Skill: {Counter.COUNT_PACKET_SKILL_SENDED.ToString("#,##")}\r\n" +
             $"[-] Hút beri thành công: {Counter.COUNT_SO_LAN_HUT_BERI_THANH_CONG.ToString("#,##")}\r\n" +
             $"------";
        GameCanvas.chatTabScr.addNewChat("Zig", "", tonghop, 1, false);

    }
    public static void ShowMyCustomInfo()
    {
        //if(CustomConfig.isAutoPhoBanDon == false)
        //{
        //    fAutoPhoBanDon.Handle();
        //}
        //if (CustomConfig.isDonRuong == false)
        //{
        //    GameCanvas.gI().keyPressed(99);
        //    //CustomConfig.isDonTrangBiTim = true;
        //    //CustomConfig.isDonTrangBiXanh = true;
        //    //CustomConfig.isDonTrangBiTrang = true;
        //    //CustomConfig.isMoRuongChauBau = true;
        //    fDonRuong.Hanlde();
        //}
        ////if (CustomConfig.isAutoHuntBoss == false)
        ////{
        ////    fAutoBoss.HandleSelect();
        ////}
        //if (CustomConfig.isFarmSkillTaiCho == false)
        //{
        //    fStandFarmSkill.Handle();

        //}
        //if (CustomConfig.isBugExp == false)
        //{
        //    fBugExp.Handle();
        //}

        mFont.tahoma_7b_violet.drawString(GameCanvas.gI().g, "Ấn \"Z\" để bật menu mod.", 50, GameCanvas.h - 200, mFont.CENTER);
        Type t = typeof(CustomConfig);
        FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
        int count = 1;
        for(int i = 0; i < fields.Length; i++)
        {
            FieldInfo fi = fields[i];
            if(fi.FieldType == typeof(bool))
            {
                bool value = (bool)fi.GetValue(null);
                string name = fi.Name;
                if(value == true)
                {
                    CustomConfig.GetShow(name,count);
                    
                    count+=2;
                }
                
            }
        }
    }
}
