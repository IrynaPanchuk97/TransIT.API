using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;
using TransIT.DAL.UnitOfWork;
using NUnit;
using NUnit.Framework;

namespace TransIT.BLL.NTests.Services
{
    public abstract class CrudServiceTest<TEntity> where TEntity : class, IEntity, new()
    {
        protected Mock<IUnitOfWork> _unitOfWork;
        protected Mock<IBaseRepository<TEntity>> _repository;
        protected Mock<ILogger<CrudService<TEntity>>> _logger;
        protected CrudService<TEntity> _crudService;
        protected List<TEntity> _context;
        protected int _idCounter;

        [SetUp]
        public void Setup()
        {
            InitializeMocks();
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetAsync_GivenValidId_ReturnsEntity(int id)
        {
            var result = await _crudService.GetAsync(id);

            Assert.AreEqual(result, _context[id - 1]);
        }

        protected void InitializeMocks()
        {
            InitializeContext();
            InitializeRepositoryMock();
            InitializeUnitOfWork();
            InitializeLogger();
            InitializeService();
        }

        protected void InitializeContext()
        {
            _idCounter = 5;
            _context = new List<TEntity>
            {
                new TEntity { Id = 1 },
                new TEntity { Id = 2 },
                new TEntity { Id = 3 },
                new TEntity { Id = 4 },
            };
        }

        protected void InitializeRepositoryMock()
        {
            _repository = new Mock<IBaseRepository<TEntity>>();

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(_context);

            _repository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _context.Find(g => g.Id == id));

            _repository.Setup(r => r.GetRangeAsync(It.IsAny<uint>(), It.IsAny<uint>()))
                .Returns<uint, uint>((offset, size) => Task.FromResult(_context.Skip((int)offset).Take((int)size)));

            _repository.Setup(r => r.AddAsync(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity =>
                {
                    entity.Id = _idCounter++;
                    _context.Add(entity);
                })
                .ReturnsAsync((TEntity entity) => entity);

            _repository.Setup(r => r.Remove(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity =>
                {
                    var founded = _context.Find(g => g.Id == entity.Id);
                    if (founded == null)
                    {
                        throw new DbUpdateException("", new Exception());
                    }
                    _context.Remove(founded);
                });

            _repository.Setup(r => r.Update(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity => _context[_context.FindIndex(e => e.Id == entity.Id)] = entity);
        }

        protected void InitializeUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(_context.Count);
        }

        protected void InitializeLogger()
        {
            _logger = new Mock<ILogger<CrudService<TEntity>>>();
        }


        protected abstract void InitializeService();







    }


}
