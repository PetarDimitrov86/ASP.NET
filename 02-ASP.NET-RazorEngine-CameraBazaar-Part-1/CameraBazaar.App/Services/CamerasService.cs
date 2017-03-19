namespace CameraBazaar.App.Services
{
    using Data;
    using Models.BindingModels;
    using CameraBazaar.Models;
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;
    using System;
    using System.Web;

    public class CamerasService
    {
        private CameraBazaarContext context;

        public CamerasService(CameraBazaarContext ctx)
        {
            this.context = ctx;
        }

        public Camera CreateCameraEntity(AddCameraBindingModel acbm, CameraBazaarContext db)
        {
            var camera = new Camera
            {
                 Description = acbm.Description,
                 ImageUrl = acbm.ImageUrl,
                 IsFullFrame = acbm.IsFullFrame,
                 Make = acbm.Make,
                 MaxISO = acbm.MaxISO,
                 MinISO = acbm.MinISO,
                 MinShutterSpeed = acbm.MinShutterSpeed,
                 MaxShutterSpeed = acbm.MaxShutterSpeed,
                 Model = acbm.Model,
                 Quantity = acbm.Quantity,
                 Price = acbm.Price,
                 VideoResolution = acbm.VideoResolution,
                 LightMeterings = GetCameraLightMeterings(acbm.LightMeterings, db)
            };

            return camera;
        }

        private ICollection<LightMetering> GetCameraLightMeterings(ICollection<int> lightMeterings, CameraBazaarContext db)
        {
            var lightMeteringCollection = new HashSet<LightMetering>();

            foreach (var id in lightMeterings)
            {
                lightMeteringCollection.Add(db.LightMeterings.Find(id));
            }

            return lightMeteringCollection;
        }

        public IEnumerable<GeneralCameraViewModel> GetAllCams(ICollection<Camera> cameras)
        {
            var camVMs = new HashSet<GeneralCameraViewModel>();

            foreach (var camera in cameras)
            {
                camVMs.Add(new GeneralCameraViewModel
                {
                    Id = camera.Id,
                    ImageUrl = camera.ImageUrl,
                    Make = camera.Make.ToString("G"),
                    Model = camera.Model,
                    Price = camera.Price,
                    InStock = camera.Quantity != 0
                });
            }

            return camVMs;
        }

        public CameraDetailsViewModel CameraDetails(int id)
        {
            var camera = context.Cameras.Find(id);
            return  new CameraDetailsViewModel
            {
                Description = camera.Description,
                ImageUrl = camera.ImageUrl,
                InStock = camera.Quantity != 0,
                IsFullFrame =  camera.IsFullFrame,
                Model = camera.Model,
                Make = camera.Make.ToString("G"),
                MaxISO = camera.MaxISO,
                MinISO = camera.MinISO,
                MaxShutterSpeed = camera.MaxShutterSpeed,
                MinShutterSpeed = camera.MinShutterSpeed,
                Price = camera.Price,
                SellerId = camera.Seller.Id,
                SellerName = camera.Seller.Username,
                VideoResolution = camera.VideoResolution,
                LightMetering = GetAllLightMetering(camera.LightMeterings)               
            };
        }

        private ICollection<string> GetAllLightMetering(ICollection<LightMetering> lightMeterings)
        {
            var lightMeteringStrings = new HashSet<string>();

            foreach (var lightMetering in lightMeterings)
            {
                lightMeteringStrings.Add(lightMetering.Name);
            }

            return lightMeteringStrings;
        }

        public UserDetailsViewModel GetUserDetails(int id, HttpSessionStateBase session)
        {
            User user = context.Users.Find(id);

            return new UserDetailsViewModel
            {
                Email = user.Email,
                Phone = user.Phone,
                Username = user.Username,
                CamerasInStock = user.Cameras.Count(c => c.Quantity > 0),
                CamerasOutOfStock = user.Cameras.Count(c => c.Quantity == 0),
                UserCameras = this.GetAllCams(user.Cameras),
                CurrentUserId = context.Sessions.FirstOrDefault(s => s.SessionId == session.SessionID).User.Id,
                SellerId = user.Id
            };
        }

        public DeleteCameraViewModel GetCamDeleteInfo(int id)
        {
            Camera camEntity = context.Cameras.Find(id);
            
            return new DeleteCameraViewModel
            {
                CameraId = camEntity.Id,
                Make = camEntity.Make,
                Model = camEntity.Model,
                UserId = camEntity.Seller.Id              
            };
        }

        public EditCameraViewModel GetCamEditInfo(int id)
        {
            Camera camEntity = context.Cameras.Find(id);

            return new EditCameraViewModel()
            {
                Id = camEntity.Id,
                Price = camEntity.Price,
                Quantity = camEntity.Quantity,
                Description = camEntity.Description,
                Name = camEntity.Make.ToString("G") + " - " + camEntity.Model,
                UserId = camEntity.Seller.Id
            };
        }
    }
}