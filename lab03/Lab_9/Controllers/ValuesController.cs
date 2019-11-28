using PIS_MVC5_1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace Lab_9.Controllers
{
    public class ValuesController : ApiController
    {
        PhoneContext _context = new PhoneContext();

        public IHttpActionResult Get(
                int limit = 2,
                int offset = 0,
                int minId = 0,
                int maxId = 100,
                string sort = "ID",
                string columns = null,
                string like = null,
                string globalLike = null
            )
        {
            IQueryable<User> users = null;

            if (sort.ToLower() == "name")
            {
                users = _context.Users.OrderBy(prop => prop.Name);
            }
            else if (sort.ToLower() == "id")
            {
                users = _context.Users.OrderBy(prop => prop.Id);
            }
            else
            {
                return BadRequest();
            }

            if (like != null)
            {
                users = users.Where(prop => prop.Name.ToLower().Contains(like.ToLower()));
            }

            if (globalLike != null)
            {
                users = users.Where(prop => (prop.Name + prop.Id.ToString() + prop.Phone).ToLower().Contains(globalLike.ToLower()));
            }

            IEnumerable<User> resultUsers = null;
            List<dynamic> res = new List<dynamic>();

            if (columns != null)
            {
                resultUsers = users.Select(DynamicSelectGenerator<User, User>(columns));
                List<User> tempUsers = users.ToList();
                int i = 0;
                foreach (var item in resultUsers)
                {
                    dynamic temp = new ExpandoObject();
                    if (item.Id != 0)
                    {
                        temp.Id = item.Id;
                    }
                    if (item.Name != null)
                    {
                        temp.Name = item.Name;
                    }
                    if (item.Phone != null)
                    {
                        temp.Phone = item.Phone;
                    }

                    temp.Links = new
                    {
                        href = $"api/values/{tempUsers[i].Id}",
                        rel = "User",
                        type = Request.Method.Method
                    };
                    i++;
                    res.Add(temp);
                }

                return Ok(res);
            }

            users = users.Skip(offset)
                .Take(limit + 1)
                .Where(prop => prop.Id >= minId && prop.Id <= maxId);

            resultUsers = users.ToList();
            User userPrev = new User ();
            User userNext = new User ();

            for (int i = 0; i < resultUsers.Count(); i++)
            {
                var user = resultUsers.ElementAt(i);
                if (user != null)
                {                        
                    dynamic temp = new ExpandoObject();
                    temp.Id = user.Id;
                    temp.Name = user.Name;
                    temp.Phone = user.Phone;
                    temp.Links = new List<dynamic>
                {
                    new {
                    href = $"/api/values/{user.Id - 1}",
                    rel = "User",
                    type = Request.Method.Method
                    },
                    new {
                    href = $"/api/values/{user.Id + 1}",
                    rel = "User",
                    type = Request.Method.Method
                    }

                };

                    res.Add(temp);
                }
            }

            return Ok(res);
        }

        public IHttpActionResult Get(int id)
        {
            User user = _context.Users.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]User item)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.Add(item);
                _context.SaveChanges();
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromBody]User item)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(p => p.Id == item.Id);
                if (user != null)
                {
                    user.Name = item.Name;
                    user.Phone = item.Phone;
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(user);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            User user = _context.Users.FirstOrDefault(p => p.Id == id);
            if (user != null)
            {
                user = _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok(user);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        public static Func<Y, T> DynamicSelectGenerator<Y, T>(string fields)
        {
            var xParameter = Expression.Parameter(typeof(T), "o");
            var xNew = Expression.New(typeof(Y));

            var bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o =>
                {
                    var mi = typeof(T).GetProperty(o);
                    var xOriginal = Expression.Property(xParameter, mi);
                    return Expression.Bind(mi, xOriginal);
                }
            );

            var xInit = Expression.MemberInit(xNew, bindings);
            var lambda = Expression.Lambda<Func<Y, T>>(xInit, xParameter);
            return lambda.Compile();
        }
    }
}
