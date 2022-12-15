using FastFrame.Entity.Enums;

namespace FastFrame.Application.Basis
{
    public class EnumItemModel: TreeModel
    { 
        public int? Key { get; set; }

        public int? IntKey { get; set; }

        public IEnumerable<EnumItemModel> Children { get; set; }
    }
}