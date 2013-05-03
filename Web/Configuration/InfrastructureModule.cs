﻿using Autofac;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Mapping;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.Configuration
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => AutoMapperEngine.Configure(new AutoMapperConfiguration())).SingleInstance();
            builder.RegisterType<AppSettings>().As<ISettings>();
            builder.RegisterType<OpenIdGateway>().As<IOpenIdGateway>();
        }
    }
}