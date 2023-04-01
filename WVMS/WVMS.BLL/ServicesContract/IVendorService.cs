using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;

namespace WVMS.BLL.ServicesContract
{
    public interface IVendorService
    {
        Task<Vendor> GetVendor(int id);
        //Task<Vendor> GetVendorByProduct();

        Task<IEnumerable<Vendor>> GetAllVendors();

        bool VendorExist(int prodId);
    }
}
