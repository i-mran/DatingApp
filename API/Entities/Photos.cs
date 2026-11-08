using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace Entities;

[Table("Photos")]
public class Photos
{
 public int Id { get; set; }    
  public string  Url { get; set; }    
 public bool IsMain { get; set; }    
 public string PublicId { get; set; }    
public int AppUserID { get; set; } 
public AppUser AppUser  { get; set; }
}
