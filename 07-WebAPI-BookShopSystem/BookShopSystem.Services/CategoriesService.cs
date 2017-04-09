namespace BookShopSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using BookShopSytem.Models.ViewModels.Categories;
    using BookShopSytem.Models.BindingModels.Categories;
    using BookShopSytem.Models.EntityModels;

    public class CategoriesService : Service, ICategoriesService
    {
        public IEnumerable<AllCategoriesViewModel> GetAllCategories()
        {
            var categories = this.Context.Categories.ToList();
            IEnumerable<AllCategoriesViewModel> categoriesVms =
                Mapper.Map<IEnumerable<AllCategoriesViewModel>>(categories);
            return categoriesVms;
        }

        public bool ContainsCategory(int id)
        {
            return this.Context.Categories.Find(id) != null;
        }

        public AllCategoriesViewModel GetCategoryInfo(int id)
        {
            Category category = this.Context.Categories.Find(id);
            AllCategoriesViewModel categoryVm = Mapper.Instance.Map<AllCategoriesViewModel>(category);
            return categoryVm;
        }

        public bool CategoryNameAlreadyExists(EditCategoryBindingModel ecbm)
        {
            return this.Context.Categories.FirstOrDefault(c => c.Name == ecbm.Name) != null;
        }

        public void UpdateCategoryName(int id, EditCategoryBindingModel ecbm)
        {
            Category categoryToEdit = this.Context.Categories.Find(id);
            categoryToEdit.Name = ecbm.Name;
            this.Context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var unwantedCategory = this.Context.Categories.Find(id);
            this.Context.Categories.Remove(unwantedCategory);
            this.Context.SaveChanges();
        }

        public void AddNewCategory(EditCategoryBindingModel acbm)
        {
            Category category = new Category { Name = acbm.Name};
            this.Context.Categories.Add(category);
            this.Context.SaveChanges();
        }
    }
}