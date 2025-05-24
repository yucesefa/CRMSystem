using CRMSystem.Domain.Entities;
using CRMSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRMSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {

        private readonly CRMDbContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(CRMDbContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] string? region, [FromQuery] DateTime? registrationDate)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name));

            if (!string.IsNullOrEmpty(region))
                query = query.Where(c => c.Region.ToLower().Contains(region.ToLower()));
            var result = await query.ToListAsync();
            _logger.LogInformation("Filtered customers retrieved: {Count}", result.Count);
            return Ok(result);
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            customer.RegistrationDate = customer.RegistrationDate;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer created: {Name}", customer.FirstName + " " + customer.LastName);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.Region = updatedCustomer.Region;
            customer.RegistrationDate = updatedCustomer.RegistrationDate;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Customer updated: ID {Id}", customer.Id);

            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Sadece Admin silebilir
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer deleted: ID {Id}", id);
            return NoContent();
        }

    }
}
