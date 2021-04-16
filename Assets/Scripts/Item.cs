using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum itemType
    {
        none,
        handgun,
        shotgun,
        smg,
        pills,
        medkit
    }

    public itemType iType;
    public int amount;

    public Sprite getSprite()
    {
        switch (iType)
        {
            default:
            case itemType.handgun: return ItemAssets.Instance.handgunSprite;
            case itemType.shotgun: return ItemAssets.Instance.shotgunSprite;
            case itemType.smg: return ItemAssets.Instance.smgSprite;
            case itemType.pills: return ItemAssets.Instance.pillsSprite;
            case itemType.medkit: return ItemAssets.Instance.medkitSprite;
        }
    }

    public bool isStackable()
    {
        switch (iType)
        {
            default:
            case itemType.handgun:
            case itemType.shotgun:
            case itemType.smg:
                return false;
            case itemType.pills:
            case itemType.medkit:
                return true;
        }
    }

    public string getDescription()
    {
        switch (iType)
        {
            default:
            case itemType.handgun:
                return "It is handgun";
            case itemType.shotgun:
                return "It is shotgun";
            case itemType.smg:
                return "It is smg";
            case itemType.pills:
                return "These are pills";
            case itemType.medkit:
                return "It is medkit";
        }
    }

}
