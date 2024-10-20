using JalilApiSecurity.Data;
using JalilApiSecurity.Entities;
using JalilApiSecurity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JalilApiSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetCars()
        {
            var cars = await _context.Cars
                .Select(c => new GetCarsModel()
                {
                    CarId = c.CarId,
                    Model = c.Model,
                    Price = c.Price,
                })
            .ToListAsync();

            return Ok(cars);
        }


        [HttpGet("{carId}")]
        [Authorize(AuthenticationSchemes = "userAuth")]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car is null)
            {
                return NotFound($"There isn't souch whit given id:{carId}");
            }
            return Ok(car);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "userAuth")]
        public async Task<IActionResult> CreateCar(PostCarsModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var car = new Car()
            {
                Model = postModel.Model,
                Color = postModel.Color,
                Price = postModel.Price,
                CreatedDate = DateTime.UtcNow.AddHours(4),
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateCar", car);

        } 
    }
}
