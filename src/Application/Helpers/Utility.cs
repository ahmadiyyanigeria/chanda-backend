using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Utility
    {
        public static string GenerateReference(string? prefix)
        {
            var reference = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7).ToUpper();
            return string.IsNullOrEmpty(prefix) ? reference : $"{prefix}-{reference}";
        }
    }
}
