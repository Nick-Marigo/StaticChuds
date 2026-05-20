using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public SpellUI spellui;
    public SpellUIContainer spellUIContainer;
    public int speed;

    public Unit unit;

    public bool isDead = false;

    public Vector3 position{get { return transform.position; }}

    private Dictionary<string, Classes> classes;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        classes = ClassesLoader.GetClasses();
        InitPlayer();
        // REMOVE
        Relic r = RelicLoader.Relics["Green Gem"]();
        //r.Owner = gameObject;
    }

    public void InitPlayer()
    {
        hp = new Hittable(100, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        spellcaster = new SpellCaster(Hittable.Team.PLAYER);
        ScaleStats(GameManager.Instance.currentWave);
        StartCoroutine(spellcaster.ManaRegeneration());

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellUIContainer.RefreshSpells(spellcaster.spells);
        spellui.SetSpell(spellcaster.GetSelectedSpell());
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
        unit.movement = value.Get<Vector2>()*speed;
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
    }

    void OnDisable()
    {
        EventBus.Instance.OnWaveStart -= ScaleStats;
    }

    void ScaleStats(int wave)
    {
        Dictionary<string, int> variables = new Dictionary<string, int>
        {
            { "wave", wave }
        };

        int newHp = RPNEvaluator.RPNEvaluator.Evaluate("95 wave 5 * +", variables);
        int newMana = RPNEvaluator.RPNEvaluator.Evaluate("90 wave 10 * +", variables);
        int newManaRegen = RPNEvaluator.RPNEvaluator.Evaluate("10 wave +", variables);
        int newSpellPower = RPNEvaluator.RPNEvaluator.Evaluate("wave 10 *", variables);
        this.speed = RPNEvaluator.RPNEvaluator.Evaluate("5", variables);                  // Assignment says to do this but what is the point?

        hp.SetMaxHP(newHp);
        spellcaster.SetStats(newMana, newManaRegen, newSpellPower);
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
        
        Debug.Log("Selected slot: " + spellSelected + " Spell modifier: " + selected.GetType().Name);
        
    }

}
