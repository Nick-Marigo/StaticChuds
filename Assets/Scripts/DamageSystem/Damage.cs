using Newtonsoft.Json;
using UnityEngine;

public class Damage 
{
    public int amount;
    public Type type;

    public enum Type
    {
        PHYSICAL, ARCANE, NATURE, FIRE, ICE, DARK, LIGHT
    }

    /* If something is read into stringizedType via NewtonSoft, the type will
     * be set to that, otherwise, it will use whatever is passed to the constructor */
    public Damage(int amount, string type)
    {
        this.amount = amount;
        this.type = TypeFromString(type);
    }

    public static Type TypeFromString(string type)
    {
        string t = type.ToLower();
        if (t == "arcane") return Type.ARCANE;
        if (t == "nature") return Type.NATURE;
        if (t == "fire") return Type.FIRE;
        if (t == "ice") return Type.ICE;
        if (t == "dark") return Type.DARK;
        if (t == "light") return Type.LIGHT;
        return Type.PHYSICAL;
    }
}
