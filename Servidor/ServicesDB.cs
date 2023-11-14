using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public static class ServicesDB
    {
        static ConnSingleton cs = ConnSingleton.getDbInstance();

        public static bool iniciarSesion()
        {
            User user = new User("", "", "");

            Console.Write("Ingresa tu usuario:");

            user.userName = Console.ReadLine();

            Console.Write("Ingresa tu contraseña:");

            user.password = Console.ReadLine();

            return validarSesion(user);
        }


        static bool validarSesion(User user)
        {
            var bd = cs.GetDBConnection();

            string masterKey = "ClaveMaestraSecreta";

            
            string consultaUser1 = "select ISNULL(nombreUsuario, 0) from Usuario where id=1";

            SqlCommand comandoUser = new SqlCommand(consultaUser1, bd);

            string usuarioBD = comandoUser.ExecuteScalar().ToString();


            string consultaSalt1 = "select ISNULL(contraseniaSalt, 0) from Usuario where id=1";

            SqlCommand comandoSalt = new SqlCommand(consultaSalt1, bd);

            string salt = comandoSalt.ExecuteScalar().ToString();


            string consultaHash1 = "select ISNULL(contraseniaHashed, 0) from Usuario where id=1";

            SqlCommand comandoHash = new SqlCommand(consultaHash1, bd);

            string hashedPassword = comandoHash.ExecuteScalar().ToString();


            byte[] saltb = Convert.FromBase64String(salt);


            bool isLoginSuccessful = VerifyPassword(user.password, saltb, hashedPassword, masterKey);

            if ((user.userName == usuarioBD) && isLoginSuccessful)
            {
                Console.WriteLine("Inicio de sesión exitoso.");
                return true;
            }
            else
            {
                Console.WriteLine("Inicio de sesión fallido. Verificar usuario y/o contraseña");
                bd.Close();
                return false;
            }
        }

        static string HashPassword(string password, byte[] salt, string masterKey)
        {
            int iterations = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(masterKey)))
                {
                    byte[] masterKeyBytes = hmac.ComputeHash(hashBytes);
                    return Convert.ToBase64String(masterKeyBytes);
                }
            }
        }
        static bool VerifyPassword(string userInputPassword, byte[] salt, string hashedPassword, string masterKey)
        {
            string hashedInputPassword = HashPassword(userInputPassword, salt, masterKey);
            return hashedInputPassword == hashedPassword;
        }

    }
}
