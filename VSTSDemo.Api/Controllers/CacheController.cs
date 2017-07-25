using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using VSTSDemo.Api.Models;

namespace VSTSDemo.Api.Controllers
{
    [Route("api/cache")]
    public class CacheController : Controller
    {
        private readonly IConnectionMultiplexer _connection;
        public CacheController(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetKeys()
        {
            return await Task.Run(() =>
            {
                var keys = _connection.GetServer(_connection.GetEndPoints().First()).Keys().Select(x => x.ToString());
                return Ok(keys);
            });
        }

        [HttpGet("{key}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetByKey(string key)
        {
            var result = await _connection.GetDatabase().StringGetAsync(key);
            if (result.HasValue)
            {
                var obj = new CacheEntry()
                {
                    Key = key,
                    Value = result.ToString()
                };
                return Ok(obj);
            }
            return BadRequest();
        }

        [HttpPost("{key}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetByKey([FromRoute] string key, [FromBody] CacheEntry model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _connection.GetDatabase().StringSetAsync(key, model.Value);
            return Ok();
        }
    }
}
