using Domain.DataAccess.Abstract.Brand;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Entity.Enum;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Brand
{
    public class BrandDAL : UnitOfWork<DomainDBContext>, IBrandDAL
    {
        public PagedList<BrandApplicationComplexType> GetPagedListSearchedBrandApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                    	b.BrandApplicationDetailId
                                       ,b.BrandName
                                       ,b.BrandCategoryDescription
                                       ,b.BrandApplicationTypeId
                                       ,b.SpecialClass
                                       ,b.SendCoverLetter
                                       ,b.ContactId
                                       ,b.CustomerApplicationId
                                       ,bf.BeneficiaryId
                                       ,bf.FirmName
                                       ,bf.TCNumber
                                       ,bf.TaxOffice
                                       ,bf.TaxNumber
                                       ,bf.Address
                                       ,bf.CityId
                                       ,bf.TownId
                                       ,bf.PhoneNumber
                                       ,bf.FirmStatuTypeId
                                       ,pat.Description AS ApplicationTypeDescription
                                       ,c.CityName
                                       ,t.TownName
                                       ,ca.ApplicationTypeId
                                    FROM BrandApplicationDetail b {0}
                                    INNER JOIN CustomerApplication ca {0} ON b.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN Beneficiary bf {0} ON b.BrandApplicationDetailId = bf.BrandApplicationDetailId
                                    INNER JOIN PrmApplicationType pat {0} ON ca.ApplicationTypeId = pat.ApplicationTypeId
                                    LEFT JOIN Contact ct {0} ON ct.ContactId = b.ContactId
                                    LEFT JOIN City c {0} ON c.CityId = ct.CityId
                                    LEFT JOIN Town t {0} ON t.TownId = ct.TownId
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            var response = GetPagedListSqlQueryWithSearchFilters<BrandApplicationComplexType>(sqlQuery.ToString(), searchFilters, dtParameterModel);
            return response;
        }

        public PagedList<BrandForWatchingApplicationComplexType> GetPagedListSearchedBrandForWatchingApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                        bw.BrandWatchingApplicationDetailId
                                       ,bf.BrandForWatchingId
                                       ,bf.FirmName AS BrandFirmName
                                       ,bf.BrandName
                                       ,bf.ClassesToWatch
                                       ,bf.RegistryNumber
                                       ,bw.FirmName AS ApplicationFirmName
                                       ,bw.IdentityNumber
                                       ,bw.Address
                                       ,bw.Phone                                  	
                                       ,c.CityName
                                       ,t.TownName
                                    FROM BrandForWatching bf {0}
                                    INNER JOIN BrandWatchingApplicationDetail bw {0} ON bf.BrandWatchingApplicationDetailId = bw.BrandWatchingApplicationDetailId
                                    INNER JOIN CustomerApplication ca {0} ON bw.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN City c {0} ON c.CityId = bw.CityId
                                    LEFT JOIN Town t {0} ON t.TownId = bw.TownId
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            var response = GetPagedListSqlQueryWithSearchFilters<BrandForWatchingApplicationComplexType>(
                sqlQuery.ToString(), searchFilters, dtParameterModel);
            return response;
        }

        public List<BrandApplicationComplexType> GetListSearchedBrandApplicationCT(
            string userId, bool isBrand)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();
            sqlQuery.AppendFormat(@"SELECT
                                    	b.BrandApplicationDetailId
                                       ,b.BrandName
                                       ,b.BrandCategoryDescription
                                       ,b.BrandApplicationTypeId
                                       ,b.SpecialClass
                                       ,b.SendCoverLetter
                                       ,b.ContactId
                                       ,b.CustomerApplicationId
                                       ,bf.BeneficiaryId
                                       ,bf.FirmName
                                       ,bf.TCNumber
                                       ,bf.TaxOffice
                                       ,bf.TaxNumber
                                       ,bf.Address
                                       ,bf.CityId
                                       ,bf.TownId
                                       ,bf.PhoneNumber
                                       ,bf.FirmStatuTypeId
                                       ,pat.Description AS ApplicationTypeDescription
                                       ,c.CityName
                                       ,t.TownName
                                       ,ca.ApplicationTypeId
                                    FROM BrandApplicationDetail b {0}
                                    INNER JOIN CustomerApplication ca {0} ON b.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN Beneficiary bf {0} ON b.BrandApplicationDetailId = bf.BrandApplicationDetailId
                                    INNER JOIN PrmApplicationType pat {0} ON ca.ApplicationTypeId = pat.ApplicationTypeId
                                    LEFT JOIN Contact ct {0} ON ct.ContactId = b.ContactId
                                    LEFT JOIN City c {0} ON c.CityId = ct.CityId
                                    LEFT JOIN Town t {0} ON t.TownId = ct.TownId
                                    WHERE 1=1 AND ca.CustomerId={1} ", 
                                        " WITH(NOLOCK) ", userId);

            if (isBrand)
            {
                var param = new SqlParameter("prmApplicationTypeId", (int)EnumApplicationType.Marka_Basvurusu);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "ca.ApplicationTypeId", param.ParameterName);
                prmLst.Add(param);
            }
            else
            {
                var param = new SqlParameter("prmApplicationTypeId", (int)EnumApplicationType.Marka_Basvurusu);
                sqlQuery.AppendFormat(" AND {0} != @{1} ", "ca.ApplicationTypeId", param.ParameterName);
                prmLst.Add(param);
            }

            var response = ExecuteSqlCommand<BrandApplicationComplexType>(sqlQuery.ToString(), prmLst.ToArray()).ToList();
            return response;
        }

        public List<BrandForWatchingApplicationComplexType> GetListSearchedBrandForWatchingApplicationCT(
            string userId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                        bw.BrandWatchingApplicationDetailId
                                       ,bf.BrandForWatchingId
                                       ,bf.FirmName AS BrandFirmName
                                       ,bf.BrandName
                                       ,bf.ClassesToWatch
                                       ,bf.RegistryNumber
                                       ,bw.FirmName AS ApplicationFirmName
                                       ,bw.IdentityNumber
                                       ,bw.Address
                                       ,bw.Phone                                  	
                                       ,c.CityName
                                       ,t.TownName
                                    FROM BrandForWatching bf {0}
                                    INNER JOIN BrandWatchingApplicationDetail bw {0} ON bf.BrandWatchingApplicationDetailId = bw.BrandWatchingApplicationDetailId
                                    INNER JOIN CustomerApplication ca {0} ON bw.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN City c {0} ON c.CityId = bw.CityId
                                    LEFT JOIN Town t {0} ON t.TownId = bw.TownId
                                    WHERE 1=1 AND ca.CustomerId={1}", " WITH(NOLOCK) ", userId);

            var response = ExecuteSqlCommand<BrandForWatchingApplicationComplexType>(
                sqlQuery.ToString());
            return response.ToList();
        }
    }
}
