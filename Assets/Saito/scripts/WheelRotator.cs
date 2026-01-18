using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    public Transform pivotPoint;


    void Update()
    {
        if (pivotPoint != null)
        {
            // pivotPoint‚ğ’†S‚É‰ñ“]
            transform.RotateAround(pivotPoint.position, Vector3.forward, speed * Time.deltaTime);
        }
        else
        {
            // pivotPoint‚ª–¢İ’è‚È‚ç’Êí‚Ì‰ñ“]
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }

    public void Reverse()
    {
        speed = -speed;
    }
}
