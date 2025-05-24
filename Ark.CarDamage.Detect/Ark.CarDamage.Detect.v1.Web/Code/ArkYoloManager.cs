using Compunet.YoloSharp;
using Compunet.YoloSharp.Plotting;

namespace Ark.CarDamage.Detect.v1
{
    public class ArkYoloManager
    {
        public static Func<string> unique_file_name = () => $"{ark.net.util.DateUtil.CurrentTimeStamp()}_{ark.net.util.ArkHelper.RandomString(8, true)}";
        public static async Task<dynamic> Predict(string img_path)
        {
            using var predictor = new YoloPredictor(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Models", "car_scratch_v2.onnx"));
            //return await predictor.PredictAndSaveAsync(@"C:\Immi\NTT\Hybris_Intern\test_7_resize.jpg");
            return await predictor.PredictAndSaveAsync(img_path);
        }
    }
}
