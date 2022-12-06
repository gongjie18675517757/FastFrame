using FastFrame.Entity;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FieldInfo = FastFrame.CodeGenerate.Info.FieldInfo;
using MethodInfo = FastFrame.CodeGenerate.Info.MethodInfo;
using ParameterInfo = FastFrame.CodeGenerate.Info.ParameterInfo;
using TargetInfo = FastFrame.CodeGenerate.Info.TargetInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class ServiceCodeBuilder : BaseCShapeCodeBuilder
    {
        //private readonly DtoBuilder dtoBuild;

        public override string BuildName => "服务";
        public ServiceCodeBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
            //dtoBuild = new DtoBuilder(solutionDir, baseEntityType);
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Application";


        public override IEnumerable<TargetInfo> GetTargetInfoList()
        {
            foreach (Type type in GetTypes())
            {
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
                    Parms = depends
                        .Select(x => new ParameterInfo() { TypeName = x.type, DefineName = x.name })
                        .Concat(new[] {
                            new  ParameterInfo {
                                TypeName="IServiceProvider",
                                DefineName="loader"
                            }
                        }),
                    Super = new string[] { "loader", $"{type.Name.ToFirstLower()}Repository" },
                    CodeBlock = depends.Select(x => $"this.{x.name}={x.name};")
                },
                MethodInfos = GetMethods(type),
                Summary = $"{T4Help.GetClassSummary(type, XmlDocDir)} 服务实现",
                Path = $"{TargetPath}\\{areaName}\\{type.Name}\\{type.Name}Service.template.cs"
            };
        }

        public static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            yield return new MethodInfo()
            {
                IsOverride = true,
                MethodName = "DefaultQueryable",
                Modifier = "protected",
                ResultTypeName = $"IQueryable<{type.Name}Dto>",
                CodeBlock = GetDefaultQueryableCodeBlock(type)
            };


            if (typeof(IViewModelable<>).MakeGenericType(type).IsAssignableFrom(type))
                yield return new MethodInfo()
                {
                    IsOverride = true,
                    MethodName = "DefaultViewModelQueryable",
                    Modifier = "protected",
                    ResultTypeName = $"IQueryable<Entity.IViewModel>",
                    CodeBlock = new[] {
                        $"return repository.Select({type.Name}.BuildExpression());"
                    }
                };

            if (typeof(ITreeEntity).IsAssignableFrom(type))
                yield return new MethodInfo()
                {
                    IsOverride = true,
                    MethodName = "DefaultTreeModelQueryable",
                    Modifier = "protected",
                    ResultTypeName = $"IQueryable<ITreeModel>",
                    CodeBlock = new[] {
                        $"return from a in repository",
                        $"\t\tjoin b in repository.Select({type.Name}.BuildExpression()) on a.Id equals b.Id",
                        $"\t\tselect new TreeModel",
                        $"\t\t{{",
                        $"\t\tId = a.Id,",
                        $"\t\tSuper_Id = a.Super_Id,",
                        $"\t\tValue = b.Value,",
                        $"\t\tChildCount = repository.Count(v => v.Super_Id == a.Id),",
                        $"\t\tTotalChildCount = repository.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),",
                        $"\t}};",
                    }
                };
        }



        public static IEnumerable<string> GetDefaultQueryableCodeBlock(Type type)
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
                yield return $"var {relatedTypeName}Queryable = {relatedTypeName}Repository.Queryable.Select({relateType.Name}.BuildExpression());";
            }

            yield return $"var repository = {typeName}Repository.Queryable;";
            yield return $"var query = from _{typeName} in repository ";

            foreach (var prop in relateProps)
            {
                string name = "_" + prop.Prop.Name.ToFirstLower();
                Type relateType = prop.Attr.RelatedType;
                yield return $"\t\t\tjoin {name} in {relateType.Name.ToFirstLower()}Queryable on _{typeName}.{prop.Prop.Name} equals {name}.Id into t_{name}";
                yield return $"\t\t\tfrom {name} in t_{name}.DefaultIfEmpty()";
            }


            yield return $"\t\t\tselect new {type.Name}Dto";
            yield return "\t\t\t{";

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                yield return $"\t\t\t\t{prop.Name} = _{typeName}.{prop.Name},";
            }

            foreach (var prop in relateProps)
            {
                string name = "_" + prop.Prop.Name.ToFirstLower();
                yield return $"\t\t\t\t{prop.Prop.Name.Replace("_Id", "_Value")} = {name}.Value,";
            }

            //if (typeof(ITreeEntity).IsAssignableFrom(type))
            //    yield return $"\t\t\t\tChildCount = repository.Count(c => c.Super_Id == _{typeName}.Id)";

            yield return "\t\t\t};";
            yield return "return query;";
        }
    }
}
