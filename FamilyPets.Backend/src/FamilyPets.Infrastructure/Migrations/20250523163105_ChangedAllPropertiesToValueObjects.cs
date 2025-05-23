using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyForPets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAllPropertiesToValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_neutered",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_vaccinated",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "volunteers",
                newName: "volunteer_description");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "pets",
                newName: "pet_description");

            migrationBuilder.AlterColumn<double>(
                name: "weight",
                table: "pets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "height",
                table: "pets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "castration_status_enum",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "length_type_enum",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mass_type_enum",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pet_vaccienes",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Height_BothOrNone",
                table: "pets",
                sql: "(height IS NULL AND length_type_enum IS NULL) OR (height IS NOT NULL AND length_type_enum IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Weight_BothOrNone",
                table: "pets",
                sql: "(weight IS NULL AND mass_type_enum IS NULL) OR (weight IS NOT NULL AND mass_type_enum IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Height_BothOrNone",
                table: "pets");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Weight_BothOrNone",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "castration_status_enum",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "length_type_enum",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "mass_type_enum",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "pet_vaccienes",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "volunteer_description",
                table: "volunteers",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "pet_description",
                table: "pets",
                newName: "description");

            migrationBuilder.AlterColumn<int>(
                name: "weight",
                table: "pets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "height",
                table: "pets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<bool>(
                name: "is_neutered",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_vaccinated",
                table: "pets",
                type: "boolean",
                nullable: true);
        }
    }
}
