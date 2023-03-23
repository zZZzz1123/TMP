using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class CustomConfig
{
    public static String Author = "Zig";
    public static String Version = "1.1";
    public static String BanQuyen = "Bản quyền thuộc về Zig - Nhận code các loại tool theo yêu cầu. Contact for work: 0937935715";

    public static bool isAutoNVL = false;

    public static bool isX2Speed = false;

    public static bool isTeleDanhQuai = false;

    public static bool isDonRuong = false;
    public static bool isDonTrangBiTrang = false;
    public static bool isDonTrangBiXanh = false;
    public static bool isDonTrangBiTim = false;
    public static bool isMoRuongChauBau = false;
   
    public static bool isAutoPhoBanLienTang = false;

    public static bool isFarmSkillTaiCho = false;
    public static int idSKillAutoFarm = 0;

    public static bool isHutVatPham = false;

    public static bool isAutoBuff = false;

    public static bool isBugExp = false;

    public static bool isAutoInviteClan = false;

    public static bool isAutoHuntBoss = false;
    public static bool isAutoPK = false;

    public static bool isAutoGomQuai = false;

    public static bool isAutoPhoBanDon = false;
    public static void GetShow(string propName,int i)
    {
        string content = "";
        switch (propName)
        {
            case "isAutoNVL":
                content = $"Tự động nhiệm vụ lặp: On (Đã làm: {Counter.COUNT_NVL})";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 38, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                return;
            case "isX2Speed":
                content = "Tăng x1.5 tốc độ trò chơi.";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 24, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                return;
            case "isDonRuong":
                content = "Tự động dọn rương";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 23, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                return;
            case "isAutoPhoBanLienTang":
                content = "Tự động liên tầng";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 20, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                return;
            case "isTeleDanhQuai":
                content = "Tele đánh quái";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                return;
            case "isFarmSkillTaiCho":
                content = "Auto up skill: " + (idSKillAutoFarm + 1);
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isHutVatPham":
                content = "Hút vật phẩm: On";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isAutoBuff":
                content = "Auto buff: On";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isBugExp":
                content = "Bug Exp: On";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isAutoInviteClan":
                content = "Auto mời clan";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isAutoPK":
                content = "Auto PK: On";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
            case "isAutoHuntBoss":
                content = "Auto BOSS: On";
                mFont.tahoma_7b_black.drawString(GameCanvas.gI().g, content, content.Length + 17, GameCanvas.h - (200 - (i + 1) * 5), mFont.CENTER);
                break;
        }
    }
}