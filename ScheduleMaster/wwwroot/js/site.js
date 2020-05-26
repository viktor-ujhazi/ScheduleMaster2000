const grid = document.querySelector("#headerGrid");
let currentProfileEmail = null;
let currentProfileID = null;

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

function SchedulePage() {
    HideTaskPage();
    HideScheduleTable();
    let scheduleForm = document.querySelector("#scheduleForm");
    scheduleForm.setAttribute("style", "display: unset");
};

function HideSchedulePage() {
    let scheduleForm = document.querySelector("#scheduleForm");
    scheduleForm.setAttribute("style", "display: none");
}

function TaskPage() {
    HideSchedulePage();
    HideScheduleTable();
    let taskForm = document.querySelector("#taskForm");
    taskForm.setAttribute("style", "display: unset");

};

function HideTaskPage() {
    let taskForm = document.querySelector("#taskForm");
    taskForm.setAttribute("style", "display: none");
}

function HideScheduleTable() {
    let table = document.querySelector("#ScheduleTable");
    table.setAttribute("style", "display:none");
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
    element.textContent = "Create Schedule";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "scheduleHeader");
    element.addEventListener("click", SchedulePage);
    grid.appendChild(element);
}

function ShowTaskOption() {
    element = document.createElement("a");
    element.textContent = "Create Task";
    element.setAttribute("class", "headerElement");
    element.setAttribute("id", "taskHeader");
    element.addEventListener("click", TaskPage);
    grid.appendChild(element);
}

function CreateSchedules(form) {
    let data = new FormData();
    data.append('numOfDays', form.numOfDays.value);
    data.append('title', form.title.value);
    data.append('userID', currentProfileID)

    SendDataToSchedule('Schedule/AddSchedule', data)
}

function CreateTasks(form) {
    let data = new FormData();
    data.append('content', form.content.value);
    data.append('title', form.title.value);
    data.append('userID', currentProfileID)

    SendDataToSchedule('Task/AddTask', data)
}


function Login(form) {
    var data = new FormData();
    data.append('pwd', form.password.value);
    data.append('email', form.email.value);

    SendDataToLogin("User/Login", data);
}


function Register(form) {
    var data = new FormData();
    data.append('username', form.username.value);
    data.append('pwd', form.password.value);
    data.append('email', form.email.value);

    SendDataToRegister("User/NewUser", data);

    registerForm.setAttribute("style", "display: none");
}


function Logout() {
    let headerToRemove = document.querySelector("#logoutHeader");
    grid.removeChild(headerToRemove);

    headerToRemove = document.querySelector("#scheduleHeader");
    grid.removeChild(headerToRemove);

    headerToRemove = document.querySelector("#taskHeader");
    grid.removeChild(headerToRemove);

    let sidebar = document.querySelector(".sidenav");
    sidebar.setAttribute("style", "display: none");
    while (sidebar.firstChild) {
        sidebar.removeChild(sidebar.lastChild);
    }
    HideTaskPage();
    HideSchedulePage();
    HideScheduleTable();
    let xhr = new XMLHttpRequest();
    xhr.open('GET', 'User/Logout', true);
    xhr.send();

    let headerToShow = document.querySelector("#loginHeader");
    headerToShow.setAttribute("style", "display: unset");

    headerToShow = document.querySelector("#registerHeader");
    headerToShow.setAttribute("style", "display: unset");

    currentProfileEmail = null;
    currentProfileID = null
};


function SendDataToLogin(destination, data) {
    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                if (xhr.responseText !== 0) {
                    loginForm.setAttribute("style", "display: none");
                    currentProfileID = xhr.responseText;

                    HideLoginForm();
                    ShowLogout();

                    ShowScheduleOption();
                    ShowTaskOption();

                    var data = new FormData();
                    data.append('userid', xhr.responseText);
                    currentProfileID = xhr.responseText;
                    SendDataToSchedule("Schedule/Index", data);
                }
            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }
}



function SendDataToRegister(destination, data) {
    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                alert(xhr.responseText);
                if (xhr.responseText != '"Success!"') {

                    RegisterPage();
                } else {
                    LoginPage();
                }
            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }
}

function SendDataToSchedule(destination, data) {
    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let obj = JSON.parse(xhr.responseText);
                //obj.value[0].title az első elem a scheduleok között

                var sidebar = document.querySelector(".sidenav");

                HideSchedulePage();
                HideTaskPage();

                sidebar.setAttribute("style", "display: unset");
                while (sidebar.firstChild) {
                    sidebar.removeChild(sidebar.lastChild);
                }
                let selectorbuttons = document.createElement("div");
                let btnSchedule = document.createElement("input");
                let btnTask = document.createElement("input");
                btnSchedule.setAttribute("type", "button");
                btnSchedule.setAttribute("value", "Schedules");

                btnTask.setAttribute("type", "button");
                btnTask.setAttribute("value", "Tasks");

                selectorbuttons.appendChild(btnSchedule);
                selectorbuttons.appendChild(btnTask);

                sidebar.appendChild(selectorbuttons);
                btnSchedule.addEventListener("click", () => {
                    SendDataToSchedule("Schedule/Index", data);
                });
                btnTask.addEventListener("click", () => {
                    SendDataToSchedule("Task/Index", data);
                });

                if (destination === "Schedule/Index" || destination === "Schedule/UpdateSchedule" || destination === 'Schedule/AddSchedule') {
                    for (let i = 0; i < obj.length; i++) {
                        let sidePoint = document.createElement("a");
                        let uniqueId = "sidebar" + obj[i].scheduleID;


                        let sID = obj[i].scheduleID;
                        let title = obj[i].title;
                        let daysNum = obj[i].numOfDays;
                        let userID = obj[i].userID;
                        let isPublic = obj[i].isPublic;

                        sidePoint.setAttribute("id", uniqueId);
                        sidePoint.textContent = obj[i].title;
                        sidePoint.addEventListener("click", () => {
                            SidePointSelected(uniqueId, daysNum);
                        })
                        sidebar.appendChild(sidePoint);
                        sidePoint.addEventListener("dblclick", () => {
                            EditSchedule(sID, title, daysNum, userID, isPublic);
                        })
                    }
                }
                if (destination === "Task/Index" || destination === 'Task/AddTask' || destination === "Task/UpdateTask") {

                    for (let i = 0; i < obj.length; i++) {
                        let sidePoint = document.createElement("a");
                        let uniqueId = "sidebar" + obj[i].TaskID;



                        let tID = obj[i].taskID;
                        let title = obj[i].title;
                        let taskContent = obj[i].content;
                        let userID = obj[i].userID;

                        sidePoint.setAttribute("id", uniqueId);
                        sidePoint.textContent = obj[i].title;
                        //sidePoint.addEventListener("click", () => {
                        //    SidePointSelected(uniqueId, daysNum);
                        //})
                        sidebar.appendChild(sidePoint);
                        sidePoint.addEventListener("dblclick", () => {
                            EditTask(tID, title, taskContent, userID);
                        })
                    }
                }



            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }

}


function SendDataToDay(destination, data) {
    let scheduleTable = document.querySelector("#ScheduleTable");

    while (scheduleTable.firstChild) {
        scheduleTable.removeChild(scheduleTable.lastChild);
    }


    let xhr = new XMLHttpRequest();
    if (xhr != null) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {


                let obj = JSON.parse(xhr.responseText);
                let dayList = [];
                 

                for (let i = 0; i < obj.length; i++) {
                    dayList.push(obj[i]);
                }
                let numOfDays = dayList.length;
                for (let hour = 0; hour < 25; hour++) {
                    let tableRow = document.createElement("tr");
                    if (hour === 0) {
                        for (let day = 0; day < numOfDays + 1; day++) {
                            let tableCell = document.createElement("td");

                            if (day === 0) {
                                tableCell.textContent = "Time";
                                tableCell.setAttribute("id", `tableCell-${0}-${hour}`);
                                tableCell.setAttribute("class", "tableCell");
                            } else {
                                tableCell.textContent = dayList[day - 1].title;
                                tableCell.setAttribute("id", `tableCell-${dayList[day - 1].dayID}-${hour}`);
                                tableCell.addEventListener("click", () => { ModifyTitle(dayList[day - 1].title, dayList[day - 1].dayID, dayList[0].scheduleID) });
                            }
                            tableRow.appendChild(tableCell);
                        }
                    } else {
                        for (let day = 0; day < numOfDays + 1; day++) {


                            let tableCell = document.createElement("td");

                            if (day === 0) {
                                tableCell.textContent = hour + " h";
                                tableCell.setAttribute("id", `tableCell-${0}-${hour}`);
                                tableCell.setAttribute("class", "tableCell");

                            } else {
                                LoadTask(dayList[day - 1].scheduleID, dayList[day - 1].dayID, hour);
                                tableCell.setAttribute("id", `tableCell-${dayList[day - 1].dayID}-${hour}`);
                                tableCell.setAttribute("class", "tableCell");
                                tableCell.addEventListener("click", AddTask);
                            }
                            tableRow.appendChild(tableCell);
                        }
                    }
                    
                    scheduleTable.appendChild(tableRow);
                }
            }
        }
        xhr.open('POST', destination, true);
        xhr.send(data);
    }
}

function LoadTask(scheduleId, dayId, startSlot) {
    var data = new FormData();
    data.append('scheduleId', scheduleId);
    data.append('dayId', dayId);
    data.append('startSlot', startSlot);

    let xhr = new XMLHttpRequest();
    xhr.open('POST', 'Slot/TaskToSlot', true);
    xhr.onload = function () {
        let cellToChange = document.getElementById(`tableCell-${dayId}-${startSlot}`);
        let result = JSON.parse(xhr.response);
        if (result !== null) {
            cellToChange.removeEventListener("click", AddTask);
            cellToChange.setAttribute("rowspan", `${result.slotLength}`);
            for (let i = 1; i < result.slotLength; i++) {
                let cellToSpan = document.getElementById(`tableCell-${dayId}-${startSlot + i}`);
                cellToSpan.remove();
            }
            cellToChange.textContent = result.taskModel_.title;
        }
    };
    xhr.send(data);
}

function AddTask() {
    alert("MIke");
}

function ModifyTitle(title, dayId, scheduleId) {
    let dayTitle=  prompt("Please enter the title", title);

    let data = new FormData();
    data.append('dayId', dayId);
    data.append('title', dayTitle);

    let xhr = new XMLHttpRequest();
    xhr.open('POST', 'Day/UpdateTitle', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var scheduledata = new FormData();
            scheduledata.append('scheduleId', scheduleId);

            SendDataToDay("Day/Index", scheduledata);
        }
    }

    xhr.send(data);

    

}

function SidePointSelected(uncutId, numOfDays) {
    let scheduleId = uncutId.slice(7);
    let scheduleTable = document.querySelector("#ScheduleTable");

    while (scheduleTable.firstChild) {
        scheduleTable.removeChild(scheduleTable.lastChild);
    }

    scheduleTable.setAttribute("style", "display: unset");
    scheduleTable.setAttribute("style", "content: none");


    var data = new FormData();
    data.append('scheduleId', scheduleId);
    SendDataToDay("Day/Index", data);
}

function EditSchedule(sID, title, daysNum, userID, isPublic) {

    var scheduleTitle = prompt("Please enter the title", title);
    var ScheduleNumOfDays = prompt("Please the number of days", daysNum);
    var ScheduleIsPublic = prompt("Is it public?", isPublic).toLowerCase();
    if (ScheduleIsPublic === 'true' || ScheduleIsPublic === 'yes' || ScheduleIsPublic === 1) {
        ScheduleIsPublic = true;
    } else {
        ScheduleIsPublic = false;
    }
    if (typeof ScheduleNumOfDays !== 'number' || ScheduleNumOfDays > 7 || ScheduleNumOfDays < 1) {
        ScheduleNumOfDays = daysNum;
    }

    var data = new FormData();
    data.append('scheduleID', sID);
    data.append('title', scheduleTitle);
    data.append('numOfDays', ScheduleNumOfDays);
    data.append('userID', userID);
    data.append('isPublic', ScheduleIsPublic);

    SendDataToSchedule('Schedule/UpdateSchedule', data);
}

function EditTask(tID, title, taskContent, userID) {


    var taskTitle = prompt("Please enter the title", title);
    var taskCont = prompt("Please enter the task content", taskContent);


    var data = new FormData();
    data.append('taskID', tID);
    data.append('title', taskTitle);
    data.append('content', taskCont);
    data.append('userID', userID);


    SendDataToSchedule('Task/UpdateTask', data);
}









// function SendDataToDay(destination, data, scheduleTable, numOfDays) {
//     let xhr = new XMLHttpRequest();
//     if (xhr != null) {
//         xhr.onreadystatechange = function () {
//             if (xhr.readyState === 4 && xhr.status === 200) {
//                 console.log(xhr.responseText);

//                     let obj = JSON.parse(xhr.responseText);
//                     let dayList = [];
//                     let dayIdList = [];     //aktuális day-ek ID-ját tárolja
//                     let listOfTasks = [];

//                     for(let i = 0; i < obj.length; i++){
//                         dayList.push(obj[i].title);
//                         dayIdList.push(obj[i].dayID)
//                     }

//                     for(let hour = 0; hour < 25; hour++){
//                         let tableRow = document.createElement("tr");
//                         if(hour === 0){
//                             for(let day = 0; day < numOfDays+1; day++){
//                                 let tableCell = document.createElement("td");

//                                 if(day === 0){
//                                     tableCell.textContent = "Time";
//                                     tableCell.setAttribute("class", "tableCell");
//                                 }else{
//                                     tableCell.textContent = dayList[day-1];     //day titlejét írja ki az ARRAY-ből
//                                     tableCell.setAttribute("class", "tableCell");
//                                 }
//                                 tableRow.appendChild(tableCell);
//                             }    
//                         }else{
//                             var data2 = new FormData();
//                             data2.append('dayIds', dayIdList);

//                             SendDataToSlot("Slot/Index", data2, numOfDays, listOfTasks);   //Itt küldi át az id-kat a slot controllerbe
//                         }

//                         scheduleTable.appendChild(tableRow);
//                     }
//                 }
//             }
//             xhr.open('POST', destination, true);
//             xhr.send(data);
//         }
//     }


//     function SendDataToSlot(destination, data, numOfDays, listOfTasks){
//         let xhr = new XMLHttpRequest();
//         if (xhr != null) {
//             xhr.onreadystatechange = function () {
//                 if (xhr.readyState === 4 && xhr.status === 200) {

//                     let obj = JSON.parse(xhr.responseText); //itt van a dictionary
//                     console.log(obj);       //egész array kiírása (az összes azonos userId-t írja kis a schedule-hoz)

//                     for(let i=0;i<obj.length;i++){
//                         for(let j=0;j<obj[i].length;j++){
//                             console.log("taskID: "+obj[i][j].taskID);

//                             var data = new FormData();
//                             data.append('taskid', obj[i][j].taskID);
//                             SendDataToTask("Task/TaskHandler", data, listOfTasks);
//                         }
//                     }

//                     for(let day = 0; day < numOfDays+1; day++){
//                         let tableCell = document.createElement("td");

//                         if(day === 0){
//                             tableCell.textContent = hour +" h";
//                             tableCell.setAttribute("class", "tableCell");
//                             let cellId = "tableCell" + day + "_" + hour;
//                             tableCell.setAttribute("id", cellId);
//                         }else{
//                             tableCell.textContent = "toDo " + day;      //itt kéne a taskek titljét kiírni
//                             tableCell.setAttribute("class", "tableCell");
//                             let cellId = "tableCell" + day + "_" + hour;
//                             tableCell.setAttribute("id", cellId);
//                     }
//                         tableRow.appendChild(tableCell);
//                     }

//                 }
//             }
//             xhr.open('POST', destination, true);
//             xhr.send(data);
//         }
//     }


// function SendDataToTask(destination, data, listOfTasks){
//     let xhr = new XMLHttpRequest();
//         if (xhr != null) {
//             xhr.onreadystatechange = function () {
//                 if (xhr.readyState === 4 && xhr.status === 200) {
//                     let obj = JSON.parse(xhr.responseText);

//                     listOfTasks.push(obj);
//                 }
//             }
//             xhr.open('POST', destination, true);
//             xhr.send(data);
//         }
//     }