
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using AWSQueueAndTopic.Model;
using System.Text.Json;

namespace AWSQueueAndTopic.Handlers.Any
{
    public class AnyNotificationQueueHandler : SQSBaseHandler<SNSEvent.SNSMessage>
    {
        internal override async Task HandleRequest(SNSEvent.SNSMessage snsMessage)
        {
            AnyEvent? anyEvent = JsonSerializer.Deserialize<AnyEvent>(snsMessage.Message);

            await DoSomething(anyEvent);
        }

        private Task DoSomething(AnyEvent? anyEvent)
        {
            LambdaLogger.Log($"Event received { anyEvent?.Name }");

            return Task.CompletedTask;
        }
    }
}
