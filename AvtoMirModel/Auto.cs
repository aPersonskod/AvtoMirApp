using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvtoMirModel;

[Table("Автомобиль")]
public class Auto
{
    [Key]
    [Column("id_автомобиля")]
    public int Id { get; set; }
    [Column("Номер")]
    public string RegNumber { get; set; }
    [Column("vin_номер")]
    public string VinNumber { get; set; }
    [Column("Год_выпуска")]
    public string CreationYear { get; set; }
    [Column("Цена")]
    public int Price { get; set; }
    [Column("Цвет")]
    public string Color { get; set; }
    [Column("id_типа")]
    public int IdType { get; set; }
    [Column("photo")]
    public string Image { get; set; }
}
public class AutoModel
{
    public int Id { get; set; }
    public string RegNumber { get; set; }
    public string VinNumber { get; set; }
    public string CreationYear { get; set; }
    public int Price { get; set; }
    public string Color { get; set; }
    public AutoTypeModel Type { get; set; }
    public string Image { get; set; }
}

[Table("Договор")]
public class Dogovor
{
    [Key] 
    [Column("id_договора")]
    public int Id { get; set; }
    [Column("Дата_продажи")]
    public DateTime SaleDate { get; set; }
    [Column("Сумма_продажи")]
    public int Cost { get; set; }
    [Column("id_сотрудника")]
    public int IdEmployee { get; set; }
    [Column("id_автомобиля")]
    public int IdAvto { get; set; }
    [Column("id_клиента")]
    public int IdClient { get; set; }
}
public class DogovorModel
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public int Cost { get; set; }
    public int IdEmployee { get; set; }
    public int IdAvto { get; set; }
    public int IdClient { get; set; }
    public Employee Employee { get; set; }
    public Client Client { get; set; }
    public string CarInfo { get; set; }
}

[Table("Клиент")]
public class Client
{
    [Key] 
    [Column("id_клиента")]
    public int Id { get; set; }
    [Column("ФИО")]
    public string Fio { get; set; }
    [Column("Адрес")]
    public string Adress { get; set; }
    [Column("Телефон")]
    public string Mobile { get; set; }
}

[Table("Магазин")]
public class Shop
{
    [Key] 
    [Column("id_магазина")]
    public int Id { get; set; }
    [Column("Адрес")]
    public string Adress { get; set; }
}

[Table("МаркаАвтомобиля")]
public class CarMake
{
    [Key] 
    [Column("id_марка")]
    public int Id { get; set; }
    [Column("Марка")]
    public string Mark { get; set; }
}

[Table("Сотрудник")]
public class Employee
{
    [Key] 
    [Column("id_сотрудника")]
    public int Id { get; set; }
    [Column("ФИО")]
    public string Fio { get; set; }
    [Column("Телефон")]
    public string Mobile { get; set; }
    [Column("id_магазина")]
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
}

[Table("ТипАвтомобиля")]
public class AutoType
{
    [Key] 
    [Column("id_типа")]
    public int Id { get; set; }
    [Column("Модель")]
    public string Model { get; set; }
    [Column("id_марка")]
    public int MarkId { get; set; }
}

public class AutoTypeModel
{
    public int Id { get; set; }
    public string Model { get; set; }
    public CarMake Mark { get; set; }
}