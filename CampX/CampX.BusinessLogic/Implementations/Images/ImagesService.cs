using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Images.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Trips.Validations;
using CampX.Common.Exceptions;
using CampX.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CampX.BusinessLogic.Implementations.Images
{
    public class ImagesService : BaseService
    {
        private readonly TripValidator TripValidator;

        public ImagesService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.TripValidator = new TripValidator(UnitOfWork);
        }

        public  List<int> AddImages(List<IFormFile> images)
        {
            var imgList = new List<int>();
            foreach (var imagetoConvert in images)
            {
                using (var stream = new MemoryStream())
                {
                    imagetoConvert.CopyTo(stream);
                    var bytes = stream.ToArray();




                    var imageEntity = Mapper.Map<ImageModel, Entities.Image>(new ImageModel
                    {
                        ImageData = bytes
                    });



                    var insertedImage = UnitOfWork.Images.Insert(imageEntity);
                    UnitOfWork.SaveChanges();
                    imgList.Add(insertedImage.Id);
                }
            }

            return imgList;
        }
        public byte[] GetImgContent(int id)
        {
            var img = UnitOfWork.Images.Get().FirstOrDefault(i => i.Id == id);
            if (img == null)
            {
                throw new NotFoundErrorException("image not found");
            }
            return img.ImageData;
        }
        public void DeleteImages(List<int> imgListToDelete)
        {
            var imagesToDelete = UnitOfWork.Images.Get()
                .Where(i => imgListToDelete.Contains(i.Id))
                .ToList();  

            foreach(var img in imagesToDelete)
            {
                UnitOfWork.Images.Delete(img);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
