using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Model.ToDo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProduct();

        Task<ProductResponse> GetProductDetail(int id);
    }
}
