using Microsoft.ML.Data;
using System.Diagnostics.CodeAnalysis;

namespace MLONNXFaceDetection.ML.DataModels
{
    public class FaceRecOutput
    {
        [VectorType(512)]
        [ColumnName("fc1")]
        [AllowNull]
        public float[] Embedding { get; set; }
    }
}
