﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookAutoPost.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoPosts",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    UserAPI = table.Column<string>(type: "TEXT", nullable: true),
                    PostTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    Frequency = table.Column<string>(type: "TEXT", nullable: true),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: true),  
                    Uri = table.Column<string>(type: "TEXT", nullable: true) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPosts", x => x.PageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPosts");
        }
    }
}
