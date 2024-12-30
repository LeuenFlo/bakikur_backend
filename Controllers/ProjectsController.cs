using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakikurBackend.Models;
using BakikurBackend.Data;
using BakikurBackend.Services;

namespace BakikurBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ImageService _imageService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(
        ApplicationDbContext context,
        ImageService imageService,
        ILogger<ProjectsController> logger)
    {
        _context = context;
        _imageService = imageService;
        _logger = logger;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    // GET: api/Projects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    // POST: api/Projects
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject([FromForm] ProjectCreateDto projectDto)
    {
        try
        {
            // Save the image and get its path
            var imagePath = await _imageService.SaveImageAsync(projectDto.Image);

            // Create new project
            var project = new Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                Category = projectDto.Category,
                CompletionDate = projectDto.CompletionDate,
                ImageUrl = imagePath
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating project");
            return StatusCode(500, "Error creating project");
        }
    }

    // PUT: api/Projects/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Projects/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
} 