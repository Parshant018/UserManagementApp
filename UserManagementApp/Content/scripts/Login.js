
function Login() {
    var errorList = document.getElementsByClassName("Error");
    for (var i = 0; i < errorList.length; i++) {
        errorList[i].style.display = "none";
    }
    var Credentials = {
        Password : document.getElementById("Password").value,
        Email: document.getElementById("Email").value
    };

    if(!ValidateCredentials(Credentials.Email,Credentials.Password)){
        return;
    }



    var DataToSend = JSON.stringify(Credentials);
    Parameters = {
        method: "post",
        headers: {
            "content-type":"application/json"
        },
        body:DataToSend
    }

    fetch("http://localhost:63694/Login/Login", Parameters).then((response) => {
            return response.json();
    }).then((message) => {
        if (message == "Success" || message == "User") {
            window.location.replace("http://localhost:63694");
        } else {
            ShowError("CommonError",JSON.parse(message));
        }
    });
}

function ValidateCredentials(email,password){
    
    var IsValid = true;
    if (email.trim() == "" || email == null){
        ShowError("EmailError","Please enter an email");
        IsValid = false;
    }
    else if (!email.match("[a-z0-9]+@[a-z]+\.[a-z]{2,3}")) {
        ShowError("EmailError", "Invalid email entered");
        IsValid = false;
    }

    if (password == "" || password == null) {
        ShowError("PasswordError","Please enter a password");
        IsValid = false;
    }
    else if (!password.match("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}")) {
        ShowError("PasswordError", "Invalid password entered");
        IsValid = false;
    }

    return IsValid;
}

function ShowError(span,message){
    document.getElementById(span).innerText = message;
    document.getElementById(span).style.display = "block";
}