﻿using BancoChuSA.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BancoChuSA.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}