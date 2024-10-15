using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class GoogleCloudEventConfig
    {
        public const string SectionName = "GoogleCloudEvent";
        public int RetryCount { get; set; } = default!;
        public string ProjectId { get; set; } = default!;
        public IReadOnlyDictionary<string, string> EventTopicMap { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}
