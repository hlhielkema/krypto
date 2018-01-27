using ProjectBluefox.Database.Tables;
using ProjectBluefox.Database.Util;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace ProjectBluefox.Database.Managers
{
    public class LinksManager
    {
        /// <summary>
        /// Get all categories and their items
        /// </summary>
        /// <returns>categories and their items</returns>
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

        /// <summary>
        /// Create a new link category
        /// </summary>
        /// <param name="title">category title</param>
        /// <param name="createdBy">username of the creator</param>
        /// <returns>external category id</returns>
        public static Guid CreateLinkCategory(string title, string createdBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the tables
                Table<LinkCategoryTable> categories = connection.GetTable<LinkCategoryTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Create the external id for the new category
                Guid externalId = Guid.NewGuid();
                
                // Insert the new category
                categories.InsertOnSubmit(new LinkCategoryTable()
                {
                    ExternalId = externalId,
                    Title = title,
                    CreatedBy = AccountsManager.GetAccountId(connection, createdBy),
                    DateCreated = DateTime.Now,
                    Deleted = false,
                });

                // Submit the changes
                connection.SubmitChanges();

                // Return the external id of the new category
                return externalId;
            }
        }

        /// <summary>
        /// Create a new link item
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public static Guid CreateLinkItem(Guid categoryId, string title, string url, string createdBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the tables
                Table<LinkCategoryTable> categories = connection.GetTable<LinkCategoryTable>();
                Table<LinkItemTable> items = connection.GetTable<LinkItemTable>();

                // Try to get the category entry
                LinkCategoryTable category = categories.FirstOrDefault(x => x.ExternalId == categoryId && !x.Deleted);
                if (category == null)
                    throw new Exception("Category not found");

                // Create the external id for the new link item
                Guid externalId = Guid.NewGuid();

                // Insert the new item
                items.InsertOnSubmit(new LinkItemTable()
                {
                    Category = category.Id,
                    ExternalId = externalId,
                    Title = title,
                    Url = url,
                    CreatedBy = AccountsManager.GetAccountId(connection, createdBy),
                    DateCreated = DateTime.Now,
                    Deleted = false,
                });

                // Submit the changes
                connection.SubmitChanges();

                // Return the external id of the new item
                return externalId;
            }
        }

        /// <summary>
        /// Delete a link category
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <param name="deletedBy">username of the deletor</param>
        /// <returns>
        ///     true = deleted state changed
        ///     false = deleted state did not change
        /// </returns>
        public static bool DeleteLinkCategory(Guid categoryId, string deletedBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<LinkCategoryTable> categories = connection.GetTable<LinkCategoryTable>();                

                // Try to get the category entry
                LinkCategoryTable category = categories.FirstOrDefault(x => x.ExternalId == categoryId && !x.Deleted);
                if (category == null)
                    return false; // The deleted state did not changed, return false

                // Set the deleted flag to true
                category.Deleted = true;

                // Set the deletor of the category
                category.DeletedBy = AccountsManager.GetAccountId(connection, deletedBy);

                // Submit the changes
                connection.SubmitChanges();

                // The deleted state changed, return true
                return true;
            }
        }

        /// <summary>
        /// Delete a link item
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <param name="deletedBy">username of the deletor</param>
        /// <returns>
        ///     true = deleted state changed
        ///     false = deleted state did not change
        /// </returns>
        public static bool DeleteLinkItem(Guid categoryId, string deletedBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table             
                Table<LinkItemTable> items = connection.GetTable<LinkItemTable>();

                // Try to get the category item
                LinkItemTable item = items.FirstOrDefault(x => x.ExternalId == categoryId && !x.Deleted);
                if (item == null)
                    return false; // The deleted state did not changed, return false

                // Set the deleted flag to true
                item.Deleted = true;

                // Set the deletor of the item
                item.DeletedBy = AccountsManager.GetAccountId(connection, deletedBy);

                // Submit the changes
                connection.SubmitChanges();

                // The deleted state changed, return true
                return true;
            }
        }
    }
}