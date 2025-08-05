using Inventory_Management_System.Dtos.SaleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interface
{
    public interface ISaleApprovalService
    {
        Task<bool> CanApproveSale(SaleManagerDto salemanagerDto);
    }
}
