using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomID
{
    private static char[] base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

    public static string GetBase62(int length)
    {
        char[] output = new char[length]; 

        for (int i = 0; i < length; i++)
        {
            output[i] = base62Characters[Random.Range(0, base62Characters.Length)];
        }

        return new string(output);
    }

}
