const grid = document.querySelector("#headerGrid")

document.addEventListener("DOMContentLoaded", () => {
    let element = document.createElement("a")
    element.textContent = "Home"
    element.setAttribute("id", "firstElement")

    grid.appendChild(element)

    element = document.createElement("a")
    element.textContent = "Login"
    element.setAttribute("class", "headerElement")
    element.setAttribute("id", "loginHeader")
    element.addEventListener("click", Login)

    grid.appendChild(element)

    element = document.createElement("a")
    element.textContent = "Register"
    element.setAttribute("class", "headerElement")
    element.setAttribute("id", "registerHeader")
    element.addEventListener("click", Register)
    grid.appendChild(element)
})


function Login(form) {
    let loginForm = document.querySelector("#loginForm")
    loginForm.setAttribute("style", "display: unset")

    let username = form.username.value;
    let password = form.password.value;

    console.log("Name: " + username + " Password: " + password)
    alert("Welcome!")
    loginForm.setAttribute("style", "display: none")

    let headerToHide = document.querySelector("#loginHeader")
    headerToHide.setAttribute("style", "display: none")
    headerToHide = document.querySelector("#registerHeader")
    headerToHide.setAttribute("style", "display: none")


    element = document.createElement("a")
    element.textContent = "Logout"
    element.setAttribute("class", "headerElement")
    element.setAttribute("id", "logoutHeader")
    element.addEventListener("click", Logout)
    grid.appendChild(element)

    //if logged in: document.querySelector("#login").textContent = "Logout"
}


function Register(form) {
    let registerForm = document.querySelector("#registerForm")
    registerForm.setAttribute("style", "display: unset")

    let username = form.username.value;
    let password = form.password.value;

    console.log("Name: " + username + " Password: " + password)
    alert("Welcome to our services noob!")
    registerForm.setAttribute("style", "display: none")
}


function Logout() {
    let logoutHeader = document.querySelector("#logoutHeader")
    grid.removeChild(logoutHeader)

    let headerToHide = document.querySelector("#loginHeader")
    headerToHide.setAttribute("style", "display: unset")

    headerToHide = document.querySelector("#registerHeader")
    headerToHide.setAttribute("style", "display: unset")
}