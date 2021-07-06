// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using CrawlCenter.Web.Models;
// using Microsoft.EntityFrameworkCore;
//
// namespace CrawlCenter.Web.Data.Repositories.Impl {
//     public class SqlAppRepository<T> : IAppRepository<T> where T : class {
//         private readonly AppDbContext _context;
//
//         private DbSet<T> DbSet {
//             get {
//                 if (typeof(CrawlTask) is T) {
//                     return _context.CrawlTasks as DbSet<T>;
//                 }
//
//                 if (typeof(Project) is T) {
//                     return _context.Projects as DbSet<T>;
//                 }
//
//                 if (typeof(ProjectTag) is T) {
//                     return _context.ProjectTags as DbSet<T>;
//                 }
//
//                 if (typeof(User) is T) {
//                     return _context.Users as DbSet<T>;
//                 }
//
//                 return null;
//             }
//         }
//
//         public SqlAppRepository(AppDbContext context) {
//             _context = context;
//         }
//
//         public T GetById(Guid guid) {
//             return DbSet.Find(guid);
//         }
//
//         public IEnumerable<T> GetAll() {
//             return DbSet;
//         }
//
//         public T Insert(T obj) {
//             DbSet.Add(obj);
//             _context.SaveChanges();
//             return obj;
//         }
//
//         public T Update(T obj) {
//             var item = DbSet.Attach(obj);
//             item.State = EntityState.Modified;
//             _context.SaveChanges();
//             return obj;
//         }
//
//         public T Delete(Guid guid) {
//             var item = DbSet.Find(guid);
//             if (item != null) {
//                 DbSet.Remove(item);
//                 _context.SaveChanges();
//             }
//
//             return item;
//         }
//     }
// }