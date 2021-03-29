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
    /// Controller for LetterBags
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LetterBagsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.LetterBag, LetterBagDTO> _mapper = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public LetterBagsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/LetterBags
        /// <summary>
        /// Get the list of all LetterBags .
        /// </summary>
        /// <returns>List of LetterBags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LetterBagDTO>>> GetLetterBags()
        {
            var letterBags = (await _bll.LetterBags.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));

            return Ok(letterBags);
        }

        // GET: api/LetterBags/5
        /// <summary>
        /// Get single LetterBag by given id
        /// </summary>
        /// <param name="id">Id of the LetterBag that we are returning</param>
        /// <returns>LetterBag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LetterBagDTO>> GetLetterBag(Guid id)
        {
            var letterBag = await _bll.LetterBags.FirstOrDefaultAsync(id);

            if (letterBag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(letterBag));
        }

        // PUT: api/LetterBags/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing LetterBag by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the LetterBag from DB</param>
        /// <param name="letterBagDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLetterBag(Guid id, LetterBagDTO letterBagDTO)
        {
            if (id != letterBagDTO.Id)
            {
                return BadRequest();
            }

            await _bll.LetterBags.UpdateAsync(_mapper.Map(letterBagDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LetterBags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new LetterBag to the DB.
        /// </summary>
        /// <param name="letterBagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<LetterBagDTO>> PostLetterBag(LetterBagDTO letterBagDTO)
        {
            var bllEntity = _mapper.Map(letterBagDTO);
            _bll.LetterBags.Add(bllEntity);
            await _bll.SaveChangesAsync();

            letterBagDTO.Id = bllEntity.Id;

            return Ok(letterBagDTO);
        }

        // POST: api/LetterBags/createbag
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new LetterBag to the Shipments and create bag for it.
        /// </summary>
        /// <param name="letterBagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>:)</returns>
        [HttpPost("addToShipments")]
        public async Task<ActionResult> PostLetterBagToShipment(LetterBagAddDTO letterBagDTO)
        {
            var bllEntity = _mapper.Map(new LetterBagDTO
            {
                Count = letterBagDTO.Count,
                Weight = letterBagDTO.Weight,
                Price = letterBagDTO.Price
            });
            await _bll.LetterBags.AddLetterBagToShipment(letterBagDTO.ShipmentId, bllEntity);
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/LetterBags/5
        /// <summary>
        /// Deletes a LetterBag record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LetterBagDTO>> DeleteLetterBag(Guid id)
        {
            var letterBag = await _bll.LetterBags.FirstOrDefaultAsync(id);
            if (letterBag == null)
            {
                return NotFound();
            }

            await _bll.LetterBags.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(letterBag));
        }
    }
}