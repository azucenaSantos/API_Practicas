//Clase que contendrá las respuesta de lo que enviamos al cliente una vez sucede una excepcion
namespace API.Errors
{
    public class ApiException
    {
        //Constructor -> usado para devolver una excepcion (codigo, mensage y detalles)
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        //Propiedades
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}