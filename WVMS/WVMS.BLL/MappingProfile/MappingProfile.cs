using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;

namespace WVMS.BLL.MappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, AppUsers>();

            CreateMap<VendorForRegistration, AppUsers>();

            CreateMap<CustomerForRegistration, AppUsers>();

            CreateMap<Product, ProductDto>();

            CreateMap<Vendor, VendorDto>();

        }
    }
}
