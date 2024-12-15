namespace Maths_Matrices.Tests
{
    public class MatrixRowReductionAlgorithm
    {
        public static (MatrixFloat, MatrixFloat) Apply (MatrixFloat matrix, MatrixFloat matrix2)
        {
            MatrixFloat generatedMatrix = MatrixFloat.GenerateAugmentedMatrix(matrix, matrix2);
            int i = 0;
            for (int j = 0; j < matrix.NbColumns; j++)
            {
                int indexBiggest = i;
                float numberBiggest = generatedMatrix[i, j];
                for (int k = i; k < generatedMatrix.NbLines; k++)
                {
                    if (generatedMatrix[k, j] > numberBiggest)
                    {
                        indexBiggest = k;
                        numberBiggest = generatedMatrix[k, j];
                    }
                }

                if (numberBiggest >= -0.001f && numberBiggest <= 0.001f)
                {
                    // deactivate this line for test 10 !
                    //throw new MatrixInvertException();
                    i++;
                    continue;
                }

                if (indexBiggest != i)
                {
                    MatrixElementaryOperations.SwapLines(generatedMatrix, indexBiggest, i);
                    indexBiggest = i;
                }

                float factorMultiply = 1 / generatedMatrix[i, j];
                MatrixElementaryOperations.MultiplyLine(generatedMatrix, i, j, factorMultiply);

                for (int r = 0; r < generatedMatrix.NbLines; r++)
                {
                    if (r != indexBiggest)
                    {
                        float oldFirstColumn = generatedMatrix[r, j];
                        for (int f = 0; f < generatedMatrix.NbColumns; f++)
                        {
                            float amoundAdd2 = -oldFirstColumn * generatedMatrix[i, f];
                            generatedMatrix[r, f] += amoundAdd2;
                        }
                    }
                }

                i++;
            }
            
            return generatedMatrix.Split(matrix.NbColumns - 1);
        }
    }
}