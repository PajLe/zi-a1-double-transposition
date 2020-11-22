using System;
using System.Collections.Generic;
using System.Text;

namespace zadaci_2
{
    public static class PlayfairCrypto
    {
        private static readonly List<char> _alphabetWithoutLetterJ = new List<char>
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static string Encrypt(string plaintext, string key)
        {
            char[,] keyAlphabet = FormKeyAlphabet(key);
            Dictionary<char, int[]> charCoords = FormCharCoordinatesDictionary(keyAlphabet);

            StringBuilder cipherText = new StringBuilder(plaintext.Length);
            char? first = null, second = null;
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (_alphabetWithoutLetterJ.Contains(char.ToLower(plaintext[i])))
                {
                    char charToTake = (plaintext[i] == 'j')
                        ? 'i'
                        : (plaintext[i] == 'J')
                            ? 'I'
                            : plaintext[i];

                    if (first == null)
                        first = charToTake;
                    else if (second == null)
                        second = charToTake;

                    if (first.HasValue && second.HasValue)
                    {
                        AppendEncryptedValues(first.Value, second.Value, charCoords, keyAlphabet, cipherText);
                        first = null;
                        second = null;
                    }
                }
                else
                {
                    if (first != null)
                    {
                        second ??= 'z';
                        AppendEncryptedValues(first.Value, second.Value, charCoords, keyAlphabet, cipherText);
                        first = null;
                        second = null;
                    }
                    cipherText.Append(plaintext[i]);
                }

            }

            if (first != null)
            {
                second ??= 'z';
                AppendEncryptedValues(first.Value, second.Value, charCoords, keyAlphabet, cipherText);
            }
            return cipherText.ToString();
        }

        private static void AppendEncryptedValues(char first, char second, Dictionary<char, int[]> charCoords, char[,] keyAlphabet, StringBuilder cipherText)
        {
            int[] firstCoords = (int[])charCoords[char.ToLower(first)].Clone();
            int[] secondCoords = (int[])charCoords[char.ToLower(second)].Clone();
            ProcessEncryptCoordinates(ref firstCoords, ref secondCoords, keyAlphabet);
            if (char.IsUpper(first))
                cipherText.Append(char.ToUpper(keyAlphabet[firstCoords[0], firstCoords[1]]));
            else
                cipherText.Append(keyAlphabet[firstCoords[0], firstCoords[1]]);

            if (char.IsUpper(second))
                cipherText.Append(char.ToUpper(keyAlphabet[secondCoords[0], secondCoords[1]]));
            else
                cipherText.Append(keyAlphabet[secondCoords[0], secondCoords[1]]);
        }

        private static void ProcessEncryptCoordinates(ref int[] firstCoords, ref int[] secondCoords, char[,] keyAlphabet)
        {
            int firstRow = firstCoords[0],
                firstColumn = firstCoords[1];
            int secondRow = secondCoords[0],
                secondColumn = secondCoords[1];

            if (firstColumn == secondColumn) // If both the letters are in the same column
            {
                firstRow = (firstRow + 1) % keyAlphabet.GetLength(0);
                secondRow = (secondRow + 1) % keyAlphabet.GetLength(0);
            }
            else if (firstRow == secondRow) // If both the letters are in the same row
            {
                firstColumn = (firstColumn + 1) % keyAlphabet.GetLength(1);
                secondColumn = (secondColumn + 1) % keyAlphabet.GetLength(1);
            }
            else // If neither of the above rules is true
            {
                int p = firstColumn;
                firstColumn = secondColumn;
                secondColumn = p;
            }

            firstCoords[0] = firstRow;
            firstCoords[1] = firstColumn;
            secondCoords[0] = secondRow;
            secondCoords[1] = secondColumn;
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

        private static Dictionary<char, int[]> FormCharCoordinatesDictionary(char[,] keyAlphabet)
        {
            Dictionary<char, int[]> charCoords = new Dictionary<char, int[]>();

            for (int i = 0; i < keyAlphabet.GetLength(0); i++)
            {
                for (int j = 0; j < keyAlphabet.GetLength(1); j++)
                {
                    int[] coords = new int[] { i, j };
                    charCoords.Add(keyAlphabet[i, j], coords);
                }
            }

            return charCoords;
        }

        public static string Decrypt(string cipherText, string key)
        {
            char[,] keyAlphabet = FormKeyAlphabet(key);
            Dictionary<char, int[]> charCoords = FormCharCoordinatesDictionary(keyAlphabet);

            StringBuilder plaintext = new StringBuilder(cipherText.Length);
            char? first = null, second = null;
            for (int i = 0; i < cipherText.Length; i++)
            {
                char lowerChar = char.ToLower(cipherText[i]);
                if (_alphabetWithoutLetterJ.Contains(lowerChar))
                {
                    char charToTake = cipherText[i];
                    if (first == null)
                        first = charToTake;
                    else if (second == null)
                        second = charToTake;

                    if (first.HasValue && second.HasValue)
                    {
                        AppendDecryptedValues(first.Value, second.Value, charCoords, keyAlphabet, plaintext);
                        first = null;
                        second = null;
                    }
                }
                else
                {
                    if ((plaintext.Length - 1).IsInArrayRange(plaintext.Length)
                        && plaintext[plaintext.Length - 1] == 'z')
                        plaintext.Remove(plaintext.Length - 1, 1);
                    plaintext.Append(cipherText[i]);
                }

            }

            if ((plaintext.Length - 1).IsInArrayRange(plaintext.Length)
                && plaintext[plaintext.Length - 1] == 'z')
                plaintext.Remove(plaintext.Length - 1, 1);
            return plaintext.ToString();
        }

        private static void AppendDecryptedValues(char first, char second, Dictionary<char, int[]> charCoords, char[,] keyAlphabet, StringBuilder plaintext)
        {
            int[] firstCoords = (int[])charCoords[char.ToLower(first)].Clone();
            int[] secondCoords = (int[])charCoords[char.ToLower(second)].Clone();
            ProcessDecryptCoordinates(ref firstCoords, ref secondCoords, keyAlphabet);
            if (char.IsUpper(first))
                plaintext.Append(char.ToUpper(keyAlphabet[firstCoords[0], firstCoords[1]]));
            else
                plaintext.Append(keyAlphabet[firstCoords[0], firstCoords[1]]);

            if (char.IsUpper(second))
                plaintext.Append(char.ToUpper(keyAlphabet[secondCoords[0], secondCoords[1]]));
            else
                plaintext.Append(keyAlphabet[secondCoords[0], secondCoords[1]]);
        }

        private static void ProcessDecryptCoordinates(ref int[] firstCoords, ref int[] secondCoords, char[,] keyAlphabet)
        {
            int firstRow = firstCoords[0],
                firstColumn = firstCoords[1];
            int secondRow = secondCoords[0],
                secondColumn = secondCoords[1];

            if (firstColumn == secondColumn) // If both the letters are in the same column
            {
                firstRow = (firstRow - 1).Mod(keyAlphabet.GetLength(0));
                secondRow = (secondRow - 1).Mod(keyAlphabet.GetLength(0));
            }
            else if (firstRow == secondRow) // If both the letters are in the same row
            {
                firstColumn = (firstColumn - 1).Mod(keyAlphabet.GetLength(1));
                secondColumn = (secondColumn - 1).Mod(keyAlphabet.GetLength(1));
            }
            else // If neither of the above rules is true
            {
                int p = firstColumn;
                firstColumn = secondColumn;
                secondColumn = p;
            }

            firstCoords[0] = firstRow;
            firstCoords[1] = firstColumn;
            secondCoords[0] = secondRow;
            secondCoords[1] = secondColumn;
        }
    }
}
