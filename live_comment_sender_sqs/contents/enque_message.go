package contents

import (
	"fmt"
	"github.com/aws/aws-sdk-go/aws/credentials"
	"github.com/aws/aws-sdk-go/aws/session"

	"github.com/aws/aws-sdk-go/aws"
	"github.com/aws/aws-sdk-go/service/sqs"
)

const (
	QUEUE_URL  = "***"
)

// メッセージを送信する
func SendMessage(message string) error {

	cred := credentials.NewStaticCredentials("***", "***", "")
	// クレデンシャルとリージョンをセットしたコンフィグの作成
	region := "ap-northeast-1"
	conf := &aws.Config{
		Credentials: cred,
		Region:      &region,
	}
	// セッションの作成
	sess, err := session.NewSession(conf)
	if err != nil {
		fmt.Println(err.Error())
		panic(err)
	}
	
	svc := sqs.New(sess)
	
	// 送信内容を作成
	params := &sqs.SendMessageInput{
		MessageBody:  aws.String(message),
		QueueUrl:     aws.String(QUEUE_URL),
		DelaySeconds: aws.Int64(1),
	}

	sqsRes, err := svc.SendMessage(params)
	if err != nil {
		fmt.Println(err.Error())
		return err
	}

	fmt.Println("SQSMessageID", *sqsRes.MessageId)

	return nil
}