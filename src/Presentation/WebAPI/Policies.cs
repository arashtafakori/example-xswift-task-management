namespace Module.Presentation.Configuration.AuthDefinitions
{
    public class Policies
    {
        public const string ClientsConstraint = "clients-constraint";
        public const string ToAccessToTheDevelopmentFeatures = $"{ApplicationScopes.Development}";
        public const string ToAccessToTheSettings = $"{ApplicationScopes.ProjectSettings}";
        public const string ToAccessToTheBoradActitvitis = $"{ApplicationScopes.Board}";
    }
}
