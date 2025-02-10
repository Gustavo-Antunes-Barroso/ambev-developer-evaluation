namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IRabbitMQProducer<T> where T : class
    {
        Task<bool> SendMessage<T>(T message, string method);
    }
}
