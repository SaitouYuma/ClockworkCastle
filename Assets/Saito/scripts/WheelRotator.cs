using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    public float speed = 30f; // ³‚Ì’l‚Å‚à•‰‚Ì’l‚Å‚àOK

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    public void Reverse()
    {
        speed = -speed;
    }
}
