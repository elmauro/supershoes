using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperShoes.Models;

namespace SuperShoes.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/Articles
        public HttpResponseMessage GetArticles()
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

            var message = new
            {
                articles = articles,
                success = "true",
                total_elements = articles.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, message);
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

            if (articles.Count() == 0){

                var message = new
                {
                    error_msg = "Record not Found",
                    error_code = "404",
                    success = "false"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
            }
            else {
                var message = new
                {
                    articles = articles,
                    success = "true",
                    total_elements = articles.Count()
                };
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }            
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