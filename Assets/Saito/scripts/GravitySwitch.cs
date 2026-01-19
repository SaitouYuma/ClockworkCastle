using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    // シングルトン
    public static GravitySwitch Instance { get; private set; }

    [SerializeField] private bool isOn;

    // 外部から読み取り専用で状態を確認できる
    public bool IsGravityReversed => isOn;

    // 外部から状態を確認できる（従来の方法）
    public bool IsOn => isOn;

    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Toggle()
    {
        isOn = !isOn;
    }

    public void SetOn(bool value)
    {
        isOn = value;
    }
}