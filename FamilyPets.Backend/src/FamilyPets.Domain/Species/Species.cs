namespace FamilyForPets.Domain.Species
{
    public class Species
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        private List<Breed> _breeds = [];
        public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();

        public Species(string name)
        {
            Name = name;
        }
    }
}
