using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace LiveComment
{
    public class CommentTester : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private InputField inputField;

        [SerializeField] TMP_InputField imageInputField;

        [SerializeField] private Spawner spawner;
        [SerializeField] private CommentSpawner commentSpawner;
        [SerializeField] ImageSpawner imageSpawner;

        [SerializeField] private Canvas canvas;
        [SerializeField] private Toggle deleteToggle;

        public IObservable<bool> OnChangeDelete => deleteToggle.OnValueChangedAsObservable();
        public string ImageInputFieldText => imageInputField.text;

        public void Initialize(bool isDelete)
        {
            button.OnClickAsObservable().Subscribe(_ =>
            {
                spawner.Spawn(inputField.text);
                commentSpawner.Spawn(inputField.text);
                imageSpawner.Spawn(inputField.text, imageInputField.text).Forget();
            });
            deleteToggle.isOn = isDelete;

            this.UpdateAsObservable().Subscribe(_ =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    canvas.enabled = !canvas.enabled;
                }
            });
        }
    }
}