using System.Diagnostics.Metrics;

namespace Ez.UrlShortener.Application.Diagnostics
{
    public static class AppMeters
    {
        private const string METER_NAME = "UrlShortener.Api";
        public static readonly Meter ApiMeter = new(METER_NAME);
    }
}
