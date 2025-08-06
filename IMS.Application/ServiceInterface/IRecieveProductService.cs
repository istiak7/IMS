using IMS.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IRecieveProductService
    {
        Task<bool> RecieveProducts(RecieveProductDto rDto);
    }
}
