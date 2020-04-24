using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApi.Data;
using WeatherApi.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace WeatherApi.Controllers {
    
    [ApiController]
    [Route("migrations")]
    public class MigrationController : ControllerBase {
        readonly ILogger<MigrationController> _logger;
        readonly ILocationLogDbContext _db;

        public MigrationController(ILogger<MigrationController> logger, LocationLogDbContext dbContext) {
            _logger = logger;
            _db = dbContext;
        }

        [HttpGet]
        [Route("run")]
        public IActionResult Run() {
            try {
                if (!_db.Database.EnsureCreated()) {
                    _db.Database.Migrate();
                }
            } catch (Exception ex) {
                _logger.LogError($"Failed to migrate DB, {ex.Message}");
            }
                
            var migrated = "Migrated";
            return Ok(migrated);
        }
    }
}
