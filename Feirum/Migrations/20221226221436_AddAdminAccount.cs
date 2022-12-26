using Feirum.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Feirum.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "bcd077a1-c144-408b-bc2d-1427db320be1";
        const string ADMIN_ROLE_GUID = "c4c9bffe-2571-47b6-b3b3-0543252ca5a6";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // hash the Admin's password
            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(null, "Abcd123!");

            // Adicionar conta do Admin à tabela AspNetUsers
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,FirstName, LastName, Phone, Balance, Address)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}'");
            sb.AppendLine(",'admin@feirum.pt'");
            sb.AppendLine(",'ADMIN@FEIRUM.PT'");
            sb.AppendLine(",'admin@feirum.pt'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(",'ADMIN@FEIRUM.PT'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(",'Admin'");
            sb.AppendLine(",'Admin'");
            sb.AppendLine(",'910222333'");
            sb.AppendLine(", 50.0");
            sb.AppendLine(", 'Gualtar'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Apagar a entrada da conta do Admin da tabela
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");

        }
    }
}