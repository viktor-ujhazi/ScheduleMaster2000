document.addEventListener("DOMContentLoaded",() => {
    let grid = document.querySelector("#headerGrid")

    let element = document.createElement("a")
    element.textContent = "Home"
    element.setAttribute("id","firstElement")

    grid.appendChild(element)

    element = document.createElement("a")
    element.textContent = "Login"
    element.setAttribute("class","headerElement")
    element.setAttribute("id","login")
    element.addEventListener("click",Login)

    grid.appendChild(element)
    
    element = document.createElement("a")
    element.textContent = "Register"
    element.setAttribute("class","headerElement")
    element.addEventListener("click",Register)
    grid.appendChild(element)
})


function Login (form) {
    let loginForm = document.querySelector("#loginForm")
    loginForm.setAttribute("style","display: unset")

    let username = form.username.value;
    let password = form.password.value;
    
    console.log("Name: "+username+" Password: "+password)
    alert("Welcome!")
    loginForm.setAttribute("style","display: none")

    //if logged in: document.querySelector("#login").textContent = "Logout"
}


function Register(form){
    let registerForm = document.querySelector("#registerForm")
    registerForm.setAttribute("style","display: unset")

    let username = form.username.value;
    let password = form.password.value;

    console.log("Name: "+username+" Password: "+password)
    alert("Welcome to our services noob!")
    registerForm.setAttribute("style","display: none")
}
