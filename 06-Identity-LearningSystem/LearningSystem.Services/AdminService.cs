namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Admin;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.BindingModels;

    public class AdminService : Service
    {
        public IEnumerable<ManageCourseViewModel> GetManageableCourses()
        {
            var courses = this.Context.Courses.ToList();
            return Mapper.Map<IEnumerable<Course>, IEnumerable<ManageCourseViewModel>>(courses);
        }

        public AddCourseViewModel GetAddCourseDetails()
        {
            var addCourseVm = new AddCourseViewModel
            {
                Trainer = GetAllTrainers()
            };
            return addCourseVm;
        }

        public void AddNewCourse(AddCourseBindingModel acbm)
        {
            Course course = Mapper.Map<AddCourseBindingModel, Course>(acbm);
            ApplicationUser trainer = this.Context.Users.Find(acbm.Trainer);
            course.Trainer = trainer;
            this.Context.Courses.Add(course);
            this.Context.SaveChanges();
        }

        public EditCourseViewModel GetEditInfo(int id)
        {
            var editCourseVm = Mapper.Map<Course, EditCourseViewModel>(this.Context.Courses.Find(id));
            editCourseVm.Trainer = GetAllTrainers();
            return editCourseVm;
        }

        private IEnumerable<TrainerDropdownViewModel> GetAllTrainers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
            var adminRoleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Trainer")?.Id;

            var trainers = Context.Users.Where(u => u.Roles.Any(r => r.RoleId == adminRoleId)).OrderBy(t => t.Name);
            return Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<TrainerDropdownViewModel>>(trainers);
        }

        public void EditCourse(EditCourseBindingModel ecbm)
        {
            Course course = this.Context.Courses.Find(ecbm.Id);
            ApplicationUser trainer = this.Context.Users.Find(ecbm.Trainer);

            course.Trainer = trainer;
            course.Name = ecbm.Name;
            course.Description = ecbm.Description;
            course.EndDate = ecbm.EndDate;
            course.StartDate = ecbm.StartDate;
            this.Context.SaveChanges();
        }

        public DeleteCourseViewModel GetUnwantedCourse(int id)
        {
            return Mapper.Map<Course, DeleteCourseViewModel>(this.Context.Courses.Find(id));
        }

        public void DeleteCourse(DeleteCourseBindingModel dcbm)
        {
            Course removeMe = this.Context.Courses.Find(dcbm.Id);
            IEnumerable<Grade> grades = this.Context.Grades.Where(g => g.Course.Id == dcbm.Id);
            foreach (var grade in grades)
            {
                this.Context.Grades.Remove(grade);
            }
            this.Context.Courses.Remove(removeMe);
            this.Context.SaveChanges();
        }

        public UserRolesViewModel GetAllUsersAndRoles()
        {
            UserRolesViewModel urvm = new UserRolesViewModel();
            var users = this.Context.Users.ToList();
            var roles = this.Context.Roles.ToList();

            foreach (var user in users)
            {
                var userVm = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Roles = roles.Where(role => user.Roles.Any(r => r.RoleId == role.Id)).Select(r => r.Name)
                };
                urvm.Users.Add(userVm);
            }

            urvm.Roles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);

            return urvm;
        }

        public void ChangeUserRoles(string id)
        {
            ApplicationUser user = this.Context.Users.Find(id);
            user.Roles.Clear();
            this.Context.SaveChanges();
        }
    }
}