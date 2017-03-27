namespace CameraBazaar.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models.EntityModels;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<CameraBazaar.Data.CameraBazaarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CameraBazaarContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Seller"))
            {
                roleManager.Create(new IdentityRole("Seller"));
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            context.LightMeterings.AddOrUpdate(l => l.Name,
                new LightMetering { Name = "Spot" },
                new LightMetering { Name = "Center-Weighted" },
                new LightMetering { Name = "Evaluative" });

            context.SaveChanges();

            context.Cameras.AddOrUpdate(
                new Camera
                {
                    Description = "The D600 is an excellent DSLR, with top-notch photo quality, a well-built body and impressive movie recording capabilities. Unfortunately for Nikon, the D600 is probably best known for collecting oil on the sensor - an issue for which Nikon only initially issued a service advisory, without admitting the actual cause of the problem. Nikon only directly acknowledged the issue over four months after the launch of the D610, saying: 'Nikon has thoroughly evaluated these reports and has determined that these spots are caused by dust particles which may become visible when the camera is used in certain circumstances'. It's now offering D600 users free inspection, cleaning and replacement of the shutter assembly and related parts even if the warranty has expired.",
                    ImageUrl = @"https://www.dpreview.com/files/p/articles/7148267499/images/front.jpeg",
                    IsFullFrame = true,
                    LightMeterings = new List<LightMetering>() { context.LightMeterings.Find(1), context.LightMeterings.Find(2) },
                    Make = Make.Nikon,
                    Model = "D610",
                    MinISO = 100,
                    MaxISO = 6400,
                    MaxShutterSpeed = 4000,
                    MinShutterSpeed = 30,
                    Price = 1496.95m,
                    Quantity = 0,
                    VideoResolution = "1920x1080",
                    Seller = context.Sellers.Find(1)
                },
                new Camera
                {
                    Description = "The Canon EOS 80D is an enthusiast-level DSLR, and the successor to the 70D. It sports a new 24MP APS-C CMOS sensor which, like the 70D, offers Canon's Dual Pixel on-sensor phase-detection autofocus system. The 80D also gains a new 45-point hybrid AF system with all of the points being cross-type. This is a step up from the 19-point AF system in the 70D, though not quite at the same level as the 65-point coverage offered by the more professionally-oriented 7D Mark II.",
                    ImageUrl = @"https://3.img-dpreview.com/files/p/E~TS520x0~articles/4317390892/poweradapter.jpeg",
                    IsFullFrame = false,
                    LightMeterings = new List<LightMetering>() { context.LightMeterings.Find(3) },
                    Make = Make.Canon,
                    Model = "EOS80D",
                    MinISO = 100,
                    MaxISO = 16000,
                    MaxShutterSpeed = 8000,
                    MinShutterSpeed = 30,
                    Price = 1199.95m,
                    Quantity = 3,
                    VideoResolution = "1920x1080",
                    Seller = context.Sellers.Find(2)
                },
                new Camera
                {
                    Description = "Inheriting many pro-style features from the acclaimed A77 II, the A68 features Sony’s unique '4D FOCUS system' that delivers extraordinary AF performance under any shooting conditions – even in lighting as low as EV-2 where other cameras struggle. This phase detection system uses no less than 79 autofocus detection points including 15 cross points, plus a dedicated F2.8 AF sensor point for dimly-lit scenes. It all adds up to fast, wide area AF with predictive tracking that locks faithfully onto fast-moving subjects.And thanks to Sony’s unique Translucent Mirror Technology, the A68 delivers constant AF tracking at up to 8 fps continuous shooting. Full HD movies use the efficient XAVC S format for high bit rate recordings at up to 50Mbps with outstanding detail and low noise.And thanks to Sony’s unique Translucent Mirror Technology, you can enjoy non - stop continuous autofocus that effortlessly tracks moving subjects for crisp, professional looking footage, whichever format you choose to record in.",
                    ImageUrl = @"https://3.img-dpreview.com/files/p/TS600x450~sample_galleries/5396895872/3032386836.jpg",
                    IsFullFrame = true,
                    LightMeterings = new List<LightMetering> { context.LightMeterings.Find(2) },
                    Make = Make.Sony,
                    Model = "À68",
                    MinISO = 100,
                    MaxISO = 25600,
                    MaxShutterSpeed = 4000,
                    MinShutterSpeed = 30,
                    Price = 598.99m,
                    Quantity = 0,
                    VideoResolution = "1920x1080",
                    Seller = context.Sellers.Find(3)
                });
        }
    }
}
