using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

        [HttpGet("~/api/v1/" + nameof(Document) + "/{id}/file")]
        public async virtual Task<IActionResult> DownloadFile(int id)
        {
            var result = await _documentService.GetAsync(id);
            byte[] fileData = System.IO.File.ReadAllBytes(result.Path);

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(Path.GetFileName(result.Path), out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(fileData, contentType);
        }


        [HttpPost]
        public override async Task<IActionResult> Create([FromForm] DocumentDTO document)
        {
            if (document.File == null&& !(document.File.Length > 0))
                return Content("file not selected");
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            _ = provider.TryGetContentType(Path.GetFileName(document.File.FileName), out contentType);
           if(contentType!=  "application/pdf") return Content("format is not pdf");
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "//source//" + "TransportITDocuments";
          //  var filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + "TransportITDocuments";
            System.IO.Directory.CreateDirectory(filePath);

            filePath = Path.Combine(filePath, DateTime.Now.ToString("MM/dd/yyyy/HH/mm/ss") +document.File.FileName);

            document.Path = filePath;
            var entity = _mapper.Map<Document>(document);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            entity.ModId = userId;
            entity.CreateId = userId;

            var createdEntity = await _documentService.CreateAsync(entity);
            using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                await document.File.CopyToAsync(fileStream);
            }

            return createdEntity != null
                ? CreatedAtAction(nameof(Create), _mapper.Map<DocumentDTO>(createdEntity))
                : (IActionResult)BadRequest();
        }
    }
}
