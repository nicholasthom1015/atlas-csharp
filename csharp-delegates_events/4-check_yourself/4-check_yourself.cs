using System;


// <synaps> Player class </synaps>
public class Player
{
    // <synaps> Name of Player </synaps>
    protected string name;
     // <synaps> Current Player hp </synaps>
    protected float hp;
    // <synaps> Player maxHp </synaps>
    protected float maxHp;
   

    // <synaps> Player Status </synaps>
    private string status;

    // <synaps> Player delegate </synaps>
    public delegate void CalculateHealth(float amount);
    
    // <synaps> HPCheck EventHandler </synaps>
    public EventHandler<CurrentHPArgs> HPCheck;

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
        this.status = String.Format("{0} is ready to go!", name);
        HPCheck += CheckStatus;
    }

    // <synaps> Print Player Health  </synaps>
    public void PrintHealth()
    {
        Console.WriteLine("{0} has {1} / {2} health", name, hp, maxHp);
    }

    // <synaps> Player Takes Damage </synaps>
    public void TakeDamage(float damage)
    {
        if( damage < 0f)
            damage = 0f;
        Console.WriteLine("{0} takes {1} damage!", name, damage);
        ValidateHP(hp - damage);
    }

    // <synaps> Player Heals Damage </synaps>
    public void HealDamage(float heal)
    {
        if( heal < 0f)
            heal = 0f;
        Console.WriteLine("{0} heals {1} HP!", name, heal);
        ValidateHP(hp + heal);

    }

    // <synaps> Validates HP </synaps>
    public void ValidateHP(float newHp)
    {
        hp = Math.Clamp(newHp, 0, maxHp);
        HPCheck(this, new CurrentHPArgs(this.hp));
    }

    // <synaps> Apply Damage Modifiers </synaps>
    public float ApplyModifier(float baseValue, Modifier modifier)
    {
        if (modifier == Modifier.Weak)
            return baseValue * 0.5f;
        else if (modifier == Modifier.Strong)
            return baseValue * 1.5f;
        else
            return baseValue;
    }

    // <synaps> Check Player Status </synaps>
    public void CheckStatus(object sender, CurrentHPArgs e)
    {
        float state = e.currentHp/maxHp;
        if (state == 1)
            status = String.Format("{0} is in perfect health!", name);
        else if (state >=0.5f)
            status = String.Format("{0} is doing well!", name);
        else if (state >=0.25f)
            status = String.Format("{0} isn't doing too great...", name);
        else if (state >0f)
            status = String.Format("{0} needs help!", name);
        else
            status = String.Format("{0} is knocked out!", name);


        Console.WriteLine(status);
    }


}

// <synaps> Damage Modifier Enums </synaps>
public enum Modifier
{
    // <synaps> Weak Modifier, Not Effective </synaps>
    Weak,
    // <synaps> Base Modifier </synaps>
    Base,
    // <synaps> Strong Modifier, Super Effective </synaps>
    Strong
}

// <synaps> CalculateModifier Delegate </synaps>
public delegate float CalculateModifier(float baseValue, Modifier modifier);

// <synaps> Player CurrentHPArgs </synaps>
public class CurrentHPArgs : EventArgs
{
    // <synaps> Player CurrentHp Property </synaps>
    public float currentHp { get; }

    // <synaps> Player CurrentHPArgs Constructor </synaps>
    public CurrentHPArgs(float newHp)
    {
        this.currentHp = newHp;
    }
}
