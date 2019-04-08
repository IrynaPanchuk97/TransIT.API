using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.BLL.Tests.Services
{
    public class MalfunctionGroupServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMalfunctionGroupRepository> _repository;
        private Mock<ILogger<MalfunctionGroupService>> _logger;
        private MalfunctionGroupService _malfunctionGroupService;
        private List<MalfunctionGroup> _context;
        private int _idCounter;
        public MalfunctionGroupServiceTests()
        {
            InitializeMocks();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetAsync_GivenValidId_ReturnsGroup(int id)
        {
            var result = await _malfunctionGroupService.GetAsync(id);

            Assert.Equal(result, _context[id - 1]);
        }

        [Fact]
        public async Task GetAsync_GivenInvalidId_ReturnsNull()
        {
            int id = 0;

            var result = await _malfunctionGroupService.GetAsync(id);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(5, 3)]
        [InlineData(0, 0)]
        public async Task GetAllAsync_GivenOffsetAndSize_ReturnsAllGroupsInRange(uint offset, uint size)
        {
            var result = await _malfunctionGroupService.GetRangeAsync(offset, size);

            Assert.Equal(result, _context.Skip((int)offset).Take((int)size));
        }

        [Theory]
        [MemberData(nameof(SampleData))]
        public async Task CreateAsync_GivenMalfunctionGroup_AddsGroupToContext(MalfunctionGroup group)
        {
            await _malfunctionGroupService.CreateAsync(group);

            Assert.Contains(group, _context);
            Assert.Equal(group.Id, _idCounter - 1);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(typeof(Exception))]
        [InlineData(typeof(DbUpdateException))]
        public async Task CreateAsync_GivenWrongGroup_ReturnsNull(Type type)
        {
            var group = new MalfunctionGroup { Id = 1 };
            var exception = Activator.CreateInstance(type, "", new Exception()) as Exception;
            _repository.Setup(r => r.AddAsync(group)).Throws(exception);

            var result = await _malfunctionGroupService.CreateAsync(group);

            Assert.Null(result);
            _logger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), exception, It.IsAny<Func<object, Exception, string>>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task UpdateAsync_GivenExistingItem_ReturnsUpdatedItem(int id)
        {
            var item = _context.Find(g => g.Id == id);
            int previousCount = _context.Count;
            item.Name = "Test";

            var result = await _malfunctionGroupService.UpdateAsync(item);

            item = _context.Find(g => g.Id == id);
            Assert.Equal(result, item);
            Assert.True(_context.Count == previousCount);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(typeof(Exception))]
        [InlineData(typeof(DbUpdateException))]
        public async Task UpdateAsync_GivenWrongGroup_ReturnsNull(Type type)
        {
            var group = new MalfunctionGroup { Id = 1 };
            var exception = Activator.CreateInstance(type, "", new Exception()) as Exception;
            _repository.Setup(r => r.Update(group)).Throws(exception);

            var result = await _malfunctionGroupService.UpdateAsync(group);

            Assert.Null(result);
            _logger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), exception, It.IsAny<Func<object, Exception, string>>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task DeleteAsync_GivenExistingItemId_DeletesItem(int id)
        {
            int previousCount = _context.Count;

            await _malfunctionGroupService.DeleteAsync(id);

            var item = _context.Find(g => g.Id == id);
            Assert.Null(item);
            Assert.True(_context.Count == previousCount - 1);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteAsync_GivenNonExistingItemId_ReturnsNothing(int id)
        {
            int previousCount = _context.Count;

            await _malfunctionGroupService.DeleteAsync(id);

            Assert.True(_context.Count == previousCount);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(typeof(Exception))]
        [InlineData(typeof(DbUpdateException))]
        public async Task DeleteAsync_GivenExceptionInRepository_ReturnsNull(Type type)
        {
            var exception = Activator.CreateInstance(type, "", new Exception()) as Exception;
            _repository.Setup(r => r.Remove(It.IsAny<MalfunctionGroup>())).Throws(exception);

            await _malfunctionGroupService.DeleteAsync(3);

            _logger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), exception, It.IsAny<Func<object, Exception, string>>()), Times.Once);
        }

        private void InitializeMocks()
        {
            InitializeContext();
            InitializeRepositoryMock();
            InitializeUnitOfWork();
            InitializeLogger();
            InitializeService();
        }

        private void InitializeContext()
        {
            _idCounter = 5;
            _context = new List<MalfunctionGroup>
            {
                new MalfunctionGroup { Id = 1, Name = "Abc" },
                new MalfunctionGroup { Id = 2, Name = "Bcd" },
                new MalfunctionGroup { Id = 3, Name = "Cde" },
                new MalfunctionGroup { Id = 4, Name = "Def" }
            };
        }

        private void InitializeRepositoryMock()
        {
            _repository = new Mock<IMalfunctionGroupRepository>();

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(_context);

            _repository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _context.Find(g => g.Id == id));

            _repository.Setup(r => r.GetRangeAsync(It.IsAny<uint>(), It.IsAny<uint>()))
                .Returns<uint, uint>((offset, size) => Task.FromResult(_context.Skip((int)offset).Take((int)size)));

            _repository.Setup(r => r.AddAsync(It.IsAny<MalfunctionGroup>()))
                .Callback<MalfunctionGroup>(group =>
                {
                    group.Id = _idCounter++;
                    _context.Add(group);
                })
                .ReturnsAsync((MalfunctionGroup group) => group);

            _repository.Setup(r => r.Remove(It.IsAny<MalfunctionGroup>()))
                .Callback<MalfunctionGroup>(group => _context.RemoveAll(g => g.Id == group.Id));

            _repository.Setup(r => r.Update(It.IsAny<MalfunctionGroup>()))
                .Callback<MalfunctionGroup>(group => _context[_context.FindIndex(g => g.Id == group.Id)] = group);
        }

        private void InitializeUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(_context.Count);
            _unitOfWork.Setup(u => u.MalfunctionGroupRepository).Returns(_repository.Object);
        }

        private void InitializeLogger()
        {
            _logger = new Mock<ILogger<MalfunctionGroupService>>();
        }

        private void InitializeService()
        {
            _malfunctionGroupService = new MalfunctionGroupService(_unitOfWork.Object, _logger.Object);
        }

        public static IEnumerable<object[]> SampleData =>
            new[]
            {
                new object [] { new MalfunctionGroup { Id = 2, Name = "Roka" } },
                new object [] { new MalfunctionGroup { Id = 0, Name = "TEst" } },
                new object [] { new MalfunctionGroup { Id = 5, Name = "Example" } }
            };
    }
}
