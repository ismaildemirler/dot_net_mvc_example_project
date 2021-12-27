using Domain.DataAccess.Abstract.Patent;
using Domain.DataAccess.Abstract.Slider;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Slider
{
    public class SliderDAL : UnitOfWork<DomainDBContext>, ISliderDAL
    {
    }
}
