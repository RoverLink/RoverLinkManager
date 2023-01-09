using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities;

public class Translation
{
    public string OriginalLanguageKey { get; set; } = "en";
    public string Content { get; set; } = string.Empty;
    public string TranslatedLanguageKey { get; set; } = string.Empty;
    public string TranslatedContent { get; set; } = string.Empty;
}
