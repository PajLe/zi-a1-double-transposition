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
                char lowerChar = char.ToLower(plaintext[i]);
                if (!_alphabetWithoutLetterJ.Contains(lowerChar))
                {
                    cipherText.Append(plaintext[i]);
                }
                else
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
                }

                if (i == plaintext.Length - 1 && second == null)
                    second = 'z';
                
                if (first.HasValue && second.HasValue)
                {
                    int[] firstCoords = charCoords[char.ToLower(first.Value)];
                    int[] secondCoords = charCoords[char.ToLower(second.Value)];
                    ProcessCoordinates(ref firstCoords, ref secondCoords, keyAlphabet);
                    if (char.IsUpper(first.Value))
                        cipherText.Append(char.ToUpper(keyAlphabet[firstCoords[0], firstCoords[1]]));
                    else
                        cipherText.Append(keyAlphabet[firstCoords[0], firstCoords[1]]);

                    if (char.IsUpper(second.Value))
                        cipherText.Append(char.ToUpper(keyAlphabet[secondCoords[0], secondCoords[1]]));
                    else
                        cipherText.Append(keyAlphabet[secondCoords[0], secondCoords[1]]);
                    first = null;
                    second = null;
                }
            }

            return cipherText.ToString();
        }

        private static void ProcessCoordinates(ref int[] firstCoords, ref int[] secondCoords, char[,] keyAlphabet)
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


    }
}
