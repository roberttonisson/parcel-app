using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.API.DTO;
using API.DTO.API.DTO.Mappers;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for ParcelBags
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ParcelBagsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.ParcelBag, ParcelBagDTO> _mapper = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ParcelBagsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ParcelBags
        /// <summary>
        /// Get the list of all ParcelBags .
        /// </summary>
        /// <returns>List of ParcelBags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelBag>>> GetParcelBags()
        {
            var parcelBags = (await _bll.ParcelBags.GetAllAsync(null, false))
                .Select(bllEntity => _mapper.Map(bllEntity));

            return Ok(parcelBags);
        }

        // GET: api/ParcelBags/5
        /// <summary>
        /// Get single ParcelBag by given id
        /// </summary>
        /// <param name="id">Id of the ParcelBag that we are returning</param>
        /// <returns>ParcelBag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelBagDTO>> GetParcelBag(Guid id)
        {
            var parcelBag = await _bll.ParcelBags.FirstOrDefaultAsync(id);

            if (parcelBag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(parcelBag));
        }

        // PUT: api/ParcelBags/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing ParcelBag by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the ParcelBag from DB</param>
        /// <param name="parcelBagDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcelBag(Guid id, ParcelBagDTO parcelBagDTO)
        {
            if (id != parcelBagDTO.Id)
            {
                return BadRequest();
            }

            await _bll.ParcelBags.UpdateAsync(_mapper.Map(parcelBagDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ParcelBags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new ParcelBag to the DB.
        /// </summary>
        /// <param name="parcelBagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<ParcelBagDTO>> PostParcelBag(ParcelBagDTO parcelBagDTO)
        {
            var bllEntity = _mapper.Map(parcelBagDTO);
            _bll.ParcelBags.Add(bllEntity);
            await _bll.SaveChangesAsync();

            parcelBagDTO.Id = bllEntity.Id;

            return Ok(parcelBagDTO);
        }

        // POST: api/ParcelBags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new ParcelBag to the DB.
        /// </summary>
        /// <param name="parcelBagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost("addToShipments")]
        public async Task<ActionResult> PostParcelBagToShipments(ParcelBagAddDTO parcelBagDTO)
        {
            var bag = _bll.Bags.Add(new Bag());
            await _bll.SaveChangesAsync();
            _bll.ShipmentBags.Add(new ShipmentBag
            {
                ShipmentId = parcelBagDTO.ShipmentId,
                BagId = bag.Id,
            });
            await _bll.SaveChangesAsync();

            var d =_bll.ParcelBags.Add(new ParcelBag
            {
                BagId = bag.Id,
                ParcelId = parcelBagDTO.ParcelId,
            });
            await _bll.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/ParcelBags/5
        /// <summary>
        /// Deletes a ParcelBag record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParcelBagDTO>> DeleteParcelBag(Guid id)
        {
            var parcelBag = await _bll.ParcelBags.FirstOrDefaultAsync(id);
            if (parcelBag == null)
            {
                return NotFound();
            }

            await _bll.ParcelBags.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(parcelBag));
        }
    }
}