using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using JobOpportunities.Models;
[assembly: OwinStartupAttribute(typeof(JobOpportunities.Startup))]


namespace JobOpportunities
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            // Render this method that I created down     on configuration   to excute
            CreateDefaultRolesAndUsers();

        }

        public void CreateDefaultRolesAndUsers()
        {

            // I create a variable named reloManger   this variable is an instance from  $$ RoleManger class $$  =>  
            // $$ RoleManger class $$ it is allow for us to manage Roles and store this in dataBase  by  $$ RoleStore class $$
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            // I create a variable named userManger   this variable is an instance from  $$ UserManger class $$  =>  
            // $$ UserManger class $$ it is allow for us to manage Users and store this in dataBase  by  $$ UserStore class $$
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            // Create an object from IdentityRole    and check if there is no role =>> create a new role
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleManager.Create(role);

                // After create new role  =>> create new user as  Admin   with   UserName and Email
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Daif";
                user.Email = "di.hamdan55@gmail.com";

                // variable to check   user  and  password
                var check = userManager.Create(user, "@Dd39261330h");
                if (check.Succeeded)
                {
                    // if user  and passord are succceeded      add the role   Admins
                    userManager.AddToRole(user.Id, "Admins");
                    

                    // Authentication ==>>  Who are you ?  and do you have an account in this site ?
                    // Authorization ==>> Who can see what ?
                }
            }

        }
    }
}
