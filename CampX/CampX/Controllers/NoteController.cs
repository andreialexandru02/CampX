using CampX.BusinessLogic.Implementations.Notes;
using CampX.BusinessLogic.Implementations.Notes.Models;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly NoteService Service;

        public NoteController(ControllerDependencies dependencies, NoteService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        public IActionResult AddNote(AddNoteModel model)
        {
            Service.AddNote(model);

            return RedirectToAction("TripDetails", "trip", new { id = model.TripId });
        }
        [HttpGet]

        public IActionResult ShowNotes(int id)
        {
            return Json(Service.ShowNotes(id));
        }

        [HttpPost]

        public IActionResult DeleteNote(ShowNoteModel model)
        {
            if (!Service.CheckNoteOwner(model.Id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.DeleteNote(model);
            return RedirectToAction("TripDetails", "trip", new { id = model.TripId });
        }
        public IActionResult EditNote(ShowNoteModel model)
        {
            if (!Service.CheckNoteOwner(model.Id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.EditNote(model);
            return RedirectToAction("TripDetails", "trip", new { id = model.TripId });
        }

    }
}
