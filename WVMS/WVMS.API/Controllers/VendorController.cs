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

        /// <summary>
        /// Gets lists of all the vendors
        /// </summary>
        /// <returns>Vendor list</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Vendor>))]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAllVendors()
        {
            IEnumerable<Vendor> vendors = await _vendorService.GetAllVendors();
            return Ok(vendors);
        }

        /// <summary>
        /// Gets a particular vendor by Id
        /// </summary>
        /// <returns>The order list</returns>
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
