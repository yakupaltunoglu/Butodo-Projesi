using System;

namespace ButodoProject.Core.Model.FixType
{
    public enum ResultStatusCode
    {
        None = 0,
        Success = 200,
        NotFound = 404,
        ValidationError = 800,
        RequiredLogin = 100,
        NotAuthorize = 403
    }
  
}