namespace Nexai.Kaonashi.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nexai.Kaonashi.Core.Models;
    using Nest;


    public class ElasticRepository : iElasticRepository<Log>
    {
        protected ESSServerSettings _config;


        public ElasticRepository(ESSServerSettings config)
        {
            _config = config;

        }

        public IEnumerable<string> Save<TEntity>(params TEntity[] entities)
            where TEntity : EntityBase
        {
            foreach (var entity in entities)
                yield return SaveImpl(entity);
        }

        public IEnumerable<string> Save<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : EntityBase
        {
            foreach (var entity in entities)
                yield return SaveImpl(entity);
        }

        public string Save<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            return SaveImpl(entity);
        }

        public Object Load<TEntity>(string id)
            where TEntity : EntityBase
        {
            return LoadImpl<TEntity>(id);
        }

        public IReadOnlyCollection<TEntity> Search<TEntity>(string freeText)
            where TEntity : EntityBase
        {
            return SearchImpl<TEntity>(freeText);
        }

        public string Save(Log entity)
        {
            return SaveImpl(entity);
        }
        private Object LoadImpl<TEntity>(string id) where TEntity : EntityBase
        {
            Object entity;
            try
            {
                var uri = new Uri(_config.Url);
                bool hasPassword = false;
                // check as a password 
                if (_config.Password != null && _config.User != null)
                {
                    if (_config.Password.Length > 0 && _config.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_config.User, _config.Password)
                    .DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_config.Url)).DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);

                var response = client.Get<TEntity>(id, g => g.Index(typeof(TEntity).Name.ToLower()));
                entity = response.Source;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }
        /// <summary>
        /// Search and get results based on fuzzysearch
        /// Have a look on the declaration of entities - IReadOnlyCollection<TEntity> !! 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="freeText"></param>
        /// <returns>List of objects from ElasticSearch</returns>
        private IReadOnlyCollection<TEntity> SearchImpl<TEntity>(string freeText) where TEntity : EntityBase
        {
            IReadOnlyCollection<TEntity> entity;
            try
            {
                var uri = new Uri(_config.Url);
                bool hasPassword = false;
                // check as a password 
                if (_config.Password != null && _config.User != null)
                {
                    if (_config.Password.Length > 0 && _config.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_config.User, _config.Password)
                    .DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_config.Url)).DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);
                //TODO - Extend the search method
                var searchResponse3 = client.Search<TEntity>(s => s
                                                                .From(0)
                                                                .Size(10)
                                                                .Size(10)
                                                                .Query(q => q
                                                                        .Fuzzy(c => c
                                                                            .Name("fuzzy")
                                                                            .Boost(1.1)
                                                                            .Field(p => p.Comment)
                                                                            .Fuzziness(Fuzziness.Auto)
                                                                            .Value(freeText)
                                                                            .MaxExpansions(100)
                                                                              )
                                                                      )
                                                            );
                entity = searchResponse3.Documents;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }
        /// <summary>
        /// Save
        /// </summary>
        private string SaveImpl<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string res = "";
            var uri = new Uri(_config.Url);
            bool hasPassword = false;
            // check as a password 
            if (_config.Password != null && _config.User != null)
            {
                if (_config.Password.Length > 0 && _config.User.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_config.User, _config.Password)
                .DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_config.Url)).DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);
            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }

        private string UpdateImpl<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string res = "";
            var uri = new Uri(_config.Url);
            bool hasPassword = false;
            // check as a password 
            if (_config.Password != null && _config.User != null)
            {
                if (_config.Password.Length > 0 && _config.User.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_config.User, _config.Password)
                .DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_config.Url)).DefaultIndex(_config.Prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);

            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }
    }
}
