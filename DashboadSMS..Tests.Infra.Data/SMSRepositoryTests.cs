using DashboardSMS.Infra.Data;
using System;
using Xunit;

namespace DashboadSMS.Tests.Infra.Data
{
    public class SMSRepositoryTests
    {
        public SMSContext _context { get; set; }
        public SMSRepository _smsRepository { get; set; }

        public SMSRepositoryTests()
        {
            _context = new SMSContext("Data Source=,;Initial Catalog=;User Id=;Password=");
            _smsRepository = new SMSRepository(_context);
        }

        [Fact(DisplayName = "Teste 1")]
        public void ConsultaOnlineTest()
        {
            _smsRepository.ConsultaOnline();
        }
    }
}
