jQuery(document).ready(function () {
    Metronic.init(); // init metronic core components
    Layout.init(); // init current layout
    //Login.init();
    // init background slide images
 //   $.backstretch([
 //    "/content/layout/pages/media/bg/1.jpg"
 //   ], {
 //       fade: 1000,
 //       duration: 8000
 //   }
 //);

    $.done("#loginForm", function (data) {
        debugger;
        if (data.Success) {
            window.localStorage.setItem("spa", data.Data.spa);
            window.localStorage.setItem("sidebar", data.Data.sidebar);
            window.localStorage.setItem("pcode", data.Data.pcode);
            debugger;
            if (data.Data.returnUrl)
                window.location.href = data.Data.returnUrl;
            else
                window.location.href = "/";
        } else {
            App.Alert(data.Message);
            App.UnblockUI($(".form-actions"));
        }
    }, $(".form-actions"), true);

});