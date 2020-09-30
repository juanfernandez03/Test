using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeyondTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        private readonly IMatrix _matrix;
        private readonly IWordFinder _wordFinder;
        private readonly ILogger<MatrixController> _logger;

        public MatrixController(IMatrix matrix, IWordFinder wordFinder, ILogger<MatrixController> logger)
        {
            _matrix = matrix;
            _wordFinder = wordFinder;
            _logger = logger;
        }

        // GET: api/Matrix
        /// <summary>
        /// Get matrix
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<IEnumerable<char>> Get()
        {
            try
            {
                var m = _matrix.CreateMatrix();
                var matrix = m.GetColumns().ToArray();
                return matrix;
            }
            catch (Exception e)
            {
                _logger.LogError("Error in CreateMatrix " + e.Message);
                throw;
            }
        }



        // GET: api/Matrix/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Matrix
        /// <summary>
        /// Find words in matrix
        /// </summary>
        /// <param name="value">Matrix</param>
        /// <returns>Return found words</returns>
        [HttpPost]
        public IEnumerable<string> GetMatrix([FromBody] string value)
        {
            try
            {
                var matrix = value.Split('\n').ToArray();
                var words = _wordFinder.Find(matrix);
                return words;
            }
            catch (Exception e)
            {
                _logger.LogError("Error in FindWords " + e.Message);
                throw;
            }
        }

        // PUT: api/Matrix/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
