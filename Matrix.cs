
using System;


namespace MyMath
{
    public class Matrix
    {
        private double[,] _data;
        private int _rows;
        private int _columns;

        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Matrix dimensions must be positive");

            this._rows = rows;
            this._columns = columns;
            _data = new double[rows, columns];
        }

        public Matrix(double[,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            _rows = array.GetLength(0);
            _columns = array.GetLength(1);
            _data = (double[,])array.Clone();
        }

        public double this[int i, int j]
        {
            get
            {
                ValidateIndices(i, j);
                return _data[i, j];
            }
            set
            {
                ValidateIndices(i, j);
                _data[i, j] = value;
            }
        }

        public int Rows => _rows;
        public int Columns => _columns;

        private void ValidateIndices(int i, int j)
        {
            if (i < 0 || i >= _rows)
                throw new ArgumentOutOfRangeException(nameof(i));
            if (j < 0 || j >= _columns)
                throw new ArgumentOutOfRangeException(nameof(j));
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a._rows != b._rows || a._columns != b._columns)
                throw new ArgumentException("Matrices must have the same dimensions");

            Matrix result = new Matrix(a._rows, a._columns);
            for (int i = 0; i < a._rows; i++)
                for (int j = 0; j < a._columns; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a._rows != b._rows || a._columns != b._columns)
                throw new ArgumentException("Matrices must have the same dimensions");

            Matrix result = new Matrix(a._rows, a._columns);
            for (int i = 0; i < a._rows; i++)
                for (int j = 0; j < a._columns; j++)
                    result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a._columns != b._rows)
                throw new ArgumentException("Number of columns in first matrix must equal number of rows in second matrix");

            Matrix result = new Matrix(a._rows, b._columns);
            for (int i = 0; i < a._rows; i++)
                for (int j = 0; j < b._columns; j++)
                    for (int k = 0; k < a._columns; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public static Matrix operator *(Matrix a, double scalar)
        {
            Matrix result = new Matrix(a._rows, a._columns);
            for (int i = 0; i < a._rows; i++)
                for (int j = 0; j < a._columns; j++)
                    result[i, j] = a[i, j] * scalar;

            return result;
        }

        public Matrix ToDiagonal()
        {
            Matrix result = new Matrix(_rows, _columns);
            int minDimension = Math.Min(_rows, _columns);
            
            for (int i = 0; i < minDimension; i++)
            {
                result[i, i] = _data[i, i];
            }

            return result;
        }
        
        public Matrix Transpose()
        {
            Matrix result = new Matrix(_columns, _rows);
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _columns; j++)
                    result[j, i] = _data[i, j];

            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    result += _data[i, j].ToString("F2").PadLeft(8);
                }
                result += Environment.NewLine;
            }
            return result;
        }
    }
}
