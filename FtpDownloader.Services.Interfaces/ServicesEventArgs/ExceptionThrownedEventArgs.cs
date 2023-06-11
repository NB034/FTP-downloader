using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class ExceptionThrownedEventArgs : EventArgs
    {
        private readonly Exception _exception;

        public ExceptionThrownedEventArgs(Exception exception)
        {
            _exception = exception;
        }

        public Exception Exception => _exception;
    }
}
