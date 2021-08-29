using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqConsole
{
    /// <summary>
    /// IQueryable"T"的拓展操作
    /// </summary>
    public static partial class QueryableExtension
    {
        #region 动态组建Linq条件
        //扩展Where方法 
        //动态组建Linq条件  例如 s=>s.id=1 && s.name=="name" && s.time>="time" && s.time<"time"
        // 需要比较大小，属性名称命名规则，针对DateTime,float,int,long,decimal 类型
        // 最小值以后缀min(不区分大小写)结尾（如：a >= b），最大值后缀max(不区分大小写)结尾（如：a < b）
        public static IQueryable<TSource> WhereFilter<TSource, Query>(this IQueryable<TSource> source, Query filterQuery) where Query : class
        {
            ParameterExpression body = Expression.Parameter(typeof(TSource), "w");
            Expression condition = null;
            var parameterArray = filterQuery.GetType().GetProperties();
            foreach (var propertyName in parameterArray)
            {
                var val = propertyName.GetValue(filterQuery, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    //字符串类型 Contains
                    if (propertyName.PropertyType.Equals(typeof(string)))
                    {
                        //某个方法调用，如在 obj.sampleMethod() 表达式。
                        //创建一个表示调用带参数的方法的 MethodCallExpression。
                        Expression call = Expression.Call(
                            Expression.Property(body, typeof(TSource).GetProperty(propertyName.Name)),
                            typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                            Expression.Constant(val));
                        condition = condition != null ? Expression.And(call, condition) : call;
                    }
                    //大于或等于： DateTime,int,float,long,decimal 
                    else if (propertyName.Name.EndsWith("min", true, null)
                        && (propertyName.PropertyType.Equals(typeof(DateTime))
                        || propertyName.PropertyType.Equals(typeof(int))
                        || propertyName.PropertyType.Equals(typeof(float))
                        || propertyName.PropertyType.Equals(typeof(long))
                        || propertyName.PropertyType.Equals(typeof(decimal))
                        || propertyName.PropertyType.Equals(typeof(DateTime?))
                        || propertyName.PropertyType.Equals(typeof(int?))
                        || propertyName.PropertyType.Equals(typeof(float?))
                        || propertyName.PropertyType.Equals(typeof(long?))
                        || propertyName.PropertyType.Equals(typeof(decimal?))))
                    {
                        string compareNameKey = "";
                        //获取比较属性
                        Object[] compareName = propertyName.GetCustomAttributes(typeof(CompareNameAttribute), false);
                        foreach (CompareNameAttribute record in compareName)
                        {
                            compareNameKey = record.CompareName;
                            break;
                        }

                        //"大于或等于"比较，如 (a >= b)
                        Expression call = Expression.GreaterThanOrEqual(
                            Expression.Property(body, typeof(TSource).GetProperty(compareNameKey)),
                            Expression.Constant(val));
                        condition = condition != null ? Expression.And(call, condition) : call;
                    }
                    //小于： DateTime,int,float,long,decimal 
                    else if (propertyName.Name.EndsWith("max", true, null)
                        && (propertyName.PropertyType.Equals(typeof(DateTime))
                        || propertyName.PropertyType.Equals(typeof(int))
                        || propertyName.PropertyType.Equals(typeof(float))
                        || propertyName.PropertyType.Equals(typeof(long))
                        || propertyName.PropertyType.Equals(typeof(decimal)))
                        || propertyName.PropertyType.Equals(typeof(DateTime?))
                        || propertyName.PropertyType.Equals(typeof(int?))
                        || propertyName.PropertyType.Equals(typeof(float?))
                        || propertyName.PropertyType.Equals(typeof(long?))
                        || propertyName.PropertyType.Equals(typeof(decimal?)))
                    {
                        string compareNameKey = "";
                        //获取比较属性
                        Object[] compareName = propertyName.GetCustomAttributes(typeof(CompareNameAttribute), false);
                        foreach (CompareNameAttribute record in compareName)
                        {
                            compareNameKey = record.CompareName;
                            break;
                        }

                        //"小于"比较，如 (a < b)
                        Expression call = Expression.LessThan(
                            Expression.Property(body, typeof(TSource).GetProperty(compareNameKey)),
                            Expression.Constant(val));
                        condition = condition != null ? Expression.And(call, condition) : call;
                    }
                    //等于 DateTime,int,float,long,decimal
                    else if (propertyName.PropertyType.Equals(typeof(DateTime))
                        || propertyName.PropertyType.Equals(typeof(DateTime?))
                        ||
                        (int.Parse(val.ToString())!=0 && 
                        (propertyName.PropertyType.Equals(typeof(int))
                        || propertyName.PropertyType.Equals(typeof(float))
                        || propertyName.PropertyType.Equals(typeof(long))
                        || propertyName.PropertyType.Equals(typeof(decimal))
                        || propertyName.PropertyType.Equals(typeof(int?))
                        || propertyName.PropertyType.Equals(typeof(float?))
                        || propertyName.PropertyType.Equals(typeof(long?))
                        || propertyName.PropertyType.Equals(typeof(decimal?)))))
                    {
                        //一个表示相等比较，如节点 (a == b)
                        Expression call = Expression.Equal(
                            Expression.Property(body, typeof(TSource).GetProperty(propertyName.Name)),
                            Expression.Constant(val));
                        condition = condition != null ? Expression.And(call, condition) : call;
                    }
                    else if (propertyName.PropertyType.Equals(typeof(Guid)) && Guid.Parse(val.ToString()) != Guid.Empty)
                    {
                        //一个表示相等比较，如节点 (a == b)
                        Expression call = Expression.Equal(
                            Expression.Property(body, typeof(TSource).GetProperty(propertyName.Name)),
                            Expression.Constant(val));
                        condition = condition != null ? Expression.And(call, condition) : call;
                    }
                }
            }
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(condition, new ParameterExpression[] { body });
            return source.Where(lambda);
        }
        #endregion
    }
}
