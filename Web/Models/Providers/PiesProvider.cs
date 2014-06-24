using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.Models.Providers
{
    public class PiesProvider : IPiesProvider
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;
        private readonly IPageProvider _pageProvider;

        public PiesProvider(IRepository repository, IMappingEngine mapper, IPageProvider pageProvider)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
            Verify.NotNull(pageProvider, "pageProvider");            
                        
            _repository = repository;
            _mapper = mapper;
            _pageProvider = pageProvider;
        }

        public IEnumerable<PieModel> GetRecent()
        {
            var pies = _repository.Find<Core.ReadModels.Pie>(pie => pie.IsPrivate == false && pie.IsEmpty == false)
                                  .OrderByDescending(pie => pie.CreatedOn)
                                  .Take(12);
            return ToModels(pies);
        }

        public PieModel Get(string id)
        {
            Guid guid;
            return Guid.TryParse(id, out guid) ? Get(guid) : new PieModel();
        }

        public PieModel Get(Guid id)
        {
            var pie = _repository.FindById<Guid, Core.ReadModels.Pie>(id);            
            return pie == null ? new PieModel() : _mapper.Map<Core.ReadModels.Pie, PieModel>(pie);
        }

        public IEnumerable<PieModel> Find(string tag = "")
        {
            return tag.IsEmpty() ? GetRecent() : ToModels(_repository.Find<Core.ReadModels.Pie>(pie => pie.IsPrivate == false && pie.Tags.Contains<string>(new SearchableTag(tag))));            
        }

        public IEnumerable<PieModel> GetPiesFor(IPiesIdentity identity)
        {
            var found = _repository.Find<Core.ReadModels.Pie>(pie => pie.OwnerId == identity.User.Id && pie.IsEmpty == false).OrderBy(pie => pie.Caption);
            return ToModels(found);  
        }

        public IPageResult GetPage(int? page)
        {
            return _pageProvider.Get(page);
        }
     
        private IEnumerable<PieModel> ToModels(IEnumerable<Core.ReadModels.Pie> pies)
        {
            return _mapper.Map<IEnumerable<Core.ReadModels.Pie>, IEnumerable<PieModel>>(pies);
        }
    }
}