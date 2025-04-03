using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Visit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "In Progress"; // "In Progress" or "Completed"

        public User User { get; set; }
        public Store Store { get; set; }
    }

}
