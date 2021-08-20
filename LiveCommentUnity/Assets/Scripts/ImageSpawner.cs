using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LiveComment
{
    public class ImageSpawner : MonoBehaviour
    {
        [SerializeField] RawImage image;
        [SerializeField] private float rangeX;
        [SerializeField] private float rangeY = 0.2f;
        
        public async UniTaskVoid Spawn(string comment, string condition)
        {
            var conditions = condition.Split(new[] { "\n" }, StringSplitOptions.None);
            foreach (var c in conditions)
            {
                var s = c.Split(',');
                if (s.Length != 2) continue;
                if (comment.Contains(s[0]))
                {
                    var request = UnityWebRequestTexture.GetTexture(s[1]);
                    var result = await request.SendWebRequest();
                    if(result.result == UnityWebRequest.Result.ConnectionError || result.result == UnityWebRequest.Result.ProtocolError) {
                        Debug.Log(request.error);
                    }
                    else {
                        var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                        var instance = Instantiate(image, image.transform.parent, false);
                        instance.rectTransform.anchoredPosition = new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY));
                        instance.texture = tex;
                        FixAspect(instance, instance.rectTransform.rect.size);
                        instance.gameObject.SetActive(true);
                    }
                }
            }
        }

        private void FixAspect(RawImage image, Vector3 originalSize)
        {
            var texture = image.texture;
            var textureSize = new Vector2(texture.width, texture.height);

            var heightScale = originalSize.y / textureSize.y;
            var widthScale = originalSize.x / textureSize.x;
            var rectSize = textureSize * Mathf.Min(heightScale, widthScale);

            var rectTransform = image.rectTransform;
            var anchorDiff = rectTransform.anchorMax - rectTransform.anchorMin;
            var parentSize = ((RectTransform)image.transform.parent).rect.size;
            var anchorSize = parentSize * anchorDiff;

            rectTransform.sizeDelta = rectSize - anchorSize;
        }
    }
}