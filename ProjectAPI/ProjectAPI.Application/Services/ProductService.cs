using AutoMapper;
using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Model.ToDo.Response;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductResponse>> GetAllProduct()
        {
            var temp = await _unitOfWork.ProductRepository.GetAll();

            return _mapper.Map<IEnumerable<ProductResponse>>(temp);
        }

        public async Task<ProductResponse> GetProductDetail(int id)
        {
            var temp = await _unitOfWork.GetProductDetail(id);
            return _mapper.Map<ProductResponse>(temp);
        }

        
    }
}
