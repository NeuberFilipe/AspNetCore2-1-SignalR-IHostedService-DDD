using Dapper;
using DashboardSMS.Domain.Entities.Data;
using DashboardSMS.Domain.Interfaces.Infra.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DashboardSMS.Infra.Data
{
    public class SMSRepository : ISMSRepository
    {
        private readonly SMSContext _smsContext;

        public SMSRepository(SMSContext olaContext)
        {
            _smsContext = olaContext;
        }

        public List<ConsultaOnlineSMS> ConsultaOnline()
        {
            using (IDbConnection dbConnection = _smsContext.Connection)
            {
                string sQuery = @"select 
		                          from XXXX
		                          where XX.DataUltimaAtualizacao > GETDATE() - 5";

                return dbConnection.Query<ConsultaOnlineSMS>(sQuery).ToList();
            }
        }

        public List<ConsultarStatusEnvioSMS> ConsultarStatusEnvio()
        {
            using (IDbConnection dbConnection = _smsContext.Connection)
            {
                string sQuery = @"select 
                                  XXX
		                      from XXX
		                           XX
		                      where XX.DataUltimaAtualizacao > GETDATE() - 7
							       XX";

                return dbConnection.Query<ConsultarStatusEnvioSMS>(sQuery).ToList();
            }
        }
    }
}
