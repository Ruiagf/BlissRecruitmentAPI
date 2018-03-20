namespace BlissRecruitment.Infrastructure.Serialization
{
    using Newtonsoft.Json.Serialization;

    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}