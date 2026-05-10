using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public abstract class Spell 
{
    [JsonProperty]
    protected string name;
    [JsonProperty]
    protected string description;

    public float last_cast;
    public SpellCaster owner;
    public Hittable.Team team;

    public string GetName()
    {
        return name;
    }

    public virtual int GetDamage() {
        return -1;
    }

    public int GetManaCost()
    {
        return 10;
    }

    public float GetCooldown()
    {
        return 0.75f;
    }

    public virtual int GetIcon()
    {
        return 0;
    }

    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }
    
   public virtual IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        yield return new WaitForEndOfFrame();
    }
}
