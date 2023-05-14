using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain;

namespace UserManagement.Api.Extensions
{
    public static class AuthorizationConfiguration
    {
         public static void AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminOnly", policy => policy.RequireRole(UserRole.Admin));
                options.AddPolicy("RequireTeacherOnly", policy => policy.RequireRole(UserRole.Teacher));
                options.AddPolicy("RequireStudentOnly", policy => policy.RequireRole(UserRole.Student));
                options.AddPolicy("RequiresTeachersAndAdmin", policy => policy.RequireRole(UserRole.Admin, UserRole.Teacher));
                options.AddPolicy("RequiresTeacherAdminStudent", policy => policy.RequireRole(UserRole.Admin, UserRole.Teacher, UserRole.Student));
            });
        }
    }
}