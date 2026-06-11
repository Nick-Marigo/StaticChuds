public class SkillTree
{
    public int skillPoints = 0;

    private PlayerInstance _owner;
    private (string affinity, string weakness) _types;

    int spellbranchCount = 0;
    int relicbranchCount = 0;
    int statsbranchCount = 0;

    public SkillTree(PlayerInstance owner) {
        this._owner = owner;
        _types.affinity = owner.PlayerClass.affinity;
        _types.weakness = owner.PlayerClass.weakness;
    }

    public Node CreateRoot()
    {
        Node newNode = new Node("Root", null, _types);
        return newNode;
    }

    public Node CreateNodeSpell()
    {
        spellbranchCount++;
        Node newNode = new Node("Spells", _owner.spellCaster, _types);
        return newNode.obj == null ? null : newNode;
    }

    public Node CreateNodeRelic()
    {
        relicbranchCount++;
        Node newNode = new Node("Relics", _owner.relicInventory, _types);
        return newNode.obj == null ? null : newNode;
    }

    public Node CreateNodeStat()
    {
        statsbranchCount++;
        Node newNode = new Node("Stats", _owner.AttributePackage, _types);
        return newNode.obj == null ? null : newNode;
    }
    
    public bool CanPurchase() {
        return skillPoints > 0;
    }
}
