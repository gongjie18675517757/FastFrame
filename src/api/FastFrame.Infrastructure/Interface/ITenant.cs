namespace FastFrame.Infrastructure.Interface
{
    public interface ITenant
    {
        string Id { get; }

        string Parent_Id { get; }
    }
}
