using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _dataService.GetList();
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
 
        [HttpPost("add")]
        public IActionResult Add(Data data)
        {
         
            var result = _dataService.Add(data);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _dataService.GetById(id);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Data data)
        {
            var result = _dataService.Update(data);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Data data)
        {

            var result = _dataService.Delete(data);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}