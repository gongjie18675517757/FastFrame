namespace FastFrame.Application.Basis
{
    public partial class PermissionModel
    {
        public string Id { get; set; }

        public string Super_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Name { get; set; }
    }
}
