using System;
using System.Collections.Generic;
using System.Text;

namespace zadaci_2
{
    public static class PlayfairCrypto
    {
        private static readonly char[] _alphabetWithoutLetterJ =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static void Encrypt(string plaintext, string key)
        {
            char[,] keyAlphabet = FormKeyAlphabet(key);

        }

        private static char[,] FormKeyAlphabet(string key)
        {
            char[,] keyAlphabet = new char[5, 5];
            HashSet<char> alphabetKeysUsed = new HashSet<char>();
            int i = 0, j = 0;
            foreach (char glyph in key)
            {
                char glyphToLower = char.ToLower(glyph);
                if (!alphabetKeysUsed.Contains(glyphToLower))
                {
                    keyAlphabet[i, j] = glyphToLower;
                    alphabetKeysUsed.Add(glyphToLower);
                    j++;
                    if (j == 5)
                    {
                        i++;
                        j = 0;
                    }
                    if (i == 5)
                        break;
                }
            }
            foreach (char glyph in _alphabetWithoutLetterJ)
            {
                if (!alphabetKeysUsed.Contains(glyph))
                {
                    keyAlphabet[i, j] = glyph;
                    alphabetKeysUsed.Add(glyph);
                    j++;
                    if (j == 5)
                    {
                        i++;
                        j = 0;
                    }
                    if (i == 5)
                        break;
                }
            }

            return keyAlphabet;
        }

    }
}
