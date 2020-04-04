namespace FastFrame.Infrastructure.Interface
{
    public interface ITenant
    {
        string Id { get; }

        string Super_Id { get; }
    }
}
