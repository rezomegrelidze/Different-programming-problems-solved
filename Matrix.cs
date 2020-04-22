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

        private double Determinant(Matrix M)
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
                    var minor = GetMinorMatrix(M,col);
                    sum += Math.Pow(sign, col) * M[0, col] * Determinant(minor);
                }

                return sum;
            }
        }

        public double Det => Determinant(this);

        private Matrix GetMinorMatrix(Matrix M,int col)
        {
            int rows = M.RowCount;
            int cols = M.ColumnCount;
            var minor = new Matrix(rows-1,cols-1);
                                          // u - row index for minor
                                          // u - column index for minor
            for (int i = 1,u = 0; i < rows; i++,u++) // first row is not relevant
            {
                for (int j = 0,k = 0; j < cols; j++)
                {
                    if(j == col) continue;     // ignore the column that is not included in minor matrix

                    minor[u, k] = M[i, j];
                    k++;                       // only increase the column index if j != col
                }                        
            }

            return minor;
        }

        public double this[int row, int column]
        {
            get => matrix[row, column];
            set => matrix[row, column] = value;
        }
    }
