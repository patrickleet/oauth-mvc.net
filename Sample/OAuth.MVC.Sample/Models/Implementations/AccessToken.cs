using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core;

namespace OAuth.MVC.Sample.Models.Implementations
{
  public class AccessToken : TokenBase
  {
      public string UserName { get; set; }
    public DateTime ExpireyDate { get; set; }
  }
}