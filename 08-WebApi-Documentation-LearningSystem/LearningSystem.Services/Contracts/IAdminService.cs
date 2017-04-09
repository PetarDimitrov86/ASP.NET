namespace LearningSystem.Services.Contracts
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.ViewModels.Admin;

    public interface IAdminService
    {
        IEnumerable<ManageCourseViewModel> GetManageableCourses();
        AddCourseViewModel GetAddCourseDetails();
        void AddNewCourse(AddCourseBindingModel acbm);
        EditCourseViewModel GetEditInfo(int id);
        void EditCourse(EditCourseBindingModel ecbm);
        DeleteCourseViewModel GetUnwantedCourse(int id);
        void DeleteCourse(DeleteCourseBindingModel dcbm);
        UserRolesViewModel GetAllUsersAndRoles();
        void ChangeUserRoles(string id);
    }
}