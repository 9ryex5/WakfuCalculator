using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    private Calculator C;
    private SaveData SD;

    [Header("Colors")]
    public Color redRemove;
    public Color grayButton;

    [Header("Sprites")]
    public Sprite spriteButton;
    public Sprite spriteButtonPressed;
    public Sprite spriteWater;
    public Sprite spriteEarth;
    public Sprite spriteWind;
    public Sprite spriteFire;

    [Header("Tabs")]
    public Image[] buttonsTabs;
    public GameObject[] panelsTabs;

    [Header("Character")]
    public InputField fieldWaterMastery;
    public InputField fieldEarthMastery;
    public InputField fieldWindMastery;
    public InputField fieldFireMastery;
    public InputField fieldDamageInflicted;
    public InputField fieldHealsPerformed;
    public InputField fieldCriticalHits;
    public InputField fieldCriticalMastery;
    public InputField fieldRearMastery;
    public InputField fieldMeleeMastery;
    public InputField fieldDistanceMastery;
    public InputField fieldSingleTargetMastery;
    public InputField fieldAreaMastery;
    public InputField fieldHealingMastery;
    public InputField fieldBerserkMastery;

    [Header("Load Character")]
    public GameObject panelLoadCharacter;
    public Transform parentCharacters;
    public GameObject prefabItemCharacter;
    public Image buttonRemoveCharacter;
    private bool isRemovingCharacter;

    [Header("Save Character")]
    public GameObject panelSaveCharacter;
    public InputField fieldSaveCharacterName;

    [Header("Target")]
    public InputField fieldWaterResist;
    public InputField fieldEarthResist;
    public InputField fieldWindResist;
    public InputField fieldFireResist;
    public Text textWaterResist;
    public Text textEarthResist;
    public Text textWindResist;
    public Text textFireResist;

    [Header("Load Target")]
    public GameObject panelLoadTarget;
    public Transform parentTargets;
    public GameObject prefabItemTarget;
    public Image buttonRemoveTarget;
    private bool isRemovingTarget;

    [Header("Save Target")]
    public GameObject panelSaveTarget;
    public InputField fieldSaveTargetName;

    [Header("Spell")]
    public Image buttonSpellElement;
    private Element spellElement;
    public Toggle toggleArea;
    public InputField fieldSpellAP;
    public InputField fieldSpellMP;
    public InputField fieldSpellWP;
    public InputField fieldSpellDmg;
    public InputField fieldSpellCritDmg;
    public InputField fieldSpellNRDmg;
    public InputField fieldSpellNRCritDmg;

    [Header("Compare Spells")]
    public GameObject panelCompareSpells;
    public Transform parentCompareSpells;
    public GameObject prefabItemSpellCompare;
    public Text textOrderSpells;
    private int orderSpells;

    [Header("Load Spell")]
    public GameObject panelLoadSpell;
    public Transform parentSpells;
    public GameObject prefabItemSpell;
    public Image buttonRemoveSpell;
    private bool isRemovingSpell;

    [Header("Save Spell")]
    public GameObject panelSaveSpell;
    public InputField fieldSaveSpellName;

    [Header("Positioning")]
    public Text textPositioning;

    [Header("Status")]
    public InputField fieldHealth;
    public InputField fieldExtraDmgInflicted;

    [Header("Formula")]
    public Text textMasteryMultiplier;
    public Text textPercentResist;
    public Text textDamage;

    [Header("Result")]
    public Text textResultDamage;
    public Text textResultCritDamage;
    public Text textResultAvgDmg;
    public Text textResultAvgDmgAP;

    private void Start()
    {
        C = GetComponent<Calculator>();
        SD = GetComponent<SaveData>();
        SelectTab(0);
        UpdateFormula();
    }

    public void SelectTab(int _tab)
    {
        for (int i = 0; i < buttonsTabs.Length; i++)
        {
            buttonsTabs[i].sprite = spriteButton;
            buttonsTabs[i].color = grayButton;
            panelsTabs[i].SetActive(false);
        }

        buttonsTabs[_tab].sprite = spriteButtonPressed;
        buttonsTabs[_tab].color = Color.white;
        panelsTabs[_tab].SetActive(true);
    }

    #region Character
    public void UpdateCharacter()
    {
        C.character = new Character
        {
            waterMastery = fieldWaterMastery.text == string.Empty ? 0 : int.Parse(fieldWaterMastery.text),
            earthMastery = fieldEarthMastery.text == string.Empty ? 0 : int.Parse(fieldEarthMastery.text),
            windMastery = fieldWindMastery.text == string.Empty ? 0 : int.Parse(fieldWindMastery.text),
            fireMastery = fieldFireMastery.text == string.Empty ? 0 : int.Parse(fieldFireMastery.text),
            damageInflicted = fieldDamageInflicted.text == string.Empty ? 0 : int.Parse(fieldDamageInflicted.text),
            healsPerformed = fieldHealsPerformed.text == string.Empty ? 0 : int.Parse(fieldHealsPerformed.text),
            criticalHits = fieldCriticalHits.text == string.Empty ? 0 : int.Parse(fieldCriticalHits.text),
            criticalMastery = fieldCriticalMastery.text == string.Empty ? 0 : int.Parse(fieldCriticalMastery.text),
            rearMastery = fieldRearMastery.text == string.Empty ? 0 : int.Parse(fieldRearMastery.text),
            meleeMastery = fieldMeleeMastery.text == string.Empty ? 0 : int.Parse(fieldMeleeMastery.text),
            distanceMastery = fieldDistanceMastery.text == string.Empty ? 0 : int.Parse(fieldDistanceMastery.text),
            singleTargetMastery = fieldSingleTargetMastery.text == string.Empty ? 0 : int.Parse(fieldSingleTargetMastery.text),
            areaMastery = fieldAreaMastery.text == string.Empty ? 0 : int.Parse(fieldAreaMastery.text),
            healingMastery = fieldHealingMastery.text == string.Empty ? 0 : int.Parse(fieldHealingMastery.text),
            berserkMastery = fieldBerserkMastery.text == string.Empty ? 0 : int.Parse(fieldBerserkMastery.text)
        };

        C.Calculate();
    }
    #endregion

    #region LoadCharacter
    public void OpenLoadCharacter()
    {
        for (int i = 0; i < parentCharacters.childCount; i++)
            Destroy(parentCharacters.GetChild(i).gameObject);

        for (int i = 0; i < SD.characters.Count; i++)
        {
            GameObject go = Instantiate(prefabItemCharacter, parentCharacters);
            string aux = SD.characters[i].name;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadCharacter(aux);
            });
            go.GetComponentInChildren<Text>().text = SD.characters[i].name;
        }

        isRemovingCharacter = false;
        buttonRemoveCharacter.color = grayButton;
        panelLoadCharacter.SetActive(true);
    }

    public void LoadCharacter(string _name)
    {
        if (isRemovingCharacter)
        {
            SD.RemoveCharacter(_name);
            for (int i = 0; i < parentCharacters.childCount; i++)
                if (parentCharacters.GetChild(i).GetComponentInChildren<Text>().text == _name)
                    Destroy(parentCharacters.GetChild(i).gameObject);
        }
        else
        {
            C.character = SD.LoadCharacter(_name);

            fieldWaterMastery.text = C.character.waterMastery.ToString();
            fieldEarthMastery.text = C.character.earthMastery.ToString();
            fieldWindMastery.text = C.character.windMastery.ToString();
            fieldFireMastery.text = C.character.fireMastery.ToString();
            fieldDamageInflicted.text = C.character.damageInflicted.ToString();
            fieldHealsPerformed.text = C.character.healsPerformed.ToString();
            fieldCriticalHits.text = C.character.criticalHits.ToString();
            fieldCriticalMastery.text = C.character.criticalMastery.ToString();
            fieldRearMastery.text = C.character.rearMastery.ToString();
            fieldMeleeMastery.text = C.character.meleeMastery.ToString();
            fieldDistanceMastery.text = C.character.distanceMastery.ToString();
            fieldSingleTargetMastery.text = C.character.singleTargetMastery.ToString();
            fieldAreaMastery.text = C.character.areaMastery.ToString();
            fieldHealingMastery.text = C.character.healingMastery.ToString();
            fieldBerserkMastery.text = C.character.berserkMastery.ToString();

            C.Calculate();

            panelLoadCharacter.SetActive(false);
        }
    }

    public void RemoveCharacter()
    {
        isRemovingCharacter = !isRemovingCharacter;

        buttonRemoveCharacter.color = isRemovingCharacter ? redRemove : grayButton;

        for (int i = 0; i < parentCharacters.childCount; i++)
            parentCharacters.GetChild(i).GetComponent<Image>().color = isRemovingCharacter ? redRemove : new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
    #endregion

    #region SaveCharacter
    public void OpenSaveCharacter()
    {
        fieldSaveCharacterName.text = string.Empty;
        panelSaveCharacter.SetActive(true);
    }

    public void SaveCharacter()
    {
        if (fieldSaveCharacterName.text == string.Empty)
            return;

        C.character.name = fieldSaveCharacterName.text;

        SD.AddCharacter(C.character);

        panelSaveCharacter.SetActive(false);
    }
    #endregion

    #region Target
    public void UpdateTarget()
    {
        C.target = new Target
        {
            waterResist = fieldWaterResist.text == string.Empty ? 0 : int.Parse(fieldWaterResist.text),
            earthResist = fieldEarthResist.text == string.Empty ? 0 : int.Parse(fieldEarthResist.text),
            windResist = fieldWindResist.text == string.Empty ? 0 : int.Parse(fieldWindResist.text),
            fireResist = fieldFireResist.text == string.Empty ? 0 : int.Parse(fieldFireResist.text)
        };

        C.Calculate();

        UpdatePercentResists();
    }

    private void UpdatePercentResists()
    {
        textWaterResist.text = C.CalculatePercentResist(C.target.waterResist) * 100 + "%";
        textEarthResist.text = C.CalculatePercentResist(C.target.earthResist) * 100 + "%";
        textWindResist.text = C.CalculatePercentResist(C.target.windResist) * 100 + "%";
        textFireResist.text = C.CalculatePercentResist(C.target.fireResist) * 100 + "%";
    }
    #endregion

    #region LoadTarget
    public void OpenLoadTarget()
    {
        for (int i = 0; i < parentTargets.childCount; i++)
            Destroy(parentTargets.GetChild(i).gameObject);

        for (int i = 0; i < SD.targets.Count; i++)
        {
            GameObject go = Instantiate(prefabItemTarget, parentTargets);
            Target t = SD.targets[i];
            string aux = t.name;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadTarget(aux);
            });

            go.GetComponent<ItemTarget>().StartThis(t.name, C.CalculatePercentResist(t.waterResist), C.CalculatePercentResist(t.earthResist), C.CalculatePercentResist(t.windResist), C.CalculatePercentResist(t.fireResist));
        }

        isRemovingTarget = false;
        buttonRemoveTarget.color = grayButton;
        panelLoadTarget.SetActive(true);
    }

    public void LoadTarget(string _name)
    {
        if (isRemovingTarget)
        {
            SD.RemoveTarget(_name);
            for (int i = 0; i < parentTargets.childCount; i++)
                if (parentTargets.GetChild(i).GetComponentInChildren<Text>().text == _name)
                    Destroy(parentTargets.GetChild(i).gameObject);
        }
        else
        {
            C.target = SD.LoadTarget(_name);

            fieldWaterResist.text = C.target.waterResist.ToString();
            fieldEarthResist.text = C.target.earthResist.ToString();
            fieldWindResist.text = C.target.windResist.ToString();
            fieldFireResist.text = C.target.fireResist.ToString();

            C.Calculate();

            UpdatePercentResists();

            panelLoadTarget.SetActive(false);
        }
    }

    public void RemoveTarget()
    {
        isRemovingTarget = !isRemovingTarget;

        buttonRemoveTarget.color = isRemovingTarget ? redRemove : grayButton;

        for (int i = 0; i < parentTargets.childCount; i++)
            parentTargets.GetChild(i).GetComponent<Image>().color = isRemovingTarget ? redRemove : new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
    #endregion

    #region SaveTarget
    public void OpenSaveTarget()
    {
        panelSaveTarget.SetActive(true);
        fieldSaveTargetName.text = string.Empty;
    }

    public void SaveTarget()
    {
        if (fieldSaveTargetName.text == string.Empty)
            return;

        C.target.name = fieldSaveTargetName.text;

        SD.AddTarget(C.target);

        panelSaveTarget.SetActive(false);
    }
    #endregion

    #region Spell
    public void UpdateSpell()
    {
        if (panelLoadSpell.activeSelf) //Avoid toggleArea to trigger this
            return;

        C.spell = new Spell
        {
            element = spellElement,
            area = toggleArea.isOn,
            ap = fieldSpellAP.text == string.Empty ? 0 : int.Parse(fieldSpellAP.text),
            mp = fieldSpellMP.text == string.Empty ? 0 : int.Parse(fieldSpellMP.text),
            wp = fieldSpellWP.text == string.Empty ? 0 : int.Parse(fieldSpellWP.text),
            damage = fieldSpellDmg.text == string.Empty ? 0 : int.Parse(fieldSpellDmg.text),
            critDamage = fieldSpellCritDmg.text == string.Empty ? 0 : int.Parse(fieldSpellCritDmg.text),
            NRDamage = fieldSpellNRDmg.text == string.Empty ? 0 : int.Parse(fieldSpellNRDmg.text),
            NRCritDamage = fieldSpellNRCritDmg.text == string.Empty ? 0 : int.Parse(fieldSpellNRCritDmg.text)
        };

        C.Calculate();
    }

    public void NextElement()
    {
        switch (spellElement)
        {
            case Element.WATER:
                spellElement = Element.EARTH;
                break;
            case Element.EARTH:
                spellElement = Element.WIND;
                break;
            case Element.WIND:
                spellElement = Element.FIRE;
                break;
            case Element.FIRE:
                spellElement = Element.WATER;
                break;
        }

        UpdateElementButton();
        UpdateSpell();
    }

    private void UpdateElementButton()
    {
        switch (spellElement)
        {
            case Element.WATER:
                buttonSpellElement.sprite = spriteWater;
                break;
            case Element.EARTH:
                buttonSpellElement.sprite = spriteEarth;
                break;
            case Element.WIND:
                buttonSpellElement.sprite = spriteWind;
                break;
            case Element.FIRE:
                buttonSpellElement.sprite = spriteFire;
                break;
        }
    }

    public void OpenCompareSpells(int _order)
    {
        panelCompareSpells.SetActive(true);

        orderSpells = _order;

        List<SpellComparable> spells = new List<SpellComparable>();

        switch (_order)
        {
            case 0:
                textOrderSpells.text = "Compare by: Damage";
                break;
            case 1:
                textOrderSpells.text = "Compare by: Crit Damage";
                break;
            case 2:
                textOrderSpells.text = "Compare by: Avg Damage";
                break;
            case 3:
                textOrderSpells.text = "Compare by: Avg Damage/AP";
                break;
        }

        Spell auxSpell = C.spell;

        for (int i = 0; i < parentCompareSpells.childCount; i++)
            Destroy(parentCompareSpells.GetChild(i).gameObject);

        for (int i = 0; i < SD.spells.Count; i++)
        {
            C.spell = SD.spells[i];
            C.Calculate();

            SpellComparable sc = new SpellComparable
            {
                spell = C.spell,
                damage = C.result.damage,
                critDamage = C.result.critDamage,
                avgDamage = C.result.avgDamage,
                avgDamageAP = C.result.avgDamageAP
            };

            spells.Add(sc);
        }

        switch (_order)
        {
            case 0:
                spells = new List<SpellComparable>(spells.OrderByDescending(s => s.damage));
                break;
            case 1:
                spells = new List<SpellComparable>(spells.OrderByDescending(s => s.critDamage));
                break;
            case 2:
                spells = new List<SpellComparable>(spells.OrderByDescending(s => s.avgDamage));
                break;
            case 3:
                spells = new List<SpellComparable>(spells.OrderByDescending(s => s.avgDamageAP));
                break;
        }

        for (int i = 0; i < spells.Count; i++)
        {
            GameObject go = Instantiate(prefabItemSpellCompare, parentCompareSpells);
            switch (_order)
            {
                case 0:
                    go.GetComponent<ItemSpell>().StartThis(spells[i].spell, spells[i].damage);
                    break;
                case 1:
                    go.GetComponent<ItemSpell>().StartThis(spells[i].spell, spells[i].critDamage);
                    break;
                case 2:
                    go.GetComponent<ItemSpell>().StartThis(spells[i].spell, spells[i].avgDamage);
                    break;
                case 3:
                    go.GetComponent<ItemSpell>().StartThis(spells[i].spell, spells[i].avgDamageAP);
                    break;
            }
        }

        C.spell = auxSpell;
        C.Calculate();
    }

    public void NextOrderSpells()
    {
        orderSpells++;
        if (orderSpells >= 4) orderSpells = 0;
        OpenCompareSpells(orderSpells);
    }
    #endregion

    #region LoadSpell
    public void OpenLoadSpell()
    {
        for (int i = 0; i < parentSpells.childCount; i++)
            Destroy(parentSpells.GetChild(i).gameObject);

        for (int i = 0; i < SD.spells.Count; i++)
        {
            GameObject go = Instantiate(prefabItemSpell, parentSpells);
            string aux = SD.spells[i].name;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadSpell(aux);
            });

            go.GetComponent<ItemSpell>().StartThis(SD.spells[i], 0);
        }

        isRemovingSpell = false;
        buttonRemoveSpell.color = grayButton;
        panelLoadSpell.SetActive(true);
    }

    public void LoadSpell(string _name)
    {
        if (isRemovingSpell)
        {
            SD.RemoveSpell(_name);
            for (int i = 0; i < parentSpells.childCount; i++)
                if (parentSpells.GetChild(i).GetComponentInChildren<Text>().text == _name)
                    Destroy(parentSpells.GetChild(i).gameObject);
        }
        else
        {
            C.spell = SD.LoadSpell(_name);

            spellElement = C.spell.element;
            UpdateElementButton();
            fieldSpellAP.text = C.spell.ap.ToString();
            fieldSpellMP.text = C.spell.mp.ToString();
            fieldSpellWP.text = C.spell.wp.ToString();
            toggleArea.isOn = C.spell.area;
            fieldSpellDmg.text = C.spell.damage.ToString();
            fieldSpellCritDmg.text = C.spell.critDamage.ToString();
            fieldSpellNRDmg.text = C.spell.NRDamage.ToString();
            fieldSpellNRCritDmg.text = C.spell.NRCritDamage.ToString();

            C.Calculate();

            panelLoadSpell.SetActive(false);
        }
    }

    public void RemoveSpell()
    {
        isRemovingSpell = !isRemovingSpell;

        buttonRemoveSpell.color = isRemovingSpell ? redRemove : grayButton;

        for (int i = 0; i < parentSpells.childCount; i++)
            parentSpells.GetChild(i).GetComponent<Image>().color = isRemovingSpell ? redRemove : new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
    #endregion

    #region SaveSpell
    public void OpenSaveSpell()
    {
        panelSaveSpell.SetActive(true);
        fieldSaveSpellName.text = string.Empty;
    }

    public void SaveSpell()
    {
        if (fieldSaveSpellName.text == string.Empty)
            return;

        C.spell.name = fieldSaveSpellName.text;

        SD.AddSpell(C.spell);

        panelSaveSpell.SetActive(false);
    }
    #endregion

    #region Positioning
    public void UpdatePositioning(int _index)
    {
        Positioning p = new Positioning();

        p.isDistance = _index > 3;

        switch (_index)
        {
            case 0:
            case 4:
                p.dmgMultiplier = 0;
                textPositioning.text = "Front";
                break;
            case 3:
            case 7:
                p.isRear = true;
                p.dmgMultiplier = 0.25f;
                textPositioning.text = "Rear";
                break;
            default:
                p.dmgMultiplier = 0.1f;
                textPositioning.text = "Side";
                break;
        }

        textPositioning.text += p.isDistance ? " Distance" : " Melee";

        C.positioning = p;
        C.Calculate();
    }
    #endregion

    #region Status
    public void UpdateStatus()
    {
        Status s = new Status
        {
            health = fieldHealth.text == string.Empty ? 100 : int.Parse(fieldHealth.text),
            extraDmgInflicted = fieldExtraDmgInflicted.text == string.Empty ? 0 : int.Parse(fieldExtraDmgInflicted.text),
        };

        C.status = s;
        C.Calculate();
    }
    #endregion

    #region Result
    public void UpdateResult(Result _r)
    {
        textResultDamage.text = _r.damage.ToString();
        textResultCritDamage.text = _r.critDamage.ToString();
        textResultAvgDmg.text = _r.avgDamage.ToString();
        textResultAvgDmgAP.text = _r.avgDamageAP.ToString();

        UpdateFormula();
    }
    #endregion

    #region Formula
    public void UpdateFormula()
    {
        string element = string.Empty;
        string relPosition = string.Empty;

        switch (C.spell.element)
        {
            case Element.WATER:
                element = "Water";
                break;
            case Element.EARTH:
                element = "Earth";
                break;
            case Element.WIND:
                element = "Wind";
                break;
            case Element.FIRE:
                element = "Fire";
                break;
        }

        switch (C.positioning.dmgMultiplier)
        {
            case 0:
                relPosition = "Front";
                break;
            case 0.1f:
                relPosition = "Side";
                break;
            case 0.25f:
                relPosition = "Rear";
                break;
        }

        textMasteryMultiplier.text = "MasteryMultiplier = (" + element + "Mastery" + " + " +
            (C.spell.area ? "AreaMastery" : "SingleTMastery") + " + " +
            (C.positioning.isDistance ? "DistanceMastery" : "MeleeMastery") +
            (C.positioning.isRear ? " + RearMastery" : string.Empty) +
            (C.status.health <= 50 ? " + BerserkMastery" : string.Empty) + ") / 100 + 1";

        textPercentResist.text = "PercentResist = 1 - 0.8 ^ (" + element + "Resist / 100)";

        textDamage.text = "Damage = SpellDamage * MasteryMultiplier \n" +
            " * (1 - PercentResist) * " + relPosition + "Bonus [" + (1 + C.positioning.dmgMultiplier) + "] \n" +
            " * (1 + DamageInflicted + ExtraDamageInflicted)";
    }
    #endregion
}