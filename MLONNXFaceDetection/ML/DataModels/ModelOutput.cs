using Microsoft.ML.Data;
using System.Diagnostics.CodeAnalysis;

namespace MLONNXFaceDetection.ML.DataModels
{
    public class ModelOutput
    {
        [VectorType(1, 5, 8400)]
        [ColumnName("output0")]
        [AllowNull]
        public float[] Output { get; set; }
    }
}
