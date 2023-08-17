using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Notes.Models;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.DataAccess;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Notes
{
    public class NoteService : BaseService
    {


        public NoteService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<NoteModel> ShowNotes(int id) {

            var notes = UnitOfWork.Notes.Get()
                .Where(n => n.TripId == id)
                .Select(r => new NoteModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    TripId = r.TripId
                })
            .ToList();
            return notes;

        }
    }
}
