using AnnonsWebApi.Data;
using AnnonsWebApi.Models;
using AnnonsWebApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public AdsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("{id}")]
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

        [HttpPost]
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
            adDto.Id = ad.Id;
            return Ok(adDto);
        }

        [HttpPut]
        [Route("{id}")]
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

        [HttpDelete]
        [Route("{id}")]
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
