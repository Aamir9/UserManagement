using System;

namespace AutoSmartTechAPI.UserComm
{
    public static class UserComm
    {
        public static Guid NullableGuidAssigToGuid(Guid? userId)
        {
            return userId.HasValue ? userId.Value : Guid.Empty;
        }
    }
}
