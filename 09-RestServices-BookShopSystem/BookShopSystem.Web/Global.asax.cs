using BookShopSystem.Models.BindingModels.Authors;
using BookShopSystem.Models.BindingModels.Books;
using BookShopSystem.Models.BindingModels.Categories;
using BookShopSystem.Models.EntityModels;
using BookShopSystem.Models.ViewModels.Authors;
using BookShopSystem.Models.ViewModels.Books;
using BookShopSystem.Models.ViewModels.Categories;
using BookShopSystem.Models.ViewModels.Users;

namespace BookShopSystem.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using System.Linq;

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
                m.CreateMap<Purchase, UserPurchaseViewModel>()
                    .ForMember(upvm => upvm.PurchasePrice, expr => expr.MapFrom(b => b.Book.Price))
                    .ForMember(upvm => upvm.BookTitle, expr => expr.MapFrom(b => b.Book.Title))
                    .ForMember(upvm => upvm.Username, expr => expr.MapFrom(b => b.User.UserName));
            });
        }
    }
}