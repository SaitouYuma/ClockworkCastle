using UnityEngine;

public class cameraaa : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] float _zOffset = -10f;  // 奥行き
    float _fixedY;                           // 固定Y位置

    void Start()
    {
        _fixedY = transform.position.y;      // ゲーム開始時のY位置を固定
    }

    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.transform.position;
        transform.position = new Vector3(targetPos.x, _fixedY, _zOffset);
    }
}
