using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

internal class fCustomGoMenu
{
    public static iCommand cmdAutoQuaySo;
    public static iCommand cmdHutVatPham;
    public static iCommand cmdAutoBuff;
    public static iCommand cmdBugExp;
    public static iCommand cmdAutoInviteClan;
    public static iCommand cmdAutoHuntBoss;
    public static iCommand cmdAutoPK;
    public static iCommand cmdUpgradeHeart;
    public static iCommand cmdUpgradeTrangBi;
    public static iCommand cmdAutoGomQuai;
    public static iCommand cmdAutoPhoBanDon;
    public static iCommand cmdAutoSendTrade;
    public static iCommand cmdAutoRevTrade;
    public static void StartMenuChest()
    {
        mVector mVector2 = new mVector();

        if (CustomConfig.isAutoPK == false)
            cmdAutoPK = new iCommand("Bật auto PK", 993, 6, GameCanvas.readMess);
        else
            cmdAutoPK = new iCommand("Tắt auto PK", 993, 6, GameCanvas.readMess);
        mVector2.addElement(cmdAutoPK);

        if (CustomConfig.isAutoHuntBoss == false)
            cmdAutoHuntBoss = new iCommand("Bật auto săn boss", 993, 5, GameCanvas.readMess);
        else
            cmdAutoHuntBoss = new iCommand("Tắt auto săn boss", 993, 5, GameCanvas.readMess);
        mVector2.addElement(cmdAutoHuntBoss);


        if (CustomConfig.isAutoPhoBanDon == false)
            cmdAutoPhoBanDon = new iCommand("Bật auto phó bản đơn", 993, 10, GameCanvas.readMess);
        else
            cmdAutoPhoBanDon = new iCommand("Tắt auto phó bản đơn", 993, 10, GameCanvas.readMess);
        mVector2.addElement(cmdAutoPhoBanDon);

        if (CustomConfig.isHutVatPham == false)
            cmdHutVatPham = new iCommand("Bật hút vật phẩm", 993, 1, GameCanvas.readMess);
        else
            cmdHutVatPham = new iCommand("Tắt hút vật phẩm", 993, 1, GameCanvas.readMess);
        mVector2.addElement(cmdHutVatPham);

        if (CustomConfig.isAutoBuff == false)
            cmdAutoBuff = new iCommand("Bật auto buff all", 993, 2, GameCanvas.readMess);
        else
            cmdAutoBuff = new iCommand("Tắt auto buff all", 993, 2, GameCanvas.readMess);
        mVector2.addElement(cmdAutoBuff);

        if (CustomConfig.isAutoGomQuai == false)
            cmdAutoGomQuai = new iCommand("Bật gom quái", 993, 9, GameCanvas.readMess);
        else
            cmdAutoGomQuai = new iCommand("Tắt gom quái", 993, 9, GameCanvas.readMess);
        mVector2.addElement(cmdAutoGomQuai);

        if (CustomConfig.isAutoInviteClan == false)
            cmdAutoInviteClan = new iCommand("Bật auto mời clan", 993, 4, GameCanvas.readMess);
        else
            cmdAutoInviteClan = new iCommand("Tắt auto mời clan", 993, 4, GameCanvas.readMess);
        mVector2.addElement(cmdAutoInviteClan);


        if (CustomConfig.isBugExp == false)
            cmdBugExp = new iCommand("Bật auto bug exp", 993, 3, GameCanvas.readMess);
        else
            cmdBugExp = new iCommand("Tắt auto bug exp", 993, 3, GameCanvas.readMess);
        mVector2.addElement(cmdBugExp);


        cmdAutoQuaySo = new iCommand("Quay số Buggy", 993, 0, GameCanvas.readMess);
        mVector2.addElement(cmdAutoQuaySo);

        cmdUpgradeHeart = new iCommand("Nâng cấp tim 3 lần", 993, 7, GameCanvas.readMess);
        mVector2.addElement(cmdUpgradeHeart);

        cmdUpgradeTrangBi = new iCommand("Cường hóa trang bị", 993, 8, GameCanvas.readMess);
        mVector2.addElement(cmdUpgradeTrangBi);

        cmdAutoSendTrade = new iCommand("Set SEND", 993, 11, GameCanvas.readMess);
        mVector2.addElement(cmdAutoSendTrade);

        cmdAutoRevTrade = new iCommand("SetREV", 993, 12, GameCanvas.readMess);
        mVector2.addElement(cmdAutoRevTrade);

        GameCanvas.menu.startAt(mVector2, 2, "Zig Auto Server GO Menu " + "- V" + CustomConfig.Version);
    }

    public static void HandleSelect(int subindex)
    {
        switch (subindex)
        {
            case 0:
                fAutoQuaySoBuggy.Handle();
                break;
            case 1:
                fHutVatPham.Handle();
                break;
            case 2:
                fAutoBuff.Handle();
                break;
            case 3: 
                fBugExp.Handle();
                break;
            case 4:
                fClanAutoInvite.Handle();
                break;
            case 5:
                fAutoBoss.HandleSelect();
                break;
            case 6:
                fAutoPK.Handle();
                break;
            case 7:
                fAutoHeart.Handle();
                break;
            case 8:
                fAutoCuongHoa.StartMenuChest();
                break;
            case 9:
                fGomQuai.Handle();
                break;
            case 10:
                fAutoPhoBanDon.Handle();
                break;
            case 11:
                fAutoGiaoDich.Handle();
                break;
            case 12:
                fAutoGiaoDich.isReceive = true;
                break;
        }
    }
}
