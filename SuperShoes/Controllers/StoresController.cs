using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperShoes.Models;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SuperShoes.Controllers
{
    [RoutePrefix("api/stores")]
    public class StoresController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/stores
        public IHttpActionResult GetStores()
        {
            //return db.Stores;

            var stores = from s in db.Stores
            select new StoreDTO()
            {
                id = s.id,
                name = s.name,
                address = s.address
            };

            if (stores.Count() == 0)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }
            else
            {
                var message = new
                {
                    stores = stores,
                    success = "true",
                    total_elements = stores.Count()
                };

                return new TextResult(HttpStatusCode.OK, Request, message);
            }
        }

        // GET: api/Stores/5
        [Route("{id:int}")]
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> GetStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }

            return Ok(store);
        }

        // PUT: api/Stores/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            if (id != store.id)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return new TextResult(HttpStatusCode.NotFound, Request, null);
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stores
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            db.Stores.Add(store);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = store.id }, store);
        }

        // DELETE: api/Stores/5
        [Route("{id:int}")]
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> DeleteStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return Ok(store);
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