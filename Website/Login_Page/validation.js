function validate() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    if (username == "companyA" && password == "password") {
        window.close();
        window.open('../Home_Page/home.html');
        return false;
    }
    else {
        alert("Login Failed");
    }
}