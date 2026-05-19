using System.Collections.Generic;

[System.Serializable]
public class Classes {
    public int sprite;
    public string health;
    public string mana;
    public string mana_regeneration;
    public string spellpower;
    public string speed;

    public int CalculateHP(int wave)
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(health, new Dictionary<string, int> { {"wave", wave} });
    }

    public int CalculateMana(int wave)
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(mana, new Dictionary<string, int> { {"wave", wave} });
    }

    public int CalculateManaRegeneration(int wave)
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(mana_regeneration, new Dictionary<string, int> { {"wave", wave} });
    }

    public int CalculateSpellPower(int wave)
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(spellpower, new Dictionary<string, int> { {"wave", wave} });
    }

    public int CalculateSpeed(int wave)
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(speed, new Dictionary<string, int> { {"wave", wave} });
    }
}
