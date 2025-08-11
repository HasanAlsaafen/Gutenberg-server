﻿using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase

{
    private readonly IServiceService _serviceService;
    public ServicesController(IServiceService serviceService)

    {
        _serviceService = serviceService;
    }
    [HttpGet]
     [AllowAnonymous]  
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceService.GetAllAsync();
        var dtos = services.Select(s => new ServiceDTO
        {
            ServiceId = s.ServiceId,
            Title = s.Title,
            Description = s.Description,
            Image = s.Image
        });
        return Ok(dtos);
    }
    [HttpGet("{id}")]
       [AllowAnonymous]  


    public async Task<IActionResult> GetById(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();

        var dto = new ServiceDTO
        {
            ServiceId = service.ServiceId,
            Title = service.Title,
            Description = service.Description,
            Image = service.Image
        };
        return Ok(dto);
    }
    [HttpPost]
        [Authorize]  


    public async Task<IActionResult> Create([FromBody] ServiceDTO dto)
    {
        var service = new Service
        {
            Title = dto.Title,
            Description = dto.Description,
            Image = dto.Image
        };

        var created = await _serviceService.CreateAsync(service);
        dto.ServiceId = created.ServiceId;
        return CreatedAtAction(nameof(GetById), new { id = dto.ServiceId }, dto);
    }
    [HttpPut("{id}")]
        [Authorize]  


    public async Task<IActionResult> Update(int id, [FromBody] ServiceDTO dto)
    {
        var existing = await _serviceService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Image = dto.Image;
        var updated = await _serviceService.UpdateAsync(existing);
        return Ok(dto);
    }
    [HttpDelete("{id}")]
        [Authorize]  


    public async Task<IActionResult> Delete(int id)

    {
        var deleted = await _serviceService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}