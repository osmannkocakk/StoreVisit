using Api.Services.Interfaces;
using DbComtext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implementation
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _context;

        public StoreService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Store>> GetStores(int page, int pageSize)
        {
            return await _context.Stores.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Store?> GetStoreById(int storeId)
        {
            return await _context.Stores.FindAsync(storeId);
        }

        public async Task<Store> CreateStore(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<bool> UpdateStore(int storeId, Store updatedStore)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null) return false;

            store.Name = updatedStore.Name;
            store.Location = updatedStore.Location;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStore(int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null) return false;

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
