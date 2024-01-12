using lar_tech.Data.Database;
using lar_tech.Data.Repositories;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using lar_tech.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Tests.API
{
    public class PersonControllerTest
    {
        private PersonRelationalRepository<Person> _mockRepository;
        private ApplicationDbContext _mockContext;
        public PersonControllerTest()
        {
            _mockContext = MockApplicationDbContext.GetMock();
            _mockRepository = new Mock<PersonRelationalRepository<Person>>(_mockContext).Object;
        }
        [Fact]
        public async void Post_SendInvalidRequest()
        {
            //Arrange
            var badPerson = new Person { IsActive = false, Name = "Clodovaldo" };
            //Act
            await _mockRepository.PostAsync(badPerson);
            //Assert
            Assert.False(_mockRepository.PostAsync(badPerson).IsCompletedSuccessfully);
        }
    }
}
