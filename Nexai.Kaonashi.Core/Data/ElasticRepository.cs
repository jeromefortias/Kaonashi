﻿namespace Nexai.Kaonashi.Core.Data
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
        protected ESSServerSettings _ESconfig;


        public ElasticRepository(ESSServerSettings ESconfig)
        {
            _ESconfig = ESconfig;

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
                var uri = new Uri(_ESconfig.Url);
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.Password != null && _ESconfig.User != null)
                {
                    if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                    .DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
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

        public IReadOnlyCollection<TEntity> SearchTextInDocuments<TEntity>(string searchTerm) where TEntity : EntityBase
        {
            IReadOnlyCollection<TEntity> entities;
            bool fuzzy = true;
            int from = 0;
            int size = 25;
            try
            {
                var uri = new Uri(_ESconfig.Url);
                string indexName = "";
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.Password != null && _ESconfig.User != null)
                {
                    if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                indexName = _ESconfig.Prefix + typeof(TEntity).Name.ToLower();

                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                    .DefaultIndex(indexName);
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(indexName);
                }
                var client = new ElasticClient(settings);
                //TODO - Extend the search method
                var searchResponse3 = client.Search<TEntity>(s => s
                                    .Index(indexName)
                                    .From(from)
                                    .Size(size)
                                    .Query(q => q
                                        .MultiMatch(mm => mm
                                            .Query(searchTerm)
                                            .Fields(f => f.Field("*"))           
                                            .Type(TextQueryType.BestFields)
                                            .Operator(Operator.And)              
                                            .Lenient(true)                       
                                            .Fuzziness(Fuzziness.Auto)           
                                        )
                                    )
                                    .Sort(st => st
                                        .Descending(SortSpecialField.Score)      
                                    )
                                    .Highlight(h => h
                                        .PreTags("<mark>").PostTags("</mark>")
                                        .Fields(hf => hf.Field("*"))             
                                    )
                                );

                var hitslist = searchResponse3.Hits.ToList();
                entities = searchResponse3.Documents;
            }
            catch (Exception)
            {
                throw;
            }
            return entities;
        }


        public IReadOnlyCollection<TEntity> Get10LastDocuments<TEntity>() where TEntity : EntityBase
        {
            IReadOnlyCollection<TEntity> entity;
            try
            {
                var uri = new Uri(_ESconfig.Url);
                string indexName = "";
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.Password != null && _ESconfig.User != null)
                {
                    if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                indexName = _ESconfig.Prefix + typeof(TEntity).Name.ToLower();

                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                    .DefaultIndex(indexName);
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(indexName);
                }
                var client = new ElasticClient(settings);
                //TODO - Extend the search method
                var searchResponse3 = client.Search<TEntity>(s => s
            .Index(indexName)
            .Size(10)
            .Sort(ss => ss.Descending(p => p.Date))
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
                var uri = new Uri(_ESconfig.Url);
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.Password != null && _ESconfig.User != null)
                {
                    if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                    .DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);
                //TODO - Extend the search method
                var searchResponse3 = client.Search<TEntity>(s => s
                                                                .From(0)
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
            var uri = new Uri(_ESconfig.Url);
            bool hasPassword = false;
            // check as a password 
            if (_ESconfig.Password != null && _ESconfig.User != null)
            {
                if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                .DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);
            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }

        private string UpdateImpl<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string res = "";
            var uri = new Uri(_ESconfig.Url);
            bool hasPassword = false;
            // check as a password 
            if (_ESconfig.Password != null && _ESconfig.User != null)
            {
                if (_ESconfig.Password.Length > 0 && _ESconfig.User.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.User, _ESconfig.Password)
                .DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_ESconfig.Url)).DefaultIndex(_ESconfig.Prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);

            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }
    }
}
