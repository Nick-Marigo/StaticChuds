using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public PlayerEventWrapper eventWrapper;
    public SpellCaster spellCaster;
    public RelicInventory relicInventory;
    public SpellUI spellui;
    public SpellUIContainer spellUIContainer;
    public RelicUIManager relicUIManager;

    public bool isDead = false;

    public PlayerController PlayerController { get; private set; }
    public EntityAttributePackage AttributePackage { get; private set; }

    public Classes PlayerClass { get; private set; }
    [SerializeField] GameObject playerVisual;

    void Start() {
        AttributePackage = GetComponent<EntityAttributePackage>();
        PlayerController = GetComponent<PlayerController>();
        GameManager.Instance.player = gameObject;
    }

    public void InitPlayer(Classes playerClass) {

        PlayerClass = playerClass;
        playerVisual.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerSpriteManager.Get(PlayerClass.sprite);

        PlayerInitializer playerInit = new PlayerInitializer(this);
        playerInit
            .SetEventWrapper()
            .SetHP()
            .SetSpellCaster()
            .SetUIElements();

        ScaleStats(GameManager.Instance.currentWave);
    }

    void OnEnable() {
        EventBus.Instance.OnWaveStart += ScaleStats;
    }

    void OnDisable() {
        EventBus.Instance.OnWaveStart -= ScaleStats;
        spellCaster.Dispose();
    }

    void ScaleStats(int wave) {

        int newHp = PlayerClass.CalculateHP(wave);
        int newMana = PlayerClass.CalculateMana(wave);
        int newManaRegen = PlayerClass.CalculateManaRegeneration(wave);
        int newSpellPower = PlayerClass.CalculateSpellPower(wave);
        PlayerController.speed = PlayerClass.CalculateSpeed(wave);

        hp.SetMaxHP(newHp);
        spellCaster.SetStats(newMana, newManaRegen, newSpellPower);

        /*Debug.Log("Play scaling stats");
        Debug.Log("Wave: " + wave);
        Debug.Log("HP: " + newHp);
        Debug.Log("Mana: " + newMana);
        Debug.Log("Mana Regen: " + newManaRegen);
        Debug.Log("Spell Power: " + newSpellPower);
        Debug.Log("Speed: " + this.speed);*/
    }

    // Left here in case we add a system that can transition between players
    void _ProvideAttributePackage(iRequestAttributePackage requester) {
        Debug.Log($"Package given to{requester} is {AttributePackage}");
        requester.attributePackage = AttributePackage;
    }
}
