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
                mlContext = new MLContext();

                var pipeline = mlContext.Transforms.ApplyOnnxModel(
                        modelFile: _yolov8x,
                        outputColumnNames: new[] { "output0" },
                        inputColumnNames: new[] { "images" });

                // Dummy data to fit pipeline
                var emptyData = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());
                model = pipeline.Fit(emptyData);
                predictor = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
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
    }
}
