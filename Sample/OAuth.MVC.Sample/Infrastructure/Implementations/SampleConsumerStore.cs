using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace OAuth.MVC.Sample.Infrastructure.Implementations
{
	  internal class SampleConsumerStore:IConsumerStore
  {
    public Dictionary<string,string> Secrets = new Dictionary<string, string>();
    public bool IsConsumer(IConsumer consumer)
    {
      return true;
    }

    public void SetConsumerSecret(IConsumer consumer, string consumerSecret)
    {
      Secrets[consumer.ConsumerKey] = consumerSecret;
    }

    public string GetConsumerSecret(IConsumer consumer)
    {
      if(!Secrets.ContainsKey(consumer.ConsumerKey))
        Secrets[consumer.ConsumerKey] = consumer.ConsumerKey + "Secret";
      return consumer.ConsumerKey;
    }

    public void SetConsumerCertificate(IConsumer consumer, X509Certificate2 certificate)
    {
      throw new NotImplementedException();
    }

    public X509Certificate2 GetConsumerCertificate(IConsumer consumer)
    {
      throw new NotImplementedException();
    }
  }
}