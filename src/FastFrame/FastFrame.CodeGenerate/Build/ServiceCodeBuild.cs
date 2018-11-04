using FastFrame.CodeGenerate.Info;
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
                 .SelectMany(x => new string[] { $"FastFrame.Repository.{x}", $"FastFrame.Entity.{x}", })
                 .Union(new string[] {
                    $"FastFrame.Dto.{areaName}",
                    $"FastFrame.Infrastructure.Interface",
                    "System.Linq"
                 })
                 .Distinct();

            /*要导入的依赖*/
            var depends = relatedTypes
                    .Select(x => x.RelatedType.Name)
                    .Union(new[] { "Foreign", "User", type.Name })
                    .Distinct()
                    .Select(x => new { type = $"{x}Repository", name = $"{x.ToFirstLower()}Repository" });


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
            var typeName = type.Name.ToFirstLower();
            var props = type.GetProperties().Select(x => new
            {
                Attr = x.GetCustomAttribute<RelatedToAttribute>(),
                Prop = x
            }).Where(x => x.Attr != null);

            if (!props.Any(x => x.Attr.RelatedType == type))
                yield return $"var {typeName}Queryable={typeName}Repository.Queryable;";
            yield return "var foreignQueryable = foreignRepository.Queryable;";
            yield return "var userQuerable = userRepository.Queryable;";

            foreach (var prop in props)
            {
                var relatedTypeName = prop.Attr.RelatedType.Name.ToFirstLower();
                yield return $" var {relatedTypeName}Queryable = {relatedTypeName}Repository.Queryable;";
            }

            yield return $" var query = from {typeName} in {typeName}Queryable ";
            foreach (var prop in props)
            {
                var name = prop.Prop.Name.ToFirstLower();
                var isRequired = prop.Prop.GetCustomAttribute<RequiredAttribute>() != null;
                yield return $"\t\t\tjoin {name} in {prop.Attr.RelatedType.Name.ToFirstLower()}Queryable on {typeName}.{prop.Prop.Name} equals {name}.Id "
                    + (isRequired ? "" : $"into t_{name}");
                if (!isRequired)
                    yield return $"\t\t\tfrom {name} in t_{name}.DefaultIfEmpty()";
            }

            yield return $"\t\tjoin foreing in foreignQueryable on {typeName}.Id equals foreing.EntityId into t_foreing";
            yield return "\t\tfrom foreing in t_foreing.DefaultIfEmpty()";
            yield return "\t\tjoin user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2";
            yield return "\t\tfrom user2 in t_user2.DefaultIfEmpty()";
            yield return "\t\tjoin user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3";
            yield return "\t\tfrom user3 in t_user3.DefaultIfEmpty()";

            yield return $"\t\t select new {type.Name}Dto";
            yield return "\t\t{";

            foreach (var prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;
                yield return $"\t\t\t{prop.Name}={typeName}.{prop.Name},";
            }
            foreach (var prop in props)
            {
                var relatedFieldAttribute = prop.Attr.RelatedType.GetCustomAttribute<RelatedFieldAttribute>();
                if (relatedFieldAttribute == null)
                    continue;
                var defaultName = relatedFieldAttribute.DefaultName;
                yield return $"\t\t\t{prop.Prop.Name.Replace("Id", defaultName)}={ prop.Prop.Name.ToFirstLower()}.{defaultName},";
                foreach (var item in relatedFieldAttribute.OtherNames)
                {
                    yield return $"\t\t\t{prop.Prop.Name.Replace("Id", item)}={ prop.Prop.Name.ToFirstLower()}.{item},";
                }
            }

            yield return "\t\t\tCreateAccount = user2.Account,";
            yield return "\t\t\tCreateName = user2.Name,";
            yield return "\t\t\tCreateTime = foreing.CreateTime,";
            yield return "\t\t\tModifyAccount = user3.Account,";
            yield return "\t\t\tModifyName = user3.Name,";
            yield return "\t\t\tModifyTime = foreing.ModifyTime,";

            yield return "\t\t};";

            yield return "return query;";

        }
    }
}
