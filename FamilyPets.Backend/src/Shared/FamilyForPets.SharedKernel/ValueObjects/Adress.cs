using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class Adress : ComparableValueObject
    {
        public const int MAX_ADRESS_TEXT_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private Adress(string houseNumber, string street, string city, string country)
        {
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            Country = country;
        }

        public string HouseNumber { get; } = default!;

        public string Street { get; } = default!;

        public string City { get; } = default!;

        public string Country { get; } = default!;

        public static Adress Empty() => new Adress(string.Empty, string.Empty, string.Empty, string.Empty);

        public static Result<Adress, Error> Create(string houseNumber, string street, string city, string country)
        {
            if (string.IsNullOrWhiteSpace(houseNumber))
                return Result.Failure<Adress, Error>(Errors.General.CannotBeEmpty("House number"));
            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<Adress, Error>(Errors.General.CannotBeEmpty("Street"));
            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<Adress, Error>(Errors.General.CannotBeEmpty("City"));
            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<Adress, Error>(Errors.General.CannotBeEmpty("Country"));
            return Result.Success<Adress, Error>(
                new Adress(houseNumber, street, city, country));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return HouseNumber;
            yield return Street;
            yield return City;
            yield return Country;
        }
    }
}
