using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using School.Controllers.Base;
using School.Domain.Core;
using School.Domain.Interfaces;
using School.Infrastructure.Business;
using School.Infrastructure.Business.PresentationModel;
using School.Infrastructure.Data;

namespace School.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository repository;
        private IGradeRepository gradeRepository;

        public StudentController(IStudentRepository studentRepository, IGradeRepository gradeRepository)
        {
            this.repository = studentRepository;
            this.gradeRepository = gradeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListStudents()
        {
            return PartialView(repository.GetStudents());
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var grades = gradeRepository.GetGradeList();
            ViewBag.Grades = new SelectListGarde(grades.ToList()).GetSelectList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    repository.Create(student);
                    repository.Save();

                    return new JsonResult(true);
                }
                catch (Exception ex)
                { }
            }
            return new JsonResult(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Номер и символ нового класса</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateGrade(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                GradeManager gradeManager = new GradeManager();
                Grade grade = new Grade();

                bool result = gradeManager.isValid(id, ref grade);

                if (result == true && gradeRepository.Create(grade) == true)
                {
                    gradeRepository.Save();
                    return new JsonResult(new
                    {
                        iserror = false,
                        message = "",
                        Grade = grade
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        iserror = true,
                        message = "Вы ввели не верное название класса или данный класс уже существует.",
                        Grade = ""
                    });
                }
            }
            return new JsonResult(new
            {
                iserror = true,
                message = "Введите значение",
                Grade = ""
            });
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var grades = gradeRepository.GetGradeList();
            ViewBag.Grades = new SelectListGarde(grades.ToList()).GetSelectList();

            return PartialView(repository.GetStudentByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    repository.Update(student);
                    repository.Save();

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