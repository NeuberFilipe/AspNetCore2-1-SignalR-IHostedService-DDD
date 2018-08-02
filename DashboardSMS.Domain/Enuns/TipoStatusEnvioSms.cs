namespace MonitoramentoDigital.Domain.Enuns
{
    public enum TipoStatusEnvioSMS
    {
        Enviar = 1,
        Pendente = 2,
        Reenviar = 3,
        Parado = 4,
        Respondido = 50 ,
        RespostaLida = 6,
        RespostaIncorreta = 7,
        Reenviado = 8,
        Entregue = 9,
        Falha = 10,
        Indisponivel = 11,
        NaoEntregavel = 12,
        Expirado = 13,
        Rejeitado = 14
    }
}
