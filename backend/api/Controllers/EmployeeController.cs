using Microsoft.AspNetCore.Mvc;
using DAL;
using Model;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDAL _dal = new EmployeeDAL();

        [HttpGet]
        public List<EmployeeDTO> GetAll() => _dal.GetAll();

        // Standard ID Route: api/employee/1
        [HttpGet("{id}")]
        public EmployeeDTO GetById(int id) => _dal.GetAll().Find(e => e.EmployeeID == id);

        // Query String Version (Part F): api/employee/search?id=1
        [HttpGet("search")]
        public EmployeeDTO GetByIdQuery([FromQuery] int id) => _dal.GetAll().Find(e => e.EmployeeID == id);

        [HttpPost]
        public int CreateEmployeeInfo([FromBody] EmployeeDTO employee) => _dal.Create(employee);
    }
}