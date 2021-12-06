using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShapesRecognitionTestNetCore
{
    public static class Extensions
    {
        public static T Get<T>(this Mat mat, int row, int col)
        {
            unsafe
            {
                var span = new ReadOnlySpan<T>(mat.DataPointer.ToPointer(), mat.Rows * mat.Cols * mat.ElementSize);
                return span[row * mat.Cols + col];
            }
        }
    }
}
