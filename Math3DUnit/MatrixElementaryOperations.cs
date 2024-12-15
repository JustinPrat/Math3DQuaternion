namespace Maths_Matrices.Tests
{
    public class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt matrix, int lineA, int lineB)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                int temp = matrix[lineA, i];
                matrix[lineA, i] = matrix[lineB, i];
                matrix[lineB, i] = temp;
            }
        }
        
        public static void SwapLines(MatrixFloat matrix, int lineA, int lineB)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                float temp = matrix[lineA, i];
                matrix[lineA, i] = matrix[lineB, i];
                matrix[lineB, i] = temp;
            }
        }

        public static void SwapColumns(MatrixInt matrix, int columnA, int columnB)
        {
            for (int i = 0; i < matrix.NbLines; i++)
            {
                int temp = matrix[i, columnA];
                matrix[i,columnA] = matrix[i, columnB];
                matrix[i, columnB] = temp;
            }
        }

        public static void MultiplyLine(MatrixInt matrix, int line, int factor)
        {
            if (factor == 0)
            {
                throw new MatrixScalarZeroException();
            }
            
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                matrix[line, i] *= factor;
            }
        }
        
        public static void MultiplyLine(MatrixFloat matrix, int line, int columnStart, float factor)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                if (i >= columnStart)
                {
                    matrix[line, i] *= factor;
                }
            }
        }
        
        public static void AddToLine(MatrixFloat matrix, int line, float addAmount)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                matrix[line, i] += addAmount;
            }
        }
        
        public static void MultiplyColumn(MatrixInt matrix, int Column, int factor)
        {
            if (factor == 0)
            {
                throw new MatrixScalarZeroException();
            }
            
            for (int i = 0; i < matrix.NbLines; i++)
            {
                matrix[i, Column] *= factor;
            }
        }

        public static void AddLineToAnother(MatrixInt matrix, int lineA, int lineB, int factor)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                matrix[lineB, i] += matrix[lineA, i] * factor;
            }
        }
        
        public static void AddColumnToAnother(MatrixInt matrix, int columnA, int columnB, int factor)
        {
            for (int i = 0; i < matrix.NbLines; i++)
            {
                matrix[i, columnB] += matrix[i, columnA] * factor;
            }
        }
    }
}