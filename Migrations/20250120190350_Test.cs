using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kacper_kubacki.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessEquipmentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StandardArea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPlacementContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionFacilityId = table.Column<int>(type: "int", nullable: false),
                    ProcessEquipmentTypeId = table.Column<int>(type: "int", nullable: false),
                    EquipmentQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPlacementContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContract_ProcessEquipmentType_ProcessEquipmentTypeId",
                        column: x => x.ProcessEquipmentTypeId,
                        principalTable: "ProcessEquipmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContract_ProductionFacility_ProductionFacilityId",
                        column: x => x.ProductionFacilityId,
                        principalTable: "ProductionFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductionFacility",
                columns: new[] { "Id", "Code", "Name", "StandardArea" },
                values: new object[] { 1, "PF1", "Name 1", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContract_ProcessEquipmentTypeId",
                table: "EquipmentPlacementContract",
                column: "ProcessEquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContract_ProductionFacilityId",
                table: "EquipmentPlacementContract",
                column: "ProductionFacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPlacementContract");

            migrationBuilder.DropTable(
                name: "ProcessEquipmentType");

            migrationBuilder.DropTable(
                name: "ProductionFacility");
        }
    }
}
