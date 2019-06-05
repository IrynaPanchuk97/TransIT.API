using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
        [HttpPost]
        public override async Task<IActionResult> Create([FromForm] DocumentDTO document)
        {
            if (document.File == null )
                return Content("file not selected");
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"uploadFile", document.File.FileName);

            document.Path = filePath;
            var entity = _mapper.Map<Document>(document);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            entity.ModId = userId;
            entity.CreateId = userId;


                if (document.File.Length > 0)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await document.File.CopyToAsync(fileStream);
                    }
                }
            

            var createdEntity = await _documentService.CreateAsync(entity);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), _mapper.Map<DocumentDTO>(createdEntity))
                : (IActionResult)BadRequest();
        }
    }
}
