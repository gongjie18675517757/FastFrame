using FastFrame.Entity.Enums;

namespace FastFrame.Application.Basis
{
    public class EnumItemModel
    {
        public string Id { get; set; }
        public EnumName Key { get; set; }
        public string Value { get; set; }
        public string Super_Id { get; set; }
    }
}