//FACEBOOK REGISTER/LOGIN
window.fbAsyncInit = function () {
    FB.init({
        //My (chowlick) facebook appid
        appId: '333645037057462',
        cookie: true,
        xfbml: true,
        version: 'v2.8'
    });
    FB.AppEvents.logPageView();
};
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
//runs onclick on the fb favicon with id(fb-login)
document.getElementById('fb-login').onclick = function () {
    var cb = function (response) {
        console.log(response);
        //When user completes app, will check if successful
        if (response.status === 'connected') {
            //immediately gets information and registers user
            userApi();
        } else {
            console.log('User is logged out');
        }
    };
    FB.login(cb, { scope: 'email' });
};
//gets user information
function userApi() {
    FB.api('/me?fields=id,first_name,last_name,email,picture',
        function (response) {
            var fbInfo = {
                Email: response.email,
                EmailConfirmed: "true",
                Locked: "false",
                FbId: response.id,
                FirstName: response.first_name,
                LastName: response.last_name,
                Picture: response.picture.data.url
            };
            //temporarily store email to later pass into login
            $("#hiddenEmail").val(response.email);
            //temporarily store first/last to later pass into salesforce
            $("#hiddenFirstName").val(response.first_name);
            $("#hiddenLastName").val(response.last_name);
            console.log(fbInfo);
            //facebookRegister(facebookRegisterSuccess, facebookRegisterError, fbInfo);
        });
}
//Post - new user from facebook
facebookRegister = function (onSuccess, onError, fbInfo) {
    var url = "/api/auth/facebook";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: fbInfo
        , success: onSuccess
        , error: onError
        , type: "POST"
        , xhrFields: {
            withCredentials: true
        }
    };
    $.ajax(url, settings);
};
//user will immediately be logged in 
facebookRegisterSuccess = function (response) {
    console.log(response);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};
//if user already exists, will still be logged in
facebookRegisterError = function (error) {
    console.log(error);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    console.log('hi');
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};

//GOOGLE REGISTER/LOGIN
function onLoadGoogleCallback() {
    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            //My (chowlick) google developer code
            client_id: '916399470278-pshjj2gdpaeg8uonn71h8u7hh1ura3uk.apps.googleusercontent.com',
            scope: 'profile'
        });
        //runs onclick on the google favicon with id(google-login)
        auth2.attachClickHandler(element, {},
            function (googleUser) {
                var googleInfo = {
                    Email: googleUser.getBasicProfile().getEmail(),
                    EmailConfirmed: "true",
                    Locked: "false",
                    GoogleId: googleUser.getBasicProfile().getId(),
                    FirstName: googleUser.getBasicProfile().getGivenName(),
                    LastName: googleUser.getBasicProfile().getFamilyName()
                };
                $("#hiddenEmail").val(googleUser.getBasicProfile().getEmail());
                $("#hiddenFirstName").val(googleUser.getBasicProfile().getGivenName());
                $("#hiddenLastName").val(googleUser.getBasicProfile().getFamilyName());
                console.log(googleInfo);
                //googleRegister(googleRegisterSuccess, googleRegisterError, googleInfo);
            }
        );
    });
    element = document.getElementById('google-login');
}
//Post - new user from google
googleRegister = function (onSuccess, onError, googleInfo) {
    var url = "/api/auth/google";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: googleInfo
        , success: onSuccess
        , error: onError
        , type: "POST"
        , xhrFields: {
            withCredentials: true
        }
    };
    $.ajax(url, settings);
};
//user will immediately be logged in 
googleRegisterSuccess = function (response) {
    console.log(response);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};
//if user already exists, will still be logged in
googleRegisterError = function (error) {
    console.log(error);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};


function liaAuth() {
    IN.User.authorize(function () {
        IN.API.Profile("me")
            .fields("first-name", "last-name", "email-address", "id")
            .result(function (data) {
                var linkedInInfo = {
                    Email: data.values[0].emailAddress,
                    LinkedInId: data.values[0].id,
                    FirstName: data.values[0].firstName,
                    LastName: data.values[0].lastName
                };
                console.log(linkedInInfo);
                //Your Register/Login Api Call here
            }).error(function (data) {
                console.log(data);
            });
    });
}
//function onAuth() {
    
//}




//Post - new user from linkedIn
linkedInRegister = function (onSuccess, onError, linkedInInfo) {
    var url = "/api/auth/linkedin";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: linkedInInfo
        , success: onSuccess
        , error: onError
        , type: "POST"
        , xhrFields: {
            withCredentials: true
        }
    };
    $.ajax(url, settings);
};
linkedInRegisterSuccess = function (response) {
    console.log(response);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};
linkedInRegisterError = function (error) {
    console.log(error);
    var email = $("#hiddenEmail").val();
    loginEmail = {
        "email": email
    };
    loginUser(loginUserSuccess, loginUserError, loginEmail);
};

//LOGIN USER FROM FB/GOOGLE/LINKEDIN APPS
//uses seperate login endpoint that does not require a password
loginUser = function (onSuccess, onError, loginEmail) {
    var url = "/api/auth/loginfree";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: loginEmail
        , success: onSuccess
        , error: onError
        , type: "POST"
        , xhrFields: {
            withCredentials: true
        }
    };
    $.ajax(url, settings);
};
loginUserSuccess = function (response) {
    salesforceName = {
        "FirstName": $("#hiddenFirstName").val(),
        "LastName": $("#hiddenLastName").val(),
        "Email": $("#hiddenEmail").val()
    };
    salesforceUpdate(salesforceUpdateSuccess, salesforceUpdateError, salesforceName);
};
loginUserError = function (error) {
    salesforceName = {
        "FirstName": $("#hiddenFirstName").val(),
        "LastName": $("#hiddenLastName").val(),
        "Email": $("#hiddenEmail").val()
    };
    salesforceUpdate(salesforceUpdateSuccess, salesforceUpdateError, salesforceName);
};

salesforceUpdate = function (onSuccess, onError, salesforceName) {
    var url = "/api/salesforce/register";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: salesforceName
        , success: onSuccess
        , error: onError
        , type: "POST"
        , xhrFields: {
            withCredentials: true
        }
    };
    $.ajax(url, settings);
};
salesforceUpdateSuccess = function (response) {
    console.log(response);
    window.location.href = "/Home/Loading";

};
salesforceUpdateError = function (error) {
    console.log(error);
    window.location.href = "/Home/Loading";
};