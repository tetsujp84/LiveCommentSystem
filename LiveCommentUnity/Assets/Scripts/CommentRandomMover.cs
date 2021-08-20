using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CommentRandomMover : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUgui;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float addSpeed;
    [SerializeField] private float rangeY;
    
    [SerializeField] private RectTransform rectTransform;
    public void Initialize(string comment)
    {
        var lenght = comment.Length;
        textMeshProUgui.text = comment;
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, Random.Range(-rangeY, rangeY));
        var speed = Mathf.Min(baseSpeed + lenght * addSpeed, baseSpeed * 2);
        this.UpdateAsObservable().Subscribe(_ =>
        {
            var anchoredPosition = rectTransform.anchoredPosition;
            var position = new Vector2(anchoredPosition.x + speed * Time.deltaTime, anchoredPosition.y);
            rectTransform.anchoredPosition = position;
        }).AddTo(this);
    }
}
