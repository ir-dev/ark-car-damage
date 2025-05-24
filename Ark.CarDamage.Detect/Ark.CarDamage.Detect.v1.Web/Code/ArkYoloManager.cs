using Compunet.YoloSharp;
using Compunet.YoloSharp.Plotting;

namespace Ark.CarDamage.Detect.v1
{
    public class ArkYoloManager
    {
        public static async Task<dynamic> Predict(string img_path)
        {
            using var predictor = new YoloPredictor(@"C:\Immi\NTT\Hybris_Intern\car_scratch_v2.onnx");
            return await predictor.PredictAndSaveAsync(@"C:\Immi\NTT\Hybris_Intern\test_7_resize.jpg");
        }
    }
}
