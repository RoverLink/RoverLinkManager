using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Enum.Activity;
using ServiceStack.DataAnnotations;
using Stream.Models;

namespace RoverLinkManager.Domain.Entities.Activity;

// Note: See https://github.com/ServiceStack/ServiceStack.Aws/blob/master/src/ServiceStack.Aws/S3/S3VirtualDirectory.cs

public class ImageVariant
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string Filename { get; set; } = string.Empty;
    public string DirPath { get; set; } = string.Empty; // Virtual path
    public string FilePath { get; set; } = string.Empty;
    public DateTime FileLastModified { get; set; } = DateTime.UtcNow;
    public long Length { get; set; }
    public string MimeType { get; set; } = string.Empty;
}

public class Attachment
{
    [PrimaryKey]
    public long Id { get; set; }
    public long AppUserId { get; set; }  // User Id of person who created attachment
    public ContentType ContentType { get; set; } = ContentType.Undefined;
    // Data can be different depending on content type - for example data can be a link
    public string Data { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string DirPath { get; set; } = string.Empty; // Virtual path
    public string FilePath { get; set; } = string.Empty;
    public DateTime FileLastModified { get; set; } = DateTime.UtcNow;
    public long Length { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public string ETag { get; set; } = string.Empty;
    [PgSqlJsonB]
    public Og? OpenGraphData { get; set; }
    public DateTime? OpenGraphLastModified { get; set; }
    [PgSqlJsonB]
    public List<ImageVariant> ImageVariants { get; set; } = new();
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool Deleted { get; set; }
    public bool Processed { get; set; }
    public DateTime? ProcessedDate { get; set; }
}
