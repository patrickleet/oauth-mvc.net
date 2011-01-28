using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;

namespace OAuth.MVC.Sample.Infrastructure.Implementations
{
  internal class SampleNonceStore:INonceStore
  {
    readonly Dictionary<string,List<string>> _noncesForConsumer = new Dictionary<string, List<string>>();
    public bool RecordNonceAndCheckIsUnique(IConsumer consumer, string nonce)
    {
      if(nonce==null||consumer.ConsumerKey==null)
        return true;
      if (!_noncesForConsumer.ContainsKey(consumer.ConsumerKey))
        _noncesForConsumer[consumer.ConsumerKey] = new List<string>();
      if(_noncesForConsumer[consumer.ConsumerKey].Contains(nonce))
        return false;
        _noncesForConsumer[consumer.ConsumerKey].Add(nonce);
        return true;
    }
  }
}