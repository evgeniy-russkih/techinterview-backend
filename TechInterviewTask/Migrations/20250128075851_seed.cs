using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInterviewTask.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""users"" (""id"", ""username"", ""password"") VALUES
                (1, 'User1', 'Password1'),
                (2, 'User2', 'Password2'),
                (3, 'User3', 'Password3'),
                (4, 'User4', 'Password4'),
                (5, 'User5', 'Password5')
            ");

            var random = new Random();
            var currencies = new[] { "USD", "EUR", "GBP" };

            for (int i = 0; i < 100; i++)
            {
                int fromUserId;
                int toUserId;

                do
                {
                    fromUserId = random.Next(1, 6);
                    toUserId = random.Next(1, 6);
                } while (fromUserId == toUserId);

                var amount = Math.Round((decimal)(random.Next(1, 1000) + random.NextDouble()), 2);

                var currency = currencies[random.Next(currencies.Length)];

                migrationBuilder.Sql($@"
                    INSERT INTO ""transactions"" (""from_user_id"", ""to_user_id"", ""amount"", ""currency"")
                    VALUES ({fromUserId}, {toUserId}, {amount.ToString(CultureInfo.InvariantCulture)}, '{currency}')"
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""transactions"" WHERE ""id"" IS NOT NULL;");
            migrationBuilder.Sql(@"DELETE FROM ""users"" WHERE ""id"" BETWEEN 1 AND 5;");
        }
    }
}
