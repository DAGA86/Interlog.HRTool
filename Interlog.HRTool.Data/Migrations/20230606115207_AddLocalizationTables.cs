using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interlog.HRTool.Data.Migrations
{
    public partial class AddLocalizationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => new { x.Key, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Translation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "Culture", "Name" },
                values: new object[] { 1, "en-US", "English (United States)" });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "Culture", "Name" },
                values: new object[] { 2, "fr-FR", "French (France)" });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "Culture", "Name" },
                values: new object[] { 3, "pt-PT", "Portuguese (Portugal)" });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[,]
                {
                    { "company.delete.ErrorMessage", 1, "It is not possible to delete the company!" },
                    { "company.delete.ErrorMessage", 2, "Il n''est pas possible de supprimer l''entreprise!" },
                    { "company.delete.ErrorMessage", 3, "Não é possível eliminar a empresa!" },
                    { "company.delete.SuccessMessage", 1, "Company successfully deleted!" },
                    { "company.delete.SuccessMessage", 2, "Entreprise supprimée avec succès!" },
                    { "company.delete.SuccessMessage", 3, "Empresa eliminada com sucesso!" },
                    { "company.departmentNumber", 1, "Departments" },
                    { "company.departmentNumber", 2, "Département" },
                    { "company.departmentNumber", 3, "Departamentos" },
                    { "company.name", 1, "Name" },
                    { "company.name", 2, "Nom de l''entreprise" },
                    { "company.name", 3, "Nome" },
                    { "company.title", 1, "Company" },
                    { "company.title", 2, "Entreprise" },
                    { "company.title", 3, "Empresa" },
                    { "company.title.plural", 1, "Campanies" },
                    { "company.title.plural", 2, "Entreprises" },
                    { "company.title.plural", 3, "Empresas" },
                    { "delete.confirmMessage", 1, "Are you sure you want to delete this?" },
                    { "delete.confirmMessage", 2, "Êtes-vous sûr de vouloir supprimer ce profil?" },
                    { "delete.confirmMessage", 3, "Tem a certeza de que pretende apagar este perfil?" },
                    { "department.company", 1, "Company" },
                    { "department.company", 2, "Entreprise" },
                    { "department.company", 3, "Empresa" },
                    { "department.delete.ErrorMessage", 1, "Cannot delete the department!" },
                    { "department.delete.ErrorMessage", 2, "Impossible de supprimer le département!" },
                    { "department.delete.ErrorMessage", 3, "Não é possível eliminar o departamento!" },
                    { "department.delete.successMessage", 1, "Department successfully deleted!" },
                    { "department.delete.successMessage", 2, "Département supprimé avec succès!" },
                    { "department.delete.successMessage", 3, "Departamento eliminado com sucesso!" },
                    { "department.name", 1, "Name" },
                    { "department.name", 2, "Nom de l''entreprise" },
                    { "department.name", 3, "Nome da empresa" },
                    { "department.title", 1, "Department" },
                    { "department.title", 2, "Département" },
                    { "department.title", 3, "Departamento" },
                    { "department.title.plural", 1, "Departments" },
                    { "department.title.plural", 2, "Départements" },
                    { "department.title.plural", 3, "Departamentos" },
                    { "employee.department", 1, "Department" },
                    { "employee.department", 2, "Département" },
                    { "employee.department", 3, "Departamento" }
                });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[,]
                {
                    { "employee.email", 1, "Email" },
                    { "employee.email", 2, "Courriel" },
                    { "employee.email", 3, "Email" },
                    { "employee.firstName", 1, "First Name" },
                    { "employee.firstName", 2, "Prénom" },
                    { "employee.firstName", 3, "Nome" },
                    { "employee.lastName", 1, "Last Name" },
                    { "employee.lastName", 2, "Nom de famille" },
                    { "employee.lastName", 3, "Apelido" },
                    { "employee.login", 1, "Login" },
                    { "employee.login", 2, "Connexion" },
                    { "employee.login", 3, "Iniciar sessão" },
                    { "employee.password", 1, "Password" },
                    { "employee.password", 2, "Mot de passe" },
                    { "employee.password", 3, "Palavra-passe" },
                    { "employee.profile", 1, "Profiles" },
                    { "employee.profile", 2, "Profil de l''employé" },
                    { "employee.profile", 3, "Perfis" },
                    { "employee.title", 1, "Employee" },
                    { "employee.title", 2, "Employé" },
                    { "employee.title", 3, "Funcionário" },
                    { "employee.title.plural", 1, "Employees" },
                    { "employee.title.plural", 2, "Employés" },
                    { "employee.title.plural", 3, "Funcionários" },
                    { "employee.username", 1, "Username" },
                    { "employee.username", 2, "Nom d''utilisateur" },
                    { "employee.username", 3, "Nome de utilizador" },
                    { "general.button.back", 1, "Back" },
                    { "general.button.back", 2, "Retour" },
                    { "general.button.back", 3, "Voltar atrás" },
                    { "general.button.backToList", 1, "Back To List" },
                    { "general.button.backToList", 2, "Retour à la liste" },
                    { "general.button.backToList", 3, "Voltar à lista" },
                    { "general.button.create", 1, "Create" },
                    { "general.button.create", 2, "Créer" },
                    { "general.button.create", 3, "Criar" },
                    { "general.button.createNew", 1, "Create New" },
                    { "general.button.createNew", 2, "Créer Nouveau" },
                    { "general.button.createNew", 3, "Criar Novo" },
                    { "general.button.delete", 1, "Delete" },
                    { "general.button.delete", 2, "Supprimer" },
                    { "general.button.delete", 3, "Eliminar" }
                });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[,]
                {
                    { "general.button.edit", 1, "Edit" },
                    { "general.button.edit", 2, "Editer" },
                    { "general.button.edit", 3, "Editar" },
                    { "general.button.login", 1, "Login" },
                    { "general.button.login", 2, "Connexion" },
                    { "general.button.login", 3, "Iniciar sessão" },
                    { "general.button.save", 1, "Save" },
                    { "general.button.save", 2, "Sauvegarder" },
                    { "general.button.save", 3, "Salvar" },
                    { "menu.campany.plural", 1, "Campanies" },
                    { "menu.campany.plural", 2, "Entreprises" },
                    { "menu.campany.plural", 3, "Empresas" },
                    { "menu.department.plural", 1, "Departments" },
                    { "menu.department.plural", 2, "Départements" },
                    { "menu.department.plural", 3, "Departamentos" },
                    { "menu.employee.plural", 1, "Employees" },
                    { "menu.employee.plural", 2, "Employés" },
                    { "menu.employee.plural", 3, "Funcionários" },
                    { "menu.profile.plural", 1, "Profiles" },
                    { "menu.profile.plural", 2, "Profils" },
                    { "menu.profile.plural", 3, "Perfis" },
                    { "profile.delete.errorMessage", 1, "It is not possible to delete the profile!" },
                    { "profile.delete.errorMessage", 2, "Il n''est pas possible de supprimer le profil!" },
                    { "profile.delete.errorMessage", 3, "Il n''Não é possível eliminar o perfil!" },
                    { "profile.delete.successMessage", 1, "Profile successfully deleted!" },
                    { "profile.delete.successMessage", 2, "Profil supprimé avec succès!" },
                    { "profile.delete.successMessage", 3, "Perfil eliminado com sucesso!" },
                    { "profile.employeeNumber", 1, "Employees" },
                    { "profile.employeeNumber", 2, "Employés" },
                    { "profile.employeeNumber", 3, "Funcionários" },
                    { "profile.name", 1, "Name" },
                    { "profile.name", 2, "Nom de l''entreprise" },
                    { "profile.name", 3, "Nome" },
                    { "profile.profile.title", 1, "Profile" },
                    { "profile.profile.title", 2, "Profil" },
                    { "profile.profile.title", 3, "Perfil" },
                    { "profile.title.plural", 1, "Profiles" },
                    { "profile.title.plural", 2, "Profils" },
                    { "profile.title.plural", 3, "Perfis" },
                    { "title.create", 1, "Create" },
                    { "title.create", 2, "Créer" },
                    { "title.create", 3, "Criar" }
                });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[,]
                {
                    { "title.delete", 1, "Delete" },
                    { "title.delete", 2, "Supprimer" },
                    { "title.delete", 3, "Eliminar" },
                    { "title.edit", 1, "Edit" },
                    { "title.edit", 2, "Modifier" },
                    { "title.edit", 3, "Editar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translation_LanguageId",
                table: "Translation",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
