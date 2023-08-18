using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Notes.Models
{
    public class ShowNoteModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public int TripId { get; set; }

        public int CamperId { get; set; }
    }
}
