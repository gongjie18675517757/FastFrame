using FastFrame.CodeGenerate.Info;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FieldInfo = FastFrame.CodeGenerate.Info.FieldInfo;
using MethodInfo = FastFrame.CodeGenerate.Info.MethodInfo;
using ParameterInfo = FastFrame.CodeGenerate.Info.ParameterInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class ServiceCodeBuild : BaseCodeBuild
    {
        private readonly DtoBuild dtoBuild;

        public override string ProductName => "服务";
        public ServiceCodeBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
            dtoBuild = new DtoBuild(solutionDir, baseEntityType);
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Service\\Services\\Templates";


        public override IEnumerable<Info.TargetInfo> BuildCodeInfo(string typeName)
        {
            foreach (var type in GetTypes())
            {
                if (!string.IsNullOrWhiteSpace(typeName) && type.Name != typeName)
                    continue;
                if (type.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;
                yield return Build(type);
            }
        }

        public Info.TargetInfo Build(Type type)
        {
            /*当前类型区域名称*/
            var areaName = T4Help.GenerateNameSpace(type, null);

            /*所依赖[引用]的类型*/
            var relatedTypes = type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null);

            /*要导入的命名空间*/
            var importNames = relatedTypes
                 .Select(x => T4Help.GenerateNameSpace(x.RelatedType, null))
                 .Union(new[] { areaName })
                 .SelectMany(x => new string[] { $"FastFrame.Entity.{x}", })
                 .Union(new string[] {
                    $"FastFrame.Dto.{areaName}",
                    $"FastFrame.Infrastructure.Interface",
                    "FastFrame.Infrastructure",
                    "FastFrame.Repository",
                    "FastFrame.Entity.Basis",
                    "System.Linq",
                    "Microsoft.EntityFrameworkCore"
                 })
                    .Union(type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => new[] { x.RelatedType.Namespace ,
                        $"FastFrame.Dto.{T4Help.GenerateNameSpace(x.RelatedType,"")}" }))
                 .Distinct();

            /*要导入的依赖*/
            var depends = relatedTypes
                    .Select(x => x.RelatedType.Name)
                    .Union(new[] { "User", type.Name })
                    .Distinct()
                    .Select(x => new { type = $"IRepository<{x}>", name = $"{x.ToFirstLower()}Repository" });


            return new Info.TargetInfo()
            {
                NamespaceName = $"FastFrame.Service.Services.{areaName}",
                ImportNames = importNames,
                Name = $"{type.Name}Service",
                CategoryName = "class",
                BaseNames = new string[] { $"BaseService<{type.Name}, {type.Name}Dto>" },
                FieldInfos = depends.Select(x => new FieldInfo() { FieldName = x.name, TypeName = x.type }),
                Constructor = new Info.ConstructorInfo()
                {
                    Parms = depends.Select(x => new ParameterInfo() { TypeName = x.type, DefineName = x.name })
                        .Union(new[] { new ParameterInfo() { TypeName = $"IScopeServiceLoader", DefineName = "loader" } }),
                    Super = new string[] { $"{type.Name.ToFirstLower()}Repository", "loader" },
                    CodeBlock = depends.Select(x => $"this.{x.name}={x.name};")
                },
                MethodInfos = GetMethods(type),
                Summary = $"{T4Help.GetClassSummary(type, XmlDocDir)} 服务类",
                Path = TargetPath
            };
        }

        public IEnumerable<MethodInfo> GetMethods(Type type)
        {
            yield return new MethodInfo()
            {
                IsOverride = true,
                MethodName = "QueryMain",
                Modifier = "protected",
                ResultTypeName = $"IQueryable<{type.Name}Dto>",
                CodeBlock = GetQueryMainCodeBlock(type)
            };
        }

        public IEnumerable<PropInfo> GetDtoProps(Type type)
        {
            return dtoBuild.GetPropInfos(type);//.Where(r => !r.TypeName.EndsWith("Dto"));
        }

        public IEnumerable<string> GetQueryMainCodeBlock(Type type)
        {
            var hasManage = typeof(IHasManage).IsAssignableFrom(type);
            var typeName = type.Name.ToFirstLower();
            var props = type.GetProperties().Select(x => new
            {
                Attr = x.GetCustomAttribute<RelatedToAttribute>(),
                Prop = x
            }).Where(x => x.Attr != null);

            if (!props.Any(x => x.Attr.RelatedType == type))
                yield return $"var {typeName}Queryable={typeName}Repository.Queryable;";

            foreach (var prop in props.GroupBy(x => x.Attr.RelatedType.Name))
            {
                var relatedTypeName = prop.Key.ToFirstLower();
                yield return $" var {relatedTypeName}Queryable = {relatedTypeName}Repository.Queryable;";
            }


            yield return $" var query = from _{typeName} in {typeName}Queryable ";

            foreach (var prop in props)
            {
                var name = "_" + prop.Prop.Name.ToFirstLower();
                var isRequired = prop.Prop.GetCustomAttribute<RequiredAttribute>() != null;
                var relateType = prop.Attr.RelatedType;
                yield return $"\t\t\tjoin {name} in {relateType.Name.ToFirstLower()}Queryable on _{typeName}.{prop.Prop.Name} equals {name}.Id "
                    + (isRequired ? "" : $"into t_{name}");
                if (!isRequired)
                    yield return $"\t\t\tfrom {name} in t_{name}.DefaultIfEmpty()";
            }
            if (hasManage)
            {

            }

            yield return $"\t\t\t select new {type.Name}Dto";
            yield return "\t\t\t{";

            foreach (var prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;
                if (prop.Name == "Tenant_Id" || prop.Name == "IsDeleted")
                    continue;
                yield return $"\t\t\t\t{prop.Name}=_{typeName}.{prop.Name},";
            }
            foreach (var prop in props)
            {
                if (prop.Prop.Name == "Tenant_Id" || prop.Prop.Name == "IsDeleted")
                    continue;

                var linqTempName = "_" + prop.Prop.Name.ToFirstLower();
                var isDto = prop.Attr.RelatedType.GetCustomAttribute<ExcludeAttribute>() == null;
                if (isDto)
                {
                    yield return $"\t\t\t\t{prop.Prop.Name.Replace("_Id", "")}=new {prop.Attr.RelatedType.Name}ViewModel";
                    yield return "\t\t\t\t{";
                    yield return $"\t\t\t\t\tId = {linqTempName}.Id,";

                    var fieldNames = prop.Attr.RelatedType.GetCustomAttribute<RelatedFieldAttribute>()?.FieldNames;
                    if (fieldNames == null)
                    {
                        fieldNames = prop.Attr.RelatedType.GetProperties().Select(v => v.Name).ToArray();
                        var baseFieldNames = prop.Attr.RelatedType.BaseType.GetProperties().Select(v => v.Name).ToArray();
                        fieldNames = fieldNames.Where(v => !baseFieldNames.Any(r => r == v)).ToArray();
                    }
                    foreach (var item in fieldNames)
                    {
                        if (item == "Id")
                            continue;
                        yield return $"\t\t\t\t\t{item} = {linqTempName}.{item},";
                    }

                    yield return "\t\t\t\t},";
                }
                else
                {
                    yield return $"\t\t\t\t{prop.Prop.Name.Replace("_Id", "")}={linqTempName},";
                }

            }





            yield return "\t\t};";

            yield return "return query;";

        }
    }
}
