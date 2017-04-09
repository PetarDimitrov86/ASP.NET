using LearningSystem.Models.ViewModels.Api.Students;

namespace LearningSystem.Api
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System;
    using AutoMapper;
    using Models.BindingModels.Api.Course;
    using Models.EntityModels;
    using Models.ViewModels.Api.Articles;
    using Models.ViewModels.Api.Courses;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Course, AllCoursesViewModel>()
                    .ForMember(cvm => cvm.NumberOfEnrolledStudents, expr => expr.MapFrom(c => c.Students.Count))
                    .ForMember(cvm => cvm.TrainerName, expr => expr.MapFrom(c => c.Trainer.Name));
                m.CreateMap<Course, SearchCourseViewModel>();
                m.CreateMap<Course, CourseDetailsApiViewModel>()
                    .ForMember(cvm => cvm.TrainerName, expr => expr.MapFrom(c => c.Trainer.Name));
                m.CreateMap<AddNewCourseBindingModel, Course>()
                    .ForMember(c => c.StartDate, expr => expr.MapFrom(ancbm => new DateTime(ancbm.StartDate.Value.Year, ancbm.StartDate.Value.Month, ancbm.StartDate.Value.Day)))
                    .ForMember(c => c.EndDate, expr => expr.MapFrom(ancbm => new DateTime(ancbm.EndDate.Value.Year, ancbm.EndDate.Value.Month, ancbm.EndDate.Value.Day)));
                m.CreateMap<Student, CourseEnrolledStudentViewModel>()
                    .ForMember(svm => svm.Name, expr => expr.MapFrom(c => c.User.Name));
                m.CreateMap<Article, AllArticlesApiViewModel>();
                m.CreateMap<Article, ArticleDetailsViewModel>()
                    .ForMember(arvm => arvm.AuthorName, expr => expr.MapFrom(ar => ar.Author.Name));
                m.CreateMap<Course, EnrolledCoursesStudentViewModel>();
            });
        }
    }
}
