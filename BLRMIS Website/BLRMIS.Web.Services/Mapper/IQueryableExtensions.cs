using BLRMIS.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services.Mapper
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Order the IQueryable by the given property or field.
        /// </summary>

        #region Public Methods

        /// <typeparam name="T">The type of the IQueryable being ordered.</typeparam>
        /// <param name="queryable">The IQueryable being ordered.</param>
        /// <param name="propertyOrFieldName">The name of the property or field to order by.</param>
        /// <param name="ascending">
        /// Indicates whether or not the order should be ascending (true) or descending (false.)
        /// </param>
        /// <returns>Returns an IQueryable ordered by the specified field.</returns>
        public static IQueryable<T> OrderByPropertyOrField<T>
        (this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true)
        {
            var elementType = typeof(T);
            var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

            var parameterExpression = Expression.Parameter(elementType);
            var propertyOrFieldExpression =
                Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
            var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

            var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
                new[] { elementType, propertyOrFieldExpression.Type }, queryable.Expression, selector);

            return queryable.Provider.CreateQuery<T>(orderByExpression);
        }

        #endregion Public Methods

        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByFunc<T>(this KeyValuePair<string, SortingType> keyValuePair)
        {

            Func<IQueryable<T>, IOrderedQueryable<T>> result;

            var p1 = Expression.Parameter(typeof(T), "p1");
            var prop = Expression.PropertyOrField(p1, keyValuePair.Key);
            var lambada = Expression.Lambda<Func<T, object>>(prop, new ParameterExpression[] { p1 });

            //keyValuePair.Key is name of property in type of T that I should sort T by it
            switch (keyValuePair.Value)
            {
                case SortingType.Ascending:
                    result = source => source.OrderBy(lambada);
                    break;

                case SortingType.Descending:
                    result = source => source.OrderByDescending(lambada);
                    break;

                default:
                    throw new NotImplementedException();
                    break;
            }

            return result;
        }

        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByFunc<T>(string sortOrder)
        {
            Func<IQueryable<T>, IOrderedQueryable<T>> result = null;
            if (!string.IsNullOrEmpty(sortOrder))
            {
                string[] keyvalues = sortOrder.Split('=');

                var p1 = Expression.Parameter(typeof(T), "p1");
                MemberExpression prop;
                //prop = Expression.PropertyOrField(p1, keyvalues[0]);
                if (keyvalues[0].Contains("."))
                {
                    string[] properties = keyvalues[0].Split('.');
                    prop = Expression.PropertyOrField(p1, properties[0]);
                    prop = Expression.PropertyOrField(prop, properties[1]);
                }
                else
                {
                    prop = Expression.PropertyOrField(p1, keyvalues[0]);
                }


                var lambada = Expression.Lambda<Func<T, object>>
                    (Expression.Convert(prop, typeof(object)), p1);

                //(Expression.Call(prop, typeof(object).GetMethod("ToString")), 
                //new ParameterExpression[] { p1 });

                SortingType sortType = (SortingType)Enum.Parse(typeof(SortingType), keyvalues[1]);
                //keyValuePair.Key is name of property in type of T that I should sort T by it
                switch (sortType)
                {
                    case SortingType.Ascending:
                        result = source => source.OrderBy(lambada);
                        break;

                    case SortingType.Descending:
                        result = source => source.OrderByDescending(lambada);
                        break;

                    default:
                        throw new NotImplementedException();
                        break;
                }
            }
            return result;
        }

        public static Expression<Func<T, bool>> GetFilterByFunc<T>(string filterex)
        {
            Expression<Func<T, bool>> predicate;
            var li = Expression.Parameter(typeof(T));
            Expression where = null;
            string[] filters = filterex.Split(',');
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    if (filter.Contains("||"))
                    {
                        string[] betweenFilters = filter.Split("||");

                        string[] startDateKeyValues = betweenFilters[0].Split('=');
                        string[] endDateKeyValues = betweenFilters[1].Split('=');
                        if (startDateKeyValues.Length > 0 && endDateKeyValues.Length > 0)
                        {
                            if ((startDateKeyValues[0] != null && startDateKeyValues[0] != string.Empty) && (startDateKeyValues[1] != null && startDateKeyValues[1] != string.Empty))
                            {
                                var startDateProperty = Expression.Property(li, startDateKeyValues[0]);
                                var endDateProperty = Expression.Property(li, endDateKeyValues[0]);

                                var startDate = Convert.ToDateTime(startDateKeyValues[1]);
                                var endDate = Convert.ToDateTime(endDateKeyValues[1]).AddDays(1).AddMilliseconds(-1);

                                var after = Expression.LessThanOrEqual(startDateProperty,
                                    Expression.Constant(endDate, typeof(DateTime)));

                                var before = Expression.GreaterThanOrEqual(
                                    endDateProperty, Expression.Constant(startDate, typeof(DateTime)));

                                Expression expression = Expression.And(after, before);

                                if (where == null)
                                    where = expression;
                                else
                                    where = Expression.AndAlso(where, expression);
                            }
                        }
                    }
                    else if (filter.Contains("|"))
                    {
                        string[] orFilters = filter.Split('|');
                        Expression fExpression = null;
                        foreach (var orFilter in orFilters)
                        {
                            List<string> orkeyvalues = new List<string>(); 
                            if (orFilter.Contains("=="))
                                orkeyvalues = orFilter.Split("==").ToList();
                            else
                                orkeyvalues = orFilter.Split('=').ToList();

                            if (orkeyvalues.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(orkeyvalues[0]) && !string.IsNullOrEmpty(orkeyvalues[1]))
                                {
                                    var nameObjInList = Expression.Property(li, orkeyvalues[0]);
                                    System.Reflection.MethodInfo startsWithMethod = null;
                                    if (orFilter.Contains("=="))
                                        startsWithMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                    else
                                        startsWithMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                                    var nameSearch = Expression.Constant(orkeyvalues[1], typeof(string));

                                    var namemethod = Expression.Call(nameSearch, typeof(object).GetMethod("ToString"));
                                    var namelower = Expression.Call(namemethod, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                    var nameObjInList1 = Expression.Call(nameObjInList, typeof(object).GetMethod("ToString"));
                                    var nameObjInList2 = Expression.Call(nameObjInList1, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                    var startWithCall = Expression.Call(namelower,
                                                startsWithMethod, new Expression[1] { nameObjInList2 });
                                    var right = Expression.Constant(true, typeof(bool));
                                    var expression = Expression.Equal(startWithCall, right);

                                    var startWithCall2 = Expression.Call(Expression.Call(nameObjInList2, typeof(object).GetMethod("ToString")),
                                         startsWithMethod, new Expression[1] { namelower });
                                    var expression2 = Expression.Equal(startWithCall2, right);

                                    var likeExpression = Expression.Or(expression, expression2);

                                    if (fExpression == null)
                                        fExpression = likeExpression;
                                    else
                                        fExpression = Expression.Or(fExpression, likeExpression);

                                    //where = Expression.Or(where, expression2);
                                }
                            }
                        }
                        if (where == null)
                            where = fExpression;
                        else
                            where = Expression.AndAlso(where, fExpression);
                    }
                    else if (filter.Contains("=="))
                    {
                        string[] keyvalues = filter.Split("==");
                        if (keyvalues.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(keyvalues[0]) && !string.IsNullOrEmpty(keyvalues[1]))
                            {
                                var nameObjInList = Expression.Property(li, keyvalues[0]);
                                var startsWithMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                var nameSearch = Expression.Constant(keyvalues[1], typeof(string));

                                var namemethod = Expression.Call(nameSearch, typeof(object).GetMethod("ToString"));
                                //var namelower = Expression.Call(namemethod, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var nameObjInList1 = Expression.Call(nameObjInList, typeof(object).GetMethod("ToString"));
                                //var nameObjInList2 = Expression.Call(nameObjInList1, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var startWithCall = Expression.Call(Expression.Call(nameObjInList1, typeof(object).GetMethod("ToString")),
                                    startsWithMethod, new Expression[1] { namemethod });
                                var right = Expression.Constant(true, typeof(bool));
                                var expression = Expression.Equal(startWithCall, right);
                                if (where == null)
                                    where = expression;
                                else
                                    where = Expression.AndAlso(where, expression);
                            }
                        }
                    }
                    else if (filter.Contains("!="))
                    {
                        string[] keyvalues = filter.Split("!=");
                        if (keyvalues.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(keyvalues[0]) && !string.IsNullOrEmpty(keyvalues[1]))
                            {
                                var nameObjInList = Expression.Property(li, keyvalues[0]);
                                var startsWithMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                                var nameSearch = Expression.Constant(keyvalues[1], typeof(string));

                                var namemethod = Expression.Call(nameSearch, typeof(object).GetMethod("ToString"));
                                //var namelower = Expression.Call(namemethod, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var nameObjInList1 = Expression.Call(nameObjInList, typeof(object).GetMethod("ToString"));
                                //var nameObjInList2 = Expression.Call(nameObjInList1, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var startWithCall = Expression.Call(Expression.Call(nameObjInList1, typeof(object).GetMethod("ToString")),
                                    startsWithMethod, new Expression[1] { namemethod });
                                var right = Expression.Constant(true, typeof(bool));
                                var expression = Expression.NotEqual(startWithCall, right);
                                if (where == null)
                                    where = expression;
                                else
                                    where = Expression.AndAlso(where, expression);
                            }
                        }
                    }
                    else
                    {
                        string[] keyvalues = filter.Split('=');
                        if (keyvalues.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(keyvalues[0]) && !string.IsNullOrEmpty(keyvalues[1]))
                            {
                                var nameObjInList = Expression.Property(li, keyvalues[0]);
                                var startsWithMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                var nameSearch = Expression.Constant(keyvalues[1], typeof(string));

                                var namemethod = Expression.Call(nameSearch, typeof(object).GetMethod("ToString"));
                                var namelower = Expression.Call(namemethod, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var nameObjInList1 = Expression.Call(nameObjInList, typeof(object).GetMethod("ToString"));
                                var nameObjInList2 = Expression.Call(nameObjInList1, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                                var startWithCall = Expression.Call(Expression.Call(nameObjInList2, typeof(object).GetMethod("ToString")),
                                    startsWithMethod, new Expression[1] { namelower });
                                var right = Expression.Constant(true, typeof(bool));
                                var expression = Expression.Equal(startWithCall, right);
                                if (where == null)
                                    where = expression;
                                else
                                    where = Expression.AndAlso(where, expression);
                            }
                        }
                    }
                }
            }
            //  Expression.Lambda<Func<T, bool>>(body, item);
            if (where == null)
                return null;
            else
                predicate = Expression.Lambda<Func<T, bool>>(where, li);

            return predicate;
        }

        public static void GetFilters<T>(string param, ref Expression<Func<T, bool>> predicate,
           ref Func<IQueryable<T>, IOrderedQueryable<T>> sortOrder, ref int index, ref int pageSize)
        {

            if (!string.IsNullOrEmpty(param))
            {
                string[] paramex = param.Split(';');
                if (paramex.Length > 0)
                {
                    string[] filter = paramex[0].Split(':');
                    if (filter.Length > 0)
                    {
                        predicate = GetFilterByFunc<T>(filter[1]);
                    }
                    string[] sort = paramex[1].Split(':');
                    if (sort.Length > 0)
                    {
                        sortOrder = GetOrderByFunc<T>(sort[1]);
                    }
                    pageSize = int.Parse(paramex[2].Split(':')[1]);
                    index = int.Parse(paramex[3].Split(':')[1]);
                }
            }
        }

    }
}
