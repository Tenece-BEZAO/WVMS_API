using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.DAL.Interfaces;

namespace WVMS.BLL.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Vendor> _vendorRepo;

        public VendorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendorRepo = _unitOfWork.GetRepository<Vendor>();
        }

        public async Task<IEnumerable<Vendor>> GetAllVendors()
        {
            IEnumerable<Vendor> vendorList = await _vendorRepo.GetAllAsync();
            if (!vendorList.Any())
                throw new InvalidOperationException("Vendor list is empty");

            return vendorList;
        }

        public async Task<Vendor> GetVendor(int id)
        {
            /*Vendor vendor = await _vendorRepo.GetSingleByAsync(v => v.VendorId == id);
            if (vendor == null)
                throw new InvalidOperationException("Sorry, there's no vendor with that Id");
            return vendor;*/
            throw new NotImplementedException();
        }

        /*public async Task<Vendor> GetVendorByProduct(string vendor)
        {
            IEnumerable<Vendor> vendorList = await _vendorRepo.GetSingleBy(v => v.Products = vendorList);
            if (vendorList == null)
                throw new InvalidOperationException("Sorry, there's no vendor with that Id");
            return vendorList;
        }*/

        public bool VendorExist(int prodId)
        {
            throw new NotImplementedException();
        }
    }
}
