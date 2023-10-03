using Microsoft.AspNetCore.Http.Connections.Client;

namespace MadWorld.FlightSimulator.IOS.Extensions;

public static class HttpConnectionOptionsExtensions
{
    public static void IgnoreSsl(this HttpConnectionOptions options)
    {
        options.HttpMessageHandlerFactory = (message) =>
        {
            if (message is HttpClientHandler clientHandler)
                // always verify the SSL certificate
                clientHandler.ServerCertificateCustomValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
            return message;
        };
    }
}