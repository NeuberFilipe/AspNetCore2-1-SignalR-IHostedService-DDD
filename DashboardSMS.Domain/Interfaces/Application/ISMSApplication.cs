using DashboardSMS.Domain.Entities.Data;
using System.Collections.Generic;

namespace DashboardSMS.Domain.Interfaces.Application
{
    public interface ISMSApplication
    {
        List<ConsultaOnlineSMS> ConsultarSMSOnline();
        List<ConsultarStatusEnvioSMS> ConsultarStatusEnvio();
    }
}
