using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Sample.MongoDB.DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDocumentsController : ControllerBase
    {
        private readonly IMongoCollection<MyDocument> _coll;

        public MyDocumentsController(
            IMongoCollection<MyDocument> coll)
        {
            _coll = coll;
        }

        [HttpGet]
        public async Task<IEnumerable<MyDocument>> GetAsync()
        {
            var options = new FindOptions<MyDocument, MyDocument>()
            {
                Limit = 100,
            };
            return (await _coll.FindAsync(FilterDefinition<MyDocument>.Empty, options))
                .ToEnumerable();
        }
    }
}