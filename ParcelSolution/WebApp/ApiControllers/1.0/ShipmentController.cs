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
    /// Controller for Shipments
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.Shipment, ShipmentDTO> _mapper = new();

               /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ShipmentsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Shipments
        /// <summary>
        /// Get the list of all Shipments .
        /// </summary>
        /// <returns>List of Shipments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipments()
        {
            var shipments = (await _bll.Shipments.GetAllShipmentsWithIncludedDataAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(shipments);
        }
        
        // GET: api/Shipments
        /// <summary>
        /// Get a single shipment if it is not finalized.
        /// </summary>
        /// <returns>List of Shipments</returns>
        [HttpGet("nonFinalized/{id}")]
        public ActionResult<ShipmentDTO> GetSingleNonFinalizedShipments(string id)
        {
            var shipment = _bll.Shipments.GetSingleNonFinalizedShipment(id);
            

            return Ok(_mapper.Map(shipment!));
        }

        // GET: api/Shipments/5
        /// <summary>
        /// Get single Shipment by ID
        /// </summary>
        /// <param name="id">Id of the Shipment that we are returning</param>
        /// <returns>Shipment</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentDTO>> GetShipment(Guid id)
        {
            var shipment = await _bll.Shipments.FirstOrDefaultAsync(id);

            if (shipment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(shipment));
        }
        
        // GET: api/Shipments/5
        /// <summary>
        /// Get single Shipment by SHIPMENT NUMBER
        /// </summary>
        /// <param name="id">Shipment number of the Shipment that we are asking for</param>
        /// <returns>Shipment</returns>
        [HttpGet("sn/{id}")]
        public async Task<ActionResult<ShipmentDTO>> GetShipmentByShipmentNumber(string id)
        {
            var shipment = await _bll.Shipments.FindByShipmentNumber(id);

            if (shipment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(shipment));
        }

        // PUT: api/Shipments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Shipment by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Shipment from DB</param>
        /// <param name="shipmentDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipment(Guid id, ShipmentDTO shipmentDTO)
        {
            if (id != shipmentDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Shipments.UpdateAsync(_mapper.Map(shipmentDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Shipments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Shipment to the DB.
        /// </summary>
        /// <param name="shipmentDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<ShipmentDTO>> PostShipment(ShipmentDTO shipmentDTO)
        {
            var bllEntity = _mapper.Map(shipmentDTO);
            _bll.Shipments.Add(bllEntity);
            await _bll.SaveChangesAsync();

            shipmentDTO.Id = bllEntity.Id;

            return Ok(shipmentDTO);
        }

        // DELETE: api/Shipments/5
        /// <summary>
        /// Deletes a Shipment record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShipmentDTO>> DeleteShipment(Guid id)
        {
            var shipment = await _bll.Shipments.FirstOrDefaultAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            await _bll.Shipments.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(shipment));
        }
        
    }
}
