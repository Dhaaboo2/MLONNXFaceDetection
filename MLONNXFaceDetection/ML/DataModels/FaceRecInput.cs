using Microsoft.ML.Data;
using System.Diagnostics.CodeAnalysis;
namespace MLONNXFaceDetection.ML.DataModels
{
    public class FaceRecInput
    {
        [VectorType(3, 112, 112)]
        [ColumnName("data")]
        [AllowNull]
        public float[] ImageData { get; set; }
    }
}
