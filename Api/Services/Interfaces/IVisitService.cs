using Domain;

namespace Api.Services.Interfaces
{
    public interface IVisitService
    {
        Task<Visit> CreateVisit(int userId, int storeId);
        Task<List<Visit>> GetUserVisits(int userId, int page, int pageSize);
        Task<Visit?> GetVisitById(int visitId, int userId);
        Task<bool> CompleteVisit(int visitId, int userId);
        Task<bool> AddPhotoToVisit(int visitId, int productId, string base64Image);
    }

}
