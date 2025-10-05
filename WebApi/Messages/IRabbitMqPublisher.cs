namespace WebApi.Messages
{
    public interface IRabbitMqPublisher
    {
        Task PublishMessage(string message);
    }
}
