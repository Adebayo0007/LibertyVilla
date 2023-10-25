



/*const videoElement = document.getElementById('cameraStream');
const startButton = document.getElementById('startRecording');
const stopButton = document.getElementById('stopRecording');
let mediaRecorder;
let recordedChunks = [];

async function startCameraStream() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true });
        videoElement.srcObject = stream;
        mediaRecorder = new MediaRecorder(stream);

        mediaRecorder.ondataavailable = event => {
            if (event.data.size > 0) {
                recordedChunks.push(event.data);
            }
        };

        mediaRecorder.onstop = () => {
            const blob = new Blob(recordedChunks, { type: 'video/webm' });
            const formData = new FormData();
            formData.append('video', blob, 'recorded-video.webm');

            fetch('/UploadVideo', {
                method: 'POST',
                body: formData
            });

            recordedChunks = [];
        };

        // Enable the "Start Recording" button after initialization
        startButton.disabled = false;
    } catch (error) {
        console.error('Error accessing camera:', error);
    }
}

startButton.addEventListener('click', () => {
    if (mediaRecorder) {
        mediaRecorder.start();
        startButton.disabled = true;
        stopButton.disabled = false;
    }
});

stopButton.addEventListener('click', () => {
    if (mediaRecorder) {
        mediaRecorder.stop();
        startButton.disabled = false;
        stopButton.disabled = true;
    }
});

window.addEventListener('load', () => {
    startCameraStream();
});*/










/*// Get access to the user's camera
async function startCameraStream() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true });
        const videoElement = document.getElementById('cameraStream');
        videoElement.srcObject = stream;
    } catch (error) {
        console.error('Error accessing camera:', error);
    }
}

// Start the camera stream when the page loads
window.addEventListener('load', () => {
    startCameraStream();
});*/


