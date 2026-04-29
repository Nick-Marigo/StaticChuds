using System.Collections.Generic;

[System.Serializable]
public class Spawn {
    public string enemy; 
    public string count;
    public string hp = "base";
    public int delay = 2;
    public int[] sequence = {1};
    public string location = "random";
    public string speed = "base";
    public string damage = "base";

    public int CalculateHP(int enemyHP, int wave) {
        return RPNEvaluator.RPNEvaluator.Evaluate(hp, 
                new Dictionary <string, int> { {"base", enemyHP}, {"wave", wave}} );
    }

    public int CalculateSpawnCount(int enemyHP, int wave) {
        return RPNEvaluator.RPNEvaluator.Evaluate(count, 
                new Dictionary <string, int> { {"base", enemyHP}, {"wave", wave}} );
    }

    public int CalculateSpeed(int enemySpeed, int wave) {
        return RPNEvaluator.RPNEvaluator.Evaluate(speed, 
                new Dictionary <string, int> { {"base", enemySpeed}, {"wave", wave}} );
    }

    public int CalculateDamage(int enemyDmg, int wave) {
        return RPNEvaluator.RPNEvaluator.Evaluate(damage, 
                new Dictionary <string, int> { {"base", enemyDmg}, {"wave", wave}} );
    }
}
