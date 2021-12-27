using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Infrastructure.Utilities.Common;
using Domain.Infrastructure.Utilities.Comporators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace Domain.Infrastructure.DataAccess.EntityFramework.DbContextBase
{
    public class DbContextBase : DbContext
    {
        protected DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<TableLog> TableLog { get; set; }
        public DbSet<TableLogDetail> TableLogDetail { get; set; }

        public override int SaveChanges()
        {
            var result = SaveChangesInternal();
            return result;
        }

        //[TransactionScopeAspect]
        private int SaveChangesInternal()
        {
            var changes = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified).ToList();
            var additions = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();

            if (changes.Any())
            {
                var changeLogs = GenerateTableLogs(changes);
                TableLog.AddRange(changeLogs);
            }

            // ID degerlerini alabilmek icin, yeni eklenen kayitlari base.SaveChanges() cagrisindan sonra logluyoruz.
            var result = base.SaveChanges();

            if (additions.Any())
            {
                var additionLogs = GenerateTableLogs(additions);
                TableLog.AddRange(additionLogs);
            }

            result += base.SaveChanges();
            return result;
        }
        private static HashSet<Type> FindModifiedEntityTypes(IEnumerable<DbEntityEntry> changes, IEnumerable<DbEntityEntry> additions)
        {
            var modifiedEntityTypes = new HashSet<Type>();
            foreach (var entry in changes)
            {
                var proxyType = entry.Entity.GetType();
                var realType = proxyType.Namespace == "System.Data.Entity.DynamicProxies" ? proxyType.BaseType : proxyType;
                modifiedEntityTypes.Add(realType);
            }
            foreach (var entry in additions)
            {
                var proxyType = entry.Entity.GetType();
                var realType = proxyType.Namespace == "System.Data.Entity.DynamicProxies" ? proxyType.BaseType : proxyType;
                modifiedEntityTypes.Add(realType);
            }
            return modifiedEntityTypes;
        }
        private IEnumerable<TableLog> GenerateTableLogs(IEnumerable<DbEntityEntry> entries)
        {
            foreach (var entity in entries)
            {
                var tableLog = GenerateTableLog(entity);

                if (tableLog != null)
                {
                    yield return tableLog;
                }
            }
        }
        private TableLog GenerateTableLog(DbEntityEntry entity)
        {
            if (entity.State == EntityState.Detached)
            {
                // no need to log audit entries.
                return null;
            }

            string crud = null;
            if (entity.State == EntityState.Unchanged ||
                entity.State == EntityState.Added)
            {
                crud = "C";

                if (entity.Entity.GetType().GetProperty("CreateDate") != null &&
                    entity.Property("CreateDate").CurrentValue == null)
                    entity.Property("CreateDate").CurrentValue = DateTime.Now;


                if (entity.Entity.GetType().GetProperty("CreationDate") != null &&
                    entity.Property("CreationDate").CurrentValue == null)
                    entity.Property("CreationDate").CurrentValue = DateTime.Now;
            }
            else if (entity.State == EntityState.Modified)
            {
                crud = "U";

                if (entity.Entity.GetType().GetProperty("UpdateDate") != null &&
                    entity.Member("UpdateDate").CurrentValue == null)
                    entity.Member("UpdateDate").CurrentValue = DateTime.Now;
            }
            else if (entity.State == EntityState.Deleted)
            {
                crud = "D";
            }

            var tableLog = new TableLog
            {
                State = crud,
                PrimaryKeyValue = GetPrimaryKeyValue(entity).ToString(),
                TableName = GetTableName(entity),
                TimeStamp = DateTime.Now,
                UserId = CurrentUserPrincipal.Identity.UserId,
                Ip = CurrentUserPrincipal.Identity.Ip ?? "",
                AppId = "A9361C3A-9F9B-4564-A12E-C76AAC171EDC".ToGuid(),
                ProxyUserId = CurrentUserPrincipal.Identity.ProxyUserId,
                OperationGuid = CurrentUserPrincipal.Identity.OperationGuid
            };

            if (crud != "C")
            {
                var details = GenerateChangeLogDetails(entity);
                if (!details.Any())
                    return null;
                tableLog.TableLogDetails = details;
            }
            return tableLog;
        }
        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
        private string GetTableName(DbEntityEntry entry)
        {
            var entityType = entry.Entity.GetType();
            var customAttributes = entityType.GetCustomAttributes<TableAttribute>();
            if (customAttributes.Any())
            {
                var tableName = customAttributes.First().Name;
                return tableName;
            }
            else
            {
                var entity = ObjectContext.GetObjectType(entry.Entity.GetType());
                return entity.Name;
            }
        }

        private static List<TableLogDetail> GenerateChangeLogDetails(DbEntityEntry entity)
        {
            var details = new List<TableLogDetail>();
            var values = entity.State == EntityState.Deleted ? entity.OriginalValues : entity.CurrentValues;

            foreach (var propName in values.PropertyNames)
            {
                var detail = GenerateChangeLogDetail(entity, propName);
                if (detail != null)
                    details.Add(detail);
            }
            return details;
        }
        private static TableLogDetail GenerateChangeLogDetail(DbEntityEntry entity, string propName)
        {
            object oldData = null;
            if (entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
                oldData = entity.OriginalValues.GetValue<object>(propName);

            var newData = entity.State == EntityState.Deleted ? null : entity.Property(propName).CurrentValue;
            
            if (AreEqual(entity, propName, oldData, newData))
                return null;

            var columnAttr = entity.Entity.GetType().GetProperty(propName).GetCustomAttribute<ColumnAttribute>();
            if (columnAttr != null)
                propName = columnAttr.Name;

                return new TableLogDetail
            {
                ColumnName = propName,
                OldValue = oldData?.ToString(),
                NewValue = newData?.ToString(),
            };
        }
        private static bool AreEqual(DbEntityEntry entity, string propName, object oldValue, object newValue)
        {
            var propertyType = entity.Entity.GetType().GetProperty(propName).PropertyType;
            var comparator = ComparatorFactory.GetComparator(propertyType);
            return comparator.AreEqual(oldValue, newValue);
        }
        public override Task<int> SaveChangesAsync()
        {
            throw new NotSupportedException("Async is not supported in DomainDBContext");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException("Async is not supported in DomainDBContext");
        }
    }
}
