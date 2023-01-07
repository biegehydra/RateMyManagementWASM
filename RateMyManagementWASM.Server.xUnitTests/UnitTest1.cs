using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using RateMyManagementWASM.Server.Controllers;
using RateMyManagementWASM.Server.Data;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Server.xUnitTests
{
    public class UnitTest1 : IDisposable
    {
        private ApplicationDbContext _dbContext;
        private CompanyController _companyController;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _dbContext = new ApplicationDbContext(options, operationalStoreOptions);
            //_companyController = new CompanyController(_dbContext);
        }
        [Fact]
        public async Task Save()
        {
            //await PopulateDb();
            //Assert
            var firstCompany = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == "1");
            Assert.NotNull(firstCompany);
            Assert.NotNull(firstCompany.Locations);
        }

        [Fact]
        public async Task GetAll()
        {
            var result = await _companyController.GetAll(null);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetByIdValid()
        {
            //await PopulateDb();
            var includes = new string[] { "Locations", "Locations.LocationReviews" };
            //act
            var companyInDb = await _dbContext.Companies.FirstOrDefaultAsync();
            var companies = _dbContext.Companies.AsEnumerable().ToList();
            var companyResult = await _companyController.Get("1", includes);
            //assert
            Assert.NotNull(companyResult.Value);
            Assert.Equal(companyResult.Value.Id, companyInDb.Id);
        }
        [Fact]
        public async Task GetByIdInvalid()
        {
            //await PopulateDb();
            var includes = new string[] { "Locations", "Locations.LocationReviews" };
            //act
            var companyInDb = await _dbContext.Companies.FirstOrDefaultAsync();
            var companyResult = await _companyController.Get("10534857", includes);
            //assert
            Assert.Null(companyResult.Value);
        }
        [Fact]
        public async Task CreateMany()
        {
        //    await PopulateDb();
        //    var companiesFromDb = _dbContext.Companies.AsEnumerable().ToList();
        //    var containsCompany2 = companiesFromDb.Any(x => x.Id == "2");
        //    var containsCompany3 = companiesFromDb.Any(x => x.Id == "3");
        //    Assert.True(containsCompany2);
        //    Assert.True(containsCompany3);
        //    Assert.Equal(3, companiesFromDb.Count);
        //}
        //public async Task PopulateDb()
        //{
        //    //Arrange
        //    var company = new Company()
        //    {
        //        Description = "tes",
        //        Id = "1",
        //        Industry = "music",
        //        Locations = new List<Location>()
        //        {
        //            new Location()
        //            {
        //                Address = "asdd",
        //                City = "asdd",
        //                Id = "12",
        //                LocationReviews = new List<LocationReview>()
        //                {
        //                    new LocationReview()
        //                    {
        //                        Content = "asdasd",
        //                        Id = "12",
        //                        ManagerAttributes = "asda",
        //                        ManagerName = "manager",
        //                        ManagerType = "type",
        //                        SenderUsername = "senderUsername",
        //                        SentDateAndTime = "datetime",
        //                        Stars = 5,
        //                        Type = "customer"
        //                    }
        //                },
        //            }
        //        },
        //        LogoDeleteUrl = string.Empty,
        //        LogoUrl = String.Empty,
        //        Name = "Test1"
        //    };
        //    //Act
        //    var result = await _companyController.Save(company);

        //    var company1 = new Company()
        //    {
        //        Description = "tes",
        //        Id = "2",
        //        Industry = "music",
        //        Locations = new List<Location>()
        //        {
        //            new Location()
        //            {
        //                Address = "asdd",
        //                City = "asdd",
        //                Id = "10",
        //                LocationReviews = new List<LocationReview>()
        //                {
        //                    new LocationReview()
        //                    {
        //                        Content = "asdasd",
        //                        Id = "9",
        //                        ManagerAttributes = "asda",
        //                        ManagerName = "manager",
        //                        ManagerType = "type",
        //                        SenderUsername = "senderUsername",
        //                        SentDateAndTime = "datetime",
        //                        Stars = 5,
        //                        Type = "customer"
        //                    }
        //                },
        //            }
        //        },
        //        LogoDeleteUrl = string.Empty,
        //        LogoUrl = String.Empty,
        //        Name = "Test2"
        //    };
        //    var company2 = new Company()
        //    {
        //        Description = "tes",
        //        Id = "3",
        //        Industry = "music",
        //        Locations = new List<Location>()
        //        {
        //            new Location()
        //            {
        //                Address = "asdd",
        //                City = "asdd",
        //                Id = "2",
        //                LocationReviews = new List<LocationReview>()
        //                {
        //                    new LocationReview()
        //                    {
        //                        Content = "asdasd",
        //                        Id = "2",
        //                        ManagerAttributes = "asda",
        //                        ManagerName = "manager",
        //                        ManagerType = "type",
        //                        SenderUsername = "senderUsername",
        //                        SentDateAndTime = "datetime",
        //                        Stars = 5,
        //                        Type = "customer"
        //                    }
        //                },
        //            }
        //        },
        //        LogoDeleteUrl = string.Empty,
        //        LogoUrl = String.Empty,
        //        Name = "Test3"
        //    };
        //    var companies = new List<Company>() { company1, company2 };

        //    //act
        //    await _companyController.CreateMany(companies);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}