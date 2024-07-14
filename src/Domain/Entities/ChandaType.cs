using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChandaType: BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string? Description { get; private set; }
        public Guid IncomeAccountId { get; private set; }

        public ChandaType(string name, string code, string? description, Guid incomeAccountId, string createdBy)
        {
            Name = name;
            Code = code;
            Description = description;
            IncomeAccountId = incomeAccountId;
            CreatedBy = createdBy;
        }
    }
}
