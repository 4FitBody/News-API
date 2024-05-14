using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_Api.Core.Models;

namespace News_Api.Presentation.Models;

public class NewsContent
{
    public News? News { get; set; }
    public string? ImageFileName { get; set; }
    public byte[]? ImageFileContent { get; set; }
}
