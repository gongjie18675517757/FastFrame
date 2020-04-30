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

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Application";


        public override IEnumerable<Info.TargetInfo> BuildCodeInfo(string typeName)
        {
            foreach (Type type in GetTypes())
            {
                if (!string.IsNullOrWhiteSpace(typeName) && type.Name != typeName)
                {
                    continue;
                }

                var exportAttr = type.GetCustomAttribute<ExportAttribute>();

                if (exportAttr == null || !exportAttr.ExportMarks.Contains(ExportMark.Service))
                    continue;

                yield return Build(type);
            }
        }

        public Info.TargetInfo Build(Type type)
        {
            /*当前类型区域名称*/
            string areaName = T4Help.GenerateNameSpace(type, null);

            /*所依赖[引用]的类型*/
            IEnumerable<RelatedToAttribute> relatedTypes = type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null);

            /*要导入的命名空间*/
            IEnumerable<string> importNames = relatedTypes
                 .Select(x => T4Help.GenerateNameSpace(x.RelatedType, null))
                 .Union(new[] { areaName })
                 .SelectMany(x => new string[] { $"FastFrame.Entity.{x}", })
                 .Union(new string[] {
                    $"FastFrame.Infrastructure.Interface",
                    "FastFrame.Infrastructure",
                    "FastFrame.Repository",
                    "FastFrame.Entity.Basis",
                    "System.Linq",
                    "Microsoft.EntityFrameworkCore",
                    "System.Threading.Tasks"
                 })
                    .Union(type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => new[] { x.RelatedType.Namespace ,
                        $"FastFrame.Application.{T4Help.GenerateNameSpace(x.RelatedType,"")}" }))
                 .Distinct();

            /*要导入的依赖*/
            var depends = relatedTypes
                    .Select(x => x.RelatedType.Name)
                    .Union(new[] { "User", type.Name })
                    .Distinct()
                    .Select(x => new { type = $"IRepository<{x}>", name = $"{x.ToFirstLower()}Repository" });


            return new Info.TargetInfo()
            {
                NamespaceName = $"FastFrame.Application.{areaName}",
                ImportNames = importNames,
                Name = $"{type.Name}Service",
                CategoryName = "class",
                BaseNames = new string[] { $"BaseService<{type.Name}, {type.Name}Dto>" },
                FieldInfos = depends.Select(x => new FieldInfo() { FieldName = x.name, TypeName = x.type }),
                Constructor = new Info.ConstructorInfo()
                {
                    Parms = depends.Select(x => new ParameterInfo() { TypeName = x.type, DefineName = x.name }),
                    Super = new string[] { $"{type.Name.ToFirstLower()}Repository" },
                    CodeBlock = depends.Select(x => $"this.{x.name}={x.name};")
                },
                MethodInfos = GetMethods(type),
                Summary = $"{T4Help.GetClassSummary(type, XmlDocDir)} 服务实现",
                Path = $"{TargetPath}\\{areaName}\\{type.Name}\\{type.Name}Service.template.cs"
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

            yield return new MethodInfo()
            {
                IsOverride = false,
                MethodName = "ViewModelListAsync",
                Modifier = "public",
                ResultTypeName = $"Task<PageList<{type.Name}ViewModel>>",
                CodeBlock = GetViewModelListCodeBlock(type),
                Parms = new ParameterInfo[] {
                    new ParameterInfo
                    {
                        DefineName="page",
                        TypeName="Pagination"
                    }
                }
            };
        }

        public IEnumerable<string> GetViewModelListCodeBlock(Type type)
        {
            string typeName = type.Name.ToFirstLower();

            yield return $"var query = {typeName}Repository.MapTo<{type.Name}, {type.Name}ViewModel>();";
            yield return "return query.PageListAsync(page);";
        }

        public IEnumerable<string> GetQueryMainCodeBlock(Type type)
        {
            string typeName = type.Name.ToFirstLower();
            var relateProps = type.GetProperties().Select(x => new
            {
                Attr = x.GetCustomAttribute<RelatedToAttribute>(),
                Prop = x
            }).Where(x => x.Attr != null);

            foreach (Type relateType in relateProps.GroupBy(x => x.Attr.RelatedType).Select(v => v.Key))
            {
                string relatedTypeName = relateType.Name.ToFirstLower();
                yield return $"var {relatedTypeName}Queryable = {relatedTypeName}Repository.Queryable.MapTo<{relateType.Name},{relateType.Name}ViewModel>();";
            }


            yield return $"var query = from _{typeName} in {typeName}Repository ";

            foreach (var prop in relateProps)
            {
                string name = "_" + prop.Prop.Name.ToFirstLower();
                bool isRequired = prop.Prop.GetCustomAttribute<RequiredAttribute>() != null;
                Type relateType = prop.Attr.RelatedType;
                yield return $"\t\t\tjoin {name} in {relateType.Name.ToFirstLower()}Queryable on _{typeName}.{prop.Prop.Name} equals {name}.Id "
                    + (isRequired ? "" : $"into t_{name}");
                if (!isRequired)
                {
                    yield return $"\t\t\tfrom {name} in t_{name}.DefaultIfEmpty()";
                }

            }

            foreach (var prop in relateProps)
            {
                string name = prop.Prop.Name.Replace("_Id", "");
                Type relateType = prop.Attr.RelatedType;
                string relatedTypeName = relateType.Name.ToFirstLower();


                var fieldNames = relateType.GetCustomAttribute<RelatedFieldAttribute>()?.FieldNames.ToArray();
                if (fieldNames == null)
                {
                    fieldNames = relateType.GetProperties()
                        //.Where(v => v.PropertyType == typeof(string))
                        .Select(v => v.Name).ToArray();
                    string[] baseFieldNames = relateType.BaseType.GetProperties().Select(v => v.Name).ToArray();
                    fieldNames = fieldNames.Where(v => !baseFieldNames.Any(r => r == v)).ToArray();
                }
                string linqTempName = "_" + prop.Prop.Name.ToFirstLower();
                fieldNames = fieldNames.Concat(new[] { "Id" }).Distinct().Select(v => $"{v} = {linqTempName}.{v}").ToArray();
                string fieldCtorStr = string.Join(",", fieldNames);

                //yield return $"\t\t\tlet {name} = new {relateType.Name}ViewModel {{{fieldCtorStr}}}";
            }


            yield return $"\t\t\tselect new {type.Name}Dto";
            yield return "\t\t\t{";

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<ExcludeAttribute>() != null)
                {
                    continue;
                }

                yield return $"\t\t\t\t{prop.Name} = _{typeName}.{prop.Name},";
            }

            foreach (var prop in relateProps)
            {
                var linqTempName = "_" + prop.Prop.Name.ToFirstLower();
                yield return $"\t\t\t\t{prop.Prop.Name.Replace("_Id", "")} = {linqTempName /*prop.Prop.Name.Replace("_Id", "")*/},";
            }
            yield return "\t\t\t};";
            yield return "return query;";
        }
    }
}
