using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetProOA.TemplateE.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class _000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExampleProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "����ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "����")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "�۸�"),
                    ProcurementTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProcurementType = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true, comment: "�汾��")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "����ʱ��"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "�޸�ʱ��"),
                    CreateEmplId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "������ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateEmplName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "������")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDeptId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "�����˲���ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDeptName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "�����˲���")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifyEmplId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "����޸���ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifyEmplName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "����޸���")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifyDeptId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "����޸��˲���ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifyDeptName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "����޸��˲���")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleProducts", x => x.Id);
                },
                comment: "ʾ����")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExampleProducts");
        }
    }
}
