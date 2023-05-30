
window.addEventListener("load", SearchData());
var TimeOut;

document.getElementById("Notice").innerText = localStorage.getItem("innerText");
document.getElementById("Notice").style.display = localStorage.getItem("display");
setTimeout(HideNotice, 4000);

var Salary = document.getElementById("Salary");
Salary.addEventListener("keydown", e=> {
    debugger;
    if (Salary.value > 1000000)
        Salary.value = 1000000;
})

function SearchData() {
    var DesignationNumber;
    if(document.getElementById("Designation").value.trim().toUpperCase() == "USER")
        DesignationNumber = 2;
    else
        DesignationNumber = document.getElementById("Designation").value.toUpperCase() == "ADMIN" ? 1 : 0;

    var DataToSend = JSON.stringify({
        SortOrder: document.getElementById("SortOrder").value,
        SortDirection: document.getElementById("SortDirection").value,
        SearchText: document.getElementById("SearchText").value,
        PageSize: document.getElementById("PageSize").value,
        PageIndex: document.getElementById("PageIndex").value,
        FilterOptions: {
            Name: document.getElementById("Name").value,
            Email: document.getElementById("Email").value,
            Salary:document.getElementById("Salary").value,
            DateOfBirth: document.getElementById("DateOfBirth").value,
            Designation: DesignationNumber,
            PhoneNumber: document.getElementById("PhoneNumber").value,
        }
    });

    var PageSize = document.getElementById("PageSize");
    if (PageSize.value > 70 || PageSize.value < 0) {
        PageSize.value = 20;
    }

    document.getElementById("EmptyPage").style.display = "none";
    var HttpRequest = new XMLHttpRequest();
    HttpRequest.open("POST", "http://localhost:63694/Home/SearchData");
    HttpRequest.setRequestHeader("content-type", "application/json");
    HttpRequest.send(DataToSend);
    HttpRequest.onload = (result) => {
        if (HttpRequest.status == 200) {
            var Data = HttpRequest.responseText;
            if (Data.indexOf("<td>") != -1) {
                var targetDiv = document.getElementById('innertable');
                targetDiv.innerHTML = Data;
            } else {
                document.getElementsByClassName("grid")[0].style.display = "none";
                document.getElementById("EmptyPage").style.display = "block";
            }
        }
        else if (JSON.parse(JSON.parse(HttpRequest.responseText)) == "User") {
            window.location.href = "http://localhost:63694/Home/RetrieveUser/0";
        }
        else if (HttpRequest.status == 401) {
            window.location.href = "http://localhost:63694/Login/Index";
        }
    }

}

function Debounce() {
    clearTimeout(TimeOut);
    TimeOut = setTimeout(SearchData, 500);
}

var IdToDelete;

function Delete() {
    document.getElementById("DeletePopup").style.display = "none";
    var HttpRequest = new XMLHttpRequest();
    HttpRequest.open("GET", "http://localhost:63694/Home/DeleteUser/"+IdToDelete);
    HttpRequest.setRequestHeader("content-type", "application/json");
    HttpRequest.send();
    HttpRequest.onload = () => {
        ShowNotice();
        SearchData();
    }
}

function ClearFilters() {
    document.getElementById("Name").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Designation").value = "";
    document.getElementById("Salary").value = "";
    document.getElementById("DateOfBirth").value = "";
    document.getElementById("Designation").value = "";
    document.getElementById("PhoneNumber").value = "";
    SearchData();
}

function ShowDeletePopup(id) {
    document.getElementById("DeletePopup").style.display = "block";
    IdToDelete = id;
}

function CloseDeletePopup() {
    document.getElementById("DeletePopup").style.display = "none";
}

function ShowNotice() {
    var Notice = document.getElementById("Notice");
    Notice.innerText = "User deleted successfully";
    Notice.style.display = "block";
    setTimeout(HideNotice, 4000);
}

function HideNotice() {
    document.getElementById("Notice").style.display = "none";
    localStorage.setItem("display", "none");
    localStorage.setItem("innerText","");
}