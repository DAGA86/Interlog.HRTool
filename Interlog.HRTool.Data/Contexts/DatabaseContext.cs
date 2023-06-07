using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Interlog.HRTool.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasMany(x => x.Profiles).WithMany(x => x.Employees).UsingEntity<EmployeeProfile>();
            builder.Entity<Translation>().HasKey(x => new { x.Key, x.LanguageId });

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            // ###### SEED ######

            builder.Entity<Language>().HasData(new Language { Id = 1, Name = "English (United States)", Culture = "en-US" });
            builder.Entity<Language>().HasData(new Language { Id = 2, Name = "French (France)", Culture = "fr-FR" });
            builder.Entity<Language>().HasData(new Language { Id = 3, Name = "Portuguese (Portugal)", Culture = "pt-PT" });

            builder.Entity<Translation>().HasData(new Translation { Key = "company.title.plural", Value = "Campanies", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.name", Value = "Name", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.departmentNumber", Value = "Departments", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.title", Value = "Company", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.ErrorMessage", Value = "It is not possible to delete the company!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.SuccessMessage", Value = "Company successfully deleted!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title.plural", Value = "Departments", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.name", Value = "Name", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.company", Value = "Company", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title", Value = "Department", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.ErrorMessage", Value = "Cannot delete the department!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.successMessage", Value = "Department successfully deleted!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title.plural", Value = "Employees", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.firstName", Value = "First Name", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.lastName", Value = "Last Name", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.username", Value = "Username", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.email", Value = "Email", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.password", Value = "Password", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.department", Value = "Department", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title", Value = "Employee", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.profile", Value = "Profiles", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.login", Value = "Login", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.title.plural", Value = "Profiles", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.name", Value = "Name", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.employeeNumber", Value = "Employees", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.profile.title", Value = "Profile", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.errorMessage", Value = "It is not possible to delete the profile!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.successMessage", Value = "Profile successfully deleted!", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.edit", Value = "Edit", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.create", Value = "Create", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.delete", Value = "Delete", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "delete.confirmMessage", Value = "Are you sure you want to delete this?", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.login", Value = "Login", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.create", Value = "Create", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.createNew", Value = "Create New", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.edit", Value = "Edit", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.delete", Value = "Delete", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.save", Value = "Save", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.backToList", Value = "Back To List", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.back", Value = "Back", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.campany.plural", Value = "Campanies", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.department.plural", Value = "Departments", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.profile.plural", Value = "Profiles", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.employee.plural", Value = "Employees", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "datatables.translation.jsonfile", Value = "/dist/js/en-GB.json", LanguageId = 1 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.title.plural", Value = "Entreprises", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.name", Value = "Nom de l''entreprise", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.departmentNumber", Value = "Département", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.title", Value = "Entreprise", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.ErrorMessage", Value = "Il n''est pas possible de supprimer l''entreprise!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.SuccessMessage", Value = "Entreprise supprimée avec succès!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title.plural", Value = "Départements", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.name", Value = "Nom de l''entreprise", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.company", Value = "Entreprise", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title", Value = "Département", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.ErrorMessage", Value = "Impossible de supprimer le département!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.successMessage", Value = "Département supprimé avec succès!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title.plural", Value = "Employés", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.firstName", Value = "Prénom", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.lastName", Value = "Nom de famille", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.username", Value = "Nom d''utilisateur", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.email", Value = "Courriel", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.password", Value = "Mot de passe", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.department", Value = "Département", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title", Value = "Employé", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.profile", Value = "Profil de l''employé", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.login", Value = "Connexion", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.title.plural", Value = "Profils", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.name", Value = "Nom de l''entreprise", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.employeeNumber", Value = "Employés", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.profile.title", Value = "Profil", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.errorMessage", Value = "Il n''est pas possible de supprimer le profil!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.successMessage", Value = "Profil supprimé avec succès!", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.edit", Value = "Modifier", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.create", Value = "Créer", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.delete", Value = "Supprimer", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "delete.confirmMessage", Value = "Êtes-vous sûr de vouloir supprimer ce profil?", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.login", Value = "Connexion", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.create", Value = "Créer", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.createNew", Value = "Créer Nouveau", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.edit", Value = "Editer", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.delete", Value = "Supprimer", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.save", Value = "Sauvegarder", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.backToList", Value = "Retour à la liste", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.back", Value = "Retour", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.campany.plural", Value = "Entreprises", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.department.plural", Value = "Départements", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.profile.plural", Value = "Profils", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.employee.plural", Value = "Employés", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "datatables.translation.jsonfile", Value = "/dist/js/fr-FR.json", LanguageId = 2 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.title.plural", Value = "Empresas", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.name", Value = "Nome", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.departmentNumber", Value = "Departamentos", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.title", Value = "Empresa", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.ErrorMessage", Value = "Não é possível eliminar a empresa!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "company.delete.SuccessMessage", Value = "Empresa eliminada com sucesso!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title.plural", Value = "Departamentos", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.name", Value = "Nome da empresa", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.company", Value = "Empresa", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.title", Value = "Departamento", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.ErrorMessage", Value = "Não é possível eliminar o departamento!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "department.delete.successMessage", Value = "Departamento eliminado com sucesso!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title.plural", Value = "Funcionários", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.firstName", Value = "Nome", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.lastName", Value = "Apelido", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.username", Value = "Nome de utilizador", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.email", Value = "Email", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.password", Value = "Palavra-passe", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.department", Value = "Departamento", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.title", Value = "Funcionário", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.profile", Value = "Perfis", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "employee.login", Value = "Iniciar sessão", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.title.plural", Value = "Perfis", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.name", Value = "Nome", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.employeeNumber", Value = "Funcionários", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.profile.title", Value = "Perfil", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.errorMessage", Value = "Il n''Não é possível eliminar o perfil!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "profile.delete.successMessage", Value = "Perfil eliminado com sucesso!", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.edit", Value = "Editar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.create", Value = "Criar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "title.delete", Value = "Eliminar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "delete.confirmMessage", Value = "Tem a certeza de que pretende apagar este perfil?", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.login", Value = "Iniciar sessão", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.create", Value = "Criar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.createNew", Value = "Criar Novo", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.edit", Value = "Editar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.delete", Value = "Eliminar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.save", Value = "Salvar", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.backToList", Value = "Voltar à lista", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "general.button.back", Value = "Voltar atrás", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.campany.plural", Value = "Empresas", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.department.plural", Value = "Departamentos", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.profile.plural", Value = "Perfis", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "menu.employee.plural", Value = "Funcionários", LanguageId = 3 });
            builder.Entity<Translation>().HasData(new Translation { Key = "datatables.translation.jsonfile", Value = "/dist/js/pt-PT.json", LanguageId = 3 });

            // #### END SEED ####

            base.OnModelCreating(builder);
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Language> Languages { get; set; }
        
    }

} 
