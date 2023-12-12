using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


[Serializable]
public class ScCharactereStats
{
    public float baseValue;


    private readonly List<ScStatsModifier> statsModifiers;
    public readonly ReadOnlyCollection<ScStatsModifier> StatsModifiers;

    public float Value {
     get{
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                valueSc = CalculatedFinalValue();
                isDirty = false;
            }
            return valueSc;
        }
    }
    private bool isDirty = true;
    private float valueSc;
    private float lastBaseValue = float.MinValue;

    public ScCharactereStats()
    {
        statsModifiers = new List<ScStatsModifier>();
        StatsModifiers = statsModifiers.AsReadOnly();
    }

    #region Modifier Remove and Add
    public ScCharactereStats(float BaseValue) : this()
    {
        baseValue = BaseValue;
    }

    public void AddModifier(ScStatsModifier modifier)
    {
        isDirty = true;
        statsModifiers.Add(modifier);
        statsModifiers.Sort(CompareModifierOrder);
    }

    public bool RemoveModifier(ScStatsModifier modifier)
    {
        if (statsModifiers.Remove(modifier))
        {
            isDirty = true ; 
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemoving = false;  

        for(int i = statsModifiers.Count - 1; i >= 0 ; i--)
        {
            if (statsModifiers[i].source == source)
            {
                isDirty = true;
                didRemoving = true;
                statsModifiers.RemoveAt(i);
            }
        }
        return didRemoving;
    }

    private int CompareModifierOrder(ScStatsModifier modifier1, ScStatsModifier modifier2)
    {
        if(modifier1.order < modifier2.order) {return -1;}
        else if(modifier1.order > modifier2.order) { return 1;}
        return 0;
    }
    #endregion
    #region Calculated Modifier
    private float CalculatedFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statsModifiers.Count; i++)
        {
            ScStatsModifier mod = statsModifiers[i];

            if(mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
                if(i + 1 >= statsModifiers.Count || statsModifiers[i + 1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }            
            else if (mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;
            }
            else if (mod.type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }


        }

        return (float)Math.Round(finalValue, 4);
    }
    #endregion
}
