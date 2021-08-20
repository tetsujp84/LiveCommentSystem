using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LiveComment
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float optionDuration = 1f;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(duration + Random.Range(0, optionDuration))).Subscribe(_ => Destroy(gameObject)).AddTo(this);
        }
    }
}