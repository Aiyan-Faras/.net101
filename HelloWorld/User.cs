using System.Text.Json;
using System.Text.Json.Serialization;

public partial class User {
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("firstName")]
    public String? FirstName { get; set; } = "";
    [JsonPropertyName("lastName")]
    public String? LastName { get; set; } = "";
    [JsonPropertyName("email")]
    public String? Email { get; set; } = "";
    
    [JsonPropertyName("gender")]
    public String? Gender { get; set; } = "";
    [JsonPropertyName("active")]
    public bool Active { get; set; }

}