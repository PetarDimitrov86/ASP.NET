namespace BookShopSystem.Services
{
    using AutoMapper;
    using Contracts;
    using System.Collections.Generic;
    using Models.BindingModels.Authors;
    using Models.EntityModels;
    using Models.ViewModels.Authors;

    public class AuthorsService : Service, IAuthorsService
    {
        public AuthorDetailsViewModel GetAuthorDetails(int id)
        {
            var author = this.Context.Authors.Find(id);
            AuthorDetailsViewModel authorVm = Mapper.Instance.Map<AuthorDetailsViewModel>(author);
            return authorVm;
        }

        public bool ContainsAuthor(int id)
        {
            return this.Context.Authors.Find(id) != null;
        }

        public void AddNewAuthor(AddAuthorBindingModel aabm)
        {
            Author author = Mapper.Instance.Map<Author>(aabm);
            this.Context.Authors.Add(author);
            this.Context.SaveChanges();
        }

        public bool ContainsBook(int id)
        {
            return this.Context.Books.Find(id) != null;
        }

        public IEnumerable<AuthorBooksViewModel> GetAuthorBookDetails(int id)
        {
            Author author = this.Context.Authors.Find(id);
            IEnumerable<AuthorBooksViewModel> abvm = Mapper.Map<IEnumerable<AuthorBooksViewModel>>(author.Books);
            return abvm;
        }
    }
}