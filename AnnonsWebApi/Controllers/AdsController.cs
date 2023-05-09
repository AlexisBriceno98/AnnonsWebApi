using AnnonsWebApi.Data;
using AnnonsWebApi.Models;
using AnnonsWebApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AnnonsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public AdsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // READ ALL ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ALL Ads from the database
        /// </summary>
        /// <returns>
        /// A full list of ALL Ads
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Ads
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a full list of ALL Ads
        /// </response>

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<List<AdDTO>>> GetAll()
        {
            var ads = _dbContext.AdsInfo.ToList();
            var adDTOs = ads.Select(a => new AdDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                TargetUrl = a.TargetUrl,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Ok(adDTOs);
        }

        // READ ONE ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ONE Ad from the database
        /// </summary>
        /// <returns>
        /// One Ad
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned ONE Ad
        /// </response>

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<AdDTO>> GetOne(int id)
        {
            var ad = _dbContext.AdsInfo.Find(id);
            if (ad == null)
            {
                return BadRequest("Ad not found");
            }

            var adDTO = new AdDTO
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description
            };
            return Ok(adDTO);
        }

        // CREATE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Create an Ad and save it in the database
        /// </summary>
        /// <returns>
        /// A new create Ad
        /// </returns>
        /// <remarks>
        /// Example end point: POST /api/Ads
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a created Ad
        /// </response>

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<AdDTO>> PostAd(AdDTO adDto)
        {
            var ad = new Ad
            {
                Title = adDto.Title,
                Description = adDto.Description,
                TargetUrl = adDto.TargetUrl,
                CreatedAt = DateTime.Now,
            };

            _dbContext.AdsInfo.Add(ad);
            await _dbContext.SaveChangesAsync();
            return Ok(adDto);
        }

        // UPDATE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Update an Ad and save the changes in the database
        /// </summary>
        /// <returns>
        /// An updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PUT /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned an updated Ad
        /// </response>

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<AdUpdateDTO>> UpdateAd(AdUpdateDTO adUpdateDto, int id)
        {
            var adToUpdate = _dbContext.AdsInfo.Find(id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            adToUpdate.Title = adUpdateDto.Title;
            adToUpdate.Description = adUpdateDto.Description;
            adToUpdate.TargetUrl = adUpdateDto.TargetUrl;
            adToUpdate.CreatedAt = adUpdateDto.CreatedAt;

            await _dbContext.SaveChangesAsync();

            var adDto = new AdDTO
            {
                Id = adToUpdate.Id,
                Title = adToUpdate.Title,
                Description = adToUpdate.Description,
                TargetUrl = adToUpdate.TargetUrl,
                CreatedAt = adToUpdate.CreatedAt
            };
            return Ok(adDto);
        }

        // UPDATE A PARTIAL OF AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Update a partial of an Ad and save the changes in the database
        /// </summary>
        /// <returns>
        /// A partial of the updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PATCH /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned the partial of the updated Ad
        /// </response>

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task <ActionResult<AdUpdateDTO>> PatchAd(JsonPatchDocument ad, int id)
        {
            var adToUpdate = await _dbContext.AdsInfo.FindAsync(id);

            if(adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            ad.ApplyTo(adToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.AdsInfo.ToListAsync());
        }

        // DELETE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Delete an Ad and remove it from the database
        /// </summary>
        /// <returns>
        /// Deletes the AD
        /// </returns>
        /// <remarks>
        /// Example end point: DELETE /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Ad deleted successfully
        /// </response>

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var ad = _dbContext.AdsInfo.Find(id);

            if (ad == null)
            {
                return BadRequest("Ad not found");
            }

            _dbContext.AdsInfo.Remove(ad);
            await _dbContext.SaveChangesAsync();

            return Ok("Ad deleted successfully");
        }
    }
}
