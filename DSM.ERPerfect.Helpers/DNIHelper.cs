namespace DSM.ERPerfect.Helpers
{
    public static class DNIHelper
    {
        public static bool ValidarDNI(string dni)
        {
            try
            {
                // Comprobamos la longitud del DNI
                if (dni.Length != 9)
                {
                    return false;
                }

                // Extraemos el número y la letra del DNI
                string numeroStr = dni.Substring(0, 8);
                string letra = dni.Substring(8, 1);

                // Comprobamos que el número sea válido
                if (!int.TryParse(numeroStr, out int numero))
                {
                    return false;
                }

                // Calculamos la letra esperada
                string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
                char letraEsperada = letras[numero % 23];

                // Comparamos la letra esperada con la letra proporcionada
                return letraEsperada == letra[0];
            }
            catch
            {
                return false;
            }
        }
    }
}