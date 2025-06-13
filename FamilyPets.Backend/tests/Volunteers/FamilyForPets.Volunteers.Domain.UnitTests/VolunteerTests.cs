using System.Drawing;
using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.PetValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;

namespace FamilyForPets.Volunteers.Domain.UnitTests
{
    public class VolunteerTests
    {
        [Fact]
        public void CreateNewPet_Success()
        {
            // arrange
            FullName fullname = FullName.Create("test name", "test surname", null).Value;
            EmailAdress emailAdress = EmailAdress.Create("text@test.test").Value;
            PhoneNumber phoneNumber = PhoneNumber.Create("123456789").Value;
            DetailsForPayment detailsForPaymentess = DetailsForPayment.Create("test details", null).Value;

            Volunteer volunteer = Volunteer.Create(
                fullname, emailAdress, 1, phoneNumber, detailsForPaymentess).Value;

            PetNickname name = PetNickname.Create("test nickname").Value;
            PelageColor color = PelageColor.Create(Color.Black, null, null).Value;
            PetBreedAndSpecies petBreed = PetBreedAndSpecies.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
            CastrationStatus castrationStatus = CastrationStatus.Neutered;
            HelpStatus helpStatus = HelpStatus.HelpNeeded;

            int petCountBefore = volunteer.AllPets.Count();

            // act
            Result<PetId, Error> result = volunteer.CreateNewPet(
                name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            int petCountAfter = volunteer.AllPets.Count();

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(petCountBefore + 1, petCountAfter);
            Assert.Equal(volunteer.AllPets.Last().Id, result.Value);
        }

        [Fact]
        public void ChangePetPosition_Pet3_To6Position_IsCorrect()
        {
            // arrange
            Volunteer volunteer = CreateTestVolunteerWith6Pets();

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];
            Pet pet2 = petsBefore[1];
            Pet pet3 = petsBefore[2];
            Pet pet4 = petsBefore[3];
            Pet pet5 = petsBefore[4];
            Pet pet6 = petsBefore[5];

            // act
            volunteer.ChangePetPosition(pet3, PetPosition.Create(6).Value);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);
            Pet pet2After = petsAfter.First(p => p.Id == pet2.Id);
            Pet pet3After = petsAfter.First(p => p.Id == pet3.Id);
            Pet pet4After = petsAfter.First(p => p.Id == pet4.Id);
            Pet pet5After = petsAfter.First(p => p.Id == pet5.Id);
            Pet pet6After = petsAfter.First(p => p.Id == pet6.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
            Assert.Equal(PetPosition.Create(2).Value, pet2After.PetPosition);
            Assert.Equal(PetPosition.Create(3).Value, pet4After.PetPosition);
            Assert.Equal(PetPosition.Create(4).Value, pet5After.PetPosition);
            Assert.Equal(PetPosition.Create(5).Value, pet6After.PetPosition);
            Assert.Equal(PetPosition.Create(6).Value, pet3After.PetPosition);
        }

        [Fact]
        public void ChangePetPosition_Pet5_To2Position_IsCorrect()
        {
            // arrange
            Volunteer volunteer = CreateTestVolunteerWith6Pets();

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];
            Pet pet2 = petsBefore[1];
            Pet pet3 = petsBefore[2];
            Pet pet4 = petsBefore[3];
            Pet pet5 = petsBefore[4];
            Pet pet6 = petsBefore[5];

            // act
            volunteer.ChangePetPosition(pet5, PetPosition.Create(2).Value);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);
            Pet pet2After = petsAfter.First(p => p.Id == pet2.Id);
            Pet pet3After = petsAfter.First(p => p.Id == pet3.Id);
            Pet pet4After = petsAfter.First(p => p.Id == pet4.Id);
            Pet pet5After = petsAfter.First(p => p.Id == pet5.Id);
            Pet pet6After = petsAfter.First(p => p.Id == pet6.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
            Assert.Equal(PetPosition.Create(2).Value, pet5After.PetPosition);
            Assert.Equal(PetPosition.Create(3).Value, pet2After.PetPosition);
            Assert.Equal(PetPosition.Create(4).Value, pet3After.PetPosition);
            Assert.Equal(PetPosition.Create(5).Value, pet4After.PetPosition);
            Assert.Equal(PetPosition.Create(6).Value, pet6After.PetPosition);
        }

        [Fact]
        public void ChangePetPosition_Pet_ToSamePosition_IsCorrect()
        {
            // arrange
            Volunteer volunteer = CreateTestVolunteerWith6Pets();

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];
            Pet pet2 = petsBefore[1];
            Pet pet3 = petsBefore[2];
            Pet pet4 = petsBefore[3];
            Pet pet5 = petsBefore[4];
            Pet pet6 = petsBefore[5];

            // act
            volunteer.ChangePetPosition(pet3, PetPosition.Create(3).Value);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);
            Pet pet2After = petsAfter.First(p => p.Id == pet2.Id);
            Pet pet3After = petsAfter.First(p => p.Id == pet3.Id);
            Pet pet4After = petsAfter.First(p => p.Id == pet4.Id);
            Pet pet5After = petsAfter.First(p => p.Id == pet5.Id);
            Pet pet6After = petsAfter.First(p => p.Id == pet6.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
            Assert.Equal(PetPosition.Create(2).Value, pet2After.PetPosition);
            Assert.Equal(PetPosition.Create(3).Value, pet3After.PetPosition);
            Assert.Equal(PetPosition.Create(4).Value, pet4After.PetPosition);
            Assert.Equal(PetPosition.Create(5).Value, pet5After.PetPosition);
            Assert.Equal(PetPosition.Create(6).Value, pet6After.PetPosition);
        }


        [Fact]
        public void ChangePetPosition_WhenOnlyOnePet_IsCorrect()
        {
            // arrange
            Volunteer volunteer = CreateTestVolunteerWithoutPets();

            PetNickname name = PetNickname.Create("test nickname").Value;
            PelageColor color = PelageColor.Create(Color.Black, null, null).Value;
            PetBreedAndSpecies petBreed = PetBreedAndSpecies.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
            CastrationStatus castrationStatus = CastrationStatus.Neutered;
            HelpStatus helpStatus = HelpStatus.HelpNeeded;

            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];

            // act
            volunteer.ChangePetPositionToTheVeryBegging(pet1);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
        }

        [Fact]
        public void ChangePetPosition_ByOneToBegining_IsCorrect()
        {
            Volunteer volunteer = CreateTestVolunteerWith6Pets();

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];
            Pet pet2 = petsBefore[1];
            Pet pet3 = petsBefore[2];
            Pet pet4 = petsBefore[3];
            Pet pet5 = petsBefore[4];
            Pet pet6 = petsBefore[5];

            // act
            volunteer.ChangePetPosition(pet3, PetPosition.Create(2).Value);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);
            Pet pet2After = petsAfter.First(p => p.Id == pet2.Id);
            Pet pet3After = petsAfter.First(p => p.Id == pet3.Id);
            Pet pet4After = petsAfter.First(p => p.Id == pet4.Id);
            Pet pet5After = petsAfter.First(p => p.Id == pet5.Id);
            Pet pet6After = petsAfter.First(p => p.Id == pet6.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
            Assert.Equal(PetPosition.Create(2).Value, pet3After.PetPosition);
            Assert.Equal(PetPosition.Create(3).Value, pet2After.PetPosition);
            Assert.Equal(PetPosition.Create(4).Value, pet4After.PetPosition);
            Assert.Equal(PetPosition.Create(5).Value, pet5After.PetPosition);
            Assert.Equal(PetPosition.Create(6).Value, pet6After.PetPosition);
        }

        [Fact]
        public void ChangePetPosition_ByOneToEnd_IsCorrect()
        {
            Volunteer volunteer = CreateTestVolunteerWith6Pets();

            List<Pet> petsBefore = volunteer.AllPets.ToList();
            Pet pet1 = petsBefore[0];
            Pet pet2 = petsBefore[1];
            Pet pet3 = petsBefore[2];
            Pet pet4 = petsBefore[3];
            Pet pet5 = petsBefore[4];
            Pet pet6 = petsBefore[5];

            // act
            volunteer.ChangePetPosition(pet3, PetPosition.Create(4).Value);

            List<Pet> petsAfter = volunteer.AllPets.ToList();
            Pet pet1After = petsAfter.First(p => p.Id == pet1.Id);
            Pet pet2After = petsAfter.First(p => p.Id == pet2.Id);
            Pet pet3After = petsAfter.First(p => p.Id == pet3.Id);
            Pet pet4After = petsAfter.First(p => p.Id == pet4.Id);
            Pet pet5After = petsAfter.First(p => p.Id == pet5.Id);
            Pet pet6After = petsAfter.First(p => p.Id == pet6.Id);

            // assert
            Assert.Equal(PetPosition.Create(1).Value, pet1After.PetPosition);
            Assert.Equal(PetPosition.Create(2).Value, pet2After.PetPosition);
            Assert.Equal(PetPosition.Create(3).Value, pet4After.PetPosition);
            Assert.Equal(PetPosition.Create(4).Value, pet3After.PetPosition);
            Assert.Equal(PetPosition.Create(5).Value, pet5After.PetPosition);
            Assert.Equal(PetPosition.Create(6).Value, pet6After.PetPosition);
        }

        private Volunteer CreateTestVolunteerWith6Pets()
        {
            Volunteer volunteer = CreateTestVolunteerWithoutPets();

            PetNickname name = PetNickname.Create("test nickname").Value;
            PelageColor color = PelageColor.Create(Color.Black, null, null).Value;
            PetBreedAndSpecies petBreed = PetBreedAndSpecies.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
            CastrationStatus castrationStatus = CastrationStatus.Neutered;
            HelpStatus helpStatus = HelpStatus.HelpNeeded;

            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);
            volunteer.CreateNewPet(name, color, null, petBreed, volunteer.PhoneNumber, castrationStatus, helpStatus);

            return volunteer;
        }

        private Volunteer CreateTestVolunteerWithoutPets()
        {
            FullName fullname = FullName.Create("test name", "test surname", null).Value;
            EmailAdress emailAdress = EmailAdress.Create("text@test.test").Value;
            PhoneNumber phoneNumber = PhoneNumber.Create("123456789").Value;
            DetailsForPayment detailsForPaymentess = DetailsForPayment.Create("test details", null).Value;

            Volunteer volunteer = Volunteer.Create(
                fullname, emailAdress, 1, phoneNumber, detailsForPaymentess).Value;

            return volunteer;
        }

    }
}