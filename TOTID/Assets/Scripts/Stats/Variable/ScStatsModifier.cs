
public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}



public class ScStatsModifier 
{
    public readonly float value;
    public readonly StatModType type;
    public readonly int order;
    public readonly object source;


    public ScStatsModifier(float Value, StatModType Type, int Order, object Source)
    {
        value = Value;
        type = Type;
        order = Order;
        source = Source;
    }

    public ScStatsModifier(float Value, StatModType Type) : this (Value, Type, (int)Type , null) { }
    public ScStatsModifier(float Value, StatModType Type, int Order) : this (Value, Type, Order , null) { }
    public ScStatsModifier(float Value, StatModType Type, object Source) : this (Value, Type, (int)Type , Source) { }
}
