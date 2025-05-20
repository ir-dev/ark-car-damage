from flask import Flask, request, jsonify
from ultralytics import YOLO
from PIL import Image
import io
import torch

app = Flask(__name__)

# Load YOLO model
model = YOLO("cat-damage-v2.pt")  # Replace with your path

@app.route('/')
def index():
    return 'YOLOv8 Inference API is running.'

@app.route('/predict', methods=['POST'])
def predict():
    if 'image' not in request.files:
        return jsonify({'error': 'No image uploaded'}), 400

    file = request.files['image']
    img_bytes = file.read()

    try:
        img = Image.open(io.BytesIO(img_bytes)).convert("RGB")
        results = model(img)

        # Convert results to dict
        predictions = []
        for r in results:
            for box in r.boxes:
                prediction = {
                    'class_id': int(box.cls),
                    'confidence': float(box.conf),
                    'bbox': box.xyxy[0].tolist()
                }
                predictions.append(prediction)

        return jsonify({'results': predictions})

    except Exception as e:
        return jsonify({'error': str(e)}), 500

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
