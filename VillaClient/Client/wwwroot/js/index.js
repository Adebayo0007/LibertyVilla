// wwwroot/localStorage.js
window.localStorageFunctions = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    }
};
window.localStorageFunctions = {
    getItem: function (key) {
        localStorage.getItem(key);
    }
};

window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.success(message, "Operation Failed", { timeOut: 10000 });
    }
}


window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        )
    }
}

redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51NjW6HCgduLK1UTTdO2fEdoCIpybJsn9kgEyIuxb2RcF2cRR9CEOBqsUkFPzWT854DbhSG321ztjyVpkUIWhmWh0006y4Inmfj');
    stripe.redirectToCheckout({
        sessionId: sessionId
    })
}


console.log("Calling PaystackFunctions.initiatePayment");
PaystackFunctions.initiatePayment("pk_test_fe5a54565a0b6f9a04eb69e5ce395810ea27a2bc", HotelBooking.OrderDetails.TotalCost, "ade@gmail.com");




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
});
*/



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
});

*/






