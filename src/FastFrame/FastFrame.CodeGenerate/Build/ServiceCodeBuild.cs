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
        public ServiceCodeBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Service\\Services\\Templates";

        //public override IEnumerable<Type> GetTypes()
        //{
        //    yield break;
        //}

        public override IEnumerable<TargetInfo> BuildCodeInfo()
        {
            foreach (var item in GetTypes())
            {
                if (item.GetCustomAttribute<Infrastructure.Attrs.ExcludeAttribute>() != null)
                    continue;
                yield return Build(item);
            }
        }

        public TargetInfo Build(Type type)
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
                    "System.Linq"
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
                    .Union(new[] { "Foreign", "User", type.Name })
                    .Distinct()
                    .Select(x => new { type = $"IRepository<{x}>", name = $"{x.ToFirstLower()}Repository" });


            return new TargetInfo()
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

            if (hasManage)
            {
                yield return "var foreignQueryable = foreignRepository.Queryable;";
                yield return "var userQuerable = userRepository.Queryable;";
            }


            foreach (var prop in props)
            {
                var relatedTypeName = prop.Attr.RelatedType.Name.ToFirstLower();
                yield return $" var {relatedTypeName}Queryable = {relatedTypeName}Repository.Queryable;";
            }

            yield return $" var query = from _{typeName} in {typeName}Queryable ";
            foreach (var prop in props)
            {
                var name = "_" + prop.Prop.Name.ToFirstLower();
                var isRequired = prop.Prop.GetCustomAttribute<RequiredAttribute>() != null;
                var relateType = prop.Attr.RelatedType;
                yield return $"\t\t\tjoin {name} in {relateType.Name.ToFirstLower()}Queryable.MapTo<{relateType.Name},{relateType.Name}Dto>() on _{typeName}.{prop.Prop.Name} equals {name}.Id "
                    + (isRequired ? "" : $"into t_{name}");
                if (!isRequired)
                    yield return $"\t\t\tfrom {name} in t_{name}.DefaultIfEmpty()";
            }
            if (hasManage)
            {
                yield return $"\t\tjoin foreing in foreignQueryable on _{typeName}.Id equals foreing.EntityId into t_foreing";
                yield return "\t\tfrom foreing in t_foreing.DefaultIfEmpty()";
                yield return "\t\tjoin user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2";
                yield return "\t\tfrom user2 in t_user2.DefaultIfEmpty()";
                yield return "\t\tjoin user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3";
                yield return "\t\tfrom user3 in t_user3.DefaultIfEmpty()";
            }

            yield return $"\t\t select new {type.Name}Dto";
            yield return "\t\t{";

            foreach (var prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;
                if (prop.Name == "Tenant_Id" || prop.Name == "IsDeleted")
                    continue;
                yield return $"\t\t\t{prop.Name}=_{typeName}.{prop.Name},";
            }
            foreach (var prop in props)
            {
                if (prop.Prop.Name == "Tenant_Id" || prop.Prop.Name == "IsDeleted")
                    continue;
                yield return $"\t\t\t{prop.Prop.Name.Replace("_Id", "")}={ "_" + prop.Prop.Name.ToFirstLower()},";
            }


            if (hasManage)
            {
                yield return "\t\t\tForeign = foreing,";
                yield return "\t\t\tCreate_User = user2,";
                yield return "\t\t\tModify_User = user3,";
            }


            yield return "\t\t};";

            yield return "return query;";

        }
    }
}
