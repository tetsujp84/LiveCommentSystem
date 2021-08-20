package main

import (
	"github.com/aws/aws-lambda-go/events"
	"github.com/aws/aws-lambda-go/lambda"
	"log"
	"os"
	"./contents"
)

func main() {
	var region = os.Getenv("IsAWS")
	if  region == "true" {
		log.Printf("start 開始")
		lambda.Start(entry)
		lambda.Start(challengeEntry)
		return
	}
	//entry()
}

type Token struct {
	Token string `json:"token"`
	TeamId string `json:"team_id"`
	Event Event `json:"event"`
}

type Event struct {
	Text string `json:"text"`
	User string `json:"user"`
}

func entry(request Token) (events.APIGatewayProxyResponse, error) {
	log.Println(request.Event.Text)
	contents.SendMessage(request.Event.Text)
	return events.APIGatewayProxyResponse{}, nil
}

type SlackChallenge struct {
	Type string `string:"type"`
	Token string `string:"token"`
	Challenge string `string:"challenge"`
}

func challengeEntry(request SlackChallenge) (string, error) {
	log.Println(request.Type)
	log.Println(request.Token)
	log.Println(request.Challenge)
	return request.Challenge, nil
}