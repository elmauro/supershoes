using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SuperShoes.Models;

namespace SuperShoes.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/Articles
        public ArticlesDTO GetArticles()
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

            var allarticles = new ArticlesDTO();
            allarticles.articles = articles;
            allarticles.success = "true";
            allarticles.total_elements = articles.Count();

            return allarticles;
        }

        // GET: api/Articles/stores/1
        [Route("stores/{id:int}")]
        public HttpResponseMessage GetArticlesStores(int id)
        {
            //return db.Articles;

            var articles = from a in db.Articles.Where(a => a.Store.id == id)
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
                var message = new
                {
                    error_msg = "Record not Found",
                    error_code = "404",
                    success = "false"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
            }

            var allarticles = new ArticlesDTO();

            if (ModelState.IsValid)
            {
                allarticles.articles = articles;
                allarticles.success = "true";
                allarticles.total_elements = articles.Count();

                return Request.CreateResponse(HttpStatusCode.OK, allarticles);
            }
            else
            {
                var message = new
                {
                    error_msg = "Bad Request",
                    error_code = "400",
                    success = "false"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
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
                return NotFound();
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
                return BadRequest(ModelState);
            }

            if (id != article.id)
            {
                return BadRequest();
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
                return BadRequest(ModelState);
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
                return NotFound();
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