using System;


// <summary> Player class </summary>
public class Player
{
    // <summary> Name of Player </summary>
    protected string name;
    // <summary> Maxhp of Player </summary>
    protected float maxHp;
    // <summary> Current of Player </summary>
    protected float hp;

    // <summary> Player delegate </summary>
    public delegate void CalculateHealth(float amount);

    // <summary> Player Builder </summary>
    public Player(string name="Player", float maxHp=100f)
    {
        this.name = name;
        if( maxHp <= 0f){
            Console.WriteLine("maxHp must be greater than 0. maxHp set to 100f by default.");
            maxHp = 100f;
        }
        this.maxHp = maxHp;
        this.hp = this.maxHp;
    }

    // <summary> PrintHealth </summary>
    public void PrintHealth()
    {
        Console.WriteLine("{0} has {1} / {2} health", name, hp, maxHp);
    }

    // <summary> Player takes Damage </summary>
    public void TakeDamage(float damage)
    {
        if( damage < 0f)
            damage = 0f;
        Console.WriteLine("{0} takes {1} damage!", name, damage);
        ValidateHP(hp - damage);
    }

    // <summary> Player is Healed </summary>
    public void HealDamage(float heal)
    {
        if( heal < 0f)
            heal = 0f;
        Console.WriteLine("{0} heals {1} HP!", name, heal);
        ValidateHP(hp + heal);

    }

    // <summary> Validates HP</summary>
    public void ValidateHP(float newHp)
    {
        hp = Math.Clamp(newHp, 0, maxHp);
    }

    // <summary> ApplyModifier </summary>
    public float ApplyModifier(float baseValue, Modifier modifier)
    {
        if (modifier == Modifier.Weak)
            return baseValue * 0.5f;
        else if (modifier == Modifier.Strong)
            return baseValue * 1.5f;
        else
            return baseValue;
    }
}

// <summary> Modifier Enum </summary>
public enum Modifier
{
    // <summary> Weak Modifier </summary>
    Weak,
    // <summary> Base Modifier </summary>
    Base,
    // <summary> Strong Modifier </summary>
    Strong
}

// <summary> CalculateModifier Delegate </summary>
public delegate float CalculateModifier(float baseValue, Modifier modifier);
