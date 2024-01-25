using System;


// <synaps> Player class </synaps>
public class Player
{
    // <synaps> Name of Player </synaps>
    protected string name;
    // <synaps> Maxhp of Player </synaps>
    protected float maxHp;
    // <synaps> Current of Player </synaps>
    protected float hp;

    // <synaps> Player delegate </synaps>
    public delegate void CalculateHealth(float amount);

    // <synaps> Player Builder </synaps>
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

    // <synaps> PrintHealth </synaps>
    public void PrintHealth()
    {
        Console.WriteLine("{0} has {1} / {2} health", name, hp, maxHp);
    }

    // <synaps> Player takes Damage </synaps>
    public void TakeDamage(float damage)
    {
        if( damage < 0f)
            damage = 0f;
        Console.WriteLine("{0} takes {1} damage!", name, damage);
        ValidateHP(hp - damage);
    }

    // <synaps> Player is Healed </synaps>
    public void HealDamage(float heal)
    {
        if( heal < 0f)
            heal = 0f;
        Console.WriteLine("{0} heals {1} HP!", name, heal);
        ValidateHP(hp + heal);

    }

    // <synaps> Validates HP</synaps>
    public void ValidateHP(float newHp)
    {
        hp = Math.Clamp(newHp, 0, maxHp);
    }

    // <synaps> ApplyModifier </synaps>
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

// <synaps> Modifier Enum </synaps>
public enum Modifier
{
    // <synaps> Weak Modifier </synaps>
    Weak,
    // <synaps> Base Modifier </synaps>
    Base,
    // <synaps> Strong Modifier </synaps>
    Strong
}

// <synaps> CalculateModifier Delegate </synaps>
public delegate float CalculateModifier(float baseValue, Modifier modifier);
