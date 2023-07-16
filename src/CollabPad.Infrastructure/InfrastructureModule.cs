using Autofac;
using CollabPad.Application.Features.Training.Services;
using CollabPad.Infrastructure.Features.Training.Services;
using System;
using System.Linq;

namespace CollabPad.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public InfrastructureModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProposalService>().As<IProposalService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
