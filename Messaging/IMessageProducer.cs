namespace EstoqueApi.Messaging
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
