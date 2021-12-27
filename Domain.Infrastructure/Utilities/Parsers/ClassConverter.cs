using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Infrastructure.Utilities.Parsers
{
    /// <summary>
    /// kaynak classı sonuç türüne çevirir
    /// </summary>
    /// <typeparam name="T">Kaynak Class</typeparam>
    /// <typeparam name="TT">Sonuç Class</typeparam>
    public static class ClassConverter
    {
        public static TT Convert<T, TT>(this T item) where T : class
        {
            return JsonConvert.DeserializeObject<TT>(JsonConvert.SerializeObject(item, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
        public static List<TT> ConvertList<T, TT>(this List<T> item) where T : class
        {
            var list = new List<TT>();

            foreach (var i in item)
            {
                list.Add(Convert<T, TT>(i));
            }
            return list;
        }
        public static IList<TT> ConvertIList<T, TT>(this IList<T> item) where T : class
        {
            var list = new List<TT>();

            foreach (var i in item)
            {
                list.Add(Convert<T, TT>(i));
            }
            return (IList<TT>)list;
        }
        public static PagedList<TT> ConvertPagedList<T, TT>(this PagedList<T> item) where T : class
        {
            var pagedList = new PagedList<TT>()
            {
                Items = item.Items.ConvertIList<T, TT>(),
                PageIndex = item.PageIndex,
                TotalCount = item.TotalCount,
                IndexFrom = item.IndexFrom,
                PageSize = item.PageSize,
                TotalPages = item.TotalPages
            };
            return pagedList;
        }
    }

}
