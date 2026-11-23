// JavaScript: check required fields and password match on Register form submit
var form = document.getElementById("regForm");
if (form) {
    form.addEventListener("submit", function (e) {
        var pass = document.getElementById("Password").value;
        var confirm = document.getElementById("ConfirmPassword").value;
        var msg = document.getElementById("msg");
        if (pass === "" || confirm === "") {
            msg.innerText = "Password fields are required.";
            e.preventDefault();
        } else if (pass !== confirm) {
            msg.innerText = "Passwords do not match.";
            e.preventDefault();
        }
    });
}

// jQuery: show / hide password
$("#showPass").on("click", function () {
    var type = $(this).is(":checked") ? "text" : "password";
    $("#Password, #ConfirmPassword").attr("type", type);
});

// jQuery: clear the error message when the user types again
$("#Password, #ConfirmPassword").on("keyup", function () {
    $("#msg").text("");
});

// jQuery: success message fades out by itself after 3 seconds
$(".alert").delay(3000).fadeOut(600);
