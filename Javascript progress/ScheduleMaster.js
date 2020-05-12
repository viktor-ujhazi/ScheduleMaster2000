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
    element.textContent = "ASD"
    element.setAttribute("class","headerElement")
    grid.appendChild(element)
})

function Login (form) {
    let loginForm = document.querySelector("#loginForm")
    loginForm.setAttribute("style","visibility: visible")

    let username = form.username.value;
    let password = form.password.value;
    
    console.log("Name: "+username+" Password: "+password)

    //if logged in: document.querySelector("#login").textContent = "Logout"
}
