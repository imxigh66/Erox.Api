using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Enums
{
    public enum ErrorCode
    {
        NotFound=404,
        ServerError=500,
        ValidationError=101,
        UnknownError=999,
        IdentityUserAlredyExists=201,
        IdentityCreationfailed=202,
       IdentityUserDoesNotExist=203,
       IncorrectPassword=204,
      
       PostUpdateNotPossible=300,
       PostDeleteNotPossible=301,
       InteractionRemovalNotAuthorized=302,
       UnauthorizedAccountRemoval=303,
        CommentRemovalNotAuthorized=304,
        AddReviewNotAuthorized=305


    }
}
