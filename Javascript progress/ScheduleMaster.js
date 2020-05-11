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

function Login(){
    console.log("SHIIIIIIT")
    // let loginElement = document.querySelector("#login")
    // loginElement.setAttribute("style","display: hidden")
    // document.querySelector("#login")
}