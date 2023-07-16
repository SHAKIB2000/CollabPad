using Autofac;
using CollabPad.Web.Areas.Admin.Models;

namespace CollabPad.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProposalListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProposalCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProposalUpdateModel>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
