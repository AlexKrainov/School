using Microsoft.AspNetCore.Mvc;
using Moq;
using School.Controllers;
using School.Domain.Core;
using School.Domain.Interfaces;
using School.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace School.Tests
{
    public class StudentControllerTest
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfStudents()
        {
            // Arrange
            var mock = new Mock<IStudentRepository>();
            var mockGrade = new Mock<IGradeRepository>();
            mock.Setup(repo => repo.GetStudents()).Returns(GetTestStudents());
            StudentController controller = new StudentController(mock.Object, mockGrade.Object);

            // Act
            var result = controller.ListStudents();

            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.Model);
            Assert.Equal(GetTestStudents().Count, model.Count());
        }
        private List<Student> GetTestStudents()
        {
            List<Student> students = new List<Student>
            {
                new Student { ID = 1, FirstName = "Владимир", LastName = "Ошников", GradeID = 4 },
                 new Student { ID = 2, FirstName = "Константин", LastName = "Орешкин", GradeID = 4 },
            };
            return students;
        }
    }
}
