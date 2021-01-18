public enum Element { WATER, EARTH, WIND, FIRE }

public struct SpellComparable
{
    public Spell spell;
    public int damage;
    public int critDamage;
    public int avgDamage;
    public int avgDamageAP;
}

public struct Positioning
{
    public bool isDistance;
    public bool isRear;
    public float dmgMultiplier;
}

public struct Status
{
    public int health;
    public int extraDmgInflicted;
}

public struct Result
{
    public int damage;
    public int critDamage;
    public int avgDamage;
    public int avgDamageAP;
}