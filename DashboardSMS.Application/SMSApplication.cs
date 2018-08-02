using DashboardSMS.Domain.Entities.Data;
using DashboardSMS.Domain.Interfaces.Application;
using DashboardSMS.Domain.Interfaces.Infra.Data;
using System.Collections.Generic;

namespace DashboardSMS.Application
{
    public class SMSApplication : ISMSApplication
    {
        private readonly ISMSRepository _smsRepository;
        public SMSApplication(ISMSRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }

        public List<ConsultaOnlineSMS> ConsultarSMSOnline()
        {
            return _smsRepository.ConsultaOnline();
        }

        public List<ConsultarStatusEnvioSMS> ConsultarStatusEnvio()
        {
            return _smsRepository.ConsultarStatusEnvio();
        }
    }
}
