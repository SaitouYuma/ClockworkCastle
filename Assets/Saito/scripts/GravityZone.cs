using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private GravitySwitch gravitySwitch;

    // ゾーン内のRigidbody管理（元の重力も保存）
    private Dictionary<Rigidbody2D, float> bodies = new Dictionary<Rigidbody2D, float>();
    private bool lastSwitchState;

    private void Start()
    {
        if (gravitySwitch != null)
        {
            lastSwitchState = gravitySwitch.IsOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        // 元の重力スケールを保存
        if (!bodies.ContainsKey(rb))
        {
            bodies.Add(rb, Mathf.Abs(rb.gravityScale));
        }

        ApplyGravity(rb);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        RestoreGravity(rb);
        bodies.Remove(rb);
    }

    private void Update()
    {
        // スイッチの状態が変わったときだけ更新
        if (gravitySwitch != null && gravitySwitch.IsOn != lastSwitchState)
        {
            lastSwitchState = gravitySwitch.IsOn;

            foreach (var pair in bodies)
            {
                ApplyGravity(pair.Key);
            }
        }
    }

    private void ApplyGravity(Rigidbody2D rb)
    {
        if (!bodies.ContainsKey(rb)) return;

        float originalScale = bodies[rb];

        if (gravitySwitch != null && gravitySwitch.IsOn)
        {
            rb.gravityScale = -originalScale; // 反転
        }
        else
        {
            rb.gravityScale = originalScale; // 通常
        }
    }

    private void RestoreGravity(Rigidbody2D rb)
    {
        if (bodies.ContainsKey(rb))
        {
            rb.gravityScale = bodies[rb]; // 元の値に戻す
        }
    }
}