const grid = document.querySelector("#headerGrid")

document.addEventListener("DOMContentLoaded",() => {
    let element = document.createElement("a")
    element.textContent = "Home"
    element.setAttribute("id","firstElement")

    grid.appendChild(element)

    element = document.createElement("a")
    element.textContent = "Login"
    element.setAttribute("class","headerElement")
    element.setAttribute("id","loginHeader")
    element.addEventListener("click",LoginPage)

    grid.appendChild(element)
    
    element = document.createElement("a")
    element.textContent = "Register"
    element.setAttribute("class","headerElement")
    element.setAttribute("id","registerHeader")
    element.addEventListener("click",RegisterPage)
    grid.appendChild(element)
})


function RegisterPage(){
    let registerForm = document.querySelector("#registerForm");
    registerForm.setAttribute("style","display: unset");
};


function LoginPage(){
    let loginForm = document.querySelector("#loginForm");
    loginForm.setAttribute("style","display: unset");
};




function Login (form) {
    let username = form.username.value;
    let password = form.password.value;
    console.log("Name: "+username+" Password: "+password);
    alert("Welcome!");
    loginForm.setAttribute("style","display: none");

    let headerToHide = document.querySelector("#loginHeader");
    headerToHide.setAttribute("style","display: none");
    headerToHide = document.querySelector("#registerHeader");
    headerToHide.setAttribute("style","display: none");


    element = document.createElement("a");
    element.textContent = "Logout";
    element.setAttribute("class","headerElement");
    element.setAttribute("id","logoutHeader");
    element.addEventListener("click",Logout);
    grid.appendChild(element);


    element = document.createElement("a");
    element.textContent = "Show Schedule";
    element.setAttribute("class","headerElement");
    element.setAttribute("id","scheduleHeader");
    element.addEventListener("click",ShowSchedule);
    grid.appendChild(element);
}


function Register(form){
    let username = form.username.value;
    let password = form.password.value;

    console.log("Name: "+username+" Password: "+password);
    alert("Welcome to our services noob!");
    registerForm.setAttribute("style","display: none");
}


function Logout(){
    let headerToRemove = document.querySelector("#logoutHeader")
    grid.removeChild(headerToRemove)

    headerToRemove = document.querySelector("#scheduleHeader")
    grid.removeChild(headerToRemove)

    let headerToShow = document.querySelector("#loginHeader")
    headerToShow.setAttribute("style","display: unset")

    headerToShow = document.querySelector("#registerHeader")
    headerToShow.setAttribute("style","display: unset")
}


function ShowSchedule(model){
    model.array.forEach(function(row) {
        console.log(row.title)
        console.log(row.name)
        console.log(row.adress)
    });
}