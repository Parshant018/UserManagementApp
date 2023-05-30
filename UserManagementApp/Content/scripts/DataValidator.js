function IdentifyError(message) {
    if (message.indexOf("name") != -1)
        ShowError("NameError", JSON.parse(message));

    if (message.indexOf("email") != -1)
        ShowError("EmailError", JSON.parse(message));

    if (message.indexOf("password") != -1)
        ShowError("PasswordError", JSON.parse(message));

    if (message.indexOf("date") != -1)
        ShowError("DateError", JSON.parse(message));

    if (message.indexOf("phone") != -1)
        ShowError("PhoneError", JSON.parse(message));

    if (message.indexOf("salary") != -1)
        ShowError("SalaryError", JSON.parse(message));

    if (message.indexOf("Something") != -1)
        ShowError("CommonError", JSON.parse(message));
}

function ValidateData(userData) {
    var IsValid = true;
    if (userData.Name.trim() == "" || userData.Name == null) {
        ShowError("NameError", "Please enter a name");
        IsValid = false;
    }
    if (userData.Email.trim() == "" || userData.Email == null) {
        ShowError("EmailError", "Please enter an email");
        IsValid = false;
    }
    else if (!userData.Email.match("[a-z0-9]+@[a-z]+\.[a-z]{2,3}")) {
        ShowError("EmailError", "Invalid email entered");
        IsValid = false;
    }

    if (userData.Password == "" || userData.Password == null) {
        ShowError("PasswordError", "Please enter a password");
        IsValid = false;
    }
    else if (!userData.Password.match("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}")) {
        ShowError("PasswordError", "Invalid password entered");
        IsValid = false;
    }
    if (userData.DateOfBirth == "" || userData.DateOfBirth == null) {
        ShowError("DateError", "Please enter a date");
        IsValid = false;
    }
    else if (userData.DateOfBirth >= Date.now) {
        ShowError("dateError", "Invalid date entered");
        IsValid = false;
    }

    if (userData.PhoneNumber == "" || userData.PhoneNumber == 0) {
        ShowError("PhoneError", "Please enter a phone number");
        IsValid = false;
    }
    else if (!userData.PhoneNumber.match("\\d{12}")) {
        ShowError("PhoneError", "Invalid phone number entered");
        IsValid = false;
    }
    if (userData.Salary == 0) {
        ShowError("SalaryError", "Please enter salary");
        IsValid = false;
    }
    else if (userData.Salary <= 0 || userData.Salary >= 100000000) {
        ShowError("SalaryError", "Invalid salary entered");
        IsValid = false;
    }

    return IsValid;
}

function ShowError(span, message) {
    document.getElementById(span).innerText = message;
    document.getElementById(span).style.display = "block";
}

export{IdentifyError,ValidateData,ShowError}