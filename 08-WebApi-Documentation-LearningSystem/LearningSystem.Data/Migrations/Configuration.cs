namespace LearningSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System;
    using Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<LearningSystem.Data.LearningSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearningSystemContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Student"))
                roleManager.Create(new IdentityRole("Student"));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("BlogAuthor"))
                roleManager.Create(new IdentityRole("BlogAuthor"));
            if (!roleManager.RoleExists("Trainer"))
                roleManager.Create(new IdentityRole("Trainer"));

            var admin = userManager.FindByName("admin");
            if (admin == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "admin", Name = "Admin Adminer", Email = "admin@g.c", BirthDate = new DateTime(1998, 2, 22)
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Student");
                userManager.AddToRole(newUser.Id, "Admin");
                context.Students.Add(new Student {UserId = newUser.Id});
            }

            var pesho = userManager.FindByName("pesho");
            if (pesho == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "pesho",
                    Name = "Petar Dimitrov",
                    Email = "pesho@g.c",
                    BirthDate = new DateTime(1986, 7, 8)
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
                userManager.AddToRole(newUser.Id, "Student");
                context.Students.Add(new Student { UserId = newUser.Id });
            }

            var bojo = userManager.FindByName("bojo");
            if (bojo == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "bojo",
                    Name = "Bozhidar Gevechanov",
                    Email = "bojo@g.c",
                    BirthDate = new DateTime(1996, 4, 10)
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Trainer");
                userManager.AddToRole(newUser.Id, "Student");
                context.Students.Add(new Student { UserId = newUser.Id });
            }

            var joro = userManager.FindByName("joro");
            if (joro == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "joro",
                    Name = "Georgi Stoimenov",
                    Email = "joro@g.c",
                    BirthDate = new DateTime(1996, 9, 23)
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Trainer");
                userManager.AddToRole(newUser.Id, "Student");
                userManager.AddToRole(newUser.Id, "BlogAuthor");
                context.Students.Add(new Student { UserId = newUser.Id });
            }

            var jicata = userManager.FindByName("jicata");
            if (jicata == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "jicata",
                    Name = "Svetlin Galov",
                    Email = "jicata@g.c",
                    BirthDate = new DateTime(1992, 6, 14)
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Trainer");
                userManager.AddToRole(newUser.Id, "Student");
                userManager.AddToRole(newUser.Id, "BlogAuthor");
                context.Students.Add(new Student { UserId = newUser.Id });
            }

            context.SaveChanges();

            jicata = userManager.FindByName("jicata");
            joro = userManager.FindByName("joro");
            bojo = userManager.FindByName("bojo");

            context.Articles.AddOrUpdate(a => a.Title, new Article
            {
                Author = jicata,
                Title = "Какво е 802.11ac Wi-Fi, как работи и за какво ни върши работа?",
                Content = "802.11ас – cyпep вepcиятa нa cтaндapтa 802.11n. 802.11ас, e дeceтĸи пъти пo-бъpз, ocигypявa cĸopocт в диaпaзoнa oт 433 Мbрѕ (мeгaбитa в ceĸyндa) дo няĸoлĸo гигaбитa в ceĸyндa. Зa дa ce пocтигнe тoзи вид пpoпycĸaтeлнa cпocoбнocт, 802.11ас paбoти в чecтoтнaтa лeнтa oт 5 GНz, изпoлзвa гoлям ĸaпaцитeт (80 или 160МНz и имa тexнoлoгия, нapeчeнa фopмиpaнe нa лъчa, ĸoятo изпpaщa cигнaл диpeĸтнo ĸъм ĸлиeнтcĸитe ycтpoйcтвa. 802.11ас cъщo въвeждa cтaндapтизиpaно гeнepиpaнe нa лъчи.Гeнepиpaнeтo нa лъчи пpeдaвa paдиocигнaли пo тaĸъв нaчин, чe тe ca нacoчeни ĸъм oпpeдeлeнo ycтpoйcтвo.Toвa мoжe дa yвeличи цялocтнaтa пpoизвoдитeлнocт и дa я нaпpaви пocлeдoвaтeлна, ĸaĸтo и дa нaмaли ĸoнcyмaциятa нa eнepгия.Фopмиpaнeтo нa лъчa ce пpaви c „yмни aнтeни“, ĸoитo физичecĸи ce движaт, зa дa пpocлeдят ycтpoйcтвo ĸъм ĸoeтo пpeдaвaт cигнaлa или чpeз мoдyлиpaнe нa aмплитyдaтa и пpeдaвaнeтo нa cигнaлитe, тaĸa чe тe дa нe cи пpeчaт взaимнo, ocтaвяйĸи caмo eдин тeceн, бeз cмyщeния лъч. И нaĸpaя, 802.11ас, ĸaтo и вcичĸи 802.11 cтaндapти пpeди тoвa, ca нaпълнo cъвмecтими – тaĸa чe мoжeтe дa cи ĸyпитe 802.11ас pyтep oщe днec и тoй тpябвa дa paбoти дoбpe cъc cтapитe cтaндapти, ĸaтo 802.11n и 802.11g Wі - Fі ycтpoйcтвa.",
                ImageUrl = "http://news.pcstore.bg/wp-content/uploads/2017/03/wifi_80211ac.jpg",
                PublishDate = new DateTime(2017, 3, 27)                
            }, new Article
            {
                Author = jicata,
                Title = "Най-добрите очила за виртуална реалност",
                Content = "Ѕоnу РlауЅtаtіоn VR Дизaйнът e пoчти пepфeĸтeн.Ha пъpвo мяcтo, пo - лecнo e дa ce ĸopигиpa paзмepa cъoтвeтнo пo вaшaтa глaвa, зaщoтo вмecтo дa ce paзчитa нa вeлĸpo лeнти, ĸaтo пpи мнoгo дpyги VR ycтpoйcтвa, Ѕоnу изĸycнo изпoлзвa плъзгaщ мeтoд, зa дa пpeдлoжи пo - бъpз и пo - бeзпpoблeмeн нaчин зa ĸopигиpaнe нa paзмepa.Имa бyтoн, ĸoйтo cлeд нaтиcĸaнe oт дoлнaтa cтpaнa, ocвoбoждaвa xвaтĸaтa cи.Cъщo тaĸa oт тaм ce ĸopигиpa лecнo, дoĸaтo пpилeпнe плътнo пo глaвaтa ви.Πpocтo, лecнo и бъpзo! Дpyгa зaбeлeжитeлнa xapaĸтepиcтиĸa нa РЅ VR e нeгoвият фyтypиcтичeн дизaйн, ĸoйтo e пълнa пpoтивoпoлoжнocт нa тoвa, ĸoeтo пoлyчaвaмe oт дpyги тaĸивa ycтpoйcтвa нa пaзapa.Oт Ѕоnу ca ce cпpaвили мнoгo дoбpe cъc чacттa oĸoлo нoca, ĸoятo e мнoгo yдoбнa и нe пoзвoлявa дa влизa cвeтлинa и дa ви пpeчи, дoĸaтo тoвa ce cлyчвa пpи мнoгo oт дpyгитe VR ycтpoйcтвa нa пaзapa. РlауЅtаtіоn VR изпoлзвa eдин 5.7 “ диcплeй, c 1920 x 1080 peзoлюция и пълнoцвeтeн ОLЕD RGВ диcплeй, paзпpeдeлeн мeждy двeтe ви oчи, зa дa дocтaви cтepeocĸoпичнo 3D cъдъpжaниe.Диcплeят oпpecнявa cъc cĸopocт или 90Нz или 120Нz, в зaвиcимocт oт caмoтo пpилoжeниe, c лaтeнтнocт нa пo - мaлĸo oт 18 милиceĸyнди. Cилaтa нa 3D пpocтpaнcтвeният звyĸ e впeчaтлявaщ и e пoдoбeн нa cъpayнд звyĸ.Koгaтo взeмeтe звyĸът и ĸapтинaтa в eднo, ce чyвcтвaтe ĸaтo чe ли нaиcтинa ce тpaнcпopтиpaнитe нa дpyгo мяcтo. Aĸo peшитe чe тoвa e вaшeтo VR ycтpoйcтвo, пpeпopъчитeлнaтa цeнa зa пълният ĸoмплeĸт e $499.",
                ImageUrl = "http://news.pcstore.bg/wp-content/uploads/2017/03/vr-refresh-tech-set-768x512.png",
                PublishDate = new DateTime(2017, 3, 20)
            },
             new Article
             {
                 Author = joro,
                 Title = "Witcher Series Passes 25M Units Sold, Cyberpunk 2077 Development \"Quite Advanced\"",
                 Content = "CD Projekt Red's Witcher series has passed a new sales milestone. As part of the company's newest earnings report today, it announced that the game--comprising The Witcher, The Witcher 2: Assassins of Kings, and The Witcher 3: Wild Hunt--has now passed 25 million units sold (via PC Gamer). The Polish developer did not provide a specific unit sales breakdown by title.Whatever the case, this figure is up from the 20 million number that CD Projekt Red disclosed a year ago this month. During a Q & A session as part of the briefing, CD Projekt Red management offered an update on Cyberpunk 2077, saying development progress is \"quite advanced\" at this stage.Management said the game is very ambitious and stressed that the developer will not rush the game to market. Cyberpunk 2077 is scheduled to launch sometime in the 2017 - 2021 window.Don't expect to see more of it until CD Projekt Red can prepare an impressive showcase for it.",
                 ImageUrl = "https://static5.gamespot.com/uploads/scale_tiny/1197/11970954/2847691-2568037-91v3ozkg-ml._sl1500_.jpg",
                 PublishDate = new DateTime(2016, 2, 22)
             },
             new Article
             {
                 Author = joro,
                 Title = "Bulls Top Cavs 99-93, James Moves To 7th On NBA Scoring List",
                 Content = "CHICAGO (AP) Nikola Mirotic tied season highs with 28 points and six 3-pointers, Jimmy Butler scored 25, and the Chicago Bulls beat the Cleveland Cavaliers 99-93 on a night when LeBron James moved into seventh place on the NBA’s career scoring list. James moved past Shaquille O’Neal, finishing with 26 points and giving him 28,599 for his career – three more than O’Neal. But the big night by the four-time MVP couldn’t prevent the Cavaliers from matching a season high with their third straight loss. That dropped them a half-game behind Boston for the Eastern Conference lead and left them with a 6-10 record in March. For Mirotic, it was his second straight game with 28 points and six 3s. He also had 10 rebounds, finishing a strong March. Rajon Rondo added 15 assists, Robin Lopez had 10 points and 11 rebounds, and the ninth-place Bulls moved within a game of Miami and Indiana in the Eastern Conference standings. They also finished 4-0 against Cleveland to complete their first sweep of the Cavaliers since they took all three games during the 2011-12 season. Kyrie Irving scored 20 for the Cavaliers, while Tristan Thompson added 15 points and nine rebounds. James entered needing 23 points to tie O’Neal, and matched him when he scored on a layup with 7:23 left in the game. He took sole possession of seventh place when he hit one of three free throws to make it 90-83 with 4:28 remaining. But it was not an easy night for the Cavaliers. Kevin Love fouled out with eight points and 10 rebounds when he got whistled on a basket by Butler moments earlier.",
                 ImageUrl = "http://ak-static.cms.nba.com/wp-content/uploads/sites/45/2017/03/LeBron-James-470.jpg",
                 PublishDate = new DateTime(2017, 3, 30)
             },
             new Article
             {
                 Author = jicata,
                 Title = "Nvidia GeForce GTX 1060 Graphics Card Roundup",
                 Content = "Nvidia launched its GeForce GTX 1060 6GB in July of 2016 to inevitable comparisons with AMD's Radeon RX 480 8GB. Although the 1060 was faster in DX11 games, it also commanded a premium that was harder to justify than the uncontested GeForce GTX 1070 and 1080. A month later, Nvidia quietly rolled out a 3GB version of the 1060 to battle the 4GB RX 480. Its GPU took quite a haircut in the process, though, dropping from 1280 to 1152 CUDA cores and affecting performance far more than model name suggests. Presumably, Nvidia couldn't risk the 3GB and 6GB models appearing too similar at 1920x1080. After wrapping up our initial GeForce GTX 1080 and GeForce GTX 1070 round-ups, Tom's Hardware DE set to work on a collection of 1060s, 3GB and 6GB alike. This first incarnation includes eight different boards from a field that spans anywhere from under $200 to over $300. Each individual review goes incredibly deep, covering manufacturing quality, technical features, power consumption, clock rates, cooling, and acoustics.",
                 ImageUrl = "http://images.nvidia.com/pascal/img/gtx1060/GeForce_GTX_1060_Front.png",
                 PublishDate = new DateTime(2017, 1, 13)
             },
             new Article
             {
                 Author = joro,
                 Title = "Swedish Police Raid The Pirate Bay, Site Offline",
                 Content = "Police in Sweden carried out a raid in Stockholm today, seizing servers, computers, and other equipment. At the same time The Pirate Bay and several other torrent-related sites disappeared offline. Although no official statement has been made, TF sources confirm action against TPB. For many years The Pirate Bay has been sailing by the seat of its pants so any downtime is met with concern from its millions of users. This morning, for the first time in months, The Pirate Bay disappeared offline. A number of concerned users emailed TF for information but at that point technical issues seemed the most likely culprit. However, over in Sweden authorities have just confirmed that local police carried out a raid in Stockholm this morning as part of an operation to protect intellectual property.",
                 ImageUrl = "https://torrentfreak.com/images/tpb-logo.jpg",
                 PublishDate = new DateTime(2014, 12, 9)
             });

            context.Courses.AddOrUpdate(c => c.Name, 
                new Course
            {
                Name = "C# MVC Frameworks - ASP.NET",
                Description = "Курсът \"ASP.NET MVC\" ще ви научи как се изграждат съвременни уеб приложения с архитектурата Model-View-Controller, използвайки HTML5, бази данни, Entity Framework и други технологии. Изучава се технологичната платформа ASP.NET, нейните компоненти и архитектура, създаването на MVC уеб приложения, дефинирането на модели, изгледи и частични изгледи с Razor view engine, дефиниране на контролери, работа с бази данни и интеграция с Entity Framework, LINQ и SQL Server. Курсът включа и по-сложни теми като работа с потребители, роли и сесии, използване на AJAX, кеширане, сигурност на уеб приложенията, уеб сокети и SignalR и работа с библиотеки от MVC контроли. Включват се няколко практически лабораторни упражнения (лабове) и работилници за изграждане на цялостни, пълнофункционални ASP.NET MVC уеб приложения. Курсът включва работа по екипен проект за изграждане на уеб приложение и завършва с практически изпит по уеб разработка с ASP.NET MVC.",
                StartDate = new DateTime(2017, 3, 6),
                EndDate =  new DateTime(2017, 5, 5),
                Trainer = bojo
            }, new Course
            {
                Name = "QA Fundamentals",
                Description = "Курсът QA Fundamentals ще ви запознае с основните концепции на осигуряването на качеството на софтуера (software quality assurance / QA). Щe научите как да търсите дефекти чрез въвеждане на подходящи входни данни и как да тествате потребителското изживяване. Освен ръчното тестване и описване на дефектите, ще се разгледа и тест автоматизацията. Ще се научите да работите с най-използваните в QA бранша инструменти за автоматизирано тестване като Selenium.",
                StartDate = new DateTime(2017, 1, 16),
                EndDate = new DateTime(2017, 3, 26),
                Trainer = joro
            }, new Course
            {
                Name = "Въведение в Agile Software Development",
                Description = "Курсът ще ви въведе в гъвкавите методологии за управление на проекти и софтуерно производство, които в съвременните софтуерни компании са се превърнали в стандарт за работа. Ще се запознаете с най-популярните практики и методологии като Scrum, Kanban, Lean, XP. Обучението ще ви предостави възможност да разберете как най-бързо да се адаптирате според променящите се изисквания на клиенти и заинтересовани страни. Също така ще разберете и как да разработвате точните характеристики на софтуерния продукт в точното време без излишен стрес и с постоянно темпо.",
                StartDate = new DateTime(2017, 4, 20),
                EndDate =  new DateTime(2017, 6, 4),
                Trainer = jicata
            }, new Course
            {
                Name   = "Обучение за трейнъри",
                Description = "Курсът \"Обучение за трейнъри\" развива както умения за презентиране пред голяма аудитория, така и специализиране във водене на технически обучения. Практическата част на изпита включва съставяне на различни презентации за курсистите, както и тяхното представяне на живо.\r\nКурсът е напълно безплатен и всеки желаещ може да се включи. Единственото условие е изпращане на презентация за предварително представяне.",
                StartDate = new DateTime(2017, 4, 12),
                EndDate = new DateTime(2017, 5, 5),
                Trainer = jicata
            }, new Course
            {
                Name = "PHP MVC Frameworks",
                Description = "В курса по основи на уеб програмирането, ще ви запознаем с основни принципи на уеб разработката като HTTP протокол, сесии и начините на запазване на състоянието, кеширане на данните, различните протоколи за пренос на данни, сигурност. Ще напишем собствен MVC framework на базата на PHP и MySQL, с отделяне на модели, изгледи и контролери, с front - controller и router, с поддръжка на всички базови функционалности, необходими за едно съвременно уеб приложение: визуализация на данни, таблици, CRUD операции върху данните, форми, страниране, валидация, нотификации, потребители, сесии, login / logout и други.На практика ще се научите да създавате собствени уеб приложения с PHP и MySQL и ще разберете как работят уеб приложенията като структура и как работи MVC(model - view - controller) технологията при съвременните уеб приложения. Курсът завършва с практически проект - изграждане на уеб приложение с PHP, MySQL и собствен MVC framework.",
                StartDate = new DateTime(2017, 2, 25),
                EndDate = DateTime.Now.AddDays(1),
                Trainer = bojo
            }, new Course
            {
                Name = "Основи на Photoshop",
                Description = "Курсът по Photoshop е насочен към хора, които искат да направят своите първи стъпки в графичния и дигитален дизайн. От него ще получите базови познания за работа с растерна графика, приложима във всеки аспект на дизайна. Обучението обхваща всички важни теми, които са необходими за работа със софтуера - какво представлява, за какво се използва, интерфейс, инструменти, ефекти, приложение, обработка на снимки, дизайн и т.н.",
                StartDate = new DateTime(2017, 3, 18),
                EndDate = DateTime.Now.AddDays(1),
                Trainer = joro
            });

            context.SaveChanges();
        }
    }
}