using System.Collections.Generic;
using System.Security.Claims;
using Nancy;
using Nancy.Bootstrapper;

namespace Driblop.Auth.Key {
    public class KeyAuthenticator {
        public const string key_claim = "key";

        public static void install(IPipelines pipelines, IKeyProvider provider) {
            pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx => {
                var key = ctx.Request.Headers.Authorization;
                if (string.IsNullOrEmpty(key)) return null;
                if (provider.verify(key)) {
                    ctx.CurrentUser = new ClaimsPrincipal(new ClaimsIdentity(
                        new List<Claim>() {
                            new Claim(key_claim, key)
                        }));
                }

                return null;
            });
        }
    }

    public static class KeyAuthenticatorsxtensions {
        public static void ensureKeyAuth(this NancyModule module) {
            module.Before.AddItemToEndOfPipeline(ctx => {
                if (module.Context.holdsKey()) {
                    return null;
                }

                return HttpStatusCode.Unauthorized;
            });
        }

        public static bool holdsKey(this NancyContext context) {
            return context.CurrentUser != null &&
                   context.CurrentUser.HasClaim(x => x.Type == KeyAuthenticator.key_claim);
        }
    }
}