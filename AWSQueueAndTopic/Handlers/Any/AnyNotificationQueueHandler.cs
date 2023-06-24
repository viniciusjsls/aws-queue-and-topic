
using Amazon.Lambda.SNSEvents;
using AWSQueueAndTopic.Model;
using System.Text.Json;

namespace AWSQueueAndTopic.Handlers.Any
{
    internal class AnyNotificationQueueHandler : SQSBaseHandler<SNSEvent>
    {
        internal override async Task HandleRequest(SNSEvent message)
        {
            AnyEvent? anyEvent = JsonSerializer.Deserialize<AnyEvent>(message.Records.First().Sns.Message);

            await DoSomething(anyEvent);
        }

        private Task DoSomething(AnyEvent? anyEvent)
        {
            Console.Write($"Event received { anyEvent?.Name }");

            return Task.CompletedTask;
        }
    }
}
