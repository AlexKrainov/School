using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Controllers.Base;
using School.Domain.Core;
using School.Domain.Core.ViewModel;
using School.Infrastructure.Business.PresentationModel;
using School.Infrastructure.Data;

namespace School.Controllers
{
    public class TeacherController : BaseController
    {
        private TeacherRepository repository;
        private SubjectRepository subjectRopository;

        public TeacherController(SchoolContext db)
            : base(db)
        {
            this.repository = new TeacherRepository(db);
            this.subjectRopository = new SubjectRepository(db);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListTeachers()
        {
            return PartialView(repository.GetTeachers());
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var subjects = subjectRopository.GetSubjectsWithoutTeacher();
            ViewBag.Subjects = new SelectListSubject(subjects.ToList()).GetSelectList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher, int[] Subjects)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    repository.Create(teacher);
                    repository.Save();

                    subjectRopository.UpdateByTeacherID(teacher.ID, Subjects);
                    subjectRopository.Save();

                    return new JsonResult(true);
                }
                catch (Exception ex)
                { }
            }
            return new JsonResult(false);
        }

        [HttpGet]
        public IActionResult CreateSubject(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                Subject newSubject = new Subject() { Name = id };
                try
                {
                    subjectRopository.Create(newSubject);
                    subjectRopository.Save();
                }
                catch (Exception ex)
                {
                    return new JsonResult(new
                    {
                        iserror = true,
                        message = "Не удалось сохранить значение",
                        Subject = ""
                    });
                }
                return new JsonResult(new
                {
                    iserror = false,
                    message = "",
                    Subject = newSubject
                });
            }
            return new JsonResult(new
            {
                iserror = true,
                message = "Введите значение",
                Subject = ""
            });
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var subjects = subjectRopository.GetSubjectsByTeacher(id);
            var teacher = repository.GetTeacherByID(id);

            ViewTeacher viewTeacher = new ViewTeacher(teacher);
            viewTeacher.SubjectsSelectList = new SelectListSubject(subjects.ToList()).GetSelectList();
            viewTeacher.SubjectsIDs = teacher.Subjects.Select(x => x.ID).ToArray();

            return PartialView(viewTeacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ViewTeacher teacher)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    Teacher newTeacher = new Teacher()
                    {
                        ID = teacher.ID,
                        FirstName = teacher.FirstName,
                        MiddleName = teacher.MiddleName,
                        LastName = teacher.LastName
                    };

                    repository.Update(newTeacher);
                    repository.Save();

                    subjectRopository.UpdateByTeacherID(teacher.ID, teacher.SubjectsIDs);
                    subjectRopository.Save();

                    return new JsonResult(true);
                }
                catch (Exception ex)
                { }
            }
            return new JsonResult(false);
        }
        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                repository.Delete(id);
                repository.Save();
            }
            catch (Exception ex)
            {
                return new JsonResult(false);
            }
            return new JsonResult(true);
        }
    }
}