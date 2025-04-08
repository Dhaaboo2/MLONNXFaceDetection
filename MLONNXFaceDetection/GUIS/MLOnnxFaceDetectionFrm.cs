using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using MLONNXFaceDetection.ML.DataModels;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace MLONNXFaceDetection.GUIS
{
    public partial class MLOnnxFaceDetectionFrm : Form
    {
        [AllowNull]
        private OpenFileDialog _odl;
        [AllowNull]
        private InferenceSession _session;
        [AllowNull]
        private MLContext mlContext;
        [AllowNull]
        private ITransformer model;
        [AllowNull]
        private PredictionEngine<ModelInput, ModelOutput> predictor;
        [AllowNull]
        private ITransformer arcFaceModel;
        [AllowNull]
        private PredictionEngine<FaceRecInput, FaceRecOutput> arcFacePredictor;
        private const int ModelInputSize = 640;
        [AllowNull]
        private Bitmap originalImage;
        public MLOnnxFaceDetectionFrm()
        {
            InitializeComponent();

            _odl = new OpenFileDialog { Filter = "Image Files|*.jpg;*.png;*.bmp" };
            LoadMLModel();
            //LoadModel();
        }

        private void LoadModel()
        {
            try
            {
                var _Prodir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../", "ML"));
                var _yolov8x = Path.Combine(_Prodir, "OnnxModels", "yolov5s-face.onnx");
                _session = new InferenceSession(_yolov8x);
                MessageBox.Show("Model Loaded Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private void LoadMLModel()
        {
            try
            {
                var _Prodir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../", "ML"));
                var _yolov8x = Path.Combine(_Prodir, "OnnxModels", "yolov8x-face-lindevs.onnx");
                var _yolov8x2 = Path.Combine(_Prodir, "OnnxModels", "arcfaceresnet100-8.onnx");
                mlContext = new MLContext();

                var pipeline = mlContext.Transforms.ApplyOnnxModel(
                        modelFile: _yolov8x,
                        outputColumnNames: new[] { "output0" },
                        inputColumnNames: new[] { "images" });

                // Dummy data to fit pipeline
                var emptyData = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());
                model = pipeline.Fit(emptyData);
                predictor = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

                // Face Recognition model (ArcFace)
                var recPipeline = mlContext.Transforms
                    .ApplyOnnxModel(modelFile: _yolov8x2, outputColumnNames: new[] { "fc1" }, inputColumnNames: new[] { "data" });
                arcFaceModel = recPipeline.Fit(mlContext.Data.LoadFromEnumerable(new List<FaceRecInput>()));
                arcFacePredictor = mlContext.Model.CreatePredictionEngine<FaceRecInput, FaceRecOutput>(arcFaceModel);
                MessageBox.Show("ML Model Is Loaded Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnbrow_Click(object sender, EventArgs e)
        {

            try
            {
                if (_odl.ShowDialog() == DialogResult.OK)
                {
                    originalImage = new Bitmap(_odl.FileName);
                    PB.Image = originalImage;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btndet_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null || predictor == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Bitmap resized = new Bitmap(originalImage, new Size(ModelInputSize, ModelInputSize));
                float[] inputData = PreprocessImage(resized);

                var prediction = predictor.Predict(new ModelInput { ImageData = inputData });

                var boxes = ParseDetections(prediction.Output, originalImage.Width, originalImage.Height);
                var result = DrawDetections(originalImage, boxes);

                PB.Image = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private float[] PreprocessImage(Bitmap bmp)
        {
            float[] input = new float[3 * ModelInputSize * ModelInputSize];

            for (int y = 0; y < ModelInputSize; y++)
            {
                for (int x = 0; x < ModelInputSize; x++)
                {
                    Color color = bmp.GetPixel(x, y);

                    // CHW layout
                    input[0 * ModelInputSize * ModelInputSize + y * ModelInputSize + x] = color.R / 255f;
                    input[1 * ModelInputSize * ModelInputSize + y * ModelInputSize + x] = color.G / 255f;
                    input[2 * ModelInputSize * ModelInputSize + y * ModelInputSize + x] = color.B / 255f;
                }
            }

            return input;
        }

        private List<RectangleF> ParseDetections(float[] output, int origW, int origH, float confThreshold = 0.5f)
        {
            var boxes = new List<RectangleF>();
            int numDetections = 8400;

            for (int i = 0; i < numDetections; i++)
            {
                float x_center = output[0 * numDetections + i];
                float y_center = output[1 * numDetections + i];
                float width = output[2 * numDetections + i];
                float height = output[3 * numDetections + i];
                float conf = output[4 * numDetections + i];

                if (conf < confThreshold)
                    continue;

                float scaleX = (float)origW / ModelInputSize;
                float scaleY = (float)origH / ModelInputSize;

                float x = (x_center - width / 2) * scaleX;
                float y = (y_center - height / 2) * scaleY;
                float w = width * scaleX;
                float h = height * scaleY;

                boxes.Add(new RectangleF(x, y, w, h));
            }

            return boxes;
        }

        private Bitmap DrawDetections(Bitmap original, List<RectangleF> boxes)
        {
            Bitmap result = new(original);
            using Graphics g = Graphics.FromImage(result);
            using Pen pen = new(Color.Red, 2);
            using Font font = new("Arial", 12);
            using Brush brush = new SolidBrush(Color.Yellow);

            foreach (var box in boxes)
            {
                g.DrawRectangle(pen, box.X, box.Y, box.Width, box.Height);
                g.DrawString("Face", font, brush, box.X, box.Y - 18);
            }

            return result;
        }

        private void btnCom_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null || predictor == null || arcFacePredictor == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Detect faces
                Bitmap resized = new Bitmap(originalImage, new Size(640, 640));
                float[] input = PreprocessImage(resized);
                var prediction = predictor.Predict(new ModelInput { ImageData = input });
                var boxes = ParseDetections(prediction.Output, originalImage.Width, originalImage.Height);

                if (boxes.Count < 2)
                {
                    MessageBox.Show("Please load image with 2 faces to compare.");
                    return;
                }

                // Get face 1
                Bitmap face1 = CropAndResizeFace(originalImage, boxes[0]);
                float[] faceData1 = PreprocessFaceImage(face1);
                float[] emb1 = arcFacePredictor.Predict(new FaceRecInput { ImageData = faceData1 }).Embedding;

                // Get face 2
                Bitmap face2 = CropAndResizeFace(originalImage, boxes[1]);
                float[] faceData2 = PreprocessFaceImage(face2);
                float[] emb2 = arcFacePredictor.Predict(new FaceRecInput { ImageData = faceData2 }).Embedding;

                float similarity = CosineSimilarity(emb1, emb2);

                string result = similarity > 0.5f
                    ? $"Same person ✅ (Similarity: {similarity:F3})"
                    : $"Different people ❌ (Similarity: {similarity:F3})";

                MessageBox.Show(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private Bitmap CropAndResizeFace(Bitmap source, RectangleF faceRect)
        {
            Rectangle rect = Rectangle.Round(faceRect);
            Bitmap cropped = source.Clone(rect, source.PixelFormat);
            return new Bitmap(cropped, new Size(112, 112));
        }
        private float[] PreprocessFaceImage(Bitmap bmp)
        {
            float[] input = new float[3 * 112 * 112];

            for (int y = 0; y < 112; y++)
            {
                for (int x = 0; x < 112; x++)
                {
                    var color = bmp.GetPixel(x, y);
                    input[0 * 112 * 112 + y * 112 + x] = (color.R - 127.5f) / 128f;
                    input[1 * 112 * 112 + y * 112 + x] = (color.G - 127.5f) / 128f;
                    input[2 * 112 * 112 + y * 112 + x] = (color.B - 127.5f) / 128f;
                }
            }

            return input;
        }
        private float CosineSimilarity(float[] vec1, float[] vec2)
        {
            float dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < vec1.Length; i++)
            {
                dot += vec1[i] * vec2[i];
                normA += vec1[i] * vec1[i];
                normB += vec2[i] * vec2[i];
            }

            return dot / (float)(Math.Sqrt(normA) * Math.Sqrt(normB));
        }

    }
}
