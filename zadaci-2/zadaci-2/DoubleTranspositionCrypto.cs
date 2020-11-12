using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zadaci_2
{
    public static class DoubleTranspositionCrypto
    {
        private const int _cols = 9;
        private const int _rows = 7;

        public static byte[] Encrypt(byte[] bytes, int[] rowKeys, int[] columnKeys)
        {
            if (!ValidateRowKeys(rowKeys))
                throw new ArgumentException("Row keys should all be different and in range [0-6]. Number of keys should be 7.");
            if (!ValidateColumnKeys(columnKeys))
                throw new ArgumentException("Column keys should all be different and in range [0-8]. Number of keys should be 9.");

            byte[] cipherBytes = (byte[])bytes.Clone();

            //rows 
            for (int c = 0; c < bytes.Length; c++)
            {
                int chunk = c / (_cols * _rows);
                int chunkStartIndex = chunk * _rows * _cols;

                int currentChunkRow = (c - chunkStartIndex) / _cols;
                int chunkRowToSwitchWith = rowKeys[currentChunkRow];
                if (currentChunkRow == chunkRowToSwitchWith) // optimizacija da ne bismo menjali isti red sa samim sobom
                {
                    c += _cols; // pomerimo se za broj kolona da bismo presli u sledeci red
                    c--; // da bi se neutralisao sledeci inkrement u for petlji
                    continue;
                }
                int charToSwitchWith = chunkStartIndex + chunkRowToSwitchWith * _cols + c % _cols;

                if (charToSwitchWith.IsInArrayRange(bytes.Length))
                    cipherBytes[c] = bytes[charToSwitchWith];
            }

            bytes = (byte[])cipherBytes.Clone();
            //cols
            for (int c = 0; c < bytes.Length; c++)
            {
                int currentChunkColumn = c % _cols;
                int chunkColumnToSwitchWith = columnKeys[currentChunkColumn];

                if (currentChunkColumn == chunkColumnToSwitchWith)
                    continue;

                int charToSwitchWith = c - currentChunkColumn + chunkColumnToSwitchWith;

                if (charToSwitchWith.IsInArrayRange(bytes.Length))
                    cipherBytes[c] = bytes[charToSwitchWith];
            }

            return cipherBytes;
        }

        public static byte[] Decrypt(byte[] cipherBytes, int[] rowKeys, int[] columnKeys)
        {
            if (!ValidateRowKeys(rowKeys))
                throw new ArgumentException("Row keys should all be different and in range [0-6]. Number of keys should be 7.");
            if (!ValidateColumnKeys(columnKeys))
                throw new ArgumentException("Column keys should all be different and in range [0-8]. Number of keys should be 9.");

            byte[] decryptedBytes = (byte[])cipherBytes.Clone();

            //cols
            for (int c = 0; c < cipherBytes.Length; c++)
            {
                int currentChunkColumn = c % _cols;
                int chunkColumnToInsertIn = columnKeys[currentChunkColumn];

                if (currentChunkColumn == chunkColumnToInsertIn)
                    continue;

                int positionToInsertCurrentChar = c - currentChunkColumn + chunkColumnToInsertIn;

                if (positionToInsertCurrentChar.IsInArrayRange(cipherBytes.Length))
                    decryptedBytes[positionToInsertCurrentChar] = cipherBytes[c];
            }
            cipherBytes = (byte[])decryptedBytes.Clone();

            //rows 
            for (int c = 0; c < cipherBytes.Length; c++)
            {
                int chunk = c / (_cols * _rows);
                int chunkStartIndex = chunk * _rows * _cols;

                int currentChunkRow = (c - chunkStartIndex) / _cols;
                int chunkRowToSwitchWith = rowKeys[currentChunkRow];
                if (currentChunkRow == chunkRowToSwitchWith) // optimizacija da ne bismo menjali isti red sa samim sobom
                {
                    c += _cols; // pomerimo se za broj kolona da bismo presli u sledeci red
                    c--; // da bi se neutralisao sledeci inkrement u for petlji
                    continue;
                }
                int positionToInsertCurrentChar = chunkStartIndex + chunkRowToSwitchWith * _cols + c % _cols;

                if (positionToInsertCurrentChar.IsInArrayRange(cipherBytes.Length))
                    decryptedBytes[positionToInsertCurrentChar] = cipherBytes[c];
            }

            return decryptedBytes;
        }

        private static bool ValidateColumnKeys(int[] columnKeys)
        {
            Dictionary<int, int> ColumnKeyAppearanceCount = new Dictionary<int, int>();
            if (columnKeys.Length != _cols)
                return false;
            foreach (int key in columnKeys)
            {
                if (key >= _cols || key < 0)
                    return false;
                try
                {
                    ColumnKeyAppearanceCount.Add(key, 1); // kolone(svaki 'key') u columnKeys moraju biti jedinstvene; svaki key dodajemo u dict, ako tamo vec postoji bacice se exception sto znaci da key nije jedinstven
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateRowKeys(int[] rowKeys)
        {
            Dictionary<int, int> RowKeyAppearanceCount = new Dictionary<int, int>();
            if (rowKeys.Length != _rows)
                return false;
            foreach (int key in rowKeys)
            {
                if (key >= _rows || key < 0)
                    return false;

                try
                {
                    RowKeyAppearanceCount.Add(key, 1); // redovi(svaki 'key') u rowKeys moraju biti jedinstveni; svaki key dodajemo u dict, ako tamo vec postoji bacice se exception sto znaci da key nije jedinstven
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
