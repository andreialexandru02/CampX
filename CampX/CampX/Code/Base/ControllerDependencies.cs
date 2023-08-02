using CampX.Common.ViewModels;
namespace CampX.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentCamperDTO CurrentCamper { get; set; }
    
        public ControllerDependencies(CurrentCamperDTO currentCamper) {
    
            this.CurrentCamper = currentCamper;
        }
    } 
}
