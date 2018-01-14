using ProjectBluefox.Data.Models;
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
    /// <summary>
    /// Currencies database manager
    /// </summary>
    public static class CurrenciesManager
    {
        /// <summary>
        /// Get all currencies
        /// </summary>
        /// <returns>list of currencies</returns>
        public static List<CurrencyInfo> GetCurrencies()
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyCommentTable> comments = connection.GetTable<CurrencyCommentTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();
                Table<CurrencyValueRatesTable> currencyRates = connection.GetTable<CurrencyValueRatesTable>();

                DateTime recentDate = DateTime.Now.AddDays(-7);

                // Create the query
                var query = from x in currencies
                            where !x.Deleted
                            join a in accounts on x.CreatedBy equals a.Id
                            join c in comments on x.Id equals c.Currency into allComments
                            let recentComments = (from c in allComments
                                                  where c.DateCreated >= recentDate
                                                  select c)
                            let score = (int?)recentComments.Sum(c => c.Vote) ?? 0
                            join r in currencyRates on x.Id equals r.Currency into allRates
                            let rates = allRates.FirstOrDefault()
                            orderby score descending
                            select new CurrencyInfo()
                            {
                                Id = x.ExternalId,
                                DisplayName = x.DisplayName,
                                ShortCode = x.Symbol,
                                DateCreated = x.DateCreated,
                                CreatedBy = a.Username,
                                Score = score,
                                TotalComments = allComments.Count(),
                                RecentComments = recentComments.Count(),
                                ValueRates = (rates == null) ? null : new CurrencyValueRates()
                                {
                                    Symbol = x.Symbol,
                                    Rank = rates.Rank,
                                    PriceUsd = rates.PriceUsd,
                                    VolumeUsd24h = rates.VolumeUsd24h,
                                    MarketCapUsd = rates.MarketCapUsd,
                                    AvailableSupply = rates.AvailableSupply,
                                    TotalSupply = rates.TotalSupply,
                                    MaxSupply = rates.MaxSupply,
                                    PercentChange1h = rates.PercentChange1h,
                                    PercentChange24h = rates.PercentChange24h,
                                    LastUpdated = rates.LastUpdated,
                                    PriceEur = rates.PriceEur,
                                    VolumeEur24h = rates.VolumeEur24h,
                                    MarketCapEur = rates.MarketCapEur,
                                }
                            };

                // Execute the query
                return query.ToList();
            }
        }

        /// <summary>
        /// Get a currency by its external id
        /// </summary>
        /// <param name="externalId">external currency id</param>
        /// <returns>currency info</returns>
        public static CurrencyInfo GetCurrencyById(Guid externalId)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyCommentTable> comments = connection.GetTable<CurrencyCommentTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();
                Table<CurrencyValueRatesTable> currencyRates = connection.GetTable<CurrencyValueRatesTable>();

                DateTime recentDate = DateTime.Now.AddDays(-7);

                // Create the query
                var query = from x in currencies
                            where x.ExternalId == externalId
                            where !x.Deleted
                            join a in accounts on x.CreatedBy equals a.Id
                            join c in comments on x.Id equals c.Currency into allComments
                            let recentComments = (from c in allComments
                                                  where c.DateCreated >= recentDate
                                                  select c)
                            join r in currencyRates on x.Id equals r.Currency into allRates
                            let rates = allRates.FirstOrDefault()
                            select new CurrencyInfo()
                            {
                                Id = x.ExternalId,                                
                                DisplayName = x.DisplayName,
                                ShortCode = x.Symbol,
                                DateCreated = x.DateCreated,
                                CreatedBy = a.Username,
                                Score = (int?)recentComments.Sum(c => c.Vote) ?? 0,
                                TotalComments = allComments.Count(),
                                RecentComments = recentComments.Count(),
                                ValueRates = (rates == null) ? null : new CurrencyValueRates()
                                {
                                    Symbol = x.Symbol,
                                    Rank = rates.Rank,
                                    PriceUsd = rates.PriceUsd,
                                    VolumeUsd24h = rates.VolumeUsd24h,
                                    MarketCapUsd = rates.MarketCapUsd,
                                    AvailableSupply = rates.AvailableSupply,
                                    TotalSupply = rates.TotalSupply,
                                    MaxSupply = rates.MaxSupply,
                                    PercentChange1h = rates.PercentChange1h,
                                    PercentChange24h = rates.PercentChange24h,
                                    LastUpdated = rates.LastUpdated,
                                    PriceEur = rates.PriceEur,
                                    VolumeEur24h = rates.VolumeEur24h,
                                    MarketCapEur = rates.MarketCapEur,
                                }
                            };

                // Execute the query
                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get a currency by its shortcode
        /// </summary>
        /// <param name="symbol">currency symbol</param>
        /// <returns>currency info</returns>
        public static CurrencyInfo GetCurrencyBySymbol(string symbol)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyCommentTable> comments = connection.GetTable<CurrencyCommentTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();
                Table<CurrencyValueRatesTable> currencyRates = connection.GetTable<CurrencyValueRatesTable>();

                DateTime recentDate = DateTime.Now.AddDays(-7);

                // Create the query
                var query = from x in currencies
                            where x.Symbol == symbol
                            where !x.Deleted
                            join a in accounts on x.CreatedBy equals a.Id
                            join c in comments on x.Id equals c.Currency into allComments
                            let recentComments = (from c in allComments
                                                  where c.DateCreated >= recentDate
                                                  select c)
                            join r in currencyRates on x.Id equals r.Currency into allRates
                            let rates = allRates.FirstOrDefault()
                            select new CurrencyInfo()
                            {
                                Id = x.ExternalId,
                                DisplayName = x.DisplayName,
                                ShortCode = x.Symbol,
                                DateCreated = x.DateCreated,
                                CreatedBy = a.Username,
                                Score = (int?)recentComments.Sum(c => c.Vote) ?? 0,
                                TotalComments = allComments.Count(),
                                RecentComments = recentComments.Count(),
                                ValueRates = (rates == null) ? null : new CurrencyValueRates()
                                {
                                    Symbol = x.Symbol,
                                    Rank = rates.Rank,
                                    PriceUsd = rates.PriceUsd,
                                    VolumeUsd24h = rates.VolumeUsd24h,
                                    MarketCapUsd = rates.MarketCapUsd,
                                    AvailableSupply = rates.AvailableSupply,
                                    TotalSupply = rates.TotalSupply,
                                    MaxSupply = rates.MaxSupply,
                                    PercentChange1h = rates.PercentChange1h,
                                    PercentChange24h = rates.PercentChange24h,
                                    LastUpdated = rates.LastUpdated,
                                    PriceEur = rates.PriceEur,
                                    VolumeEur24h = rates.VolumeEur24h,
                                    MarketCapEur = rates.MarketCapEur,
                                }
                            };

                // Execute the query
                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get if a currency with a given display name or short code exists
        /// </summary>
        /// <param name="displayname">display name</param>
        /// <param name="shortCode">short code</param>
        /// <returns>true=exists, false=does not exist</returns>
        public static bool CurrencyExists(string displayname, string shortCode)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                return CurrencyExists(connection, displayname, shortCode);
            }
        }

        /// <summary>
        /// Get if a currency with a given display name or short code exists
        /// </summary>
        /// <param name="connection">MSSQL database connection</param>
        /// <param name="displayname">display name</param>
        /// <param name="shortCode">short code</param>
        /// <returns>true=exists, false=does not exist</returns>
        private static bool CurrencyExists(MSSqlConnection connection, string displayname, string shortCode)
        {
            // Get the table
            Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();

            // Check if a currency exist with the same display name or short code
            return currencies.Any(x => !x.Deleted && (x.DisplayName.ToLower() == displayname.ToLower() ||
                                                      x.Symbol == shortCode.ToUpper()));
        }

        /// <summary>
        /// Get if a currency with a given short code exists
        /// </summary>        
        /// <param name="shortCode">short code</param>
        /// <returns>true=exists, false=does not exist</returns>
        public static bool CurrencyExists(string shortCode)
        {
            // Return false if the shortcode is not set
            if (string.IsNullOrWhiteSpace(shortCode))
                return false;

            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                return CurrencyExists(connection, shortCode);
            }
        }

        /// <summary>
        /// Get if a currency with a given display name or short code exists
        /// </summary>
        /// <param name="connection">MSSQL database connection</param>        
        /// <param name="shortCode">short code</param>
        /// <returns>true=exists, false=does not exist</returns>
        private static bool CurrencyExists(MSSqlConnection connection, string shortCode)
        {
            // Get the table
            Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();

            // Check if a currency exist with the same display name or short code
            return currencies.Any(x => !x.Deleted && x.Symbol == shortCode.ToUpper());
        }


        /// <summary>
        /// Get if a currency with a given external id exists
        /// </summary>
        /// <param name="externalId">external id</param>
        /// <returns>true=exists, false=does not exist</returns>
        public static bool CurrencyExists(Guid externalId)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                return CurrencyExists(connection, externalId);
            }
        }

        /// <summary>
        /// Get if a currency with a given external id exists
        /// </summary>
        /// <param name="connection">MSSQL database connection</param>
        /// <param name="externalId">external id</param>
        /// <returns>true=exists, false=does not exist</returns>
        private static bool CurrencyExists(MSSqlConnection connection, Guid externalId)
        {
            // Get the table
            Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();

            // Check if a currency exist with the same external id
            return currencies.Any(x => x.ExternalId == externalId && !x.Deleted);
        }

        /// <summary>
        /// Create a new currency
        /// </summary>
        /// <param name="displayName">display name</param>
        /// <param name="shortCode">short code</param>
        /// <param name="createdBy">creator username</param>
        /// <returns>currency external id</returns>
        public static Guid CreateCurrency(string displayName, string shortCode, string createdBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Check if the currency already exists
                if (CurrencyExists(displayName, shortCode))
                    throw new InvalidOperationException("A currency with the same display name or shortcode already exists");

                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();

                // Create the external id for the currency
                Guid externalId = Guid.NewGuid();

                // Insert the new currency
                currencies.InsertOnSubmit(new CurrencyTable()
                {
                    ExternalId = externalId,
                    DisplayName = displayName,
                    Symbol = shortCode.ToUpper(),
                    Deleted = false,
                    CreatedBy = AccountsManager.GetAccountId(connection, createdBy),
                    DateCreated = DateTime.Now,
                });

                // Submit the changes
                connection.SubmitChanges();

                // Return the external id of the new currency
                return externalId;
            }
        }

        /// <summary>
        /// Delete a currency
        /// </summary>
        /// <param name="currencyId">currency external id</param>
        /// <returns>true=currency deleted,false=currency not found</returns>
        public static bool DeleteCurrency(Guid currencyId)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();

                // Get the currency
                CurrencyTable currency = currencies.FirstOrDefault(x => x.ExternalId == currencyId && !x.Deleted);
                if (currency == null)
                    return false;

                // Set the currency deleted state
                currency.Deleted = true;

                // Submit the changes
                connection.SubmitChanges();

                // Currency set to deleted, return true
                return true;
            }
        }

        public static List<CommentModel> GetComments(Guid currencyId)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyCommentTable> comments = connection.GetTable<CurrencyCommentTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Create the query
                var query = from x in comments
                            where !x.Deleted
                            join c in currencies on x.Currency equals c.Id
                            where c.ExternalId == currencyId
                            join a in accounts on x.CreatedBy equals a.Id
                            orderby x.DateCreated descending
                            select new CommentModel()
                            {
                                Id = x.ExternalId,
                                CreatedBy = a.Username,
                                Message = x.Message,
                                Vote = x.Vote,
                                DateCreated = x.DateCreated,
                            };

                // Execute the query
                return query.ToList();
            }
        }

        public static Guid CreateComment(Guid currencyId, string message, int vote, string createdBy)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the tables
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyCommentTable> comments = connection.GetTable<CurrencyCommentTable>();
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Get the currency
                CurrencyTable currency = currencies.FirstOrDefault(x => x.ExternalId == currencyId && !x.Deleted);
                if (currency == null)
                    throw new InvalidOperationException("Currency not found");

                // Create the external id for the comment
                Guid externalId = Guid.NewGuid();

                // Insert the comment
                comments.InsertOnSubmit(new CurrencyCommentTable()
                {
                    ExternalId = externalId,
                    Currency = currency.Id,
                    Message = message,
                    Vote = vote,
                    CreatedBy = AccountsManager.GetAccountId(connection, createdBy),
                    DateCreated = DateTime.Now,
                    Deleted = false,
                });

                // Submit the changes
                connection.SubmitChanges();

                // Return the external id of the new comment
                return externalId;
            }
        }

        public static void UpdateRates(List<CurrencyValueRates> newRates)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the tables
                Table<CurrencyTable> currencies = connection.GetTable<CurrencyTable>();
                Table<CurrencyValueRatesTable> currencyRates = connection.GetTable<CurrencyValueRatesTable>();

                // Delete all current rate entries
                currencyRates.DeleteAllOnSubmit(currencyRates.ToList());

                // Create a look-up dictionary to find the currency id for a symbol
                Dictionary<string, int> currencyLookup = currencies.ToDictionary(x => x.Symbol, x => x.Id);

                // Loop through the new value rates
                foreach (CurrencyValueRates rates in newRates)
                {
                    // Check if the currency is registered int he application
                    if (currencyLookup.ContainsKey(rates.Symbol))
                    {
                        // Insert the currency rates information
                        currencyRates.InsertOnSubmit(new CurrencyValueRatesTable()
                        {
                            Currency = currencyLookup[rates.Symbol],
                            Rank = rates.Rank,
                            PriceUsd = rates.PriceUsd,
                            VolumeUsd24h = rates.VolumeUsd24h,
                            MarketCapUsd = rates.MarketCapUsd,
                            AvailableSupply = rates.AvailableSupply,
                            TotalSupply = rates.TotalSupply,
                            MaxSupply = rates.MaxSupply,
                            PercentChange1h = rates.PercentChange1h,
                            PercentChange24h = rates.PercentChange24h,
                            LastUpdated = rates.LastUpdated,
                            PriceEur = rates.PriceEur,
                            VolumeEur24h = rates.VolumeEur24h,
                            MarketCapEur = rates.MarketCapEur,
                        });
                    }
                }

                // Submit the changes
                connection.SubmitChanges();
            }
        }
    }
}