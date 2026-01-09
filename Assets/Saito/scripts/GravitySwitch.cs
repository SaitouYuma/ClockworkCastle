using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public bool isOn;

    public void Toggle()
    {
        isOn = !isOn;
    }

    public void SetOn(bool value)
    {
        isOn = value;
    }
}
