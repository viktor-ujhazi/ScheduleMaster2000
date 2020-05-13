﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class PasswordEncrypter : ICyberSecurityProvider
    {
        public string cs = "Host=localhost;Username=postgres;Password=admin;Database=ScheduleMaster";

        public string EncryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);//Create salt - injection for further security

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);//Create the Rfc2898DeriveBytes and get the hash value:
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];  // combine the salt and pw
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);//  Turn the combined salt+hash into a string for storage
            return savedPasswordHash;
        }

        public bool IsValidUser(string email, string password)
        {
            try
            {
                string savedPassword = GetUserPassword(email);// get the pw from database

                byte[] hashBytes = Convert.FromBase64String(savedPassword);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public string GetUserPassword(string email)
        {
            var sql = $"SELECT password FROM users WHERE email = '{email}'";
            var result = "";
            using (var conn = new NpgsqlConnection(cs))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    result = (string)reader["password"];
                };
            };
            return result;
        }
    }
}

