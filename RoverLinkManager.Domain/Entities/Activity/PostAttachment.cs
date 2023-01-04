using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Activity;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities.Activity;

[CompositeKey(nameof(PostId), nameof(AttachmentId))]
public class PostAttachment
{
    public long PostId { get; set; }
    public long AttachmentId { get; set; }
    [Reference]
    public Attachment? Attachment { get; set; }

}
