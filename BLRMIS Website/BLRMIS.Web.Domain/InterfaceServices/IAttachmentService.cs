using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IAttachmentService
    {
        AttachmentModel AddAttachment(AttachmentModel model);

        Task AddAttachments(List<IFormFile> FileModels, int SourceId,  int SourceType);

        List<AttachmentModel> GetAttachments(int SourceType, int SourceId);

        AttachmentModel GetAttachment(int id);

        void DeleteAttachments(List<AttachmentModel> attachments);
        void DeleteAttachment(int Id);

        void DeleteAttachments(int SourceType, int SourceId);
    }
}
