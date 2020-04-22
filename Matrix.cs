public class Matrix
    {
        public int RowCount { get; }
        public int ColumnCount { get; }
        private readonly double[,] matrix;

        public Matrix(int rows,int columns)
        {
            RowCount = rows;
            ColumnCount = columns;
            matrix = new double[RowCount,ColumnCount];
        }

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
            RowCount = matrix.GetLength(0);
            ColumnCount = matrix.GetLength(1);
        }

        public static double Determinant(Matrix M)
        {
            if      (M.RowCount != M.ColumnCount) throw new InvalidOperationException("Matrix must be NxN");
            else if (M.RowCount == 2 && M.ColumnCount == 2)
            {       
                // matrix 2x2 return ad-bc
                return M[0, 0] * M[1, 1] - M[0, 1] * M[1, 0];           
            }
            else
            {
                double sum = 0;
                double sign = -1;
                for (int col = 0; col < M.ColumnCount; col++)
                {
                    var minor = GetMinorMatrix(M,col,0);
                    sum += Math.Pow(sign, col) * M[0, col] * Determinant(minor);
                }

                return sum;
            }
        }

        public double Det => Determinant(this);

        public static Matrix operator /(Matrix a,double value)
        {
            var result = new Matrix(a.RowCount,a.ColumnCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    result[i, j] = a[i, j] / value;
                }
            }

            return result;
        }

        public Matrix GetInverse()
        {
            var matrixOfMinors = GetMatrixOfMinors(this);
            var matrixOfCofactors = GetMatrixOfCofactors(matrixOfMinors);
            var adjugate = Adjugate(matrixOfCofactors);
            return adjugate / Det;
        }

        public static Matrix GetMatrixOfCofactors(Matrix M)
        {
            var cofactorMatrix = new Matrix(M.RowCount,M.ColumnCount);
            int sign = 1;
            for (int i = 0; i < M.RowCount; i++)
            {
                for (int j = 0; j < M.ColumnCount; j++)
                {
                    cofactorMatrix[i, j] = M[i, j] * sign;
                    sign = -sign;
                }
            }

            return cofactorMatrix;
        }

        public static Matrix GetMatrixOfMinors(Matrix M)
        {
            var result = new Matrix(M.RowCount, M.ColumnCount);
            for (int i = 0; i < M.RowCount; i++)
            {
                for (int j = 0; j < M.ColumnCount; j++)
                {
                    var minorMatrix = GetMinorMatrix(M, i, j);
                    result[j,i] = Determinant(minorMatrix);
                }
            }

            return result;
        }

        public static Matrix Adjugate(Matrix M)
        {
            var adjoint = new Matrix(M.RowCount,M.ColumnCount);

            for (int i = 0; i < M.RowCount; i++)
            {
                for (int j = 0; j < M.ColumnCount; j++)
                {
                    adjoint[i, j] = M[j, i];
                }
            }

            return adjoint;
        }

        public static Matrix GetMinorMatrix(Matrix M,int col,int row)
        {   
            int rows = M.RowCount;              
            int cols = M.ColumnCount;
            var minor =  new Matrix(rows-1,cols-1);
                                             // u - row index for minor        
                                          // u - column index for minor 
            for (int i = 0,u = 0; i < rows; i++) // first row is not relevant
            {       
                if(i == row) continue;          
                for (int j = 0,k = 0; j < cols; j++)
                {
                    if(j == col) continue;     // ignore the column that is not included in minor matrix

                    minor[u, k] = M[i, j];      
                    k++;                       // only increase the column index if j != col
                }

                u++;
            }

            return minor;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    builder.Append($"{this[i, j]}\t");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public double this[int row, int column]
        {
            get => matrix[row, column];
            set => matrix[row, column] = value;
        }
    }
