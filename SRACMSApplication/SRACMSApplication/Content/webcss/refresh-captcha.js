function refreshCaptcha() {
    var captchaImg = document.getElementById("captchaImg");
    captchaImg.src = "/CPAdmin/GenerateCaptcha?" + new Date().getTime();
}
