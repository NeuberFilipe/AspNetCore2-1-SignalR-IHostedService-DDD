using DashboardSMS.Domain.Entities.Data;
using DashboardSMS.Domain.Interfaces.Application;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonitoramentoDigital.Domain.Enuns;
using MonitoramentoDigital.Service.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashboardSMS.Service
{
    #region [ + ] - TimedHosted
    internal class TimedHostedService : IHostedService, IDisposable
    {
        #region [ + ] Properties
        private readonly ILogger _logger;
        private Timer _timer;
        IHubContext<MonitoramentoHub, ITypedHubClient> _monitoramentoHubContext;
        private readonly ISMSApplication _smsApplication;
        #endregion

        public TimedHostedService(ILogger<TimedHostedService> logger,
                                  IHubContext<MonitoramentoHub,
                                  ITypedHubClient> monitoramentoHubContex,
                                  ISMSApplication smsApplication)
        {
            _logger = logger;
            _monitoramentoHubContext = monitoramentoHubContex;
            _smsApplication = smsApplication;
        }

        #region [ + ] Start
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(15));
            return Task.CompletedTask;
        }
        #endregion

        #region [ + ] DoWork - Send SignalR
        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            #region [ + ] Query
            var query = _smsApplication.ConsultarStatusEnvio();
            #endregion

            var pierData = new List<PierViewModel>();
            var barData = new List<BarViewModel>();

            foreach (var item in query.Where(t => t.DataUltimaAtualizacao.Date == DateTime.Now.Date))
            {
                #region [ + ] SplitEnumName
                string descriptionEnum = GetNameEnum(item);
                var listEnum = descriptionEnum.Split(",");
                #endregion

                #region [ + ] Dia Semana
                var DiaSemana = GetDiaSemana(item.DataUltimaAtualizacao);
                #endregion

                #region [ + ] UpdateChart
                _monitoramentoHubContext.Clients.All.UpdateChart($"#knob-{listEnum[1]}", $"{item.Total}", $"{GetEnumDescription((TipoStatusEnvioSmsColor)item.TipoStatusEnvioSMS)}");
                #endregion

                #region [ + ] CreateObject - Pier
                pierData.Add(new PierViewModel
                {
                    value = item.Total,
                    label = listEnum[1],
                    color = GetEnumDescription((TipoStatusEnvioSmsColor)item.TipoStatusEnvioSMS),
                    highlight = GetEnumDescription((TipoStatusEnvioSmsColor)item.TipoStatusEnvioSMS)
                });
                #endregion
            }

            var dataTipoBar = new List<Tuple<string, int, string>>();
            var values = Enum.GetValues(typeof(TipoStatusEnvioSMS)).Cast<TipoStatusEnvioSMS>();

            #region [ + ] Pega Dias da Semana Atual
            List<DateTime> datasSemana = PegarDiasDaSemanaAtual();
            #endregion

            #region [ + ] Cria objetos da Semana

            foreach (var dataWeek in datasSemana)//seg,ter...
            {
                var dataUltimaAtualizacao = query.GroupBy(g => g.DataUltimaAtualizacao).Where(t => t.Key == dataWeek).Select(s => s.Key);

                if (dataUltimaAtualizacao.Count() > 0)
                {
                    foreach (var item2 in dataUltimaAtualizacao)//data
                    {
                        AdicionaTuple(query, dataTipoBar, values, item2);
                    }
                }
                else
                {
                    AdicionaTuple(query, dataTipoBar, values, dataWeek);
                }
            }
            #endregion

            #region [ + ] CreateObject - Bar
            foreach (var item in values)//girar por tipos 
            {
                var dadosTipoDia = dataTipoBar.Where(t => t.Item1.Split(',')[1] == ((TipoStatusEnvioSMS)item).ToString()).Select(s => new { DadosEnum = s.Item1, Total = s.Item2 }).ToList();

                barData.Add(new BarViewModel
                {
                    label = ((TipoStatusEnvioSMS)item).ToString(),
                    fillColor = (dadosTipoDia != null && dadosTipoDia.Count > 0) ? GetEnumDescription((TipoStatusEnvioSmsColor)int.Parse(dadosTipoDia.Select(s => s.DadosEnum).FirstOrDefault().Split(",")[0])) : string.Empty,
                    strokeColor = (dadosTipoDia != null && dadosTipoDia.Count > 0) ? GetEnumDescription((TipoStatusEnvioSmsColor)int.Parse(dadosTipoDia.Select(s => s.DadosEnum).FirstOrDefault().Split(",")[0])) : string.Empty,
                    pointColor = string.Empty,
                    pointStrokeColor = "rgba(60,141,188,1)",
                    pointHighlightFill = "#fff",
                    pointHighlightStroke = "rgba(60,141,188,1)",
                    data = dadosTipoDia.Select(s => s.Total).ToList(),
                    info = Guid.NewGuid().ToString()
                });
            }
            #endregion

            #region [ + ] UpdatePier
            _monitoramentoHubContext.Clients.All.UpdatePier(pierData);
            #endregion

            #region [ + ] UpdateBar
            _monitoramentoHubContext.Clients.All.UpdateBar(barData);
            #endregion
        }

        #region [ + ] Metodo Pega Dias da Semana
        private static List<DateTime> PegarDiasDaSemanaAtual()
        {
            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                                                          (int)DateTime.Today.DayOfWeek);

            List<DateTime> datasSemana = Enumerable
                                  .Range(0, 7)
                                  .Select(i => startOfWeek.AddDays(i).Date)
                                  .ToList();
            return datasSemana;
        }
        #endregion

        #region [ + ] AdicionaTuple
        private void AdicionaTuple(List<ConsultarStatusEnvioSMS> statusPainel, List<Tuple<string, int, string>> dataTipoBar, IEnumerable<TipoStatusEnvioSMS> values, DateTime item2)
        {
            foreach (var itemEnum in values)//enums
            {
                var tiposData = statusPainel.Where(s => s.TipoStatusEnvioSMS == (int)itemEnum && s.DataUltimaAtualizacao == item2).Distinct().FirstOrDefault();

                if (tiposData != null)
                    dataTipoBar.Add(new Tuple<string, int, string>(GetNameEnum(tiposData), tiposData.Total, GetDiaSemana(item2)));
                else
                    dataTipoBar.Add((new Tuple<string, int, string>($"{(int)itemEnum},{itemEnum.ToString()}", 0, GetDiaSemana(item2))));
            }
        }
        #endregion

        #endregion

        #region [ + ] DiaSemana
        private string GetDiaSemana(DateTime data)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            return dtfi.GetDayName(data.DayOfWeek);
        }
        #endregion

        #region [ + ] Enums

        private string GetEnumDescription<T>(T item)
        {
            var enumType = item.GetType();
            var field = enumType.GetField(item.ToString());
            if (field != null)
            {
                var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length == 0 ? item.ToString() : ((DescriptionAttribute)attributes[0]).Description;
            }
            return string.Empty;
        }

        private static string GetNameEnum(ConsultarStatusEnvioSMS item)
        {
            TipoStatusEnvioSMS enumDisplayStatus = EnumConvertToString(item);
            var tupleStringValue = $"{item.TipoStatusEnvioSMS},{enumDisplayStatus.ToString()}";
            return tupleStringValue;
        }

        private static TipoStatusEnvioSMS EnumConvertToString(ConsultarStatusEnvioSMS item)
        {
            return (TipoStatusEnvioSMS)item.TipoStatusEnvioSMS;
        }
        #endregion

        #region [ + ] Stop
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        #endregion

        #region [ + ] Dispose
        public void Dispose()
        {
            _timer?.Dispose();
        }
        #endregion
    }
    #endregion
}
