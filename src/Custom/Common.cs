using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;

internal class Common
{
    public enum eItemColor
    {
        TRANG,
        XANH,
        TIM,
        CAM
    }
    public enum eNpcType
    {
        ANOTHER_PLAYER = 0,
        MONSTER = 1,
        NPC = 2
    }


    public static Item GetEquipById(int id)
    {
        for (int i = 0; i < Player.vecInventory.size(); i++)
        {
            if (Player.vecInventory.elementAt(i).GetType() == typeof(Item))
            {
                Item item = (Item)Player.vecInventory.elementAt(i);
                if(item.ID == id)
                {
                    return item;
                }
            }
        }
        return null;
    }
    public static NPC GetNpcByName(string npcName) 
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++) {
            if(GameScreen.vecPlayers.elementAt(i).GetType() == typeof(NPC))
            {
                NPC npc = (NPC)GameScreen.vecPlayers.elementAt(i);
                if (npc.name.ToLower().Equals(npcName))
                {
                    return npc; 
                }
            }
        }
        return null;
    }
    public static mVector HackDmg(mVector mVecbefire,int xDame)
    {
        mVector hijackVec = new mVector();
        for(int i =0; i < mVecbefire.size(); i++)
        {
            for(int j =0;j < xDame; j++)
            {
                hijackVec.addElement(mVecbefire.elementAt(i));
            }
        }
        return hijackVec;   
    }
    public static int Distance(Point p1, Point p2)
    {
       return ((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
    }

    public static mVector GetNearObject(MainObject mainObject, int distance)
    {
        mVector mVector2 = new mVector();
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject2 != null)
            {
                if (mainObject2 != null && mainObject2.Action != 4 && !mainObject2.isSend && mainObject2.typeObject != 10 && mainObject2.typeObject == (sbyte)eNpcType.MONSTER)
                {

                    int tmpDistance = ((mainObject.x - mainObject2.x) * (mainObject.x - mainObject2.x) + (mainObject.y - mainObject2.y) * (mainObject.y - mainObject2.y));
                    if(tmpDistance <= distance)
                    {
                        mVector2.addElement(mainObject2);   
                    }

                }
            }
        }
        return mVector2;
    }
    
    public static MainObject GetBoss()
    {
        string[] bossName = { "lân sư tử", "siêu along", "buggi", "kuroma","tashy", "tashi" };

        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 && mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                {
                    for(int j = 0; j < bossName.Count(); j++)
                    {
                        if (mainObject.name.ToLower().Contains(bossName[j])){
                            return mainObject;
                        }
                    }
                }
            }
        }

        return null;
    }

    public static Other_Player GetOtherPlayerById(int id)
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject.GetType() == typeof(Other_Player))
                {
                    if (mainObject.ID == id)
                        return (Other_Player)mainObject;

                }
            }
        }
        return null;
    }
    public static MainObject GetRandomMonster()
    {
        mVector mVector2 = new mVector();
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 && mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                {
                    mVector2.addElement(mainObject);
                }
            }
        }

        if(mVector2.size() > 0)
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, mVector2.size()-1);
            return (MainObject)mVector2.elementAt(rInt);    
        }
        else
        {
            return null;
        }    
    }

    public static MainObject GetMonserByID(int id)
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 && mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                {
                    if (mainObject.ID == id)
                        return mainObject;
                }
            }
        }
        return null;
    }
    public static MainObject GetFisrtMonster()
    {
        for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
            if (mainObject != null)
            {
                if (mainObject != null && mainObject.Action != 4 && !mainObject.isSend && mainObject.typeObject != 10 &&  mainObject.typeObject == (sbyte)eNpcType.MONSTER)
                {
                    return mainObject;

                }
            }
        }
        return null;
    }
}
