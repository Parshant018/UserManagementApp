import {ValidateData,IdentifyError,ShowError} from './DataValidator.js';

document.getElementById("SubmitButton").addEventListener("click", e=>UpdateUser());

function UpdateUser() {
    var errorList = document.getElementsByClassName("Error");
    for (var i = 0; i < errorList.length; i++) {
        errorList[i].style.display = "none";
    }

    var UserData = {
        Id: document.getElementById("Id").value,
        Name: document.getElementById("Name").value,
        Email: document.getElementById("Email").value,
        Password: document.getElementById("Password").value,
        DateOfBirth: document.getElementById("DateOfBirth").value,
        Designation: document.getElementById("Designation").value,
        Salary: document.getElementById("Salary").value,
        PhoneNumber: document.getElementById("PhoneNumber").value,
        Bio: document.getElementById("Bio").value
    };

    if (!ValidateData(UserData)) {
        return;
    }

    var DataToSend = JSON.stringify(UserData);
    var Parameters = {
        method: "post",
        headers: {
            "content-type": "application/json"
        },
        body: DataToSend
    }
    fetch("http://localhost:63694/Home/UpdateUser", Parameters).then((response) => {
         return response.json();
    }).then((message) => {
        if (JSON.parse(message) == "Success") {
            ShowNotice();
            window.location.replace("http://localhost:63694");
        }
        else if (JSON.parse(message) != "User")
            IdentifyError(message);
        else
            ShowError("CommonError","User UnAuthorized");
    });
}

function ShowNotice() {
    localStorage.setItem("innerText","User successfully updated");
    localStorage.setItem("display", "block");
}