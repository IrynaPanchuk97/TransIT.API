using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    class ActionTypeService : IActionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ActionType> GetAsync(int actionTypeId)
        {
            return _unitOfWork.ActionTypeRepository.GetByIdAsync(actionTypeId);
        }

        public Task<IEnumerable<ActionType>> GetAsync(uint offset, uint amount)
        {
            return _unitOfWork.ActionTypeRepository.GetRangeAsync(offset, amount);
        }

        public async Task<ActionType> CreateAsync(ActionType actionType)
        {
            return _unitOfWork.ActionTypeRepository.AddAsync(actionType);           
        }


    }
}
