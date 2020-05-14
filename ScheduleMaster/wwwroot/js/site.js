const grid = document.querySelector("#headerGrid");
let currentProfileEmail = null;

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

function HideLoginForm() {
    let headerToHide = document.querySelector("#loginHeader");
    headerToHide.setAttribute("style", "display: none");
    headerToHide = document.querySelector("#registerHeader");
    headerToHide.setAttribute("style", "display: none");

}

function ShowLogout() {
    element = document.createElement("a");
    element.textContent = "Logout";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "logoutHeader");
    element.addEventListener("click", Logout);
    grid.appendChild(element);
}

function ShowScheduleOption() {
    element = document.createElement("a");
    element.textContent = "Show Schedules";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "scheduleHeader");
    element.addEventListener("click", ShowScedules);
    grid.appendChild(element);
}

function Login(form) {
    var data = new FormData();
    data.append('pwd', form.password.value);
    data.append('email', form.email.value);

    SendData("User/Login", data);
    //ShowScheduleOption();
}


function Register(form) {
    var data = new FormData();
    data.append('username', form.username.value);
    data.append('pwd', form.password.value);
    data.append('email', form.email.value);

    SendData("User/NewUser", data);

    registerForm.setAttribute("style", "display: none");
}


function Logout() {
    let headerToRemove = document.querySelector("#logoutHeader");
    grid.removeChild(headerToRemove);

    let sidebar = document.querySelector(".sidenav");
    sidebar.setAttribute("style", "display: none");
    while (sidebar.firstChild) {
        sidebar.removeChild(sidebar.lastChild);
    }

    let xhr = new XMLHttpRequest();
    xhr.open('GET', 'User/Logout', true);
    xhr.send();

    let headerToShow = document.querySelector("#loginHeader");
    headerToShow.setAttribute("style", "display: unset");

    headerToShow = document.querySelector("#registerHeader");
    headerToShow.setAttribute("style", "display: unset");

    currentProfileEmail = null;
};


function SendData(destination, data) {
    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                if (xhr.responseText != 0) {
                    loginForm.setAttribute("style", "display: none");
                    HideLoginForm();
                    ShowLogout();

                    var data = new FormData();
                    data.append('userid', xhr.responseText);
                    console.log(data);
                    SendDataToSchedule("Schedule/Index", data);
                }
            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }
}

function TestSend(destination, userid) {
    var data = new FormData();
    data.append('userid', userid);
    console.log(data);
    SendDataToSchedule(destination, data);
}

function SendDataToSchedule(destination, data) {
    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
               console.log(xhr.responseText);   //TO DELETE
               let obj = JSON.parse(xhr.responseText);
                //obj.value[0].title az első elem a scheduleok között

                var sidebar = document.querySelector(".sidenav");

                sidebar.setAttribute("style", "display: unset");
                
                for(let i = 0; i < obj.value.length; i++){
                    let sidePoint = document.createElement("a");
                    let uniqueId = "sidebar" + obj.value[i].scheduleID;
                    let daysNum = obj.value[i].numOfDays;

                    sidePoint.setAttribute("id", uniqueId);
                    sidePoint.textContent = obj.value[i].title;
                    sidePoint.addEventListener("click", () => {
                        SidePointSelected(uniqueId, daysNum);
                    })
                    sidebar.appendChild(sidePoint);
                }
            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }

}

    function SidePointSelected(uncutId, numOfDays){
        let scheduleId = uncutId.slice(7);
        let scheduleTable = document.querySelector("#ScheduleTable");

        scheduleTable.setAttribute("style","display: unset");

        for(let hour = 0; hour < 25; hour++){
            let tableRow = document.createElement("tr");
            if(hour === 0){
                for(let day = 0; day < numOfDays+1; day++){
                    //titles for days           ÁTÍRNIIIIIII
                    let tableCell = document.createElement("td");

                    if(day === 0){
                        tableCell.textContent = "Time";
                        tableCell.setAttribute("id", "tableCell");
                    }else{
                        tableCell.textContent = "day " + day;
                        tableCell.setAttribute("id", "tableCell");
                    }
                    tableRow.appendChild(tableCell);
                }    
            }else{
                for(let day = 0; day < numOfDays+1; day++){
                    //toDo for days             ÁTÍRNIIIIIII
                    let tableCell = document.createElement("td");

                    if(day === 0){
                        tableCell.textContent = hour +" h";
                        tableCell.setAttribute("id", "tableCell");
                    }else{
                        tableCell.textContent = "toDo " + day;
                        tableCell.setAttribute("id", "tableCell");
                }
                    tableRow.appendChild(tableCell);
                }
            }
            scheduleTable.appendChild(tableRow);
        }
    }

