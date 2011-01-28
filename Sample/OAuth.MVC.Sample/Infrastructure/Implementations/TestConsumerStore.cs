using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;
using System.Security.Cryptography.X509Certificates;
using OAuth.MVC.Sample.Models;

namespace OAuth.MVC.Sample.Infrastructure.Implementations
{
  internal class TestConsumerStore : IConsumerStore
  {
      #region IConsumerStore Members

      public bool IsConsumer(IConsumer consumer)
      {
          return (consumer.ConsumerKey == "key" && string.IsNullOrEmpty(consumer.Realm));
      }

      public void SetConsumerSecret(IConsumer consumer, string consumerSecret)
      {
          throw new NotImplementedException();
      }

      public string GetConsumerSecret(IConsumer consumer)
      {
          return "secret";
      }

      public void SetConsumerCertificate(IConsumer consumer, X509Certificate2 certificate)
      {
          throw new NotImplementedException();
      }

      public X509Certificate2 GetConsumerCertificate(IConsumer consumer)
      {
          return TestCertificates.OAuthTestCertificate();
      }

      #endregion
  }
}