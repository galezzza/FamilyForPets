using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyForPets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    date_of_birth = table.Column<long>(type: "bigint", nullable: true),
                    pet_health_description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    weight = table.Column<int>(type: "integer", nullable: true),
                    height = table.Column<int>(type: "integer", nullable: true),
                    contact_phone_number = table.Column<string>(type: "text", nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    primary_color = table.Column<int>(type: "integer", nullable: false),
                    secondary_color = table.Column<int>(type: "integer", nullable: true),
                    tertiary_color = table.Column<int>(type: "integer", nullable: true),
                    help_status_enum = table.Column<string>(type: "text", nullable: false),
                    card_number_for_payment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    other_payment_details = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    house_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.CheckConstraint("CK_PaymentDetails_CardNumberRequiredIfOtherExists", "other_payment_details IS NULL OR card_number_for_payment IS NOT NULL");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets");
        }
    }
}
