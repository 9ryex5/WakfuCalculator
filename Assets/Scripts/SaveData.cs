using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    [HideInInspector]
    public List<Character> characters;
    [HideInInspector]
    public List<Target> targets;
    [HideInInspector]
    public List<Spell> spells;

    public void Awake()
    {
        LoadEverything();
    }

    private void LoadEverything()
    {
        if (File.Exists(Application.persistentDataPath + "/CharacterData.cd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/CharacterData.cd", FileMode.Open);
            CharacterData data = (CharacterData)bf.Deserialize(file);
            file.Close();

            characters = new List<Character>(data.characters);
        }
        else
        {
            characters = new List<Character>();
        }

        if (File.Exists(Application.persistentDataPath + "/TargetData.td"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/TargetData.td", FileMode.Open);
            TargetData data = (TargetData)bf.Deserialize(file);
            file.Close();

            targets = new List<Target>(data.targets);
        }
        else
        {
            targets = new List<Target>();
        }

        if (File.Exists(Application.persistentDataPath + "/SpellData.sd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SpellData.sd", FileMode.Open);
            SpellData data = (SpellData)bf.Deserialize(file);
            file.Close();

            spells = new List<Spell>(data.spells);
        }
        else
        {
            spells = new List<Spell>();
        }
    }

    public void AddCharacter(Character _build)
    {
        int buildIndex = FindBuild(_build.name);

        if (buildIndex == -1)
            characters.Add(_build);
        else
            characters[buildIndex] = _build;

        SaveCharacters();
    }

    public void SaveCharacters()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/CharacterData.cd");
        CharacterData data = new CharacterData();

        data.characters = characters;
        bf.Serialize(file, data);
        file.Close();
    }

    private int FindBuild(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
            if (characters[i].name == _name)
                return i;

        return -1;
    }

    public Character LoadCharacter(string _characterName)
    {
        if (File.Exists(Application.persistentDataPath + "/CharacterData.cd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/CharacterData.cd", FileMode.Open);
            CharacterData data = (CharacterData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < characters.Count; i++)
                if (characters[i].name == _characterName)
                    return data.characters[i];
        }

        Debug.LogWarning("Cannot find save file");
        return null;
    }

    public void RemoveCharacter(string _name)
    {
        characters.RemoveAt(FindBuild(_name));
        SaveCharacters();
    }

    public void AddTarget(Target _target)
    {
        int targetIndex = FindTarget(_target.name);

        if (targetIndex == -1)
            targets.Add(_target);
        else
            targets[targetIndex] = _target;

        SaveTargets();
    }

    public void SaveTargets()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/TargetData.td");
        TargetData data = new TargetData();

        data.targets = targets;
        bf.Serialize(file, data);
        file.Close();
    }

    private int FindTarget(string _name)
    {
        for (int i = 0; i < targets.Count; i++)
            if (targets[i].name == _name)
                return i;

        return -1;
    }

    public Target LoadTarget(string _targetName)
    {
        if (File.Exists(Application.persistentDataPath + "/TargetData.td"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/TargetData.td", FileMode.Open);
            TargetData data = (TargetData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < targets.Count; i++)
                if (targets[i].name == _targetName)
                    return data.targets[i];
        }

        Debug.LogWarning("Cannot find save file");
        return null;
    }

    public void RemoveTarget(string _name)
    {
        targets.RemoveAt(FindTarget(_name));
        SaveTargets();
    }

    public void AddSpell(Spell _spell)
    {
        int spellIndex = FindSpell(_spell.name);

        if (spellIndex == -1)
            spells.Add(_spell);
        else
            spells[spellIndex] = _spell;

        SaveSpells();
    }

    public void SaveSpells()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SpellData.sd");
        SpellData data = new SpellData();

        data.spells = spells;
        bf.Serialize(file, data);
        file.Close();
    }

    private int FindSpell(string _name)
    {
        for (int i = 0; i < spells.Count; i++)
            if (spells[i].name == _name)
                return i;

        return -1;
    }

    public Spell LoadSpell(string _spellName)
    {
        if (File.Exists(Application.persistentDataPath + "/SpellData.sd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SpellData.sd", FileMode.Open);
            SpellData data = (SpellData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < spells.Count; i++)
                if (spells[i].name == _spellName)
                    return data.spells[i];
        }

        Debug.LogWarning("Cannot find save file");
        return null;
    }

    public void RemoveSpell(string _name)
    {
        spells.RemoveAt(FindSpell(_name));
        SaveSpells();
    }
}

[Serializable]
public class CharacterData
{
    public List<Character> characters;

    public CharacterData()
    {
        characters = new List<Character>();
    }
}

[Serializable]
public class Character
{
    public string name;
    public int level;
    public int hp;
    public int armor;
    public int ap;
    public int mp;
    public int wp;
    public int waterMastery;
    public int earthMastery;
    public int windMastery;
    public int fireMastery;
    public int waterResist;
    public int earthResist;
    public int windResist;
    public int fireResist;
    public float damageInflicted;
    public float healsPerformed;
    public float criticalHits;
    public float block;
    public int initiative;
    public int range;
    public int dodge;
    public int lock_;
    public int forceOfWill;
    public int criticalMastery;
    public int criticalResist;
    public int rearMastery;
    public int rearResist;
    public int meleeMastery;
    public int distanceMastery;
    public int singleTargetMastery;
    public int areaMastery;
    public int healingMastery;
    public int berserkMastery;
    public float armorGiven;
    public float armorReceived;
}

[Serializable]
public class TargetData
{
    public List<Target> targets;

    public TargetData()
    {
        targets = new List<Target>();
    }
}

[Serializable]
public class Target
{
    public string name;
    public int level;
    public int hp;
    public int armor;
    public int ap;
    public int mp;
    public int wp;
    public int dodge;
    public int lock_;
    public int forceOfWill;
    public int waterResist;
    public int earthResist;
    public int windResist;
    public int fireResist;
}

[Serializable]
public class SpellData
{
    public List<Spell> spells;

    public SpellData()
    {
        spells = new List<Spell>();
    }
}

[Serializable]
public class Spell
{
    public string name;
    public Element element;
    public bool area;
    public int ap;
    public int mp;
    public int wp;
    public int damage;
    public int critDamage;
    public int heal;
    public int armor;
    public int NRDamage;
    public int NRCritDamage;
}