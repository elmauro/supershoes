using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperShoes.Models;

namespace SuperShoes.Controllers
{
    [RoutePrefix("api/stores")]
    public class StoresController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/stores
        public HttpResponseMessage GetStores()
        {
            //return db.Stores;

            var stores = from s in db.Stores
            select new StoreDTO()
            {
                id = s.id,
                name = s.name,
                address = s.address
            };

            var message = new
            {
                stores = stores,
                success = "true",
                total_elements = stores.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.id == id) > 0;
        }
    }
}