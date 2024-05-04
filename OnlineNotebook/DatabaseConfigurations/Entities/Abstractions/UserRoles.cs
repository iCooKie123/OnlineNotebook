namespace OnlineNotebook.DatabaseConfigurations.Entities.Abstractions
{
    public enum UserRoles
    {
        Admin,
        Student
    }

    public static class PolicyName
    {
        public const string RequireAnyRole = "RequireAnyRole";
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireStudentRole = "RequireStudentRole";
    }
}