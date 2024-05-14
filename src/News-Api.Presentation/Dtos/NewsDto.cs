using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Presentation.Dtos;

public class NewsDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool IsApproved { get; set; }
}
