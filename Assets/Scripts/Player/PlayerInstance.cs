using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public PlayerEventWrapper eventWrapper { get; private set; }
    public SpellCaster spellCaster;
    public RelicInventory relicInventory;
    public SpellUI spellui;
    public SpellUIContainer spellUIContainer;
    public RelicUIManager relicUIManager;

    public bool isDead = false;

    private PlayerController _playerController;
    EntityAttributePackage _attributePackage;

    private Classes _playerClass;
    [SerializeField] GameObject playerVisual;

    void Start() {
        _attributePackage = GetComponent<EntityAttributePackage>();
        _playerController = GetComponent<PlayerController>();
        GameManager.Instance.player = gameObject;
    }

    public void InitPlayer(Classes className) {

        _playerClass = className;
        playerVisual.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerSpriteManager.Get(_playerClass.sprite);
        /*Debug.Log("Player received class: " + className);
        Debug.Log("HP expression: " + currentClass.health);
        Debug.Log("Mana expression: " + currentClass.mana);
        Debug.Log("Mana Regen expression: " + currentClass.mana_regeneration);
        Debug.Log("Spell Power expression: " + currentClass.spellpower);
        Debug.Log("Speed expression: " + currentClass.speed);*/

        hp = new Hittable(_playerClass.CalculateHP(GameManager.Instance.currentWave), Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        eventWrapper = new PlayerEventWrapper();
        spellCaster = new SpellCaster(_attributePackage, Hittable.Team.PLAYER);
        relicInventory = new RelicInventory(_attributePackage);

       _playerController.unit.unitMoved += eventWrapper.InvokePlayerMoved;
        /*
        //REMOVE: adds a test relic
        Relic relic = relicInventory.FetchUnusedRelic();
        relicInventory.EquipRelic(relic);
        relicUIManager.RefreshRelicUI();
        */
        
        // TODO break up this function
        

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellCaster);
        spellUIContainer.RefreshSpells(spellCaster.spells);
        spellui.SetSpell(spellCaster.GetSelectedSpell());

        ScaleStats(GameManager.Instance.currentWave);
        StartCoroutine(spellCaster.ManaRegeneration());
    }

    void Die()
    {
        Debug.Log("You Lost");
        isDead = true;
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }

    void OnEnable()
    {
        EventBus.Instance.OnWaveStart += ScaleStats;
        //EventBus.Instance.OnClassSelected += InitPlayer;
    }

    void OnDisable()
    {
        EventBus.Instance.OnWaveStart -= ScaleStats;
        //EventBus.Instance.OnClassSelected -= InitPlayer;
    }

    void ScaleStats(int wave) {

        int newHp = _playerClass.CalculateHP(wave);
        int newMana = _playerClass.CalculateMana(wave);
        int newManaRegen = _playerClass.CalculateManaRegeneration(wave);
        int newSpellPower = _playerClass.CalculateSpellPower(wave);
        _playerController.speed = _playerClass.CalculateSpeed(wave);

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

    void SelectSpell(int spellSelected)
    {
        spellCaster.SelectSpell(spellSelected);
        spellUIContainer.UpdateSelectedHighlight(spellCaster.selectedSpellIndex);
        Spell selected = spellCaster.GetSelectedSpell();
        
        //Debug.Log("Selected slot: " + spellSelected + " Spell modifier: " + selected.GetType().Name);
        
    }

    // Left here in case we add a system that can transition between players
    void _ProvideAttributePackage(iRequestAttributePackage requester) {
        Debug.Log($"Package given to{requester} is {_attributePackage}");
        requester.attributePackage = _attributePackage;
    }

}
