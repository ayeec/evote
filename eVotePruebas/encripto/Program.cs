using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

//http://www.gutgames.com/post/AES-Encryption-in-C.aspx

namespace encripto
{
    class Program
    {
        static void Main(string[] args)
        {

            RijndaelManaged man = new RijndaelManaged();
            byte[] plain = {1};
            man.Mode = CipherMode.CBC;
            PasswordDeriveBytes derive = new PasswordDeriveBytes("contra", new byte[] { 0x23, 0x25, 0x25 }, "SHA1", 2);
            byte[] llave = derive.GetBytes(256 / 8);
            ICryptoTransform ict = man.CreateEncryptor(llave, Encoding.ASCII.GetBytes("0FRna73m*aze01xY"));
            MemoryStream mem = new MemoryStream();
            CryptoStream cryp = new CryptoStream(mem, ict, CryptoStreamMode.Write);
            cryp.Write(plain, 0, plain.Length);
            cryp.FlushFinalBlock();
            byte[] cifra = mem.ToArray();
            mem.Close();
            cryp.Close();
            man.Clear();
            Console.WriteLine(String.Join(",", plain.Select(o => o.ToString()).ToArray()));
            Console.WriteLine(String.Join(",", cifra.Select(o => o.ToString()).ToArray()));
            Console.WriteLine(Convert.ToBase64String(cifra));
            Console.WriteLine("\n");
            Console.WriteLine("des");

            ict = man.CreateDecryptor(llave, Encoding.ASCII.GetBytes("OFRna73m*aze01xY"));
            mem = new MemoryStream(cifra);
            cryp = new CryptoStream(mem, ict, CryptoStreamMode.Read);
            byte[] des=new byte[cifra.Length];
            int conteo = cryp.Read(des, 0, des.Length);
            mem.Close();
            cryp.Close();
            man.Clear();

            Console.WriteLine( String.Join(",", des.Select(o => o.ToString()).ToArray()));
            Console.WriteLine(Convert.ToBase64String(des));
        }

        static byte[] leerbytes(){
            FileStream fs = new FileStream(@"F:\prog\VC++\eVotePruebas\eVotePruebas\eVotePruebas\bin\Debug\hola.db", FileMode.Open, FileAccess.Read);
            byte[] datos = new byte[fs.Length];
            fs.Read(datos, 0, Convert.ToInt32( fs.Length));
            fs.Close();
            return datos;
        }
    }
}
