using Feirum.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Feirum.Migrations
{
    public partial class AddSellerAccount : Migration
    {

        const string SELLER_USER_GUID = "742e5f91-3089-42f3-82da-8fd6c17c54e6";
        const string SELLER_ROLE_GUID = "df57f51c-b577-4c3c-8ea1-a45e8400621f";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // hash the Seller's password
            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(null, "Abcd123!");

            // Adicionar conta do Comerciante à tabela AspNetUsers
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,FirstName, LastName, Phone, Balance, Address)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{SELLER_USER_GUID}'");
            sb.AppendLine(",'seller@feirum.pt'");
            sb.AppendLine(",'SELLER@FEIRUM.PT'");
            sb.AppendLine(",'seller@feirum.pt'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(",'SELLER@FEIRUM.PT'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(",'Seller'");
            sb.AppendLine(",'Seller'");
            sb.AppendLine(",'910111555'");
            sb.AppendLine(", 0.0");
            sb.AppendLine(", 'Gualtar'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{SELLER_ROLE_GUID}','Seller','SELLER')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{SELLER_USER_GUID}','{SELLER_ROLE_GUID}')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Apagar a entrada da conta do Comerciante da tabela
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{SELLER_USER_GUID}' AND RoleId = '{SELLER_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{SELLER_USER_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{SELLER_ROLE_GUID}'");

        }
    }
}