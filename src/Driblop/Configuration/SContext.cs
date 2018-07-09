using Driblop.Configuration;

namespace Driblop {
    public class SContext<TConfig> where TConfig : SConfig {
        public TConfig config;

        public SContext(TConfig config) {
            this.config = config;
            initialize();
        }

        protected virtual void initialize() {
            registerSelf<SContext<TConfig>>();
        }

        protected void registerSelf<TContext>() {
            SJar.jar.register<TContext>(this);
        }
    }
}