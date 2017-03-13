using System;
using System.ServiceModel.Channels;
using Tridion.ContentManager.CoreService.Client;

namespace Coreservice.Client.CoreService
{
    public interface ICoreServiceFrameworkContext : IDisposable
    {
        //internal void intCoreServiceClient(Binding channelBinding, Uri endpointUri, NetworkCredential credentials);
        Uri EndpointUri { get; }
        ISessionAwareCoreService Client { get; }
        Binding GetBinding();
    }
}
