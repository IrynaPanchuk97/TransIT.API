using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class DocumentController : DataController<Document, DocumentDTO>
    {
        private readonly IDocumentService _documentService;
        
        public DocumentController(
            IMapper mapper,
            IDocumentService documentService,
            IFilterService<Document> odService
            ) : base(mapper, documentService, odService)
        {
            _documentService = documentService;
        }

        [HttpGet("~/api/v1/" + nameof(IssueLog) + "/{issueLogId}/" + nameof(Document))]
        public async virtual Task<IActionResult> GetByIssueLog(int issueLogId)
        {
            var result = await _documentService.GetRangeByIssueLogIdAsync(issueLogId);
            return result != null
                ? Json(_mapper.Map<IEnumerable<DocumentDTO>>(result))
                : (IActionResult) BadRequest();
        }
    }
}
