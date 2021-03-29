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
    /// Controller for Parcels
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ParcelsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.Parcel, ParcelDTO> _mapper = new();

               /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ParcelsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Parcels
        /// <summary>
        /// Get the list of all  AVAILABLE Parcels .
        /// </summary>
        /// <returns>List of Parcels</returns>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetAvailableParcels()
        {
            var parcels = (await _bll.Parcels.GetAllAvailableParcels())
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(parcels);
        }
        
        // GET: api/Parcels
        /// <summary>
        /// Get the list of all Parcels .
        /// </summary>
        /// <returns>List of Parcels</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcels()
        {
            var parcels = (await _bll.Parcels.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(parcels);
        }

        // GET: api/Parcels/5
        /// <summary>
        /// Get single Parcel by given id
        /// </summary>
        /// <param name="id">Id of the Parcel that we are returning</param>
        /// <returns>Parcel</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelDTO>> GetParcel(Guid id)
        {
            var parcel = await _bll.Parcels.FirstOrDefaultAsync(id);

            if (parcel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(parcel));
        }

        // PUT: api/Parcels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Parcel by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Parcel from DB</param>
        /// <param name="parcelDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcel(Guid id, ParcelDTO parcelDTO)
        {
            if (id != parcelDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Parcels.UpdateAsync(_mapper.Map(parcelDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Parcels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Parcel to the DB.
        /// </summary>
        /// <param name="parcelDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<ParcelDTO>> PostParcel(ParcelDTO parcelDTO)
        {
            var bllEntity = _mapper.Map(parcelDTO);
            _bll.Parcels.Add(bllEntity);
            await _bll.SaveChangesAsync();

            parcelDTO.Id = bllEntity.Id;

            return Ok(parcelDTO);
        }

        // DELETE: api/Parcels/5
        /// <summary>
        /// Deletes a Parcel record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParcelDTO>> DeleteParcel(Guid id)
        {
            var parcel = await _bll.Parcels.FirstOrDefaultAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }

            await _bll.Parcels.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(parcel));
        }
        
    }
}
