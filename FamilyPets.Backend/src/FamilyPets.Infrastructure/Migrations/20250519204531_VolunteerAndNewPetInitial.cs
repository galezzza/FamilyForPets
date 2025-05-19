using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyForPets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerAndNewPetInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    experience_in_years = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    card_number_for_payment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    other_payment_details = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    additional_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    volunteer_social_networks = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

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
                    contact_phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    primary_color = table.Column<int>(type: "integer", nullable: false),
                    secondary_color = table.Column<int>(type: "integer", nullable: true),
                    tertiary_color = table.Column<int>(type: "integer", nullable: true),
                    help_status_enum = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
