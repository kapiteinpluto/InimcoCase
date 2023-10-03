using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace inimco_case_backend.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [JsonIgnore]
    public string? SkillsString { get; set; }   

    [JsonIgnore]
    public string? SocialMediaString { get; set; }

    [NotMapped] // This property won't be mapped to the database
    public List<string> Skills
    {
        get { return JsonConvert.DeserializeObject<List<string>>(SkillsString ?? ""); }
        set { SkillsString = JsonConvert.SerializeObject(value); }
    }

    [NotMapped] // This property won't be mapped to the database
    public List<SocialMediaInfo> SocialMedia
    {
        get { return JsonConvert.DeserializeObject<List<SocialMediaInfo>>(SocialMediaString ?? ""); }
        set { SocialMediaString = JsonConvert.SerializeObject(value); }
    }
}

public class SocialMediaInfo
{
    public string Type { get; set; }
    public string Address { get; set; }
}
