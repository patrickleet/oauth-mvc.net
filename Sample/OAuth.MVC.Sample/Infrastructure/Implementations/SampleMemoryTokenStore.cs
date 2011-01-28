using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;
using OAuth.Core;
using OAuth.Core.Storage;
using OAuth.MVC.Sample.Models.Repositories;
using OAuth.MVC.Sample.Models.Implementations;


namespace OAuth.MVC.Sample.Infrastructure.Implementations
{
  internal class SampleMemoryTokenStore:ITokenStore
  {
 readonly TokenRepository _repository;

 public SampleMemoryTokenStore(TokenRepository repository)
    {
      _repository = repository;
    }

    #region ITokenStore Members

    public IToken CreateRequestToken(IOAuthContext context)
    {
      var token = new RequestToken
                    {
                      ConsumerKey = context.ConsumerKey,
                      Realm = context.Realm,
                      Token = Guid.NewGuid().ToString(),
                      TokenSecret = Guid.NewGuid().ToString(),
                      AccessDenied = true,
        };

      _repository.SaveRequestToken(token);

      return token;
    }

    public void ConsumeRequestToken(IOAuthContext requestContext)
    {
      RequestToken requestToken = _repository.GetRequestToken(requestContext.Token);

      if (requestToken.UsedUp)
      {
        throw new OAuthException(requestContext, OAuthProblems.TokenRejected,
                                 "The request token has already be consumed.");
      }
      if(!requestToken.AccessDenied)
        requestToken.UsedUp = true;

      _repository.SaveRequestToken(requestToken);
    }

    public void ConsumeAccessToken(IOAuthContext accessContext)
    {
      AccessToken accessToken = _repository.GetAccessToken(accessContext.Token);

      if (accessToken.ExpireyDate < DateTime.Now)
      {
        throw new OAuthException(accessContext, OAuthProblems.TokenExpired,
                                 "Token has expired (they're only valid for 1 minute)");
      }
    }

	public IToken CreateTwoLeggedAccessToken(IOAuthContext context)
	{
		var accessToken = new AccessToken
                            {
                              ConsumerKey = context.ConsumerKey,
                              ExpireyDate = DateTime.UtcNow.AddDays(20),
                              Realm = context.Realm,
                              Token = Guid.NewGuid().ToString(),
                              TokenSecret = Guid.NewGuid().ToString(),
                              UserName = Guid.NewGuid().ToString(),
                            };
        _repository.SaveAccessToken(accessToken);

		return accessToken;
	}

    public IToken GetAccessTokenAssociatedWithRequestToken(IOAuthContext requestContext)
    {
      RequestToken request = _repository.GetRequestToken(requestContext.Token);
      return request.AccessToken;
    }

    public RequestForAccessStatus GetStatusOfRequestForAccess(IOAuthContext accessContext)
    {
      RequestToken request = _repository.GetRequestToken(accessContext.Token);

      if (request.AccessDenied) return RequestForAccessStatus.Denied;

      if (request.AccessToken == null) return RequestForAccessStatus.Unknown;

      return RequestForAccessStatus.Granted;
    }

    public IToken GetToken(IOAuthContext context)
    {
      var token = (IToken)null;
      if (!string.IsNullOrEmpty(context.Token))
      {
        token = _repository.GetAccessToken(context.Token) ??
                (IToken)_repository.GetRequestToken(context.Token);
      }
      return token;
    }

    #endregion
  }
}