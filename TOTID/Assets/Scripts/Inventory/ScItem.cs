﻿using UnityEngine;


[System.Serializable]
public struct ScItem
{
    [SerializeField] private int count;
    [SerializeField] private ScItemData data;
    #region Stats Weapon
    public void Equip(ScCharacter c)
    {
        c.strengh.AddModifier(new ScStatsModifier(10, StatModType.Flat, this));
        c.strengh.AddModifier(new ScStatsModifier(0.1f, StatModType.PercentMult, this));
    }

    public void UnEquip(ScCharacter c)
    {
        c.strengh.RemoveAllModifiersFromSource(this);
    }
    #endregion

    #region inventory Item
    public void Merge(ref ScItem other)
    {
        if (Full) return;

        if (Empty) data = other.Data;

        if (other.Data != data) throw new System.Exception("Try to merge Difference Item Types.");

        int total = other.count + count;

        if (total <= data.stackMaxCount)
        {
            count = total;
            other.count = 0;
            return;
        }
        count = data.stackMaxCount;
        other.count = total - count;

    }

    // Retourne si item peut se merge avec un autre Item
    public bool AvailableFor(ScItem other) => Empty || (Data == other.Data && !Full);

    public ScItemData Data => data;
    public bool Full => data && count >= data.stackMaxCount;

    public bool Empty => count == 0 || data == null;

    public int Count => count;

    // Essaye de futionner les stack 
    #endregion
}
