using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        public VendorController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Vendor>))]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAllVendors()
        {
            IEnumerable<Vendor> vendors = await _vendorService.GetAllVendors();
            return Ok(vendors);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Vendor))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Vendor>>>GetVendor(int Id)
        {
            Vendor vendor = await _vendorService.GetVendor(Id);
            return Ok(vendor);
        }
    }
}
