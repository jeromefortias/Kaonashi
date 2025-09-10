namespace Nexai.Kaonashi.Core.Framework
{
    using Data;

    using Models.Corpus;
    using Nexai.Kaonashi.Core.Helpers;
    using System;
    using System.Collections.Generic;
    using Nexai.Kaonashi.Core.Models;
    using Nexai.Kaonashi.Core.Models.LLM;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SessionManager
    {
        private static SessionManager instance;
        private static object locker = new object();
        private Config _config;
        protected ESSServerSettings ESSServerParam;
        protected ElasticRepository ESSServer;

        public SessionManager(Config config)
        {
            _config = config;
            try
            {
                if (_config.DataRepository != null)
                {
                    ESSServerParam = _config.DataRepository;
                    ESSServer = new ElasticRepository(ESSServerParam);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SessionManager Get(Config config)
        {
            try
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new SessionManager(config);
                        }
                    }
                }
                return instance;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string LogSave(string comment, string appName, string type            )
        {
            Log log = new Log()
            {
                Comment = comment,
                AppName = appName,
                Date = DateTime.Now,
                UserName = Environment.UserName,
                MachineName = Environment.MachineName,
                Type = type,

            };
            return ESSServer.Save<Log>(log);
        }


        public bool DocumentExist(string documentFullName)
        {
            try
            {
                string hash = CryptoMgt.SHA256HashString(documentFullName);
                var r = ESSServer.Load<Document>(hash);
                if (r != null) return true;
                else return false;

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
        }


        public string ParagrapheSave(Paragraph p)
        {
            string ret = "";
            try
            {
                ret = ESSServer.Save(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ret = "";
            }
            return ret;
        }

        public List<Entity> EntitySearchByName(string name)
        {
            List<Entity> ret = new List<Entity>();
            try
            {
                ret =  ESSServer.SearchTextInDocuments<Entity>(name).ToList<Entity>();
            }
                        catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
             throw ex;
            }



            return ret;
        }

        public ServiceReturn EntitySave(Entity entity)
        {
            ServiceReturn ret = new ServiceReturn();
            if(entity.AlternativeNames != null)
            {
                entity.AlternativeNames.Add(entity.Name);
                entity.AlternativeNames = DedupCollection<string>(entity.AlternativeNames).ToList<string>();
            }
            string returnedId = "";
            try
            {
                returnedId = ESSServer.Save(entity);
                ret.ReturnedId = returnedId;
                ret.Message = "Entity Saved";
            }
            catch (Exception ex)
            {
                ret.ReturnedId = "-1";
                ret.Message = ex.Message;
                return ret;
            }
            return ret;
        }
        public string DocumentParagraphSave(Paragraph p)
        {
            string ret = "";
            try
            {
                ret = ESSServer.Save(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ret = "";
            }
            return ret;
        }
        public string DocumentSave(Document doc)
        {
            string ret = "";
            try
            {
                ret = ESSServer.Save(doc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                LogSave(_config.AppName, Environment.MachineName, Environment.UserName, "Document " + doc.Location + " is saved !", "#doc", "INFO");
                ret = "";
            }
            return ret;
        }

        public ServiceReturn LogSave(string AppName, string MachineName, string UserName, string Message, string tags, string Type)
        {
            ServiceReturn ret = new ServiceReturn();
            if (_config == null || ESSServer == null)
            {
                throw new Exception("Config not done");
            }
            Log log = new Log();
            log.Type = Type;
            log.MachineName = MachineName;
            log.UserName = UserName;
            log.AppName = AppName;
            log.Date = DateTime.Now;
            log.Comment = Message;
            ret = LogSave(log);
            return ret;
        }
        public ServiceReturn PerformanceSave(string AppName, string MachineName, string UserName, string Message, string tags, string Type, int durationMS, string componentpart, string componentname)
        {
            ServiceReturn ret = new ServiceReturn();
            if (_config == null || ESSServer == null)
            {
                throw new Exception("Config not done");
            }
            Performance perf = new Performance();
            perf.ComponentName = componentname;
            perf.ComponentType = componentpart;
            perf.durationMS = durationMS;
            perf.Type = Type;
            perf.MachineName = MachineName;
            perf.UserName = UserName;
            perf.AppName = AppName;
            perf.Date = DateTime.Now;
            perf.Comment = Message;
            ret = PerformanceSave(perf);
            return ret;
        }
        public ServiceReturn CompletionSave(string prompt, string completion, string modelName)
        {
            ServiceReturn ret = new ServiceReturn();
            if (_config == null || ESSServer == null)
            {
                throw new Exception("Config not done");
            }
            try
            {
                string retid = "";
                Completion com = new Completion();
                com.prompttext = prompt;
                com.completiontext = completion;
                com.modelName = modelName;
                com.UserName = Environment.UserName;
                com.Comment = "completion recorded";
                com.completionLenght = completion.Length;
                com.completionNbOfWords = StringMgt.WordsCountFromTextLine(com.completiontext);
                com.promptLenght = prompt.Length;
                com.promptNbOfWords = StringMgt.WordsCountFromTextLine(com.prompttext);
                com.Date = DateTime.Now;
                com.MachineName = Environment.MachineName;
                retid = ESSServer.Save(com);
                ret.Message = retid + " id created. Completion well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";
            }
            return ret;
        }

        public ServiceReturn PerformanceSave(Performance performance)
        {
            ServiceReturn ret = new ServiceReturn();

            if (_config == null || ESSServer == null)
            {
                throw new Exception("Config not done");
            }

            try
            {
                string retid = "";
                retid = ESSServer.Save(performance);
                ret.Message = retid + " id created. Log well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";
            }
            return ret;
        }




        public ServiceReturn LogSave(Log log)
        {
            ServiceReturn ret = new ServiceReturn();
            if (_config == null || ESSServer == null)
            {
                throw new Exception("Config not done");
            }

            try
            {
                string retid = "";
                retid = ESSServer.Save(log);
                ret.Message = retid + " id created. Log well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";

            }
            return ret;
        }

        public IEnumerable<T> DedupCollection<T>(IEnumerable<T> input)
        {
            var passedValues = new HashSet<T>();

            // Relatively simple dupe check alg used as example
            foreach (T item in input)
                if (passedValues.Add(item)) // True if item is new
                    yield return item;
        }

    }
}
