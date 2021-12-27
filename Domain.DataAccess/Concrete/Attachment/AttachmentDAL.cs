using Domain.DataAccess.Abstract.Attachment;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Attachment
{
    public class AttachmentDAL : UnitOfWork<DomainDBContext>, IAttachmentDAL
    {
    }
}
