using BookStore.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace BookStore.Repositary
{
    public class MessageRepositary : IMessageRepositary
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfiguration;
        public MessageRepositary(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration;
            //newBookAlertConfiguration.OnChange(config =>
            //{
            //    _newBookAlertConfiguration = config;
            //});
        }
        public string GetName()
        {
            return _newBookAlertConfiguration.CurrentValue.BookName;
        }
    }
}
