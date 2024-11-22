using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvtoMirIsit.Entities;

[Table("Автомобиль")]
public class Auto
{
    [Key]
    [Column("id_автомобиля")]
    public int Id { get; set; }
    [Column("Номер")]
    public int RegNumber { get; set; }
    [Column("vin_номер")]
    public string VinNumber { get; set; }
    [Column("Год_выпуска")]
    public DateTime CreationYear { get; set; }
    [Column("Цена")]
    public int Price { get; set; }
    [Column("Цвет")]
    public string Color { get; set; }
    [Column("id_типа")]
    public int IdType { get; set; }
    [Column("photo")]
    public string Image { get; set; }
}