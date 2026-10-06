$(".owl-carousel").owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    center: true,
    autoplay: true,
    autoplayTimeout: 2000,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 3,
        },
        600: {
            items: 3,
        },
        1000: {
            items: 6,
        },
    },
});

// File Upload Name Show

function showFileName(inputId) {
    const fileInput = document.getElementById(inputId);
    const fileName = document.getElementById("fileName" + inputId.slice(-1));
    if (fileInput.files.length > 0) {
        fileName.textContent = fileInput.files[0].name;
    } else {
        fileName.textContent = "NA";
    }
}

// Add File Input
function addFileInput(event) {
    event.preventDefault();
    const container = document.getElementById("fileUploadContainer");
    const currentInputCount = container.querySelectorAll(".file-upload").length;

    // Check if the current input count is less than 3
    if (currentInputCount < 5) {
        const newInputCount = currentInputCount + 1;
        const newFileInput = document.createElement("div");
        newFileInput.classList.add("file-upload");

        const uploadBtn = document.createElement("div");
        uploadBtn.classList.add("file-upload-btn");

        const input = document.createElement("input");
        input.type = "file";
        input.id = "fileInput" + newInputCount;
        input.name = "files[]";
        input.classList.add("file-input");
        input.setAttribute("onchange", 'showFileName("fileInput' + newInputCount + '")');

        const label = document.createElement("label");
        label.setAttribute("for", "fileInput" + newInputCount);
        label.innerHTML =
            'Upload File <span><img src="/frontend/images/Upload.svg" alt=""></span>';
        label.classList.add("me-2");

        const fileName = document.createElement("span");
        fileName.id = "fileName" + newInputCount;
        fileName.classList.add("file-name");

        uploadBtn.appendChild(input);
        uploadBtn.appendChild(label);
        newFileInput.appendChild(uploadBtn);
        newFileInput.appendChild(fileName);
        container.appendChild(newFileInput);
    } else {
        // Optionally, you can display a message or perform some action if the limit is reached
        var errorDiv = document.getElementById('error-message');

        // Add the error message
        errorDiv.innerHTML = 'You Can Upload Upto 5';

        // Make the error message visible
        errorDiv.style.display = 'block';

        // Optionally, you can hide the error message after a few seconds
        setTimeout(() => {
            errorDiv.style.display = 'none';
        }, 5000); // Hide after 5 seconds
    }
}


// Define a function to start the counter
document.addEventListener("DOMContentLoaded", function() {
    // Select the target element
    var statsSection = document.querySelector(".stats-section");

    // Create a new Intersection Observer
    var observer = new IntersectionObserver(function(entries) {
        // Check if the target element is intersecting with the viewport
        if (entries[0].isIntersecting) {
            // If intersecting, start counting for each card
            var countingElements = document.querySelectorAll(".counting");
            countingElements.forEach(function(element) {
                var target = parseInt(element.getAttribute("data-count"));
                var current = 0;

                var timer = setInterval(function() {
                    element.textContent = current;

                    current++;

                    if (current > target) {
                        clearInterval(timer);
                        element.textContent = target;
                    }
                }, 50);
            });

            // Unobserve the target element to prevent unnecessary counting
            observer.unobserve(statsSection);
        }
    });

    // Start observing the target element
    observer.observe(statsSection);
});

// Captcha generator
function generateCaptcha() {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var captchaLength = 6;
    var captcha = '';
    for (var i = 0; i < captchaLength; i++) {
        var randomIndex = Math.floor(Math.random() * characters.length);
        captcha += characters[randomIndex];
    }
    return captcha;
}

// Render captcha image and set captcha value
function renderCaptcha() {
    var captcha = generateCaptcha();
    var captchaImage = document.getElementById('captchaImage');
    if (captchaImage) {
        captchaImage.setAttribute('src', 'data:image/svg+xml;base64,' + btoa('<svg xmlns="http://www.w3.org/2000/svg" width="100" height="50"><text x="10" y="30" font-size="24" fill="black">' + captcha + '</text></svg>'));
        // Store the captcha value in a data attribute for validation
        captchaImage.dataset.captcha = captcha;
    } else {
        //console.error("Captcha image element not found.");
    }
}


// Validate Captcha on form submission
function submitForm() {
    var userInput = document.getElementById('captchaInput').value;
    var captcha = document.getElementById('captchaImage').dataset.captcha;
    var validationMessage = document.getElementById('validationMessage');

    if (userInput === captcha) {
        validationMessage.innerText = 'Captcha is correct!';
        validationMessage.style.color = '#2dd55b';
        // Here, you can proceed with form submission
        // Example: document.getElementById('yourFormId').submit();
    } else {
        validationMessage.style.display = 'block';
        validationMessage.innerText = 'Captcha is incorrect!';
        validationMessage.style.color = '#eb0014';
        // You may prevent form submission if captcha is incorrect
    }
}

// Initialize captcha when the page loads
document.addEventListener('DOMContentLoaded', function() {
    renderCaptcha();
});