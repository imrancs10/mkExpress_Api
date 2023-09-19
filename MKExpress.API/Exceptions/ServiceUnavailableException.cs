using System;

namespace MKExpress.API.Exceptions
{

    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException(string healthReportStatus)
        {
            HealthReportStatus = healthReportStatus;
        }

        public string HealthReportStatus { get; set; }
    }
}