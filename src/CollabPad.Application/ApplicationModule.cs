using Autofac;
using System;
using System.Linq;

namespace CollabPad.Application
{
    public class ApplicationModule : Module

    {
        public ApplicationModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
