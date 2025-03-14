namespace Domain.Crollclas;

public class RabbitMqMessage<T>
{
    public string Action { get; set; }
    public T Data { get; set; }
}