namespace LearningSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Article;
    using Models.BindingModels;
    using Models.ViewModels.Trainer;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Course, CourseHomepageViewModel>()
                    .ForMember(cvm => cvm.TrainerName, expr => expr.MapFrom(c => c.Trainer.Name));
                m.CreateMap<Course, CourseDetailsVm>()
                    .ForMember(cvm => cvm.NumberOfStudentsEnrolled, expr => expr.MapFrom(c => c.Students.Count))
                    .ForMember(cvm => cvm.TrainerUserName, expr => expr.MapFrom(c => c.Trainer.UserName))
                    .ForMember(cvm => cvm.TrailerName, expr => expr.MapFrom(c => c.Trainer.Name));
                m.CreateMap<Course, ProfileCourseViewModel>();
                m.CreateMap<Student, StudentProfileViewModel>()
                    .ForMember(svm => svm.Name, expr => expr.MapFrom(c => c.User.Name))
                    .ForMember(svm => svm.Courses, expr => expr.Ignore());
                m.CreateMap<Student, EditUserViewModel>()
                    .ForMember(euvm => euvm.Name, expr => expr.MapFrom(au => au.User.Name))
                    .ForMember(euvm => euvm.BirthDate, expr => expr.MapFrom(au => au.User.BirthDate));
                m.CreateMap<Article, ArticleHomepageViewModel>()
                    .ForMember(arvm => arvm.AuthorName, expr => expr.MapFrom(ar => ar.Author.Name));
                m.CreateMap<Course, ManageCourseViewModel>()
                    .ForMember(arvm => arvm.TrainerName, expr => expr.MapFrom(ar => ar.Trainer.Name));
                m.CreateMap<ApplicationUser, TrainerDropdownViewModel>();
                m.CreateMap<Course, AddCourseViewModel>()
                    .ForMember(c => c.Trainer, expr => expr.Ignore()); ;
                m.CreateMap<AddCourseBindingModel, Course>()
                    .ForMember(c => c.Trainer, expr => expr.Ignore());
                m.CreateMap<Course, EditCourseViewModel>()
                    .ForMember(cvm => cvm.TrainerId, expr => expr.MapFrom(c => c.Trainer.Id))
                    .ForMember(c => c.Trainer, expr => expr.Ignore());
                m.CreateMap<Course, DeleteCourseViewModel>();
                m.CreateMap<Course, TrainerCoursesViewModel>()
                .ForMember(tcvm => tcvm.NumberOfStudents, expr => expr.MapFrom(c => c.Students.Count));
                m.CreateMap<IdentityRole, RoleViewModel>();
                m.CreateMap<Article, EditArticleViewModel>();
            });
        }
    }
}