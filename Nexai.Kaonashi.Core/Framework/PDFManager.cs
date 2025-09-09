using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexai.Kaonashi.Core.Framework
{
    using Nexai.Kaonashi.Core.Data;
    using Nexai.Kaonashi.Core.Models;
    public class PDFManager : SessionManager
    {
        Config _config;
        public PDFManager(Config config) : base(config)
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
    }
}
