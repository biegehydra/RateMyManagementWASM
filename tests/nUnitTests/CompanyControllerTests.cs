namespace nUnitTests
{
    public class CompanyControllerTests
    {
        private ApplicationDbContext _context;
        private CompanyController _companyController;
        private string _knowTestUserId = "0a3f25a3-79db-4c5e-8f42-83a5d6fdb7a6";
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=RateMyManagementWASMTestDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private Company _companyToTestWith = new Company()
        {
            Description = "Description",
            Id = "0a3f25a3-79db-4c5e-8f42-83a5d6fyb7a6",
            Industry = "Industry",
            Locations = new List<Location>(),
            Name = "Name",
            LogoDeleteUrl = "",
            LogoUrl = ""
        };
        private Mapper _mapper;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _context = new ApplicationDbContext(options, operationalStoreOptions);
            _mapper = new Mapper(new MapperConfiguration(x =>
            {
                x.AddProfile<CompanyProfile>();
                x.AddProfile<LocationProfile>();
                x.AddProfile<LocationReviewProfile>();
            }));
            _companyController = new CompanyController(_context, _mapper);
        }
        [Test]
        public async Task Get()
        {
            var actionResult = await _companyController.Get(_knowTestUserId, new string[] { });
            var company = GetObjectResultContent(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual("Ryan, Rau and Watsica", company.Name);
            Assert.ReferenceEquals(1, 2);
        }
        [Test]
        public async Task GetBadId_ReturnsBadRequest()
        {
            var actionResult = await _companyController.Get("123", new string[] { });
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
        }
        [Test]
        [Order(1)]
        public async Task GetAll()
        {
            var actionResult = await _companyController.GetAll(new string[] { });
            var companies = GetObjectResultContent(actionResult);
            Assert.NotNull(companies);
            Assert.AreEqual(20, companies.Count());
        }
        [Test]
        public async Task Query()
        {
            var query = "HA";
            var result = await _companyController.QueryWithRating(query, "false");
            var companies = GetObjectResultContent(result);
            Assert.AreEqual(6, companies.Count());
            Assert.True(companies.All(x => x.Name.ToLower().Contains(query.ToLower())));
        }
        [Test]
        public async Task Query_StartsWith()
        {
            var letter = "R";
            var result = await _companyController.QueryWithRating(letter, "true");
            var companies = GetObjectResultContent(result);
            Assert.AreEqual(2, companies.Count());
            Assert.True(companies.All(x => x.Name.StartsWith(letter)));
        }
        [Test]
        [Order(2)]
        public async Task SaveDelete1()
        {
            var companyDto = _mapper.Map(_companyToTestWith, new CompanyDto());
            await _companyController.Save(companyDto);

            var companyAdded = await _context.Companies.FindAsync(_companyToTestWith.Id);
            Assert.NotNull(companyAdded);
        }
        [Test]
        [Order(3)]
        public async Task SaveDelete2()
        {
            var companyExists = _context.Companies.Any(x => x.Id == _companyToTestWith.Id);
            Assert.True(companyExists);
            var companyDto = _mapper.Map(_companyToTestWith, new CompanyDto());
            await _companyController.Delete(companyDto);
            var companyRemoved = await _context.Companies.FindAsync(_companyToTestWith.Id);
            Assert.Null(companyRemoved);
        }
        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}