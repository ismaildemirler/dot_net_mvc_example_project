using Nest;
using System;
using System.Collections.Generic;

namespace Domain.Infrastructure.ElasticSearch
{
    public interface IElasticRepository<T> where T : ElasticSearchBase
    {
        /// <summary>
        /// ElasticSearch üzerinde projeAdi.T adında index oluşturacaktır insert,search vs. öncesinde bir seferligine cagrılmalıdır.Prod ortamında çalışmayacaktır.
        /// Prod ortamında indexler altyapı ekibi tarafından oluşturalacaktır.
        /// </summary>
        /// <returns>Başarılı:True,Başarısız:False</returns>
        bool CreateIndex();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">Zorunlu alandır örnekler: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/query-dsl.html  
        /// Linq üzerinden sorgulama yapılır
        /// </param>
        /// <param name="from">Zorunlu değil default:0</param>
        /// <param name="size">Zorunlu değil default:1000</param>
        /// <param name="sortDescriptor">Zorunlu değil default:null örnek:https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/sort-usage.html </param>
        /// <param name="aggregationContainer">Zorunlu değil default:null örnek: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/reference-aggregations.html </param>
        /// <returns>Başarılı:List of T ,Başarısız: null</returns>
        IEnumerable<T> Search(QueryContainer query, int from=0, int size=1000, SortDescriptor<T> sortDescriptor = null, AggregationContainer aggregationContainer = null);

        IEnumerable<T> Search(String Json);
        System.Threading.Tasks.Task<(bool, string)> InsertAsync(T Entity);
        /// <summary>
        /// projeAdi.T index'ine insert eden method
        /// </summary>
        /// <param name="Entity">T Model alir ElasticId zorunludur</param>
        /// <returns>Tupple(bool,string) Başarılı:(true,null) ,Başarısız:(false,Exception) </returns>
        (bool, string) Insert(T Entity);

        /// <summary>
        /// projeAdi.T index'ine delete eden method elasticid dolu olması yeterlidir
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns>Tupple(bool,string) Başarılı:(true,null) ,Başarısız:(false,Exception) </returns>
        (bool, string) Delete(T Entity);

        /// <summary>
        /// projeAdi.T index'ine update eden method elasticid dolu olmak zorunda
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns>Tupple(bool,string) Başarılı:(true,null) ,Başarısız:(false,Exception) </returns>
        (bool, string) Update(T Entity);

        /// <summary>
        /// projeAdi.T index'ine update eden method elasticid dolu olmak zorunda
        /// </summary>
        /// <param name="Id">ElasticId</param>
        /// <returns>Model T </returns>
        T FindById(int Id);


    }
}
