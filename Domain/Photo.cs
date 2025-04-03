using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Photo
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int ProductId { get; set; }
        public string Base64Image { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public Visit Visit { get; set; }
        public Product Product { get; set; }
    }

}
