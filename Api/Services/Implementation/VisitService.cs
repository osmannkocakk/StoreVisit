using Api.Services.Interfaces;
using DbComtext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implementation
{
    public class VisitService : IVisitService
    {
        private readonly AppDbContext _context;

        public VisitService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Visit> CreateVisit(int userId, int storeId)
        {
            var visit = new Visit { UserId = userId, StoreId = storeId, Status = "In Progress" };
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
            return visit;
        }

        public async Task<List<Visit>> GetUserVisits(int userId, int page, int pageSize)
        {
            return await _context.Visits
                .Where(v => v.UserId == userId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Visit?> GetVisitById(int visitId, int userId)
        {
            return await _context.Visits
                .FirstOrDefaultAsync(v => v.Id == visitId && v.UserId == userId);
        }

        public async Task<bool> CompleteVisit(int visitId, int userId)
        {
            var visit = await _context.Visits.FirstOrDefaultAsync(v => v.Id == visitId && v.UserId == userId);
            if (visit == null) return false;

            visit.Status = "Completed";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPhotoToVisit(int visitId, int productId, string base64Image)
        {
            var visit = await _context.Visits.FindAsync(visitId);
            if (visit == null) return false;

            var photo = new Photo { VisitId = visitId, ProductId = productId, Base64Image = base64Image };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
