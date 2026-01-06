using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] GravitySwitch gravitySwitch;

    // ゾーン内にいるRigidbody管理
    HashSet<Rigidbody2D> bodies = new HashSet<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        bodies.Add(rb);
        ApplyGravity(rb);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        RestoreGravity(rb);
        bodies.Remove(rb);
    }

    void Update()
    {
        // スイッチ切替に即追従させる
        foreach (var rb in bodies)
        {
            ApplyGravity(rb);
        }
    }

    void ApplyGravity(Rigidbody2D rb)
    {
        if (gravitySwitch.isOn)
        {
            rb.gravityScale = -Mathf.Abs(rb.gravityScale);
        }
        else
        {
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
        }
    }

    void RestoreGravity(Rigidbody2D rb)
    {
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
    }
}
