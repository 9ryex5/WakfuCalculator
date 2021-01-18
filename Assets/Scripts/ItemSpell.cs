using UnityEngine;
using UnityEngine.UI;

public class ItemSpell : MonoBehaviour
{
    public Sprite spriteWater;
    public Sprite spriteEarth;
    public Sprite spriteWind;
    public Sprite spriteFire;

    public Image imageElement;
    public Text textName;
    public GameObject layoutAP;
    public GameObject layoutMP;
    public GameObject layoutWP;
    public Text textAP;
    public Text textMP;
    public Text textWP;
    public Text textCompareValue;

    public void StartThis(Spell _spell, int _compareValue)
    {
        switch (_spell.element)
        {
            case Element.WATER:
                imageElement.sprite = spriteWater;
                break;
            case Element.EARTH:
                imageElement.sprite = spriteEarth;
                break;
            case Element.WIND:
                imageElement.sprite = spriteWind;
                break;
            case Element.FIRE:
                imageElement.sprite = spriteFire;
                break;
        }

        textName.text = _spell.name;

        if (layoutAP != null)
        {
            if (_spell.ap == 0)
                layoutAP.SetActive(false);
            else
                textAP.text = _spell.ap.ToString();
        }

        if (layoutMP != null)
        {
            if (_spell.mp == 0)
                layoutMP.SetActive(false);
            else
                textMP.text = _spell.mp.ToString();
        }

        if (layoutWP != null)
        {
            if (_spell.wp == 0)
                layoutWP.SetActive(false);
            else
                textWP.text = _spell.wp.ToString();
        }

        if (textCompareValue != null) textCompareValue.text = _compareValue.ToString();
    }
}
