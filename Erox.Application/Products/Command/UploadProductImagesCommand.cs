using Erox.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public class UploadProductImagesCommand : IRequest<OperationResult<List<string>>>
    {
        public Guid ProductId { get; set; }
        public List<IFormFile> Files { get; set; }
    }

}
