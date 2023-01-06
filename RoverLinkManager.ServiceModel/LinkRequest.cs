using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace RoverLinkManager.ServiceModel;

[Route("/link/{ShortId}")]
public class LinkRequest : IReturn<object>
{
    public string ShortId { get; set; }
}

