using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Controllers.Base;
using School.Domain.Core;
using School.Domain.Core.ViewModel;
using School.Domain.Interfaces;
using School.Infrastructure.Business;
using School.Infrastructure.Business.PresentationModel;
using School.Infrastructure.Data;

namespace School.Controllers
{
    public class ScheduleController : Controller
    {
        private IGradeRepository gradeRepository;
        private ITeacherRepository teacherRepository;
        private ILessonTimeRepository lessonTimeRepository;
        private IScheduleRepository scheduleRepository;
        public ScheduleController(IGradeRepository gradeRepository, ITeacherRepository teacherRepository, ILessonTimeRepository lessonTimeRepository, IScheduleRepository scheduleRepository)
        {
            this.teacherRepository = teacherRepository;
            this.gradeRepository = gradeRepository;
            this.lessonTimeRepository = lessonTimeRepository;
            this.scheduleRepository = scheduleRepository;
        }

        public IActionResult Index()
        {
            var grades = gradeRepository.GetGradeList();
            ViewBag.Grades = new SelectListGarde(grades.ToList()).GetSelectList();
            ViewBag.LessonTime = lessonTimeRepository.GetLessonTime().ToList();
            ViewBag.Teachers = teacherRepository.GetTeachers().ToList();
            ViewBag.DateManager = new DatesOfWeek();
            ViewBag.Schedules = new List<Schedule>();

            return View();
        }
        [HttpPost]
        public IActionResult Index(ScheduleView model)
        {
            DateManager dateManager = new DateManager(model.Week);
            DatesOfWeek dates = dateManager.GetDatesOfWeek();
            IEnumerable<Schedule> schedules = scheduleRepository.GetSchedules(dates.MondayDate, model.GradeID);
            var grades = gradeRepository.GetGradeList();

            ViewBag.Grades = new SelectListGarde(grades.ToList()).GetSelectList();
            ViewBag.LessonTime = lessonTimeRepository.GetLessonTime().ToList();
            ViewBag.Teachers = teacherRepository.GetTeachers().ToList();
            ViewBag.Schedules = schedules.ToList();
            ViewBag.DateManager = dates;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddSchedule(string date, int time_id, int teacher_id, int grade_id)
        {
            if (string.IsNullOrEmpty(date) == false
                && time_id != 0
                && teacher_id != 0
                && grade_id != 0)
            {
                DateTime newDate;
                if (DateTime.TryParse(date, out newDate))
                {
                    var schedule = new Schedule()
                    {
                        Date = newDate,
                        GradeID = grade_id,
                        TeacherID = teacher_id,
                        TimeID = time_id
                    };

                    if (scheduleRepository.IsBusyTeacher(schedule) == false)
                    {
                        var id = scheduleRepository.Create(schedule);
                        scheduleRepository.Save();
                        return new JsonResult(new { is_done = true, schedule_id = id });
                    }
                    Teacher teacher = teacherRepository.GetTeacherByID(teacher_id);

                    return new JsonResult(new { is_done = false, message = string.Format("{0} {1} занят в это время в другом классе.", teacher.FirstName, teacher.MiddleName) });
                }
            }
            return new JsonResult(new { is_done = false, message = "Не удалось сохранить." });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                scheduleRepository.Delete(id);
                scheduleRepository.Save();
            }
            catch (Exception ex)
            {
                return new JsonResult(false);
            }
            return new JsonResult(true);
        }

    }
}