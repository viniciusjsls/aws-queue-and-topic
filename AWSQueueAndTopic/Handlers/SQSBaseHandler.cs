using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System.Text.Json;

namespace AWSQueueAndTopic.Handlers
{
    internal abstract class SQSBaseHandler<T> where T : class
    {
        public async Task<SQSBatchResponse> Handle(SQSEvent sqsEvent, ILambdaContext context)
        {
            List<SQSBatchResponse.BatchItemFailure> batchItemFailures = new List<SQSBatchResponse.BatchItemFailure>();

            foreach(var sqsMessage in sqsEvent.Records)
            {
                try
                {
                    T deserializedMessage = DeserializeSQSMessage(sqsMessage);
                    await HandleRequest(deserializedMessage);
                }
                catch
                {
                    batchItemFailures.Add(new SQSBatchResponse.BatchItemFailure { ItemIdentifier = sqsMessage.MessageId });
                }
            }

            return new SQSBatchResponse(batchItemFailures);
        }

        internal abstract Task HandleRequest(T message);

        private T DeserializeSQSMessage(SQSEvent.SQSMessage sqsMessage)
        {
            var deserializedSQSMessage = JsonSerializer.Deserialize<T>(sqsMessage.Body);

            return deserializedSQSMessage ?? throw new Exception("Deserialized object is invalid");
        }
    }
}
