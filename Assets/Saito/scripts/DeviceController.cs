using UnityEngine;
using System.Collections;

public class DeviceController : MonoBehaviour
{
    [SerializeField] WheelRotator targetWheel;

    [Header("Lever Visual")]
    [SerializeField] Transform leverVisual;
    [SerializeField] float onAngle = 180f;
    [SerializeField] float offAngle = -180f;

    [Header("Pivot Control")]
    [SerializeField] Vector3 onPivotPosition;
    [SerializeField] Vector3 offPivotPosition;
    [SerializeField] float pivotMoveSpeed = 2f; // 移動速度

    bool isOn = false;
    private Coroutine currentMoveCoroutine;

    public void Activate()
    {
        isOn = !isOn;

        // レバーの向きを切り替え
        float angle = isOn ? onAngle : offAngle;
        leverVisual.localRotation = Quaternion.Euler(0, angle, 0);

        if (targetWheel != null)
        {
            targetWheel.Reverse();

            // 既存の移動を停止
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }

            // 中心点をゆっくり移動
            Vector3 targetPosition = isOn ? onPivotPosition : offPivotPosition;
            currentMoveCoroutine = StartCoroutine(MovePivotSmooth(targetWheel.pivotPoint, targetPosition));
        }
    }

    private IEnumerator MovePivotSmooth(Transform pivot, Vector3 targetPosition)
    {
        while (Vector3.Distance(pivot.position, targetPosition) > 0.01f)
        {
            pivot.position = Vector3.MoveTowards(pivot.position, targetPosition, pivotMoveSpeed * Time.deltaTime);
            yield return null;
        }

        pivot.position = targetPosition; // 最終位置を確定
    }
}