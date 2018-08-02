using System.ComponentModel;

namespace MonitoramentoDigital.Domain.Enuns
{
    public enum TipoStatusEnvioSmsColor
    {
        [Description("#00802b")]
        Enviar = 1,
        [Description("#ffcc00")]
        Pendente = 2,
        [Description("#ffe066")]
        Reenviar = 3,
        [Description("#ff0000")]
        Parado = 4,
        [Description("#00b300")]
        Respondido = 50,
        [Description("#004d00")]
        RespostaLida = 6,
        [Description("#b38f00")]
        RespostaIncorreta = 7,
        [Description("#665200")]
        Reenviado = 8,
        [Description("#80ff80")]
        Entregue = 9,
        [Description("#c63939")]
        Falha = 10,
        [Description("#b94646")]
        Indisponivel = 11,
        [Description("#9f6060")]
        NaoEntregavel = 12,
        [Description("#936c6c")]
        Expirado = 13,
        [Description("#867979")]
        Rejeitado = 14
    }
}
