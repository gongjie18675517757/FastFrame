using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 表达式扩展
    /// </summary>
    public class ExpressionExtended
    {
        public static Expression<Action<TSource, TTarget>> MapSet<TSource, TTarget>()
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var paramSourceExpr = Expression.Parameter(sourceType, nameof(TSource));
            var paramTargetExpr = Expression.Parameter(targetType, nameof(TTarget));
            var binaryExpressions = new List<Expression>();

            var sourceProps = sourceType.GetProperties().ToDictionary(x => x.Name.ToLower(), x => x);

            foreach (var targetPropInfo in targetType.GetProperties())
            {
                var targetPropName = targetPropInfo.Name.ToLower();
                if (sourceProps.TryGetValue(targetPropName, out var sourcePropInfo)
                    && sourcePropInfo.CanRead
                    && targetPropInfo.CanWrite
                    /*&& sourcePropInfo.PropertyType == targetPropInfo.PropertyType*/)
                {
                    Expression sourcePropExpr = Expression.Property(paramSourceExpr, sourcePropInfo);
                    Expression targetPropExpr = Expression.Property(paramTargetExpr, targetPropInfo);


                    if (sourcePropInfo.PropertyType == targetPropInfo.PropertyType)
                    {
                        var binaryExpression = Expression.Assign(targetPropExpr, sourcePropExpr);
                        binaryExpressions.Add(binaryExpression);
                    }

                    if (sourcePropInfo.PropertyType.IsGenericType &&
                        sourcePropInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                        sourcePropInfo.PropertyType.GetGenericArguments()[0] == targetPropInfo.PropertyType)
                    {
                        var binaryExpression = Expression.Assign(targetPropExpr, Expression.Property(sourcePropExpr, "Value"));
                        var conditionalExpression = Expression.IfThen(Expression.NotEqual(sourcePropExpr, Expression.Constant(null)), binaryExpression);
                        binaryExpressions.Add(conditionalExpression);
                    }
                }
            }

            Expression bodyExpr = Expression.Block(binaryExpressions);
            var lambdaExpr
                = Expression.Lambda<Action<TSource, TTarget>>(bodyExpr, paramSourceExpr, paramTargetExpr);
            return lambdaExpr;
        }
        public static Expression<Func<TSource, TTarget>> MapTo<TSource, TTarget>()
        {
            var sourceTypes = new Type[] { typeof(TSource) };
            GenerateExpresstion<TTarget>(sourceTypes, out ParameterExpression[] paramExprs, out Expression bodyExpr);

            var lambdaExpr
               = Expression.Lambda<Func<TSource, TTarget>>(bodyExpr, paramExprs);
            return lambdaExpr;
        }
        public static void GenerateExpresstion<TTarget>(Type[] sourceTypes, out ParameterExpression[] paramExprs, out Expression bodyExpr)
        {
            paramExprs = new ParameterExpression[sourceTypes.Length];

            var targetType = typeof(TTarget);
            var targetPropTypes = targetType.GetProperties().ToDictionary(x => x.Name.ToLower(), x => x);

            bodyExpr = null;
            var memberBindings = new List<MemberBinding>();
            var memberDic = new Dictionary<string, MemberBinding>();

            for (int i = 0; i < sourceTypes.Length; i++)
            {
                var sourceType = sourceTypes[i];
                var paramExpr = Expression.Parameter(sourceType, $"source{i + 1}");
                paramExprs[i] = paramExpr;

                foreach (var sourcePropInfo in sourceType.GetProperties())
                {
                    var targetPropName = sourcePropInfo.Name.ToLower();
                    if (targetPropTypes.TryGetValue(targetPropName, out var targetPropInfo)
                        && sourcePropInfo.CanRead
                        && targetPropInfo.CanWrite)
                    {
                        if (targetPropInfo.GetCustomAttribute<ReadOnlyAttribute>() != null)
                            continue;
                        Expression expression = Expression.Property(paramExpr, sourcePropInfo);
                        if (memberDic.TryGetValue(targetPropName, out var memberBinding))
                        {
                            memberBindings.Remove(memberBinding);
                            memberDic.Remove(targetPropName);
                        }

                        var sourcePropIsNullable = sourcePropInfo.PropertyType.IsGenericType
                                            && sourcePropInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                        var targetPropIsNullable = targetPropInfo.PropertyType.IsGenericType
                                            && targetPropInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                        if (sourcePropIsNullable && !targetPropIsNullable)
                        {
                            expression = Expression.IfThenElse(
                                Expression.Equal(expression, Expression.Constant(null)),
                                Expression.Constant(null),
                                Expression.Property(expression, "Value"));

                            //expression = Expression.Property(expression, "Value");
                        }

                        var memberAssignment = Expression.Bind(targetPropInfo, expression);

                        memberBindings.Add(memberAssignment);
                        memberDic.Add(targetPropName, memberAssignment);

                    }
                }
            }

            //ConditionalExpression   
            bodyExpr = Expression.MemberInit(Expression.New(targetType), memberBindings);
        }
    }

    /// <summary>
    /// 表达式扩展
    /// </summary> 
    public class ExpressionExtended<TResult>
    {
        public static IQueryable<TResult> GetTearPropQueryable<TSource>(IQueryable<TSource> queryable)
        {
            var expression = ExpressionExtended<TSource, TResult>.GetTearPropExpression();
            return queryable.Select(expression);
        }

        public static TResult TearProp<TSource>(TSource source)
        {
            return ExpressionExtended<TSource, TResult>.TearProp(source);
        }
    }



    /// <summary>
    /// 表达式扩展
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TTarget">目录类型</typeparam>
    public class ExpressionExtended<TSource, TTarget> : ExpressionExtended
    {
        #region 将source对象的属性,写到target对象的对应属性上
        private static Expression<Action<TSource, TTarget>> expressionMapSet;
        private static Action<TSource, TTarget> actionMapSet;

        /// <summary>
        /// 生成表达式树[将source对象的属性,写到target对象的对应属性上]
        /// </summary>
        /// <returns></returns>
        public static Expression<Action<TSource, TTarget>> GetMapSetExpression()
        {
            if (expressionMapSet == null)
                expressionMapSet = MapSet<TSource, TTarget>();
            return expressionMapSet;
        }
        /// <summary>
        /// 生成委托[将source对象的属性,写到target对象的对应属性上]
        /// </summary>
        /// <returns></returns>
        public static Action<TSource, TTarget> GetMapSetDelegate()
        {
            if (actionMapSet == null)
                actionMapSet = GetMapSetExpression().Compile();
            return actionMapSet;
        }
        /// <summary>
        /// 将source对象的属性,写到target对象的对应属性上
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目录对象</param>
        public static void MapSet(TSource source, TTarget target)
        {
            GetMapSetDelegate()(source, target);
        }
        #endregion

        #region 生成target类型的target对象,并将source对象的属性填充到target对象上
        private static Expression<Func<TSource, TTarget>> expressionMapTo;
        private static Func<TSource, TTarget> funcMapTo;

        /// <summary>
        /// 生成表达式树[生成target类型的target对象,并将source对象的属性填充到target对象上]
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<TSource, TTarget>> GetMapToExpression()
        {
            if (expressionMapTo == null)
                expressionMapTo = MapTo<TSource, TTarget>();
            return expressionMapTo;
        }

        /// <summary>
        /// 生成委托:[ 生成target类型的target对象,并将source对象的属性填充到target对象上]
        /// </summary>
        /// <returns></returns>
        public static Func<TSource, TTarget> GetMapToDelegate()
        {
            if (funcMapTo == null)
                funcMapTo = GetMapToExpression().Compile();
            return funcMapTo;
        }
        /// <summary>
        ///  生成target类型的target对象,并将source对象的属性填充到target对象上
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget MapTo(TSource source)
        {
            return GetMapToDelegate()(source);
        }

        #endregion

        #region 打散属性
        private static Expression<Func<TSource, TTarget>> tearPropExpression;

        private static Func<TSource, TTarget> tearPropFunc;

        public static Expression<Func<TSource, TTarget>> GetTearPropExpression()
        {
            if (tearPropExpression != null)
                return tearPropExpression;
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            var targetProps = targetType.GetProperties().ToDictionary(x => x.Name.ToLower());
            var parameterExpression = Expression.Parameter(sourceType, nameof(sourceType));
            var memberBindings = new Dictionary<string, MemberBinding>();
            foreach (var sourceProp in sourceType.GetProperties())
            {
                var memberExpression = Expression.Property(parameterExpression, sourceProp);
                var sourcePropTypes = sourceProp.PropertyType.GetProperties();
                foreach (var propertyInfo in sourcePropTypes)
                {
                    if (targetProps.TryGetValue(propertyInfo.Name.ToLower(), out var targetPropertyInfo) &&
                            targetPropertyInfo.PropertyType == propertyInfo.PropertyType)
                    {
                        var expression = Expression.Property(memberExpression, propertyInfo);
                        var memberAssignment = Expression.Bind(targetPropertyInfo, expression);

                        if (memberBindings.ContainsKey(propertyInfo.Name))
                        {
                            memberBindings[propertyInfo.Name] = memberAssignment;
                        }
                        else
                        {
                            memberBindings.Add(propertyInfo.Name, memberAssignment);
                        }
                    }
                }
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(targetType), memberBindings.Values);
            tearPropExpression = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, parameterExpression);
            return tearPropExpression;
        }

        public static Func<TSource, TTarget> GetTearPropDelegate()
        {
            if (tearPropFunc == null)
                tearPropFunc = GetTearPropExpression().Compile();

            return tearPropFunc;
        }

        public static TTarget TearProp(TSource source)
        {
            return GetTearPropDelegate()(source);
        }

        #endregion 
    }

    public class ExpressionClosureFactory
    {
        public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression expression)
            {
                rtn = ((MemberExpression)expression.Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression expression1)
            {
                rtn = expression1.Member.Name;
            }
            else if (expr.Body is ParameterExpression expression2)
            {
                rtn = expression2.Type.Name;
            }
            return rtn;
        }

        public static MemberExpression GetField<TValue>(TValue value)
        {
            var closure = new ExpressionClosureField<TValue>
            {
                value = value
            };

            return Expression.Field(Expression.Constant(closure), nameof(ExpressionClosureField<TValue>.value));
        }

        private class ExpressionClosureField<T>
        {
            public T value;
        }

        /// <summary>
        /// 生成取值表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Expression<Func<T, TValue>> ParseLambda<T, TValue>(Expression<Func<T, TValue>> expr)
        {
            return expr;
        }

        /// <summary>
        /// 生成取值表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="field_name"></param>
        /// <param name="para_name"></param>
        /// <returns></returns>
        public static Expression<Func<T, TValue>> ParseLambda<T, TValue>(string field_name, string para_name = "p")
        {
            var parameterExpression = Expression.Parameter(typeof(T), para_name);
            var memberExpression = Expression.PropertyOrField(parameterExpression, field_name);

            return Expression.Lambda<Func<T, TValue>>(memberExpression, parameterExpression);
        }

        /// <summary>
        /// 生成比较表达式
        /// </summary>
        /// <typeparam name="TBuild"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="compare_expression"></param>
        /// <param name="field_name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<TBuild, bool>> BuildCompareExpression<TBuild, TValue>(Func<Expression, Expression, BinaryExpression> compare_expression, string field_name, TValue value)
        {
            var field = ParseLambda<TBuild, TValue>(field_name);

            var binaryExpression = compare_expression(field.Body, GetField(value));

            var predicate = Expression.Lambda<Func<TBuild, bool>>(binaryExpression, field.Parameters);

            return predicate;
        }

        /// <summary>
        /// 生成比较表达式(==)
        /// </summary>
        /// <typeparam name="TBuild"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<TBuild, bool>> BuildEqualExpression<TBuild, TValue>(Expression<Func<TBuild, TValue>> expression, TValue value)
        {
            return BuildCompareExpression<TBuild, TValue>(Expression.Equal, GetPropertyName(expression), value);
        }

        /// <summary>
        /// 生成比较表达式(==)
        /// </summary>
        /// <typeparam name="TBuild"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="field_name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<TBuild, bool>> BuildEqualExpression<TBuild, TValue>(string field_name, TValue value)
        {
            return BuildCompareExpression<TBuild, TValue>(Expression.Equal, field_name, value);
        }

        /// <summary>
        /// 生成Contains表达式
        /// </summary>
        /// <typeparam name="TBuild"></typeparam>
        /// <typeparam name="TCompareFieldValue"></typeparam>
        /// <param name="left_field_name"></param> 
        /// <param name="compareFieldValues"></param>
        /// <returns></returns>
        public static Expression<Func<TBuild, bool>> BuildContainsExpression<TBuild, TCompareFieldValue>(
            string left_field_name,
            IQueryable<TCompareFieldValue> compareFieldValues)
        {
            var field_experssion = ParseLambda<TBuild, TCompareFieldValue>(left_field_name);
            return BuildContainsExpression<TBuild, TCompareFieldValue>(field_experssion, compareFieldValues);
        }

        /// <summary>
        /// 生成Contains表达式
        /// </summary>
        /// <typeparam name="TBuild"></typeparam>
        /// <typeparam name="TCompareFieldValue"></typeparam>
        /// <param name="left_field_expression"></param>
        /// <param name="compareFieldValues"></param>
        /// <returns></returns>
        public static Expression<Func<TBuild, bool>> BuildContainsExpression<TBuild, TCompareFieldValue>(
            Expression<Func<TBuild, TCompareFieldValue>> left_field_expression,
            IQueryable<TCompareFieldValue> compareFieldValues)
        {
            var method = typeof(Queryable)
                .GetMethods()
                .Where(v => v.Name == "Contains")
                .Where(v => v.GetParameters().Length == 2)
                .FirstOrDefault()
                ?.MakeGenericMethod(typeof(string));

            var left = GetField(compareFieldValues);
            var methodCallExpression = Expression.Call(method, left, left_field_expression.Body);

            var lambdaExpression = Expression.Lambda<Func<TBuild, bool>>(methodCallExpression, left_field_expression.Parameters);

            return lambdaExpression;
        }

        public static Expression<Func<TLeft, bool>> BuildAnyExpression<TLeft, TRight, TCompareFieldValue>(
            string left_field_name,
            string right_field_name,
            IQueryable<TRight> right_query)
        {
            var left_field_experssion = ParseLambda<TLeft, TCompareFieldValue>(left_field_name);
            var right_field_experssion = ParseLambda<TRight, TCompareFieldValue>(right_field_name);

            return BuildAnyExpression(left_field_experssion, right_field_experssion, right_query);
        }

        public static Expression<Func<TLeft, bool>> BuildAnyExpression<TLeft, TRight, TCompareFieldValue>(
            Expression<Func<TLeft, TCompareFieldValue>> left_field_experssion,
            Expression<Func<TRight, TCompareFieldValue>> right_field_experssion,
            IQueryable<TRight> right_query)
        {
            var method = typeof(Queryable)
                .GetMethods()
                .Where(v => v.Name == "Any")
                .Where(v => v.GetParameters().Length == 2)
                .FirstOrDefault()
                ?.MakeGenericMethod(typeof(TRight));

            var binaryExpression = Expression.Equal(left_field_experssion.Body, right_field_experssion.Body);
            var expression = Expression.Lambda<Func<TRight, bool>>(binaryExpression, right_field_experssion.Parameters);

            var memberExpression = GetField(right_query);
            var methodCallExpression = Expression.Call(method, memberExpression, expression);

            var lambdaExpression = Expression.Lambda<Func<TLeft, bool>>(methodCallExpression, left_field_experssion.Parameters);

            return lambdaExpression;
        }
    }
}
