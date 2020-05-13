function LoadSchedule(){
    
}



function Register(form) {
    var data = new FormData();
    data.append('username', form.username.value);
    data.append('pwd', form.password.value);
    data.append('email', form.email.value);
    //let username = form.username.value;
    //let password = form.password.value;
    //let email = form.email.value;

    SendData("User/NewUser", data)


    registerForm.setAttribute("style", "display: none");
}


function SendData(destination, data) {
    let xhr = new XMLHttpRequest();
    xhr.open('POSt', destination, true);
    xhr.onload = function () {
        console.log(data);
    };
    xhr.send(data);
}