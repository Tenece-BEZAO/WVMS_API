using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.MappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<VendorForRegistration, AppUsers>();

            CreateMap<CustomerForRegistration, AppUsers>();
            CreateMap<Product, CreateProductRequest>();

            CreateMap<CreateProductRequest, Product>();

            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, Product>();


            //var result = _mapper.Map<ProductResponse>(createProduct);

            CreateMap<Vendor, VendorDto>();
            CreateMap<ProductResponse, CreateProductRequest>();

            CreateMap<CreateProductRequest, ProductResponse>();

            CreateMap<Product, ProductResponse>();

            //
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDetail>();
            CreateMap<OrderDetail, Order>();
            
        }
    }
}
