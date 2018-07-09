using Driblop.Configuration;
using Nancy;

namespace Driblop.Modules {
    public abstract class DLModule<TContext> : NancyModule {
        public TContext serverContext;

        public DLModule(string modulePath) : base(modulePath) {
            serverContext = SJar.jar.resolve<TContext>();
        }
    }
}