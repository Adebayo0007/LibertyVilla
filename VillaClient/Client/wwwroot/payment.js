        async function payWithPaystack(amount, email, roomId) {
        // Initialize the Paystack handler with your public key
        const handler = PaystackPop.setup({
            key: paystackPublicKey, // Your Paystack public key
        email: email,
        amount: amount * 100, // Amount in kobo (100 kobo = 1 Naira)
        currency: 'NGN', // Change this to your preferred currency
        ref: '' + Math.floor(Math.random() * 1000000000 + 1), // Unique reference
        onClose: function () {
            // This callback is called when the payment window is closed
            alert('Payment window closed.');
            },
        callback: function (response) {
            // This callback is called when the payment is complete
            let message = 'Payment complete! Reference: ' + response.reference;
        alert(message);
            location.href = `https://localhost:7086/api/roomorder/updatetopaidsuccessfully?roomId=${roomId}`;
            }
        });

        // Open the payment window
        handler.openIframe();
    }



