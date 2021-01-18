using UnityEngine;

public class Calculator : MonoBehaviour
{
    private ManagerUI MUI;
    private SaveData SD;

    [HideInInspector]
    public Character character;
    [HideInInspector]
    public Target target;
    [HideInInspector]
    public Spell spell;
    [HideInInspector]
    public Positioning positioning;
    [HideInInspector]
    public Status status;
    [HideInInspector]
    public Result result;

    private void Start()
    {
        MUI = GetComponent<ManagerUI>();
        SD = GetComponent<SaveData>();

        status.health = 100;
    }

    public void Calculate()
    {
        int elementMastery = 0;
        int elementResist = 0;

        switch (spell.element)
        {
            case Element.WATER:
                elementMastery = character.waterMastery;
                elementResist = target.waterResist;
                break;
            case Element.EARTH:
                elementMastery = character.earthMastery;
                elementResist = target.earthResist;
                break;
            case Element.WIND:
                elementMastery = character.windMastery;
                elementResist = target.windResist;
                break;
            case Element.FIRE:
                elementMastery = character.fireMastery;
                elementResist = target.fireResist;
                break;
        }

        float percentResist = CalculatePercentResist(elementResist);
        float totalDmgInflicted = 1 + (character.damageInflicted + status.extraDmgInflicted) / 100;

        result.damage = Mathf.RoundToInt(spell.damage * CalculateMasteryMultiplier(elementMastery, false, spell.area, positioning.isDistance, positioning.isRear) * (1 + positioning.dmgMultiplier) * (1 - percentResist) * totalDmgInflicted);
        result.critDamage = Mathf.RoundToInt(spell.critDamage * CalculateMasteryMultiplier(elementMastery, true, spell.area, positioning.isDistance, positioning.isRear) * (1 + positioning.dmgMultiplier) * (1 - percentResist) * totalDmgInflicted);
        result.avgDamage = Mathf.RoundToInt(result.damage + character.criticalHits / 100 * (result.critDamage - result.damage));
        result.avgDamageAP = spell.ap == 0 ? 0 : Mathf.RoundToInt(result.avgDamage / spell.ap);

        MUI.UpdateResult(result);
    }

    public float CalculatePercentResist(int _resist)
    {
        return Mathf.Floor((1 - Mathf.Pow(0.8f, (float)_resist / 100)) * 100) / 100;
    }

    private float CalculateMasteryMultiplier(int _elementMastery, bool _crit, bool _area, bool _distance, bool _rear)
    {
        float masteryMultiplier = _elementMastery;
        masteryMultiplier += _crit ? character.criticalMastery : 0;
        masteryMultiplier += _area ? character.areaMastery : character.singleTargetMastery;
        masteryMultiplier += _distance ? character.distanceMastery : character.meleeMastery;
        masteryMultiplier += _rear ? character.rearMastery : 0;
        masteryMultiplier += status.health <= 50 ? character.berserkMastery : 0;
        return 1 + masteryMultiplier / 100;
    }
}