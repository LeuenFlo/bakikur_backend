using BakikurBackend.Models;
using Microsoft.Extensions.Logging;

namespace BakikurBackend.Data;

public static class DataSeeder
{
    public static async Task SeedData(ApplicationDbContext context, IWebHostEnvironment env, ILogger logger)
    {
        if (!context.Projects.Any())
        {
            logger.LogInformation("Starting data seeding...");
            
            // Ensure images directory exists
            var imagesDirectory = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
                logger.LogInformation("Created uploads directory");
            }

            // Copy images from source to uploads
            var sourceImagesDir = Path.Combine(env.ContentRootPath, "images");
            logger.LogInformation("Source images directory: {Dir}", sourceImagesDir);
            
            var projects = new List<Project>
            {
                new Project
                {
                    Title = "Eleganter Klapptisch",
                    Description = "Praktischer und stilvoller Klapptisch aus hochwertigem Holz, perfekt für flexible Raumnutzung",
                    Category = "moebel",
                    CompletionDate = new DateTime(2024, 1, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "klapptisch.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Moderne Sitzbank",
                    Description = "Handgefertigte Sitzbank mit klaren Linien und komfortabler Polsterung",
                    Category = "moebel",
                    CompletionDate = new DateTime(2023, 12, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "bank.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "72185833_102249957899946_554235000921260032_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Einbauschrank nach Maß",
                    Description = "Maßgefertigter Einbauschrank mit optimaler Raumnutzung und elegantem Design",
                    Category = "innenausbau",
                    CompletionDate = new DateTime(2023, 11, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "272858613_527332492058355_350812099419953533_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "272870698_527332482058356_8850788711117786032_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Küchenmöbel",
                    Description = "Moderne Küchenmöbel mit hochwertigen Beschlägen und durchdachter Raumaufteilung",
                    Category = "innenausbau",
                    CompletionDate = new DateTime(2023, 10, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "274230217_539119180879686_4089096177529724589_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "274144181_539119007546370_8850609268575953166_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "274129502_539119420879662_5337474781398331841_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Restaurierte Holztüren",
                    Description = "Fachgerechte Restaurierung historischer Holztüren mit Bewahrung der ursprünglichen Charakteristik",
                    Category = "restaurierung",
                    CompletionDate = new DateTime(2023, 9, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "166817284_323222232469383_8629170683145990355_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "166613364_323222212469385_7462212440238162404_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Badezimmermöbel",
                    Description = "Wasserbeständige Badezimmermöbel mit modernem Design und praktischer Aufbewahrung",
                    Category = "innenausbau",
                    CompletionDate = new DateTime(2023, 8, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "275930533_557183349073269_1027403283594418107_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Vintage Kommode",
                    Description = "Liebevoll restaurierte Vintage-Kommode mit originalem Charme und modernen Funktionen",
                    Category = "restaurierung",
                    CompletionDate = new DateTime(2023, 7, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "165218654_319780879480185_791655402348798634_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "165126696_319780842813522_600722974499482168_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "164575341_319780886146851_4680904032335696531_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Maßgefertigte Garderobe",
                    Description = "Elegante Garderobenlösung mit integrierter Sitzbank und verstecktem Stauraum",
                    Category = "innenausbau",
                    CompletionDate = new DateTime(2023, 6, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "173351580_332704121521194_3199043170407150359_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "173463508_332704011521205_1669496488695933343_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Designer Esstisch",
                    Description = "Exklusiver Esstisch mit einzigartiger Holzmaserung und modernem Metallgestell",
                    Category = "moebel",
                    CompletionDate = new DateTime(2023, 5, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "186470630_351934169598189_7499588074027143111_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "186515347_351933319598274_4894322160124164276_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "186374663_351933316264941_6102702285541633600_n.jpg", logger) }
                    }
                },
                new Project
                {
                    Title = "Antiker Schreibtisch",
                    Description = "Sorgfältig restaurierter Schreibtisch aus der Gründerzeit mit original Beschlägen",
                    Category = "restaurierung",
                    CompletionDate = new DateTime(2023, 4, 1),
                    Images = new List<ProjectImage>
                    {
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "167592435_323926579065615_4235416454899499471_n.jpg", logger) },
                        new ProjectImage { ImageUrl = await CopyImageAsync(sourceImagesDir, imagesDirectory, "167607526_323926552398951_1998301018567591949_n.jpg", logger) }
                    }
                }
            };

            logger.LogInformation("Adding {Count} projects to database", projects.Count);
            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();
            logger.LogInformation("Data seeding completed");
        }
        else
        {
            logger.LogInformation("Database already contains projects, skipping seed");
        }
    }

    private static async Task<string> CopyImageAsync(string sourceDir, string targetDir, string fileName, ILogger logger)
    {
        var sourcePath = Path.Combine(sourceDir, fileName);
        var targetPath = Path.Combine(targetDir, fileName);

        if (File.Exists(sourcePath))
        {
            logger.LogInformation("Copying image {File}", fileName);
            using (var sourceStream = File.OpenRead(sourcePath))
            using (var targetStream = File.Create(targetPath))
            {
                await sourceStream.CopyToAsync(targetStream);
            }
            return $"/uploads/{fileName}";
        }

        logger.LogWarning("Image file not found: {File}", fileName);
        return string.Empty;
    }
} 