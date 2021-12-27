using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Domain.Infrastructure.ElasticSearch
{
    public class ElasticProvider<T> : IElasticRepository<T> where T : ElasticSearchBase
    {
        private ElasticClient _context;
        private readonly string _callingProjectName;

        public ElasticProvider()
        {
            _callingProjectName = "domainapp.";
            ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["ElasticSearchURI"])).DefaultIndex(_callingProjectName + typeof(T).Name.ToLower());
            connectionSettings.RequestTimeout(TimeSpan.FromSeconds(1));
            _context = new ElasticClient(connectionSettings);
        }

        public bool CreateIndex()
        { 
            if (!checkIndexIsExist())
            {
                var response = _context.Indices.Create(_callingProjectName + typeof(T).Name.Replace("'1","").Replace("'2", "").ToLower(), o => o.Map<T>(m => m.AutoMap<T>()));
                return response.IsValid;
            }
            return false;
        }
        public (bool, string) Delete(T Entity)
        {
            var deleteResponse = _context.Delete(new DeleteRequest<T>(Entity.ElasticId));
            return (deleteResponse.IsValid, deleteResponse.ServerError == null?null: deleteResponse.ServerError.ToString());
        }
        public T FindById(int ElasticId)
        {

            var query = Query<T>.Bool(o => o.Must(a => a.Term("ElasticId", ElasticId.ToString())));
            var searchResponse = _context.Search<T>(s =>

               s.Query(o => query)
              );
            return searchResponse.Documents.FirstOrDefault();
        }
        public (bool, string) Insert(T Entity)
        {
            IndexResponse insertResponse = new IndexResponse();
            if (checkIndexIsExist())
            {
                insertResponse = _context.Index<T>(new IndexRequest<T>(Entity, _callingProjectName + typeof(T).Name.ToLower()));
                return (insertResponse.IsValid, null);
            }
            return (false, insertResponse.ServerError == null ? null : insertResponse.ServerError.ToString() );
        }
        public async System.Threading.Tasks.Task<(bool, string)> InsertAsync(T Entity)
        {
            IndexResponse insertResponse = new IndexResponse();
            if (checkIndexIsExist())
            {
                insertResponse = await _context.IndexAsync<T>(new IndexRequest<T>(Entity, _callingProjectName + typeof(T).Name.ToLower()));
                return (insertResponse.IsValid, null);
            }
            return (false, insertResponse.ServerError == null ? null : insertResponse.ServerError.ToString());
        }
        public IEnumerable<T> Search(QueryContainer query, int from=0, int size=1000, SortDescriptor<T> sortDescriptor = null, AggregationContainer aggregationContainer = null)
        {
            var searchResponse = _context.Search<T>(s =>
            {
                s.From(from);
                s.Size(size);

                s.Query(o => query);
                s.Aggregations(o => aggregationContainer);
                return s;
            });
            return searchResponse.IsValid ? searchResponse.Documents : null;
        }
        public (bool, string) Update(T Entity)
        {
            var updateResponse = _context.Update<T>(DocumentPath<T>.Id(Entity.ElasticId), o => o.RetryOnConflict(3).Doc(Entity));
            return (updateResponse.IsValid, updateResponse.ServerError == null? null: updateResponse.ServerError.ToString());
        }
        private bool checkIndexIsExist()
        {
            return _context.Indices.Exists(_callingProjectName + typeof(T).Name.ToLower()).Exists;
        }

        public IEnumerable<T> Search(string Json)
        {
            var searchResponse = _context.Search<T>(s =>
            { 
                s.Query(k => k.Raw(Json)); 
                return s;
            });
            return searchResponse.IsValid ? searchResponse.Documents : null;
        }
    }
}
