using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfKullaniciDal>().As<IKullaniciDal>().SingleInstance();

            builder.RegisterType<AcademicsManager>().As<IAcademicsService>().SingleInstance();
            builder.RegisterType<EfAcademicsDal>().As<IAcademicsDal>().SingleInstance();

            builder.RegisterType<ClassManager>().As<IClassService>().SingleInstance();
            builder.RegisterType<EfClassDal>().As<IClassDal>().SingleInstance();

            builder.RegisterType<ClassCourseManager>().As<IClassCourseService>().SingleInstance();
            builder.RegisterType<EfClassCourseDal>().As<IClassCourseDal>().SingleInstance();

            builder.RegisterType<CoursesManager>().As<ICoursesService>().SingleInstance();
            builder.RegisterType<EfCoursesDal>().As<ICoursesDal>().SingleInstance();

            builder.RegisterType<ScheduleSettingManager>().As<IScheduleSettingService>().SingleInstance();
            builder.RegisterType<EfScheduleSettingDal>().As<IScheduleSettingDal>().SingleInstance();

            builder.RegisterType<StudentsManager>().As<IStudentsService>().SingleInstance();
            builder.RegisterType<EfStudentsDal>().As<IStudentsDal>().SingleInstance();

            builder.RegisterType<TimetableManager>().As<ITimetableService>().SingleInstance();
            builder.RegisterType<EfTimetableDal>().As<ITimetableDal>().SingleInstance();

            builder.RegisterType<PermissionDal>().As<IPermissionDal>().SingleInstance();

            builder.RegisterType<PermissionRoleManager>().As<IPermissionRoleService>().SingleInstance();
            builder.RegisterType<EfPermissionRoleDal>().As<IPermissionRoleDal>().SingleInstance();

            builder.RegisterType<RoleUserManager>().As<IRoleUserService>().SingleInstance();
            builder.RegisterType<EfRoleUserDal>().As<IRoleUserDal>().SingleInstance();

            builder.RegisterType<RoleManager>().As<IRoleService>().SingleInstance();
            builder.RegisterType<EfRolDal>().As<IRoleDal>().SingleInstance();




            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
