using Domain.Business.Abstract.Common;
using Domain.DataAccess.Abstract.Common;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Entity.Enum;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Common
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserDAL _systemUserDAL;
        private readonly IRepository<SystemUser> repSystemUser;

        public SystemUserService(ISystemUserDAL systemUserDAL)
        {
            _systemUserDAL = systemUserDAL;
            repSystemUser = _systemUserDAL.GetRepository<SystemUser>();
        }

        public SystemUser GetSystemUser(RequestSystemUser request)
        {
            //Entity Framework kullanarak veri çekme işlemi
            return repSystemUser.Queryable().Where(w => w.Email == request.EMail.Trim() && w.Password == request.Password.Trim() && 
                w.StateId == (byte)EnumSystemUserState.Active && w.UserTypeId == (byte)request.UserType).FirstOrDefault();

            //throw new System.Exception("elastic test");

            //DataAccess Layer kullanarak veri çekme
            //return _systemUserDAL.GetSystemUser(request);
        }

        public int CreateSystemUser(RequestSystemUser request)
        {
            if (repSystemUser.Queryable().Any(w => w.Email == request.SystemUserInfo.Email && w.StateId == (byte)EnumSystemUserState.Active && w.UserTypeId == request.SystemUserInfo.UserTypeId))
                throw new BusinessException("Bu mail adına kayıtlı kullanıcı bulunmaktadır. Şifrenizi unutmanız durumunda Şifremi Unuttum bölümüne tıklayınız.");

            var systemUser = _systemUserDAL.GetRepository<SystemUser>().Insert(request.SystemUserInfo);
            return systemUser.SystemUserId;
        }

        public PagedList<SystemUserComplexType> GetUserPagedList(DtParameterModel dtParameterModel, List<SearchFilter> searchFilters)
        {
            return _systemUserDAL.GetUserPagedList(dtParameterModel, searchFilters);
        }

        public void ChangeUserState(RequestSystemUser request)
        {
            
        }
    }
}
