$(document).ready(function () {
    var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
    var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
    var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
    var CLIENTID = '724353238217-snsh7apcs157tk49ltpj9uoe2qg3dg2q.apps.googleusercontent.com';
    var REDIRECT = 'http://localhost:53539/';
    
    var TYPE = 'token';
    var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
    var acToken;
    var tokenType;
    var expiresIn;
    var user;
    var loggedIn = false;
    var win;
   

    $("a#g_button").click(function (e) {
        e.preventDefault();
        $('#myModal').modal('hide');
        $('#loginrestrict').removeClass("displayvalidation");
        
         win = window.open(_url, "windowname1", 'width=800, height=600');

        var pollTimer = window.setInterval(function () {
            try {
                if (win && win.closed) {
                    clearInterval(pollTimer);
                    $('#loginrestrict').addClass("displayvalidation");
                }
                if (win.document.URL.indexOf(REDIRECT) != -1) {
                    window.clearInterval(pollTimer);
                    var url = win.document.URL;
                    acToken = gup(url, 'access_token');
                    tokenType = gup(url, 'token_type');
                    expiresIn = gup(url, 'expires_in');
                    win.close();

                    validateToken(acToken);
                }
            } catch (e) {
            }
        }, 500);

    });
    

    $("a#g_button_signup").click(function (e) {
        e.preventDefault();
        $('#userReg').modal('hide');
        $('#loginrestrict').removeClass("displayvalidation");
            win = window.open(_url, "windowname1", 'width=800, height=600');
            var pollTimer = window.setInterval(function () {

                try {
                    if (win && win.closed) {
                        clearInterval(pollTimer);
                        $('#loginrestrict').addClass("displayvalidation");
                    }
                 if (win.document.URL.indexOf(REDIRECT) != -1) {
                    window.clearInterval(pollTimer);
                    var url = win.document.URL;
                    acToken = gup(url, 'access_token');
                    tokenType = gup(url, 'token_type');
                    expiresIn = gup(url, 'expires_in');
                    win.close();

                    validateToken(acToken);
                }
            } catch (e) {
            }
        }, 500);
        
    });
    
    //$(document).on('click', 'a#g_logout', function(e){
    //   // alert("logout");
    //    e.preventDefault();
    //    $.ajax({
    //        type: "POST",
    //        dataType: "text",
    //        url: "/Customer/LogOut",
    //        data: null,
    //        success: function (responseText) {
    //            var obj = jQuery.parseJSON(responseText);



    //            if (obj.sourcecontrol == 'Google') {
    //                document.location.href = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=http://localhost:53539/"
    //            }
    //            else if (obj.sourcecontrol == 'OntoDeals' || obj.sourcecontrol == '' || obj.sourcecontrol == 'Null' || obj.sourcecontrol == null)
    //            {
    //                document.location.href = "http://localhost:53539/";
    //            }
    //        },

    //    });
    //});


    function validateToken(token) {
        $.ajax({
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
                getUserInfo();
                loggedIn = true;
                //$('#loginText').hide();
                //$('#logoutText').show();
            },
            dataType: "jsonp"
        });
    }

    function getUserInfo() {
        $.ajax({
            url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
            data: null,
            success: function (resp) {
                user = resp;
                $.ajax({

                    type: "POST",
                    dataType: "text",
                    url: "http://" + window.location.host + "/Customer/Register?name=" + user.name + '&email=' + user.email + '&password=' + '' + '&source='+ 'Google',
                    beforeSend: function () {
                        $('.cust_login').text('Please wait...');
                        $('.cust_login').attr('disabled', 'disabled');
                    },
                    success: function (data) {
                        var obj = jQuery.parseJSON(data);



                        if (obj.Authentication == 'UnSuccessfull') {
                            $('.cust_login').removeAttr('disabled');
                            $('#loginrestrict').addClass("displayvalidation");
                            $('.userError').text(obj.Message);
                            $('.userError').fadeIn();
                            $('.cust_login').text('Register')
                        }

                        else {
                            $('.userError').fadeOut();
                            $('#loginrestrict').addClass("displayvalidation");
                            document.location.href = "http://" + window.location.host + "/Customer/DashBoard";
                        }


                    }
                })




            },
            dataType: "jsonp"
        });
    }

   
    function gup(url, name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\#&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(url);
        if (results == null)
            return "";
        else
            return results[1];
    }

     

});

