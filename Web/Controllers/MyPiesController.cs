using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models;

namespace Codell.Pies.Web.Controllers
{
    public class MyPiesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public MyPiesController(IRepository repository, IMappingEngine mapper)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
                        
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(IPiesIdentity identity)
        {
            var found = _repository.Find<Pie>(pie => pie.UserEmail == identity.User.Email);
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return View(pies);  
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var pie = _repository.FindById<Guid, Pie>(id);
            return View(_mapper.Map<Pie, PieModel>(pie));
        }
    }
}