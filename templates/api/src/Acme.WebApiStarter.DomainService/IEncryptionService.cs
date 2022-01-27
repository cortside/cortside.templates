namespace Acme.WebApiStarter.DomainService {
    public interface IEncryptionService {
        string EncryptString<T>(T objectToEncrypt);

        T DecryptString<T>(string cipherText);
    }
}
