namespace MascotasCRUDApi.Models
{
    //propiedades aqui estamos creando un modelo para crear apartir de codigo las tablas
    //para crear la base de datos apartir del codigo se tiene dos opciones
    // 1: los dataanotation y la otra es flueapi, en este curso usaremos data anotations
    //apertir de esta clase creamos la base de datos con las migraciones
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public int Edad{ get; set; }
        public float Peso{ get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
