using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // ‰ñ“]‘¬“xi“x/•bj
    [SerializeField] private bool isRotating = true; // ‰ñ“]’†‚©‚Ç‚¤‚©

    // ŠO•”‚©‚ç‰ñ“]ó‘Ô‚ğŠm”F
    public bool IsRotating => isRotating;

    private void Update()
    {
        if (isRotating)
        {
            // Z²‚ğ’†S‚É‰ñ“]
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // ‰ñ“]‚ğŠJn/’â~
    public void SetRotating(bool value)
    {
        isRotating = value;
    }

    // ‰ñ“]‚ğƒgƒOƒ‹
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    // ‰ñ“]‚ğ’â~
    public void Stop()
    {
        isRotating = false;
    }

    // ‰ñ“]‚ğŠJn
    public void Start()
    {
        isRotating = true;
    }
}