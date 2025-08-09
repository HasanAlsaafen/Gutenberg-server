using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepo;

        public ApplicationService(IApplicationRepository applicationRepo)
        {
            _applicationRepo = applicationRepo;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllAsync()
        {
            var apps = await _applicationRepo.GetAllAsync();
            return apps.Select(a => new ApplicationDTO
            {
                ApplicationId = a.ApplicationId,
                JobId = a.JobId,
                ApplicantName = a.ApplicantName,
                ApplicantEmail = a.ApplicantEmail,
                ApplicantPhone = a.ApplicantPhone,
                Attachment = a.Attachment,
                ApplicationDate = a.ApplicationDate,
                ApplicationStatus = a.ApplicationStatus.ToString()
            });
        }

        public async Task<ApplicationDTO?> GetByIdAsync(int id)
        {
            var app = await _applicationRepo.GetByIdAsync(id);
            if (app == null) return null;

            return new ApplicationDTO
            {
                ApplicationId = app.ApplicationId,
                JobId = app.JobId,
                ApplicantName = app.ApplicantName,
                ApplicantEmail = app.ApplicantEmail,
                ApplicantPhone = app.ApplicantPhone,
                Attachment = app.Attachment,
                ApplicationDate = app.ApplicationDate,
                ApplicationStatus = app.ApplicationStatus.ToString()
            };
        }

        public async Task<ApplicationDTO> CreateAsync(ApplicationDTO dto)
        {
            var app = new Application
            {
                JobId = dto.JobId,
                ApplicantName = dto.ApplicantName,
                ApplicantEmail = dto.ApplicantEmail,
                ApplicantPhone = dto.ApplicantPhone,
                Attachment = dto.Attachment,
                ApplicationDate = DateTime.UtcNow,
                ApplicationStatus = ApplicationStatus.Pending
            };

            await _applicationRepo.AddAsync(app);

            dto.ApplicationId = app.ApplicationId;
            dto.ApplicationDate = app.ApplicationDate;
            dto.ApplicationStatus = app.ApplicationStatus.ToString();
            return dto;
        }

        public async Task<ApplicationDTO?> UpdateAsync(int id, ApplicationDTO dto)
        {
            var app = await _applicationRepo.GetByIdAsync(id);
            if (app == null) return null;

            app.JobId = dto.JobId;
            app.ApplicantName = dto.ApplicantName;
            app.ApplicantEmail = dto.ApplicantEmail;
            app.ApplicantPhone = dto.ApplicantPhone;
            app.Attachment = dto.Attachment;
            app.ApplicationStatus = Enum.Parse<ApplicationStatus>(dto.ApplicationStatus);

            await _applicationRepo.UpdateAsync(app);
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _applicationRepo.DeleteAsync(id);
        }
    }
}
