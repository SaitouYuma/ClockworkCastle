using UnityEngine;

public class cameraaa : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] float _zOffset = -10f;  // ‰œs‚«
    public void Settarget(GameObject target)
    {
        _target = target;
    }
    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.transform.position;
        transform.position = new Vector3(targetPos.x, targetPos.y, _zOffset);
    }
}
