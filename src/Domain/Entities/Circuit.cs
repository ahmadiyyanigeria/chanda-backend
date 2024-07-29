namespace Domain.Entities
{
    public class Circuit: BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public IReadOnlyList<Jamaat> Jamaats 
        { 
            get => _jamaats.AsReadOnly(); 
            private set => _jamaats.AddRange(value); 
        }

        private readonly List<Jamaat> _jamaats = [];

        public Circuit(string name, string code, string createdBy)
        {
            Name = name;
            Code = code;
            CreatedBy = createdBy;
        }

        public void AddJamaats(params Jamaat[] jamaats)
        {
            foreach (var jamaat in jamaats)
            {
                if(!_jamaats.Any(j => j.Id == jamaat.Id))
                {
                    _jamaats.Add(jamaat);
                }
            }
        }
    }
}
