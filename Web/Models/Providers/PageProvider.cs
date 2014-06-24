using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Models.Providers
{
    public class PageProvider : IPageProvider
    {
        private int _piesPerPage;
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public PageProvider(IRepository repository, IMappingEngine mapper, ISettings settings)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
            Verify.NotNull(settings, "settings");
            
            _repository = repository;
            _mapper = mapper;
            _piesPerPage = settings.Get<int>(Keys.PiesPerPage);
        }

        public IPageProvider PiesPerPage(int piesPerPage)
        {
            _piesPerPage = piesPerPage;
            return this;
        }

        public IPageResult Get(int? page)
        {
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }
            var previous = (page.Value == 1 ? 0 : page.Value - 1) * _piesPerPage;
            var pies = _repository.Get<Core.ReadModels.Pie>().Where(PieIsViewable())
                                             .Skip(previous)
                                             .Take(_piesPerPage)
                                             .OrderByDescending(pie => pie.CreatedOn)
                                             .ToList();
            var totalPages = (_repository.Get<Core.ReadModels.Pie>().Count(PieIsViewable()) + (_piesPerPage - 1)) / _piesPerPage;
            return new Result(page.Value, _mapper.Map<IEnumerable<Core.ReadModels.Pie>, IEnumerable<PieModel>>(pies), totalPages);
        }

        private Expression<Func<Core.ReadModels.Pie, bool>> PieIsViewable()
        {
            return pie => pie.IsPrivate == false && pie.IsEmpty == false;
        }

        private class Result : IPageResult
        {
            public int CurrentPage { get; private set; }

            public IEnumerable<PieModel> Pies { get; private set; }

            public int TotalPages { get; private set; }

            public Result(int currentPage, IEnumerable<PieModel> pies, int totalPages)
            {
                CurrentPage = currentPage;
                Pies = pies;
                TotalPages = totalPages;
            }
        }
    }
}