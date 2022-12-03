// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


///QR Code
window.addEventListener("load", () => {
    const uri = document.getElementById("qrCodeData").getAttribute('data-url');
    new QRCode(document.getElementById("qrCode"),
        {
            text: uri,
            width: 150,
            height: 150
        });
});

/// Avatar
function OnSelectChange(select) {
    var selectOption = select.value;
    document.getElementById("avatar_img").setAttribute("src", "/images/avatars/" + selectOption);
};

/// Blog Messages
function OnCloseBlogAnswer(select) {
    $(select).toggleClass("commentHide");
};

///// Statuse Message
//$(window).on("load", () => {
//    $(".statusMessage").delay(5000).fadeOut(300);
//})