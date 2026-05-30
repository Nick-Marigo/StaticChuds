using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public PlayerEventWrapper eventWrapper { get; private set; }
    public SpellCaster spellcaster;
    public RelicInventory relicInventory;
    public SpellUI spellui;
    public SpellUIContainer spellUIContainer;
    public RelicUIManager relicUIManager;
    public int speed;
    private Vector2 movement;

    public Unit unit;

    public bool isDead = false;

    public Vector3 position{get { return transform.position; }}

    EntityAttributePackage _attributePackage;

    Classes currentClass;
    [SerializeField] GameObject playerVisual;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _attributePackage = gameObject.GetComponent<EntityAttributePackage>();
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
    }

    public void InitPlayer(Classes className)
    {

        currentClass = className;
        playerVisual.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerSpriteManager.Get(currentClass.sprite);
        /*Debug.Log("Player received class: " + className);
        Debug.Log("HP expression: " + currentClass.health);
        Debug.Log("Mana expression: " + currentClass.mana);
        Debug.Log("Mana Regen expression: " + currentClass.mana_regeneration);
        Debug.Log("Spell Power expression: " + currentClass.spellpower);
        Debug.Log("Speed expression: " + currentClass.speed);*/

        hp = new Hittable(currentClass.CalculateHP(GameManager.Instance.currentWave), Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        eventWrapper = new PlayerEventWrapper();
        spellcaster = new SpellCaster(_attributePackage, Hittable.Team.PLAYER);
        relicInventory = new RelicInventory(_attributePackage);
        /*
        //REMOVE: adds a test relic
        Relic relic = relicInventory.FetchUnusedRelic();
        relicInventory.EquipRelic(relic);
        relicUIManager.RefreshRelicUI();
        */
        
        // TODO break up this function
        unit.unitMoved += eventWrapper.InvokePlayerMoved;
        

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellUIContainer.RefreshSpells(spellcaster.spells);
        spellui.SetSpell(spellcaster.GetSelectedSpell());

        ScaleStats(GameManager.Instance.currentWave);
        StartCoroutine(spellcaster.ManaRegeneration());
    }

    void OnAttack(InputValue value)
    {
        GameManager.GameState gameState = GameManager.Instance.state;
        if (gameState == GameManager.GameState.PREGAME || gameState == GameManager.GameState.GAMEOVER || gameState == GameManager.GameState.WAVEEND) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        movement = value.Get<Vector2>();
    }

    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.INWAVE && GameManager.Instance.state != GameManager.GameState.COUNTDOWN)
        {
            unit.movement = Vector2.zero;
        }
        else
        {
            unit.movement = movement * speed;
        }
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

    void ScaleStats(int wave)
    {

        int newHp = currentClass.CalculateHP(wave);
        int newMana = currentClass.CalculateMana(wave);
        int newManaRegen = currentClass.CalculateManaRegeneration(wave);
        int newSpellPower = currentClass.CalculateSpellPower(wave);
        this.speed = currentClass.CalculateSpeed(wave);

        hp.SetMaxHP(newHp);
        spellcaster.SetStats(newMana, newManaRegen, newSpellPower);

        /*Debug.Log("Play scaling stats");
        Debug.Log("Wave: " + wave);
        Debug.Log("HP: " + newHp);
        Debug.Log("Mana: " + newMana);
        Debug.Log("Mana Regen: " + newManaRegen);
        Debug.Log("Spell Power: " + newSpellPower);
        Debug.Log("Speed: " + this.speed);*/
    }

    void OnSpell1() => SelectSpell(0);
    void OnSpell2() => SelectSpell(1);
    void OnSpell3() => SelectSpell(2);
    void OnSpell4() => SelectSpell(3);
    void SelectSpell(int spellSelected)
    {
        spellcaster.SelectSpell(spellSelected);
        spellUIContainer.UpdateSelectedHighlight(spellcaster.selectedSpellIndex);
        Spell selected = spellcaster.GetSelectedSpell();
        
        //Debug.Log("Selected slot: " + spellSelected + " Spell modifier: " + selected.GetType().Name);
        
    }

    void _ProvideAttributePackage(iRequestAttributePackage requester) {
        Debug.Log($"Package given to{requester} is {_attributePackage}");
        requester.attributePackage = _attributePackage;
    }

}
