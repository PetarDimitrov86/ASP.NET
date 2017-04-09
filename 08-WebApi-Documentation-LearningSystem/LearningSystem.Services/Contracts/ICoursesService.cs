namespace LearningSystem.Services.Contracts
{
    using System.Collections.Generic;
    using Models.BindingModels.Api.Course;
    using Models.ViewModels;
    using Models.ViewModels.Api.Courses;

    public interface ICoursesService
    {
        IEnumerable<CourseHomepageViewModel> GetAllCourses(string filter);
        CourseDetailsVm GetCourseDetails(int id, string userId);
        void EnrollStudentInCourse(int id, string userId);
        bool IsStudentEnrolledAlready(int id, string userId);
        IEnumerable<AllCoursesViewModel> GetAllCoursesApi();
        IEnumerable<SearchCourseViewModel> SearchForCourseApi(string keyword);
        bool IsTrainerIdValid(int trainerId);
        void CreateNewCourse(AddNewCourseBindingModel ancbm);
        bool DoesCourseExist(int id);
        CourseDetailsApiViewModel GetCourseDetailsApi(int id);
        void EditCourseInfo(int id, EditCourseApiBindingModel ecabm);
        void DeleteCourse(int id);
        IEnumerable<CourseEnrolledStudentViewModel> GetEnrolledStudentsInCourseApi(int id);
    }
}