namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块字段验证规则
    /// </summary>
    public class ModuleFieldRule
    {
        public ModuleFieldRule(string ruleName, params string[] rulePars)
        {
            RuleName = ruleName;
            RulePars = rulePars;
        }

        public string RuleName { get; }

        public string[] RulePars { get; }
    } 
}
