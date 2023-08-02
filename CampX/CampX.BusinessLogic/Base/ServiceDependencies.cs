using AutoMapper;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampX.DataAccess;
using CampX.Common;
using CampX.Common.ViewModels;

namespace CampX.BusinessLogic.Base
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
       
        public UnitOfWork UnitOfWork { get; set; }

        public CurrentCamperDTO CurrentCamper { get; set; }

        public ServiceDependencies(IMapper mapper, UnitOfWork unitOfWork, CurrentCamperDTO currentCamper)

        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            CurrentCamper = currentCamper;
        }
    }
}
