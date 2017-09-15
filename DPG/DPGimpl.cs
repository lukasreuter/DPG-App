/**
 * Ported from dpg.java by Lukas Reuter
 * Copyright Lukas Reuter 2017
 **/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Xamarin.Forms;

public class DPGimpl
{
    bool debug;
    bool phrase;
    bool big = true;

    string human = "";
    string result = "";

    int clrPos;

    const int ITERATIONS = 32768;
    const int KEY_LENGTH_IN_BYTES = 64;

    readonly char[] hex_array = { '0', '1', '2', '3', '4', '5', '6', '7',
                                  '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
    // This is a FIFO data structure similar to std::queue in C++
    readonly Queue<int> _numberQueue = new Queue<int>();
    StringBuilder _readableSB = new StringBuilder();

    /**
    * Method to make passwords easy for humans to read
    **/
    string MakeReadablePassword(string password)
    {
        int x = 0;
        _readableSB.Clear();
        while (x < password.Length)
        {
            _readableSB.Append(password.Substring(x, 3));
            _readableSB.Append(" ");
            x += 3;
        }
        return _readableSB.ToString();
    }

    /**
    * A method to get 64 random bytes deterministically based on user input
    * And to also fill the queue with numbers
    **/
    void FillBytes(string sentence, string word)
    {
        byte[] salt = Encoding.UTF8.GetBytes(word);
        string password = sentence;
        // 512 bit key_length (64 bytes). Python and C++ use bytes rather than bits.
        //TODO: replace with custom impl
        byte[] random_bytes = KeyDerivation.Pbkdf2(password, salt,
                                             KeyDerivationPrf.HMACSHA512,
                                             ITERATIONS, KEY_LENGTH_IN_BYTES);

        foreach (var b in random_bytes)
        {
            // & 0xFF ensures the int is unsigned
            _numberQueue.Enqueue(b & 0xFF);
        }
    }

    // NOTE: The iteration count should
    // be as high as possible without causing
    // unreasonable delay.  Note also that the password
    // and salt are byte arrays, not strings.  After use,
    // the password and salt should be cleared (with Array.Clear)
    public static byte[] PBKDF2Sha256GetBytes(int dklen, byte[] password, byte[] salt, int iterationCount)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA256(password))
        {
            int hashLength = hmac.HashSize / 8;
            if ((hmac.HashSize & 7) != 0)
                hashLength++;
            int keyLength = dklen / hashLength;
            if ((long)dklen > (0xFFFFFFFFL * hashLength) || dklen < 0)
                throw new ArgumentOutOfRangeException("dklen");
            if (dklen % hashLength != 0)
                keyLength++;
            byte[] extendedkey = new byte[salt.Length + 4];
            Buffer.BlockCopy(salt, 0, extendedkey, 0, salt.Length);
            using (var ms = new System.IO.MemoryStream())
            {
                for (int i = 0; i < keyLength; i++)
                {
                    extendedkey[salt.Length] = (byte)(((i + 1) >> 24) & 0xFF);
                    extendedkey[salt.Length + 1] = (byte)(((i + 1) >> 16) & 0xFF);
                    extendedkey[salt.Length + 2] = (byte)(((i + 1) >> 8) & 0xFF);
                    extendedkey[salt.Length + 3] = (byte)(((i + 1)) & 0xFF);
                    byte[] u = hmac.ComputeHash(extendedkey);
                    Array.Clear(extendedkey, salt.Length, 4);
                    byte[] f = u;
                    for (int j = 1; j < iterationCount; j++)
                    {
                        u = hmac.ComputeHash(u);
                        for (int k = 0; k < f.Length; k++)
                        {
                            f[k] ^= u[k];
                        }
                    }
                    ms.Write(f, 0, f.Length);
                    Array.Clear(u, 0, u.Length);
                    Array.Clear(f, 0, f.Length);
                }
                byte[] dk = new byte[dklen];
                ms.Position = 0;
                ms.Read(dk, 0, dklen);
                ms.Position = 0;
                for (long i = 0; i < ms.Length; i++)
                {
                    ms.WriteByte(0);
                }
                Array.Clear(extendedkey, 0, extendedkey.Length);
                return dk;
            }
        }
    }

    /**
     * The GUI
     */
    public DPGimpl()
    {
        var sentence = "";//txtSentence.getPassword();
        var word = "";//txtWord.getPassword();
        result = "";
        _numberQueue.Clear();

        //TODO: change these
        if (sentence.Length == 0)
        {
            Device.BeginInvokeOnMainThread(async () => 
                    await Application.Current.MainPage.DisplayAlert("Ups :(", "The sentence is empty.", "OK"));
            return;
        }
        if (word.Length == 0)
        {
			Device.BeginInvokeOnMainThread(async () =>
		            await Application.Current.MainPage.DisplayAlert("Ups :(", "The word is empty.", "OK"));
            return;
        }

        //try
        //{
            FillBytes(sentence, word);

            if (phrase)
            {
                var words = Jars.Words();
                for (var i = 0; i < 7; ++i)
                {
                    result += words[_numberQueue.Dequeue()];
                }
                result += Jars.Special()[_numberQueue.Dequeue()];
                result += Jars.Upper()[_numberQueue.Dequeue()];
                result += Jars.Numbers()[_numberQueue.Dequeue()];
            }
            else
            {
                var iter = big ? (24 - 3) : (12 - 3);

                var lower = Jars.Lower();
                while (result.Length < iter)
                {
                    result += lower[_numberQueue.Dequeue()];
                }
                result += Jars.Special()[_numberQueue.Dequeue()];
                result += Jars.Upper()[_numberQueue.Dequeue()];
                result += Jars.Numbers()[_numberQueue.Dequeue()];
            }
        //}
        /*catch (NoSuchAlgorithmException exc)
        {
        }
        catch (InvalidKeySpecException exc)
        {
        }*/

        //TODO: copy to clipboard here
    }
}
