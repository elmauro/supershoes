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
using System.Threading;

namespace SuperShoes.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/Articles
        public IHttpActionResult GetArticles()
        {
            //return db.Articles;

            var articles = from a in db.Articles
            select new ArticleDTO()
            {
                id = a.id,
                name = a.name,
                description = a.description,
                price = a.price,
                total_in_shelf = a.total_in_shelf,
                total_in_vault = a.total_in_vault,
                store_name = a.Store.name
            };

            if (articles.Count() == 0)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }
            else
            {
                var message = new
                {
                    articles = articles,
                    success = "true",
                    total_elements = articles.Count()
                };

                return new TextResult(HttpStatusCode.OK, Request, message);
            }
        }

        // GET: api/Articles/stores/1
        [Route("stores/{id}")]
        public IHttpActionResult GetArticlesStores(string id)
        {
            int n;
            bool isNumeric = int.TryParse(id, out n);

            if (isNumeric)
            {
                var articles = from a in db.Articles.Where(a => a.Store.id == n)
                               select new ArticleDTO()
                               {
                                   id = a.id,
                                   name = a.name,
                                   description = a.description,
                                   price = a.price,
                                   total_in_shelf = a.total_in_shelf,
                                   total_in_vault = a.total_in_vault,
                                   store_name = a.Store.name
                               };

                if (articles.Count() == 0)
                {
                    return new TextResult(HttpStatusCode.NotFound, Request, articles);
                }
                else
                {
                    var message = new
                    {
                        articles = articles,
                        success = "true",
                        total_elements = articles.Count()
                    };

                    return new TextResult(HttpStatusCode.OK, Request, message);
                }
            }
            else {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }          
        }

        // GET: api/Articles/5
        [Route("{id:int}")]
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> GetArticle(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }

            return Ok(article);
        }

        // PUT: api/Articles/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            if (id != article.id)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articles
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return new TextResult(HttpStatusCode.BadRequest, Request, null);
            }

            db.Articles.Add(article);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = article.id }, article);
        }

        // DELETE: api/Articles/5
        [Route("{id:int}")]
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> DeleteArticle(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return new TextResult(HttpStatusCode.NotFound, Request, null);
            }

            db.Articles.Remove(article);
            await db.SaveChangesAsync();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.id == id) > 0;
        }
    }
}