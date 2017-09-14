using System;
using Nancy;
using Nancy.TinyIoc;

namespace NancyTodo
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(Nancy.Configuration.INancyEnvironment environment)
        {
            base.Configure(environment);

            // Set Error tracing
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}