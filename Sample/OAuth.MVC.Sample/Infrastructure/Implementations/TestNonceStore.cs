using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;

namespace OAuth.MVC.Sample.Infrastructure.Implementations
{
  /// <summary>
  /// A simple nonce store that just tracks all nonces by consumer key in memory.
  /// </summary>
  internal class TestNonceStore : INonceStore
  {
      readonly Dictionary<string, List<string>> _nonces = new Dictionary<string, List<string>>();

      #region INonceStore Members

      public bool RecordNonceAndCheckIsUnique(IConsumer consumer, string nonce)
      {
          List<string> list = GetNonceListForConsumer(consumer.ConsumerKey);
          lock (list)
          {
              if (list.Contains(nonce)) return false;
              list.Add(nonce);
              return true;
          }
      }

      #endregion

      List<string> GetNonceListForConsumer(string consumerKey)
      {
          List<string> list;

          if (!_nonces.TryGetValue(consumerKey, out list))
          {
              lock (_nonces)
              {
                  if (!_nonces.TryGetValue(consumerKey, out list))
                  {
                      list = new List<string>();
                      _nonces[consumerKey] = list;
                  }
              }
          }

          return list;
      }
  }
}