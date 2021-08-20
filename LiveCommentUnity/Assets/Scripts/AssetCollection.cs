using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiveComment
{
    [Serializable]
    [CreateAssetMenu( menuName = "ScriptableObject/AssetCollection")]
    public class AssetCollection : ScriptableObject
    {
        public string[] keys;
        public List<GameObject> gameObjects;
    }
}