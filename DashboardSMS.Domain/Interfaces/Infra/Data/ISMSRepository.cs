using DashboardSMS.Domain.Entities.Data;
using System.Collections.Generic;

namespace DashboardSMS.Domain.Interfaces.Infra.Data
{
    public interface ISMSRepository
    {
        List<ConsultaOnlineSMS> ConsultaOnline();
        List<ConsultarStatusEnvioSMS> ConsultarStatusEnvio();
    }
}
