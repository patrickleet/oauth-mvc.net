using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using OAuth.Core.Interfaces;
using OAuth.Core.Provider.Inspectors;
using OAuth.Core;
using OAuth.Core.Provider;
using OAuth.MVC.Sample.Models.Repositories;
using OAuth.MVC.Sample.Infrastructure.Implementations;

namespace OAuth.MVC.Sample.Infrastructure
{
	internal class SampleModule : NinjectModule
  {
    public override void Load()
    {
      Bind<IOAuthContextBuilder>().To<OAuthContextBuilder>();
      var nonceStoreInspector = new NonceStoreInspector(new TestNonceStore());
      var consumerStore = new TestConsumerStore();
      var signatureInspector = new SignatureValidationInspector(consumerStore);
      var consumerValidationInspector = new ConsumerValidationInspector(consumerStore);
      var timestampInspector = new TimestampRangeInspector(new TimeSpan(1,0 , 0));
      var tokenRepository = new TokenRepository();
      var tokenStore = new SampleMemoryTokenStore(tokenRepository);
      var oauthProvider = new OAuthProvider(tokenStore,consumerValidationInspector, nonceStoreInspector,timestampInspector, signatureInspector);
      Bind<IOAuthProvider>().ToConstant(oauthProvider);
      Bind<TokenRepository>().ToConstant(tokenRepository);
    }
  }
}