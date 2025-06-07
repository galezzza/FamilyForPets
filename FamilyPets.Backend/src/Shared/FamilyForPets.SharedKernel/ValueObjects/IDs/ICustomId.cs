namespace FamilyForPets.SharedKernel.ValueObjects.IDs
{
    public interface ICustomId<T>
        where T : CustomId<T>
    {
        static abstract T Create(Guid id);

        static abstract T Empty();

        static abstract T New();
    }
}