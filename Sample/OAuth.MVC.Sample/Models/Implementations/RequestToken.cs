using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core;

namespace OAuth.MVC.Sample.Models.Implementations
{
  public class RequestToken : TokenBase
  {
    public bool AccessDenied { get; set; }
    public bool UsedUp { get; set; }
    public AccessToken AccessToken { get; set; }
  }
}