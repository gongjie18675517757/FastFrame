using System;

namespace FastFrame.CodeGenerate.Build
{
    public abstract class BaseJsCodeBuilder : BaseCodeBuilder
    {
        public BaseJsCodeBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override bool Forcibly => false;
    }
}
