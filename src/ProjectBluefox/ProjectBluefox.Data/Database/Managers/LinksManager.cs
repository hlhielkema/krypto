using ProjectBluefox.Database.Tables;
using ProjectBluefox.Database.Util;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Database.Managers
{
    public class LinksManager
    {
        public static List<LinkCategoryModel> GetAllLinks()
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the tables
                Table<LinkCategoryTable> categories = connection.GetTable<LinkCategoryTable>();
                Table<LinkItemTable> items = connection.GetTable<LinkItemTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Create the query
                var query = from x in categories
                            where !x.Deleted
                            join a in accounts on x.CreatedBy equals a.Id
                            join i in items on x.Id equals i.Category into categoryItems
                            select new LinkCategoryModel()
                            {
                                Title = x.Title,
                                CreatedBy = a.Username,
                                DateCreated = x.DateCreated,
                                Items = (from i in categoryItems
                                         join a in accounts on i.CreatedBy equals a.Id
                                         where !i.Deleted
                                         select new LinkModel()
                                         {                                             
                                             Title = i.Title,
                                             Url = i.Url,
                                             CreatedBy = a.Username,
                                             DateCreated = i.DateCreated
                                         }).ToList()
                            };

                // Execute the query
                return query.ToList();
            }
        }
    }
}