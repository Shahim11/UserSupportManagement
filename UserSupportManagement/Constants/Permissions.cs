using System.Collections.Generic;

namespace UserSupportManagement.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }

        public static class Concerns
        {
            public const string View = "Permissions.Concerns.View";
            public const string Create = "Permissions.Concerns.Create";
            public const string Edit = "Permissions.Concerns.Edit";
            public const string Delete = "Permissions.Concerns.Delete";
        }
    }
}
