using Microsoft.AspNetCore.Http;
using Model.DBConstraint;
using System;

namespace Service.Modules.BaseModule
{
    public interface IAuthorization
    {
        void AuthorizeUserAccess(int loggedinRoleID, int moduleRole);
    }

    public class Authorization : IAuthorization
    {
        public static void AuthorizeUserAccess(int loggedinRoleID, int moduleRole)
        {
            if (loggedinRoleID != moduleRole)
            {
                throw new Exception(AlertConstraint.User.UnauthorizedAccess);
            }
        }
    }
}
