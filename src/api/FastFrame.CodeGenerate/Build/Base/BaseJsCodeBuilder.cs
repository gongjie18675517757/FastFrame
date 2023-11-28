using System;

namespace FastFrame.CodeGenerate.Build
{
    public abstract class BaseJsCodeBuilder(string solutionDir, Type baseEntityType) : BaseCodeBuilder(solutionDir, baseEntityType)
    {
        public override bool Forcibly => false;
    }
}
