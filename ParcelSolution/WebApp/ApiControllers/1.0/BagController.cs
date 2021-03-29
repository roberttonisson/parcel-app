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
    /// Controller for Bags
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BagsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.Bag, BagDTO> _mapper = new();

               /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public BagsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Bags
        /// <summary>
        /// Get the list of all Bags .
        /// </summary>
        /// <returns>List of Bags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BagDTO>>> GetBags()
        {
            var bags = (await _bll.Bags.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(bags);
        }

        // GET: api/Bags/5
        /// <summary>
        /// Get single Bag by given id
        /// </summary>
        /// <param name="id">Id of the Bag that we are returning</param>
        /// <returns>Bag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BagDTO>> GetBag(Guid id)
        {
            var bag = await _bll.Bags.FirstOrDefaultAsync(id);

            if (bag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(bag));
        }

        // PUT: api/Bags/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Bag by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Bag from DB</param>
        /// <param name="bagDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBag(Guid id, BagDTO bagDTO)
        {
            if (id != bagDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Bags.UpdateAsync(_mapper.Map(bagDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Bags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Bag to the DB.
        /// </summary>
        /// <param name="bagDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<BagDTO>> PostBag(BagDTO bagDTO)
        {
            var bllEntity = _mapper.Map(bagDTO);
            _bll.Bags.Add(bllEntity);
            await _bll.SaveChangesAsync();

            bagDTO.Id = bllEntity.Id;

            return Ok(bagDTO);
        }

        // DELETE: api/Bags/5
        /// <summary>
        /// Deletes a Bag record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<BagDTO>> DeleteBag(Guid id)
        {
            var bag = await _bll.Bags.FirstOrDefaultAsync(id);
            if (bag == null)
            {
                return NotFound();
            }

            await _bll.Bags.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(bag));
        }
        
    }
}
