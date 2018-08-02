using System;

namespace DashboardSMS.Domain.Entities.Data
{
    public class ConsultarStatusEnvioSMS
    {
        public int Total { get; set; }

        public int TipoStatusEnvioSMS { get; set; }

        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
