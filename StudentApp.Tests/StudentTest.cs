using Microsoft.EntityFrameworkCore;
using Moq;
using StudentApp.Api.Context;
using StudentApp.Api.Exceptions;
using StudentApp.Api.Model;
using StudentApp.Api.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudentApp.Tests
{
    public class StudentTest
    {
        private IStudentRepository _studentRepository;

        public StudentTest()
        {
            InitializeCoinContext();
        }

        [Fact]
        public void SaveShouldSaveStudentInTheDatabase()
        {
            //arrange
            var student = new Student
            {
                Id = 10,
                Name = "Lesly",
                Age = 26,
                Surname = "Dube",
                CellNumber = "087626321",
                IDorPassport = "7776665512"
            };

            //Act
            var results = _studentRepository.Save(student);

            //Assert
            Assert.Equal(student.Id, results.Result.Id);
        }

        [Fact]
        public void SaveShouldThrowStudentExceptionIfStudentExist()
        {
            //arrange
            var student = new Student
            {
                Name = "Lesly",
                Age = 26,
                Surname = "Zuko",
                CellNumber = "087626321",
                IDorPassport = "7776665512"
            };

            //Assert
            Assert.ThrowsAsync<StudentException>(() => _studentRepository.Save(student));
        }

        [Fact]
        public void UpdateShouldUpdateStudentInTheDatabaseIfStudentIsValid()
        {
            //arrange
            var student = new Student
            {
                Id = 1,
                Name = "Thapelo",
                Age = 26,
                Surname = "Mkhwanazi",
                CellNumber = "0824747431",
                IDorPassport = "11122221111"
            };

            //Act
            var updatedStd = _studentRepository.Update(student);

            //Assert
            Assert.Equal("Thapelo", updatedStd.Result.Name);
        }

        [Fact]
        public void UpdateShouldThrowExceptionIfStudentWithIdOrPassportExist()
        {
            //arrange
            var student = new Student
            {
                Id = 9,
                Name = "Cima",
                Age = 26,
                Surname = "Celo",
                CellNumber = "0824747431",
                IDorPassport = "11122221111"
            };

            //Assert
            Assert.ThrowsAsync<StudentException>(() => _studentRepository.Update(student));
        }

        private void InitializeCoinContext()
        {
            var data = new List<Student>
            {
                new Student
                {
                    Id = 1, Name = "Kefilwe", Age = 26, Surname = "Mkhwanazi", CellNumber = "0824747431",
                    IDorPassport = "11122221111"
                },
                new Student
                {
                    Id = 3, Name = "Prudence", Age = 27, Surname = "Masemene", CellNumber = "08236312321",
                    IDorPassport = "209090910101"
                },
                new Student
                {
                    Id = 6, Name = "Sipho", Age = 29, Surname = "Mokhabela", CellNumber = "0824747431",
                    IDorPassport = "90001232321321"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Student>>();

            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Students).Returns(mockSet.Object);

            _studentRepository = new StudentRepository(mockContext.Object);
        }
    }
}