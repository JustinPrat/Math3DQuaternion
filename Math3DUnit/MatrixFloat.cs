namespace Maths_Matrices.Tests
{
    public class MatrixFloat
    {
        float[,] matrix;
        private float determinant;

        public int NbLines { get; private set; }
        public int NbColumns { get; private set; }
        
        public MatrixFloat(float[,] matrixParam)
        {
            matrix = matrixParam;
            NbLines = matrixParam.GetLength(0);
            NbColumns = matrixParam.GetLength(1);
        }
        
        public MatrixFloat(int nbLines, int nbColumns)
        {
            NbLines = nbLines;
            NbColumns = nbColumns;
            matrix = new float[NbLines, NbColumns];
        }
        
        public MatrixFloat Multiply(MatrixFloat matrix2)
        {
            if (NbColumns != matrix2.NbLines)
            {
                throw new MatrixMultiplyException();
            }
            
            MatrixFloat mat = new MatrixFloat(NbLines, matrix2.NbColumns);
            for (int i = 0; i < mat.NbLines; i++)
            {
                for (int j = 0; j < mat.NbColumns; j++)
                {
                    float addition = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        addition += matrix[i, k] * matrix2[k, j];
                    }

                    mat[i, j] = addition;
                }
            }

            return mat;
        }
        
        public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat matrix, MatrixFloat matrix2)
        {
            MatrixFloat mat = new MatrixFloat(matrix.NbLines, matrix.NbColumns + matrix2.NbColumns);
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                for (int j = 0; j < matrix.NbLines; j++)
                {
                    mat[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < matrix2.NbLines; i++)
            {
                for (int j = 0; j < matrix2.NbColumns; j++)
                {
                    mat[i, matrix.NbColumns + j] = matrix2[i, j];
                }
            }

            return mat;
        }

        public MatrixFloat SubMatrix(float line, float column)
        {
            int indexX = 0, indexY = 0;
            MatrixFloat matrixDivided = new MatrixFloat(NbLines-1, NbColumns-1);
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i != line && j != column)
                    {
                        matrixDivided[indexX, indexY] = matrix[i, j];
                        indexY++;
                        if (indexY >= matrixDivided.NbColumns)
                        {
                            indexY = 0;
                            indexX++;
                        }
                    }
                }
            }

            return matrixDivided;
        }

        public float Determinant()
        {
            if (NbColumns <= 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                for (int i = 0; i < NbColumns; i++)
                {
                    if (i % 2 != 0)
                    {
                        determinant += -matrix[0, i] * Determinant(SubMatrix(0, i));
                    }
                    else
                    {
                        determinant += matrix[0, i] * Determinant(SubMatrix(0, i));
                    }
                }
            }
            
            return determinant;
        }

        public MatrixFloat Adjugate()
        {
            MatrixFloat matrixAdjugated = new MatrixFloat(NbLines, NbColumns);

            if (NbLines <= 2 && NbColumns <= 2)
            {
                matrixAdjugated[0, 0] = matrix[1, 1];
                matrixAdjugated[1, 1] = matrix[0, 0];
                matrixAdjugated[0, 1] = matrix[0, 1] * -1;
                matrixAdjugated[1, 0] = matrix[1, 0] * -1;
            }
            else
            {
                Transpose();
                for (int i = 0; i < NbLines; i++)
                {
                    int lineSign = i % 2 == 0 ? 1 : -1;
                    for (int j = 0; j < NbColumns; j++)
                    {
                        int columnSign = j % 2 == 0 ? 1 : -1;
                        columnSign *= lineSign;
                        
                        matrixAdjugated[i, j] = SubMatrix(i, j).Determinant() * columnSign;
                    }
                }
            }
            return matrixAdjugated;
        }
        
        public static MatrixFloat operator *(MatrixFloat _matrix, MatrixFloat _matrix2)
        {
            return _matrix.Multiply(_matrix2);
        }

        public void Transpose()
        {
            MatrixFloat matrixTransposed = new MatrixFloat(NbLines, NbColumns);
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrixTransposed[j, i] = matrix[i, j];
                }
            }

            matrix = matrixTransposed.matrix;
        }
        
        public MatrixFloat InvertByDeterminant()
        {
            MatrixFloat matrixInverted = new MatrixFloat(matrix);
            float determinant = matrixInverted.Determinant();
            
            if (determinant == 0)
            {
                throw new MatrixInvertException();
            }
            
            matrixInverted = matrixInverted.Adjugate();
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    matrixInverted[i, j] = matrixInverted[i, j] / determinant;
                }
            }
            return matrixInverted;
        }

        public static MatrixFloat InvertByDeterminant(MatrixFloat matrix)
        {
            return matrix.InvertByDeterminant();
        }

        public static float Determinant(MatrixFloat matrix)
        {
            return matrix.Determinant();
        }

        public static MatrixFloat SubMatrix(MatrixFloat matrix, float x, float y)
        {
            return matrix.SubMatrix(x, y);
        }

        public static MatrixFloat Adjugate(MatrixFloat matrix)
        {
            return matrix.Adjugate();
        }
        
        public (MatrixFloat, MatrixFloat) Split(int columnSeparatorIndex)
        {
            MatrixFloat mat = new MatrixFloat(NbLines, columnSeparatorIndex + 1);
            MatrixFloat mat2 = new MatrixFloat(NbLines, NbColumns - (columnSeparatorIndex + 1));
            
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

        public static MatrixFloat InvertByRowReduction(MatrixFloat matrixParam)
        {
            MatrixFloat identityMatrix = Identity(matrixParam.NbColumns);
            (MatrixFloat, MatrixFloat) mat = MatrixRowReductionAlgorithm.Apply(matrixParam, identityMatrix);
            return mat.Item2;
        }

        public MatrixFloat InvertByRowReduction()
        {
            MatrixFloat identityMatrix = Identity(NbLines);
            (MatrixFloat, MatrixFloat) mat = MatrixRowReductionAlgorithm.Apply(this, identityMatrix);
            return mat.Item2;
        }
        
        public static MatrixFloat Identity(int nb)
        {
            float[,] matrixBis = new float[nb, nb];
            for (int i = 0; i < nb; i++)
            {
                matrixBis[i , i] = 1;
            }
            
            return new MatrixFloat(matrixBis);
        }

        public static MatrixFloat RotationMatrix_X(float angle)
        {
            angle *= MathF.PI / 180;
            return new MatrixFloat(new float[,]
            {
                { 1, 0,                0,                 0 },
                { 0, MathF.Cos(angle), -MathF.Sin(angle), 0 },
                { 0, MathF.Sin(angle), MathF.Cos(angle),  0 },
                { 0, 0,                0,                 1 }
            });
        }
        
        public static MatrixFloat RotationMatrix_Y(float angle)
        {
            angle *= MathF.PI / 180;
            return new MatrixFloat(new float[,]
            {
                { MathF.Cos(angle), 0, MathF.Sin(angle),  0 },
                { 0,                1, 0,                 0 },
                { -MathF.Sin(angle),0, MathF.Cos(angle),  0 },
                { 0,                0, 0,                 1 }
            });
        }
        
        public static MatrixFloat RotationMatrix_Z(float angle)
        {
            angle *= MathF.PI / 180;
            return new MatrixFloat(new float[,]
            {
                { MathF.Cos(angle), -MathF.Sin(angle),0, 0 },
                { MathF.Sin(angle), MathF.Cos(angle), 0, 0 },
                { 0,                0,                1, 0 },
                { 0,                0,                0, 1 }
            });
        }
        
        public static MatrixFloat GetScaleMatrix(float scaleX, float scaleY, float scaleZ)
        {
            MatrixFloat matrix = MatrixFloat.Identity(4);
            matrix[0, 0] = scaleX;
            matrix[1, 1] = scaleY;
            matrix[2, 2] = scaleZ;
            matrix[3, 3] = 1f;
            return matrix;
        }
        
        public float[,] ToArray2D()
        {
            return matrix;
        }
        
        public float this[int i, int j]
        {
            get { return matrix[i, j]; }
            set => matrix[i, j] = value;
        }

    }
}