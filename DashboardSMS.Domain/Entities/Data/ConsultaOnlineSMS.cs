using System;

namespace DashboardSMS.Domain.Entities.Data
{
    public class ConsultaOnlineSMS
    {
        public int TipoStatusEnvioSMS { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string LoginUsuario { get; set; }
    }
}
