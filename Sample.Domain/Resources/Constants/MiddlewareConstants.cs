namespace Sample.Domain.Resources.Constants
{
    public static class MiddlewareConstants
    {
        public const string JSON = "application/json";
        public const string INVALID_CODE_ERROR = "El estado del objeto no permite ejecutar la operación";
        public const string UNAUTHORIZED_CODE_ERROR = "No cuenta con los permisos para realizar la operación";
        public const string FORMART_CODE_ERROR = "El formato de entrada no es el correcto";
        public const string TIME_OUT_CODE_ERROR = "La operación genero un time out.";
        public const string IO_CODE_ERROR = "Fallo al leer el archivo.";
        public const string INTERNAL_ERROR = "Ocurrió un error inesperado";
        public const string VALIDATION_ERROR = "Falló una o mas valicaiones";
    }
}
