using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [SerializeField] WheelRotator targetWheel;

    [Header("Lever Visual")]
    [SerializeField] Transform leverVisual;
    [SerializeField] float onAngle = 180f;
    [SerializeField] float offAngle = -180f;

    bool isOn = false;

    public void Activate()
    {

        isOn = !isOn;

        // ƒŒƒo[‚ÌŒü‚«‚ğØ‚è‘Ö‚¦
        float angle = isOn ? onAngle : offAngle;
        leverVisual.localRotation = Quaternion.Euler(0, angle, 0);

        if (targetWheel != null)
        {
            targetWheel.Reverse();
        }
    }
}
