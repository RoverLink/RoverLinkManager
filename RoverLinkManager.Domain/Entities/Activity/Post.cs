using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalexApi.Domain.Entities.Identity;
using JalexApi.Domain.Enum.AccessControl;
using JalexApi.Domain.Enum.Activity;
using RoverLinkManager.Domain.Entities.Activity;
using RoverLinkManager.Domain.Entities.Identity;
using RoverLinkManager.Domain.Enum.AccessControl;
using RoverLinkManager.Domain.Enum.Activity;
using ServiceStack.DataAnnotations;
using Stream.Models;

namespace JalexApi.Domain.Entities.Activity;

public class Post
{
    public long Id { get; set; }
    [Ignore]
    public string StreamForeignId => $"post:{Id}";  // The id (that we generate) that this post is known as in Getstream.io
    public string StreamId { get; set; } = string.Empty;  // Getstream.io's id for this post

    [Reference]
    public AppUser User { get; set; } = new();
    [Index(Clustered = true)]
    public int AppUserId { get; set; }
    public ActivityVerb Verb { get; set; } = ActivityVerb.Post;
    public PostAudience Audience { get; set; } = PostAudience.Public;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// The TO field allows you to specify a list of feeds to which the activity should be copied. One way to think about it is as the CC functionality of email.
    /// See https://getstream.io/activity-feeds/docs/dotnet-csharp/targeting/?language=csharp
    /// </summary>
    public List<string> To { get; set; } = new();  // secondary target feeds - e.g. user:1234 or group:1234
    public int LikeCount { get; set; }
    public int ShareCount { get; set; }
    public int CommentCount { get; set; }
    public double Popularity { get; set; } = 0;
    [StringLength(StringLengthAttribute.MaxText)] 
    public string Text { get; set; } = string.Empty;
    [Reference]
    public List<PostAttachment> Attachments { get; set; } = new();
    /*
     *   metadata: {
		    mentioned: [
		      {
		        index: 16, // string index where highlight should start
		        length: 8, // length of highlight text
		        type: 'user',
		        userId: 'userId123',
		      },
		    ],
		}
     */
    [PgSqlJsonB]
    public object? Metadata { get; set; }   // Contains result of parsing the Text for mentions 
    public bool IsPinned { get; set; }
    /// <summary>
    /// Has this post been moderated yet?
    /// </summary>
    public bool IsModerated { get; set; }
    /// <summary>
    /// If this post gets flagged by a user then it must be moderated
    /// </summary>
    public bool IsFlagged { get; set; }
    public bool IsDeleted { get; set; }
}
