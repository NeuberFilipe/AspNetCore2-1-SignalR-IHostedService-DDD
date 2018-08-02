using DashboardSMS.Domain.Enuns;
using MonitoramentoDigital.Domain.Enuns;
using System;
using System.ComponentModel.DataAnnotations;

namespace MonitoramentoDigital.Service.MVC.Models
{
    public class ConsultaOnlineViewModel
    {
        [Display(Name = "Status Envio")]
        public TipoStatusEnvioSMS TipoStatusEnvioSMS { get; set; }
        [Display(Name = "Data Envio")]
        public DateTime? DataEnvioSMS { get; set; }
        [Display(Name = "Data Ult. Atualização")]
        public DateTime? DataUltimaAtualizacao { get; set; }
        [Display(Name = "Data Envio Infobip")]
        public DateTime? DataEnvioInfoBip { get; set; }
        [Display(Name = "Data Rec. Cliente")]
        public DateTime? DataRecebimentoCliente { get; set; }
        [Display(Name = "Tipo Mensagem")]
        public TipoMensagemSMS TipoMensagemSMS { get; set; }
        [Display(Name = "Celular")]
        public string TelefoneCelular { get; set; }
        [Display(Name = "Usuario")]
        public string LoginUsuario { get; set; }
    }
}
