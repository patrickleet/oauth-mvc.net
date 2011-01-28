using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.MVC.Sample.Models.Implementations;

namespace OAuth.MVC.Sample.Models.Repositories
{
  /// <summary>
  /// A simplistic in-memory repository for access and request token models - the example implementation of
  /// <see cref="ITokenStore" /> relies on this repository - normally you would make use of repositories
  /// wired up to your domain model i.e. NHibernate, Entity Framework etc.
  /// </summary>    
  public class TokenRepository
  {
    readonly Dictionary<string, AccessToken> _accessTokens = new Dictionary<string, AccessToken>();
    readonly Dictionary<string, RequestToken> _requestTokens = new Dictionary<string, RequestToken>();

    public RequestToken GetRequestToken(string token)
    {
      if(_requestTokens.ContainsKey(token))
        return _requestTokens[token];
      return null;
    }

    public AccessToken GetAccessToken(string token)
    {
      if(_accessTokens.ContainsKey(token))
         return _accessTokens[token];
      return null;
    }

    public void SaveRequestToken(RequestToken token)
    {
      _requestTokens[token.Token] = token;
    }

    public void SaveAccessToken(AccessToken token)
    {
      _accessTokens[token.Token] = token;
    }
  }
}