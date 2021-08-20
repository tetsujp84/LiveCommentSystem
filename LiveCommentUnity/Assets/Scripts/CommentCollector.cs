using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace LiveComment
{
    public class CommentCollector : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private CommentSpawner commentSpawner;
        [SerializeField] private ImageSpawner imageSpawner;

        [SerializeField] private CommentTester commentTester;

        [SerializeField] private AWSCredentialKey credentialKey;

        bool isDelete;

        void Start()
        {
            DoAsync().Forget();
            isDelete = PlayerPrefs.GetInt("IsDelete", 0) == 1;
            commentTester.Initialize(isDelete);
            commentTester.OnChangeDelete.Subscribe(a =>
            {
                var isOn = a ? 1 : 0;
                PlayerPrefs.SetInt("IsDelete", isOn);
                isDelete = a;
            });
        }

        async UniTask DoAsync()
        {
            var credentials = new BasicAWSCredentials(credentialKey.AccessKey, credentialKey.SecretKey);
            var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.APNortheast1);

            while (true)
            {
                var receiveMessageRequest = new ReceiveMessageRequest { QueueUrl = credentialKey.Url };

                var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

                foreach (var message in receiveMessageResponse.Messages)
                {
                    Debug.Log(message.Body);
                    spawner.Spawn(message.Body);
                    commentSpawner.Spawn(message.Body);
                    imageSpawner.Spawn(message.Body, commentTester.ImageInputFieldText).Forget();

                    if (isDelete)
                    {
                        var delete = new DeleteMessageRequest(receiveMessageRequest.QueueUrl, message.ReceiptHandle);
                        var deleteMessageResponse = await sqsClient.DeleteMessageAsync(delete);   
                    }
                }
            }
        }
    }
}