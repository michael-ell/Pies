namespace Codell.Pies.Data.Storage.Mongo
{
    public interface ICollectionNameMap
    {
        string GetNameFor<T>();
        ICollectionNameMap Register<T>(string collectionName);
    }
}