using System.Linq.Expressions;

namespace FastFrame.Infrastructure
{
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
