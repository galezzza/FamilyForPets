using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel
{
    public abstract class SoftDeletableEntity<TId> : Entity<TId>
        where TId : IComparable<TId>
    {
        protected SoftDeletableEntity(TId id)
            : base(id)
        {
        }

        public bool IsDeleted { get; private set; }

        public DateTime? DeletionDate { get; private set; }

        public virtual void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }

        public virtual void Restore()
        {
            IsDeleted = false;
            DeletionDate = null;
        }
    }
}
