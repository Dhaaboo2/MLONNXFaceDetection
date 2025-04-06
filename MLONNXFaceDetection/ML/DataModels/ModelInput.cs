using Microsoft.ML.Data;
using System.Diagnostics.CodeAnalysis;

namespace MLONNXFaceDetection.ML.DataModels
{
    public class ModelInput
    {
        [VectorType(3, 640, 640)]
        [ColumnName("images")]
        [AllowNull]
        public float[] ImageData { get; set; }
    }
}
