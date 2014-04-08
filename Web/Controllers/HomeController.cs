﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Attributes;
using Codell.Pies.Web.Models.Home;
using Codell.Pies.Web.Models.Shared;
using Pie = Codell.Pies.Core.ReadModels.Pie;

namespace Codell.Pies.Web.Controllers
{
    [MobileRedirect]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;
        private readonly ISettings _settings;

        public HomeController(IRepository repository, IMappingEngine mapper, ISettings settings)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
                        
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pageSize = _settings.Get<int>(Keys.PiesPerPage);
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }
            var previous = (page.Value == 1 ? 0 : page.Value - 1) * pageSize;
            var pies = _repository.Get<Pie>().Where(pie => pie.IsPrivate == false && pie.IsEmpty == false)
                                             .Skip(previous)
                                             .Take(pageSize)
                                             .OrderByDescending(pie => pie.CreatedOn)
                                             .ToList();
            //var totalPages = _repository.Count<Pie>() / pageSize;
            var totalPages = (_repository.Get<Pie>().Count(pie => pie.IsEmpty == false) + (pageSize - 1)) / pageSize;
            return View(new IndexModel
                            {
                                Pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(pies),
                                Paging = new PagingModel{CurrentPage = page.Value, TotalPages = totalPages}
                            });
        }

        [HttpGet]
        public ActionResult Share(Guid id)
        {
            var pie = _repository.FindById<Guid, Pie>(id) ?? new Pie();
            return View("Index", new IndexModel
                                        {
                                            Pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(new[] { pie }),
                                        });
        }
    }
}