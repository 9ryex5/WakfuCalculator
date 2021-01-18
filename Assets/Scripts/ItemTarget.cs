using UnityEngine;
using UnityEngine.UI;

public class ItemTarget : MonoBehaviour
{
    public Text textName;
    public Text textWater;
    public Text textEarth;
    public Text textWind;
    public Text textFire;

    public void StartThis(string _name, float _water, float _earth, float _wind, float _fire)
    {
        textName.text = _name;
        textWater.text = _water * 100 + "%";
        textEarth.text = _earth * 100 + "%";
        textWind.text = _wind * 100 + "%";
        textFire.text = _fire * 100 + "%";
    }
}
