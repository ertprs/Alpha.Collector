using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Alpha.Collector.Utils
{
    public static class MemberAccessor
    {
        private static class Compiler<TModel, TProperty>
        {
            private static readonly ConcurrentDictionary<MemberInfo, Func<TModel, TProperty>> cache = new ConcurrentDictionary<MemberInfo, Func<TModel, TProperty>>();

            public static Func<TModel, TProperty> Compile(Expression<Func<TModel, TProperty>> e)
            {
                MemberExpression memberExpression = e.Body as MemberExpression;
                ExceptionExt.ThrowIfNull<MemberExpression>(memberExpression, "e.Body is not a MemberExpression");
                return cache.GetOrAdd(memberExpression.Member, (MemberInfo key) => e.Compile());
            }
        }

        private static class ModelCompiler<TModel>
        {
            private static readonly ConcurrentDictionary<MemberInfo, Func<TModel, object>> cache = new ConcurrentDictionary<MemberInfo, Func<TModel, object>>();

            public static Func<TModel, object> Compile(MemberInfo m)
            {
                return cache.GetOrAdd(m, delegate
                {
                    ParameterExpression parameterExpression = Expression.Parameter(typeof(TModel), "model");
                    return Expression.Lambda<Func<TModel, object>>((Expression)Expression.Convert(Expression.MakeMemberAccess(parameterExpression, m), typeof(object)), new ParameterExpression[1]
                    {
                    parameterExpression
                    }).Compile();
                });
            }
        }

        private static ConcurrentDictionary<string, Func<object, object[], object>> cache = new ConcurrentDictionary<string, Func<object, object[], object>>();

        private static ConcurrentDictionary<string, Delegate> dcache = new ConcurrentDictionary<string, Delegate>();

        public static object Process(MemberExpression e)
        {
            MemberExpression topMember = GetRootMember(e);
            if (topMember == null)
            {
                throw new InvalidOperationException("需计算的条件表达式只支持由 MemberExpression 和 ConstantExpression 组成的表达式");
            }
            if (topMember.Expression == null)
            {
                return cache.GetOrAdd(e.ToString(), (string key) => GetStaticProperty(e, topMember))(null, null);
            }
            return cache.GetOrAdd(e.ToString(), (string key) => GetInstanceProperty(e, topMember))((topMember.Expression as ConstantExpression).Value, null);
        }

        public static TProperty Process<TModel, TProperty>(Expression<Func<TModel, TProperty>> e, TModel instance)
        {
            return Compiler<TModel, TProperty>.Compile(e)(instance);
        }

        public static object Process<TModel>(TModel instance, MemberInfo m)
        {
            return ModelCompiler<TModel>.Compile(m)(instance);
        }

        public static ConstantExpression GetRootConstant(MemberExpression e)
        {
            if (e.Expression != null)
            {
                switch (e.Expression.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        return GetRootConstant(e.Expression as MemberExpression);
                    case ExpressionType.Constant:
                        return e.Expression as ConstantExpression;
                    default:
                        return null;
                }
            }
            return null;
        }

        public static MemberExpression GetRootMember(MemberExpression e)
        {
            if (e.Expression == null || e.Expression.NodeType == ExpressionType.Constant)
            {
                return e;
            }
            if (e.Expression.NodeType == ExpressionType.MemberAccess)
            {
                return GetRootMember(e.Expression as MemberExpression);
            }
            return null;
        }

        private static Func<object, object[], object> GetInstanceProperty(Expression e, MemberExpression topMember)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "local");
            ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "args");
            UnaryExpression expression = Expression.Convert(parameterExpression, topMember.Member.DeclaringType);
            MemberExpression newExpression = topMember.Update(expression);
            return Expression.Lambda<Func<object, object[], object>>(Expression.Convert(ExpressionModifier.Replace(e, topMember, newExpression), typeof(object)), new ParameterExpression[2]
            {
            parameterExpression,
            parameterExpression2
            }).Compile();
        }

        public static Func<object, object[], object> GetStaticProperty(Expression e, MemberExpression topMember)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "local");
            ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "args");
            return Expression.Lambda<Func<object, object[], object>>(Expression.Convert(e, typeof(object)), new ParameterExpression[2]
            {
            parameterExpression,
            parameterExpression2
            }).Compile();
        }
    }
}

