using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] rotateAxis;
    void Start()
    {
        var axis = rotateAxis[Random.Range(0, rotateAxis.Length)];
        this.UpdateAsObservable().Subscribe(_ =>
        {
            var rotate = speed * Time.deltaTime;
            transform.Rotate(axis, rotate);
        }).AddTo(this);
    }
}