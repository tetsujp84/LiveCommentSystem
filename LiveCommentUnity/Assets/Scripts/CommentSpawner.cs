using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace LiveComment
{
    public class CommentSpawner : MonoBehaviour
    {
        [SerializeField] private CommentRandomMover mover;

        public void Spawn(string comment)
        {
            var c = Regex.Replace(comment,":.*?:", "");
            var instance = Instantiate(mover, mover.transform.parent);
            instance.Initialize(c);
            instance.gameObject.SetActive(true);
        }
    }
}