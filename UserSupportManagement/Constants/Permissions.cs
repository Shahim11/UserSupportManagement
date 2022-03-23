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

        // [01] Permission For Concern
        public static class Concerns
        {
            public const string View = "Permissions.Concerns.View";
            public const string Create = "Permissions.Concerns.Create";
            public const string Edit = "Permissions.Concerns.Edit";
            public const string Delete = "Permissions.Concerns.Delete";
        }

        // [02] Permission For Vendor
        public static class Vendors
        {
            public const string View = "Permissions.Vendors.View";
            public const string Create = "Permissions.Vendors.Create";
            public const string Edit = "Permissions.Vendors.Edit";
            public const string Delete = "Permissions.Vendors.Delete";
        }

        // [03] Permission For ProblemType
        public static class ProblemTypes
        {
            public const string View = "Permissions.ProblemTypes.View";
            public const string Create = "Permissions.ProblemTypes.Create";
            public const string Edit = "Permissions.ProblemTypes.Edit";
            public const string Delete = "Permissions.ProblemTypes.Delete";
        }

        // [04] Permission For StatusType
        public static class StatusTypes
        {
            public const string View = "Permissions.StatusTypes.View";
            public const string Create = "Permissions.StatusTypes.Create";
            public const string Edit = "Permissions.StatusTypes.Edit";
            public const string Delete = "Permissions.StatusTypes.Delete";
        }

        // [05] Permission For Problem
        public static class Problems
        {
            public const string View = "Permissions.Problems.View";
            public const string Create = "Permissions.Problems.Create";
            public const string Edit = "Permissions.Problems.Edit";
            public const string Delete = "Permissions.Problems.Delete";
        }

        // [06] Permission For Solution
        public static class Solutions
        {
            public const string View = "Permissions.Solutions.View";
            public const string Create = "Permissions.Solutions.Create";
            public const string Edit = "Permissions.Solutions.Edit";
            public const string Delete = "Permissions.Solutions.Delete";
        }

    }
}
