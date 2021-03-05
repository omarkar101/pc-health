/**
 * This functions take input the login username and password of an account and gives/rejects access to the home page. If access was given, it automatically redirects the user to the home page.
 * @returns bool indicating if the user gets access or not
 */
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