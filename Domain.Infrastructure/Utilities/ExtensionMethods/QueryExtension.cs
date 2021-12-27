using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class QueryExtension
    {
        public static IQueryable<TEntity> WhereBySearchFilters<TEntity>(this IQueryable<TEntity> query, List<Entities.SearchFilter> filters)
        {
            QueryHelper helper = new QueryHelper();
            string filter = "";

            if (filters != null && filters.Count > 0)
            {
                filter = helper.SetFilter(filters);
            }

            if (!String.IsNullOrEmpty(filter))
            {
                query = query.Where(filter);
            }

            return query;
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, List<Siralama> orderList)
        {
            string strOrder = "";
            foreach (var order in orderList)
            {
                strOrder += String.Format("{0} {1},",order.KolonAdi, order.OrderType.ToString());
            }
            return query.OrderBy(strOrder.TrimEnd(','));
        }
    }

    public class QueryHelper
    {
        public string SetFilter(List<SearchFilter> filters, bool isAnd = false)
        {
            StringBuilder sb = new StringBuilder();
            bool isOR = false;
            if (filters != null)
            {
                foreach (var listItem in filters.Select(s => s.ID).Distinct())
                {
                    if (sb.Length > 0 || isAnd)
                        sb.Append(" AND (");
                    else
                        sb.Append("(");

                    isOR = false;
                    foreach (var item in filters.Where(w => w.ID == listItem))
                    {
                        if (String.IsNullOrEmpty(item.Value))
                            continue;

                        if (isOR)
                            sb.Append(" OR ");

                        switch (item.FilterType)
                        {
                            case FilterType.Text:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("{0}!=\"{1}\"", item.ID, item.Value);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}=\"{1}\"", item.ID, item.Value);
                                }
                                break;
                            case FilterType.StartWith:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("!({0}.StartsWith(\"{1}\"))", item.ID, item.Value);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}.StartsWith(\"{1}\")", item.ID, item.Value);
                                }
                                break;
                            case FilterType.EndWith:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("!({0}.EndsWith(\"{1}\"))", item.ID, item.Value);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}.EndsWith(\"{1}\")", item.ID, item.Value);
                                }
                                break;
                            case FilterType.Like:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("!({0}.Contains(\"{1}\"))", item.ID, item.Value);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}.Contains(\"{1}\")", item.ID, item.Value);
                                }
                                break;
                            case FilterType.Numeric:
                                if (String.IsNullOrEmpty(item.Value2))
                                {
                                    if (item.IsNot)
                                    {
                                        sb.AppendFormat("{0}!={1}", item.ID, item.Value);
                                    }
                                    else
                                    {
                                        sb.AppendFormat("{0}={1}", item.ID, item.Value);
                                    }
                                }
                                else
                                {
                                    if (item.IsNot)
                                    {
                                        sb.AppendFormat("!({0}>={1} AND {0}<={2})", item.ID, item.Value, item.Value2);
                                    }
                                    else
                                    {
                                        sb.AppendFormat("({0}>={1} AND {0}<={2})", item.ID, item.Value, item.Value2);
                                    }
                                }
                                break;
                            case FilterType.Date:
                                DateTime dt = item.Value.ToDateTime();
                                if (item.Value2.IsBlank())
                                {
                                    if (item.IsNot)
                                    {
                                        sb.AppendFormat("{0}!=DateTime({1}, {2}, {3})", item.ID, dt.Year, dt.Month, dt.Day);
                                    }
                                    else
                                    {
                                        sb.AppendFormat("{0}=DateTime({1}, {2}, {3})", item.ID, dt.Year, dt.Month, dt.Day);
                                    }
                                }
                                else
                                {
                                    DateTime dt2 = item.Value2.ToDateTime();
                                    if (item.IsNot)
                                    {
                                        sb.AppendFormat("!({0}>=DateTime({1}, {2}, {3}) AND {0}<=DateTime({4}, {5}, {6}))", item.ID, dt.Year, dt.Month, dt.Day, dt2.Year, dt2.Month, dt2.Day);
                                    }
                                    else
                                    {
                                        sb.AppendFormat("({0}>=DateTime({1}, {2}, {3}) AND {0}<=DateTime({4}, {5}, {6}))", item.ID, dt.Year, dt.Month, dt.Day, dt2.Year, dt2.Month, dt2.Day);
                                    }
                                }
                                break;
                            case FilterType.Guid:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("{0}!=Guid(\"{1}\")", item.ID, item.Value);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}=Guid(\"{1}\")", item.ID, item.Value);
                                }
                                break;
                            case FilterType.Bool:
                                if (item.IsNot)
                                {
                                    sb.AppendFormat("{0}!={1}", item.ID, item.Value.ToLower() == "true" ? item.Value.ToLower() : "false");
                                }
                                else
                                {
                                    sb.AppendFormat("{0}={1}", item.ID, item.Value.ToLower() == "true" ? item.Value.ToLower() : "false");
                                }
                                break;
                            default:
                                break;
                        }

                        isOR = true;
                    }
                    sb.Append(")");
                }
            }
            return sb.ToString();
        }

        public List<SqlParameter> SetFilterSqlParameter(out StringBuilder queryBuilder, List<SearchFilter> filters)
        {
            var prmLst = new List<SqlParameter>();
            queryBuilder = new StringBuilder();

            bool isOR = false;
            if (filters != null)
            {
                foreach (var listItem in filters.Select(s => s.ID).Distinct())
                {
                    queryBuilder.Append(" AND (");
                 
                    isOR = false;
                    foreach (var item in filters.Where(w => w.ID == listItem))
                    {
                        if (String.IsNullOrEmpty(item.Value))
                            continue;

                        SqlParameter prmSystemUserId = null;

                        switch (item.FilterType)
                        {
                            case FilterType.Text:
                                if (String.IsNullOrEmpty(item.Value2))
                                {
                                    if (item.IsNot)
                                    {
                                        prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        queryBuilder.AppendFormat(" {0} {1}!=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                    else
                                    {
                                        prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        queryBuilder.AppendFormat(" {0} {1}=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                }
                                break;
                            case FilterType.StartWith:
                                throw new System.Exception("Henüz geliştirilmedi!");
                                if (item.IsNot)
                                {
                                    //sb.AppendFormat("!({0}.StartsWith(\"{1}\"))", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                else
                                {
                                    //sb.AppendFormat("{0}.StartsWith(\"{1}\")", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                break;
                            case FilterType.EndWith:
                                throw new System.Exception("Henüz geliştirilmedi!");
                                if (item.IsNot)
                                {
                                    //sb.AppendFormat("!({0}.EndsWith(\"{1}\"))", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                else
                                {
                                    //sb.AppendFormat("{0}.EndsWith(\"{1}\")", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                break;
                            case FilterType.Like:
                                if (item.IsNot)
                                {
                                    prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                    queryBuilder.AppendFormat(" {0} {1} not like '%' + @{2} + '%' ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                }
                                else
                                {
                                    prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                    queryBuilder.AppendFormat(" {0} {1} like '%' + @{2} + '%' ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                }
                                break;
                            case FilterType.Numeric:
                                if (String.IsNullOrEmpty(item.Value2))
                                {
                                    if (item.IsNot)
                                    {
                                        prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        queryBuilder.AppendFormat(" {0} {1}!=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                    else
                                    {
                                        prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        queryBuilder.AppendFormat(" {0} {1}=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                }
                                else
                                {
                                    throw new System.Exception("Henüz geliştirilmedi!");
                                    if (item.IsNot)
                                    {
                                        //prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        //queryBuilder.AppendFormat(" {0} {1}!=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                    else
                                    {
                                        //prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                        //queryBuilder.AppendFormat(" {0} {1}=@{2} ", isOR ? "OR" : "", item.ID.Replace('_', '.'), prmSystemUserId.ParameterName);
                                    }
                                }
                                break;
                            case FilterType.Date:
                                throw new System.Exception("Henüz geliştirilmedi!");
                                DateTime dt = item.Value.ToDateTime();
                                if (item.Value2.IsBlank())
                                {
                                    if (item.IsNot)
                                    {
                                        //sb.AppendFormat("{0}!=DateTime({1}, {2}, {3})", item.ID, dt.Year, dt.Month, dt.Day);
                                        //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                        //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                    }
                                    else
                                    {
                                        //sb.AppendFormat("{0}=DateTime({1}, {2}, {3})", item.ID, dt.Year, dt.Month, dt.Day);
                                        //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                        //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                    }
                                }
                                else
                                {
                                    DateTime dt2 = item.Value2.ToDateTime();
                                    if (item.IsNot)
                                    {
                                        //sb.AppendFormat("!({0}>=DateTime({1}, {2}, {3}) AND {0}<=DateTime({4}, {5}, {6}))", item.ID, dt.Year, dt.Month, dt.Day, dt2.Year, dt2.Month, dt2.Day);
                                        //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                        //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                    }
                                    else
                                    {
                                        //sb.AppendFormat("({0}>=DateTime({1}, {2}, {3}) AND {0}<=DateTime({4}, {5}, {6}))", item.ID, dt.Year, dt.Month, dt.Day, dt2.Year, dt2.Month, dt2.Day);
                                        //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                        //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                    }
                                }
                                break;
                            case FilterType.Guid:
                                throw new System.Exception("Henüz geliştirilmedi!");
                                if (item.IsNot)
                                {
                                    //sb.AppendFormat("{0}!=Guid(\"{1}\")", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                else
                                {
                                    //sb.AppendFormat("{0}=Guid(\"{1}\")", item.ID, item.Value);
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                break;
                            case FilterType.Bool:
                                throw new System.Exception("Henüz geliştirilmedi!");
                                if (item.IsNot)
                                {
                                    //sb.AppendFormat("{0}!={1}", item.ID, item.Value.ToLower() == "true" ? item.Value.ToLower() : "false");
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                else
                                {
                                    //sb.AppendFormat("{0}={1}", item.ID, item.Value.ToLower() == "true" ? item.Value.ToLower() : "false");
                                    //prmSystemUserId = new SqlParameter("prmSystemUserId", item.Value);
                                    //query.AppendFormat(" {0} {1} like @{2} ", isOR ? "OR" : "", item.ID, prmSystemUserId.ParameterName);
                                }
                                break;
                            case FilterType.IsNull:
                                if (item.IsNot)
                                {
                                    prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                    queryBuilder.AppendFormat(" {0} {1} IS NOT NULL ", isOR ? "OR" : "", item.ID.Replace('_', '.'));
                                }
                                else
                                {
                                    prmSystemUserId = new SqlParameter("prm_" + Guid.NewGuid().ToString().Replace('-', '_'), item.Value);
                                    queryBuilder.AppendFormat(" {0} {1} IS NULL ", isOR ? "OR" : "", item.ID.Replace('_', '.'));
                                }
                                break;
                            default:
                                break;
                        }

                        prmLst.Add(prmSystemUserId);
                        isOR = true;
                    }
                    queryBuilder.Append(")");
                }
            }
            return prmLst;
        }
    }
}
