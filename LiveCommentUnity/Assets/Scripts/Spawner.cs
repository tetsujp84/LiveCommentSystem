using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LiveComment
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<AssetCollection> assetDictionary;
        [SerializeField] private float rangeX;
        [SerializeField] private float rangeY = 0.2f;
        [SerializeField] private int spawnCount = 3;

        public void Spawn(string text)
        {
            if (!text.Contains(":")) return;
            var texts = text.Split(':');
            foreach (var t in texts)
            {
                var assetCollection = assetDictionary.FirstOrDefault(a => a.keys.FirstOrDefault(k => t.Contains(k)) != null);
                if (assetCollection == null)
                {
                    continue;
                }
                var count = Random.Range(1, spawnCount + 1);
                for (int i = 0; i < count; i++)
                {
                    var target = assetCollection.gameObjects[Random.Range(0, assetCollection.gameObjects.Count)];
                    var position = target.transform.position;
                    var instance = Instantiate(target, new Vector3(Random.Range(-rangeX, rangeX), position.y + Random.Range(-rangeY, rangeY), position.z), target.transform.rotation);   
                }
            }
        }
    }
}