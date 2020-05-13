const grid = document.querySelector("#headerGrid");

document.addEventListener("DOMContentLoaded", () => {
    let element = document.createElement("a");
    element.textContent = "Home";
    element.setAttribute("id", "firstElement");

    grid.appendChild(element);

    element = document.createElement("a");
    element.textContent = "Login";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "loginHeader");
    element.addEventListener("click", LoginPage);

    grid.appendChild(element);

    element = document.createElement("a");
    element.textContent = "Register";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "registerHeader");
    element.addEventListener("click", RegisterPage);
    grid.appendChild(element);
});


function RegisterPage() {
    HideLoginPage()
    let registerForm = document.querySelector("#registerForm");
    registerForm.setAttribute("style", "display: unset");
};


function HideRegisterPage() {
    let registerForm = document.querySelector("#registerForm");
    registerForm.setAttribute("style", "display: none");
}


function LoginPage() {
    HideRegisterPage()
    let loginForm = document.querySelector("#loginForm");
    loginForm.setAttribute("style", "display: unset");
};


function HideLoginPage() {
    let loginForm = document.querySelector("#loginForm");
    loginForm.setAttribute("style", "display: none");
}


function Login(form) {
    let username = form.username.value;
    let password = form.password.value;
    console.log("Name: " + username + " Password: " + password);
    alert("Welcome!");
    loginForm.setAttribute("style", "display: none");

    let headerToHide = document.querySelector("#loginHeader");
    headerToHide.setAttribute("style", "display: none");
    headerToHide = document.querySelector("#registerHeader");
    headerToHide.setAttribute("style", "display: none");


    element = document.createElement("a");
    element.textContent = "Logout";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "logoutHeader");
    element.addEventListener("click", Logout);
    grid.appendChild(element);


    element = document.createElement("a");
    element.textContent = "Show Schedule";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "scheduleHeader");
    element.addEventListener("click", SendAjaxGET);
    grid.appendChild(element);
}


function Register(form) {
    let username = form.username.value;
    let password = form.password.value;
    let email = form.email.value;

    console.log("Name: " + username + " Password: " + password + " E-mail: " + email);
    alert("Welcome to our services noob!");
    registerForm.setAttribute("style", "display: none");
}


function Logout() {
    let headerToRemove = document.querySelector("#logoutHeader");
    grid.removeChild(headerToRemove);

    headerToRemove = document.querySelector("#scheduleHeader");
    grid.removeChild(headerToRemove);

    let headerToShow = document.querySelector("#loginHeader");
    headerToShow.setAttribute("style", "display: unset");

    headerToShow = document.querySelector("#registerHeader");
    headerToShow.setAttribute("style", "display: unset");
};


//function ShowSchedule(model) {
//    model.array.forEach(function (row) {
//        console.log(row.title);
//        console.log(row.name);
//        console.log(row.adress);
//    });
//} ;


function SendAjaxGET() {
    var xmlHttp;
    //Let us create the XML http object
    xmlHttp = null;

    if (window.XMLHttpRequest) {
        //for new browsers
        xmlHttp = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {
        //for old ones
        //xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        var strName = "Msxml2.XMLHTTP"
        if (navigator.appVersion.indexOf("MSIE 5.5") >= 0) {
            strName = "Microsoft.XMLHTTP"
        }
        try {
            xmlHttp = new ActiveXObject(strName);
        }
        catch (e) {
            alert("Error. Scripting for ActiveX might be disabled")
            return false;
        }
    }

    if (xmlHttp != null) {
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                alert(xmlHttp.responseText);
            }
        }

        //Pass the value to a web page on server as query string using XMLHttpObject.
        xmlHttp.open("GET", "Home/Index?userName=manasm", true);
        xmlHttp.send();
    }
}