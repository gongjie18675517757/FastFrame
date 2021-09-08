namespace FastFrame.Infrastructure.Identity
{
    public class CurrUser : ICurrUser
    {
        public string Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public string ToKen { get; set; }
    }
}
