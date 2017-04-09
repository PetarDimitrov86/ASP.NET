using BookShopSytem.Models.BindingModels.Categories;

namespace BookShopSystem.Web
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using BookShopSytem.Models.BindingModels.Authors;
    using BookShopSytem.Models.BindingModels.Books;
    using BookShopSytem.Models.EntityModels;
    using BookShopSytem.Models.ViewModels.Authors;
    using BookShopSytem.Models.ViewModels.Books;
    using BookShopSytem.Models.ViewModels.Categories;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Author, AuthorDetailsViewModel>()
                    .ForMember(avm => avm.BookTitles, expr => expr.MapFrom(a => a.Books.Select(b => b.Title)));
                m.CreateMap<AddAuthorBindingModel, Author>();
                m.CreateMap<Book, AuthorBooksViewModel>()
                    .ForMember(abvm => abvm.CategoriesNames, expr => expr.MapFrom(b => b.Categories.Select(c => c.Name)));
                m.CreateMap<Book, BookDetailsViewModel>()
                    .ForMember(bdvm => bdvm.CategoriesNames, expr => expr.MapFrom(b => b.Categories.Select(c => c.Name)))
                    .ForMember(bdvm => bdvm.AuthorName, expr => expr.MapFrom(b => b.Author.FirstName + " " + b.Author.LastName));
                m.CreateMap<Book, SearchBookViewModel>();
                m.CreateMap<EditBookBindingModel, Book>();
                m.CreateMap<AddBookBindingModel, Book>();
                m.CreateMap<Category, AllCategoriesViewModel>();
                m.CreateMap<EditCategoryBindingModel, Category>();
            }       
            );
        }
    }
}