using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.API.DTO;
using API.DTO.API.DTO.Mappers;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;



namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for ShipmentBag
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ShipmentBagsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.ShipmentBag, ShipmentBagDTO> _mapper = new();

               /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ShipmentBagsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/ShipmentBags
        /// <summary>
        /// Get the list of all ShipmentBags .
        /// </summary>
        /// <returns>List of ShipmentBags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentBagDTO>>> GetShipmentBags()
        {
            var shipmentBags = (await _bll.ShipmentBags.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(shipmentBags);
        }

        // GET: api/ShipmentBags/5
        /// <summary>
        /// Get single ShipmentBag by given id
        /// </summary>
        /// <param name="id">Id of the ShipmentBag that we are returning</param>
        /// <returns>ShipmentBag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentBagDTO>> GetShipmentBag(Guid id)
        {
            var shipmentBag = await _bll.ShipmentBags.FirstOrDefaultAsync(id);

            if (shipmentBag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(shipmentBag));
        }

        // PUT: api/ShipmentBags/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing ShipmentBag by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the ShipmentBag from DB</param>
        /// <param name="shipmentBagDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentBag(Guid id, ShipmentBagDTO shipmentBagDTO)
        {
            if (id != shipmentBagDTO.Id)
            {
                return BadRequest();
            }

            await _bll.ShipmentBags.UpdateAsync(_mapper.Map(shipmentBagDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ShipmentBags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new ShipmentBag to the DB.
        /// </summary>
        /// <param name="shipmentBagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<ShipmentBagDTO>> PostShipmentBag(ShipmentBagDTO shipmentBagDTO)
        {
            var bllEntity = _mapper.Map(shipmentBagDTO);
            _bll.ShipmentBags.Add(bllEntity);
            await _bll.SaveChangesAsync();

            shipmentBagDTO.Id = bllEntity.Id;

            return Ok(shipmentBagDTO);
        }

        // DELETE: api/ShipmentBags/5
        /// <summary>
        /// Deletes a ShipmentBag record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShipmentBagDTO>> DeleteShipmentBag(Guid id)
        {
            var shipmentBag = await _bll.ShipmentBags.FirstOrDefaultAsync(id);
            if (shipmentBag == null)
            {
                return NotFound();
            }

            await _bll.ShipmentBags.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(shipmentBag));
        }
        
    }
}
