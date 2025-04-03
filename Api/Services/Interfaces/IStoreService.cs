using Domain;

namespace Api.Services.Interfaces
{
    public interface IStoreService
    {
        Task<List<Store>> GetStores(int page, int pageSize);
        Task<Store?> GetStoreById(int storeId);
        Task<Store> CreateStore(Store store);
        Task<bool> UpdateStore(int storeId, Store updatedStore);
        Task<bool> DeleteStore(int storeId);
    }

}
