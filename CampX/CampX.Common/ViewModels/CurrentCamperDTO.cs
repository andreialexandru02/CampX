using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CampX.Common.ViewModels
{

    public class CurrentCamperDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthenticated { get; set; }


        public List<string> Roles { get; set; }

        public CurrentCamperDTO()
        {
            
            Roles = new List<string>(); 
        }
        
    }
}
