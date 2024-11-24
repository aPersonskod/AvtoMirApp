using System;

namespace AvtoMirClient.DTO;

public class AutoDto
{
    public int id { get; set; }
    public int regNumber { get; set; }
    public string vinNumber { get; set; }
    public DateTime creationYear { get; set; }
    public int price { get; set; }
    public string color { get; set; }
    public int idType { get; set; }
    public string image { get; set; }
}