using Domain.Business.Abstract.ReferenceFirm;
using Domain.DataAccess.Abstract.ReferenceFirm;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.ReferenceFirm
{
    public class ReferenceFirmService : IReferenceFirmService
    {
        private readonly IReferenceFirmDAL _referenceFirmDAL;

        public ReferenceFirmService(IReferenceFirmDAL referenceFirmDAL)
        {
            _referenceFirmDAL = referenceFirmDAL;
        }

        public List<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirms()
        {
            return _referenceFirmDAL.GetRepository<Entity.Concrete.ReferenceFirm>().Queryable().Where(w => w.State == (byte)EnumState.Active).ToList();
        }

        public PagedList<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirmPagedList(int pageIndex)
        {
            return _referenceFirmDAL.GetActiveReferenceFirmPagedList(pageIndex);
        }

        public PagedList<Entity.Concrete.ReferenceFirm> GetReferenceFirmPagedList(DtParameterModel dtParameterModel, List<SearchFilter> filters)
        {
            var rep = _referenceFirmDAL.GetRepository<Entity.Concrete.ReferenceFirm>();
            return rep.GetPagedList(rep.Queryable()
                        .OrderByDescending(o => o.InsertDate),
                        dtParameterModel.AramaKriteri.Baslangic.Value,
                        dtParameterModel.AramaKriteri.Uzunluk.Value, true
                    );
        }

        public Entity.Concrete.ReferenceFirm GetReferenceFirm(Guid referenceId)
        {
            var reference = _referenceFirmDAL.GetRepository<Entity.Concrete.ReferenceFirm>()
                .Queryable().Where(w => w.ReferenceFirmId == referenceId).FirstOrDefault();
            if (reference == null)
                throw new BusinessException(Messages.KayitBulunamadi);

            return reference;
        }

        public Guid SaveReferenceFirm(Entity.Concrete.ReferenceFirm reference, bool imageDeleted)
        {
            var rep = _referenceFirmDAL.GetRepository<Entity.Concrete.ReferenceFirm>();

            if (reference.ReferenceFirmId != Guid.Empty)
            {
                var referenceEntity = rep.Find(reference.ReferenceFirmId);
                if (referenceEntity == null)
                    throw new BusinessException(Messages.KayitBulunamadi);
                else
                {
                    referenceEntity.Name = reference.Name;
                    referenceEntity.InsertDate = reference.InsertDate;
                    referenceEntity.State = reference.State;
                    referenceEntity.WorkName = reference.WorkName;
                    referenceEntity.Detail = reference.Detail;

                    if (referenceEntity.LogoImage != null || (referenceEntity.LogoImage == null && imageDeleted))
                    {
                        referenceEntity.LogoImage = reference.LogoImage;
                    }

                    rep.Update(referenceEntity);
                    return referenceEntity.ReferenceFirmId;
                }
            }
            else
            {
                var referenceEntity = new Entity.Concrete.ReferenceFirm
                {
                    ReferenceFirmId = Guid.NewGuid(),
                    Name = reference.Name,
                    WorkName = reference.WorkName,
                    Detail = reference.Detail,
                    LogoImage = reference.LogoImage,
                    State = (byte)EnumState.Active,
                    InsertDate = DateTime.Now
                };

                rep.Insert(referenceEntity);
                return referenceEntity.ReferenceFirmId;
            }
        }
    }
}
