namespace DashboardSMS.Domain.Enuns
{
    public enum TipoMensagemSMS
    {
        MensagemSmsSimNao = 1,//Primeiro Envio
        MensagemSmsTokenNao = 2, //Segundo Envio
        MensagemSMSInformativo = 3, //Primeiro Envio sem esperar resposta
        MensagemSMSClienteNaoRetido = 4 //Segundo Envio cliente nao retido
    }
}
