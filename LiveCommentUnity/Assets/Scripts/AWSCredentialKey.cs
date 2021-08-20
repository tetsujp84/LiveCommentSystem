using UnityEngine;

namespace LiveComment
{
    [CreateAssetMenu( menuName = "ScriptableObject/AWSCredentialKey")]
    public class AWSCredentialKey : ScriptableObject
    {
        public string AccessKey;
        public string SecretKey;
        public string Url;
    }
}