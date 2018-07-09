namespace Driblop.Auth.Key {
    public interface IKeyProvider {
        bool verify(string key);
    }
}