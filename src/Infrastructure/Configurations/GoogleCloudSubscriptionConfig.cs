namespace Infrastructure.Configurations
{
    public class GoogleCloudSubscriptionConfig
    {
        public const string SectionName = "GoogleCloudSubscription";
        public string ProjectId { get; set; } = default!;
        public IReadOnlyDictionary<string, string> EventSubscribersMap { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}
