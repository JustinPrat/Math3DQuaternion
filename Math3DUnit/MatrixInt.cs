namespace Maths_Matrices.Tests
{
    public class MatrixInt
    {
        int[,] matrix;

        public int NbLines { get; private set; }
        public int NbColumns { get; private set; }

        public MatrixInt(int nbLines, int nbColumns)
        {
            NbLines = nbLines;
            NbColumns = nbColumns;
            matrix = new int[NbLines, NbColumns];
        }

        public MatrixInt(int[,] matrixParam)
        {
            matrix = matrixParam;
            NbLines = matrixParam.GetLength(0);
            NbColumns = matrixParam.GetLength(1);
        }

        public MatrixInt(MatrixInt matrixParam)
        {
            matrix = new int[matrixParam.NbLines, matrixParam.NbColumns];
            NbLines = matrixParam.NbLines;
            NbColumns = matrixParam.NbColumns;
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrix[i, j] = matrixParam[i, j];
                }
            }
        }

        public MatrixInt Multiply(MatrixInt matrix2)
        {
            if (NbColumns != matrix2.NbLines)
            {
                throw new MatrixMultiplyException();
            }
            
            MatrixInt mat = new MatrixInt(NbLines, matrix2.NbColumns);
            for (int i = 0; i < mat.NbLines; i++)
            {
                for (int j = 0; j < mat.NbColumns; j++)
                {
                    int addition = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        addition += matrix[i, k] * matrix2[k, j];
                    }

                    mat[i, j] = addition;
                }
            }

            return mat;
        }

        public (MatrixInt, MatrixInt) Split(int columnSeparatorIndex)
        {
            MatrixInt mat = new MatrixInt(NbLines, columnSeparatorIndex + 1);
            MatrixInt mat2 = new MatrixInt(NbLines, NbColumns - (columnSeparatorIndex + 1));
            
            for (int i = 0; i < mat.NbColumns; i++)
            {
                for (int j = 0; j < mat.NbLines; j++)
                {
                    mat[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < mat2.NbColumns; i++)
            {
                for (int j = 0; j < mat2.NbLines; j++)
                {
                    mat2[j, i] = matrix[j, i + columnSeparatorIndex + 1];
                }
            }
            
            return (mat, mat2);
        }

        public static MatrixInt GenerateAugmentedMatrix(MatrixInt matrix, MatrixInt matrix2)
        {
            MatrixInt mat = new MatrixInt(matrix.NbLines, matrix.NbColumns + 1);
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                for (int j = 0; j < matrix.NbLines; j++)
                {
                    mat[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < matrix2.NbLines; i++)
            {
                mat[i, matrix.NbColumns] = matrix2[i, 0];
            }

            return mat;
        }

        public MatrixInt Transpose()
        {
            MatrixInt mat = new MatrixInt(NbColumns, NbLines);
            for (int i = 0; i < NbColumns; i++)
            {
                for (int j = 0; j < NbLines; j++)
                {
                    mat[i, j] = matrix[j, i];
                }
            }

            return mat;
        }
        
        public static MatrixInt Transpose(MatrixInt matrixParam1)
        {
            MatrixInt mat = new MatrixInt(matrixParam1);
            return mat.Transpose();
        }

        public void Add(MatrixInt matrixParam)
        {
            if (matrixParam.NbLines != NbLines)
            {
                throw new MatrixSumException();
            }
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrix[i, j] += matrixParam[i, j];
                }
            }
        }
        
        public static MatrixInt Multiply(MatrixInt matrixParam1, MatrixInt matrixParam2)
        {
            MatrixInt mat = new MatrixInt(matrixParam1);
            mat = mat.Multiply(matrixParam2);
            return mat;
        }

        public static MatrixInt Add(MatrixInt matrixParam1, MatrixInt matrixParam2)
        {
            MatrixInt mat = new MatrixInt(matrixParam1);
            mat.Add(matrixParam2);
            return mat;
        }
        
        public void Minus(MatrixInt matrixParam)
        {
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrix[i, j] -= matrixParam[i, j];
                }
            }
        }
        
        public static MatrixInt operator +(MatrixInt _matrix, MatrixInt _matrix2)
        {
            return MatrixInt.Add(_matrix, _matrix2);
        }

        public static MatrixInt operator *(MatrixInt _matrix, int factor)
        {
            return MatrixInt.Multiply(_matrix, factor);
        }
        
        public static MatrixInt operator *(MatrixInt _matrix, MatrixInt _matrix2)
        {
            return MatrixInt.Multiply(_matrix, _matrix2);
        }
        
        public static MatrixInt operator *(int factor, MatrixInt _matrix)
        {
            return MatrixInt.Multiply(_matrix, factor);
        }

        public static MatrixInt operator -(MatrixInt _matrix, MatrixInt _matrix2)
        {
            return MatrixInt.Minus(_matrix, _matrix2);
        }

        public static MatrixInt operator -(MatrixInt _matrix)
        {
           return MatrixInt.Multiply(_matrix, -1);
        }

        public static MatrixInt Multiply(MatrixInt _matrix, int factor)
        {
            MatrixInt mat = new MatrixInt(_matrix);
            mat.Multiply(factor);
            return mat;
        }
        
        public static MatrixInt Minus(MatrixInt _matrix, MatrixInt _matrix2)
        {
            MatrixInt mat = new MatrixInt(_matrix);
            mat.Minus(_matrix2);
            return mat;
        }

        public void Multiply(int factor)
        {
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrix[i, j] *= factor;
                }
            }
        }

        public bool IsIdentity()
        {
            if (NbLines != NbColumns)
            {
                return false;
            }
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i == j && matrix[i, j] != 1)
                    {
                        return false;
                    }
                    
                    if (i != j && matrix[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        public static MatrixInt Identity(int nb)
        {
            int[,] matrixBis = new int[nb, nb];
            for (int i = 0; i < nb; i++)
            {
                matrixBis[i , i] = 1;
            }
            
            return new MatrixInt(matrixBis);
        }


        public int[,] ToArray2D()
        {
            return matrix;
        }

        public int this[int i, int j]
        {
            get { return matrix[i, j]; }
            set => matrix[i, j] = value;
        }
    }
}