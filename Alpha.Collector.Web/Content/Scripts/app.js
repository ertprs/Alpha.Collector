var App = function () {

    // 远程加载 Modal
    var _loadModal = function (selector, url, params, options, callback) {
        $('body').modalmanager('loading');
        var $modal = $(selector);

        $modal.load(url, params, function () {

            if (callback && callback instanceof Function) {
                callback();
            }

            var mops = $.extend({ width: 800 }, options);

            $modal.modal(mops);

            var form = $(selector + " form")
                        .removeData("validator") /* added by the raw jquery.validate plugin */
                        .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */

            $.validator.unobtrusive.parse(form);

            //$modal.find("input[type=checkbox]").uniform();
            //$modal.find(" input[type=radio]").uniform();

        });
    }

    var _alert = function (message, callback) {
        bootbox.alert(message, callback);
    }

    var _confirm = function (message, callback) {
        bootbox.confirm(message, callback);
    }

    var _dialog = function (options) {
        bootbox.dialog(options);
    }

    var _pulsate = function (selector) {
        $(selector).pulsate({
            color: "#ff3300",
            reach: 10,
            repeat: 3,
            speed: 250,
            glow: true
        });
    }

    var _blockUI = function (selector) {
        Metronic.blockUI({
            target: selector,
            iconOnly: true
        });
    }

    var _unblockUI = function (selector) {
        Metronic.unblockUI($(selector));
    }

    var _notify = function (msg, success) {
        var settings = {
            theme: success ? "amethyst" : "lime", // amethyst,tangerine,lime
            sticky: false,
            horizontalEdge: "bottom",
            verticalEdge: "right"
        };
        $button = $(this);

        if (!settings.sticky) {
            settings.life = 5000;
        }
        settings.heading = "<i class='icon-info'></i> 提示"
        $.notific8('zindex', 11500);
        $.notific8(msg, settings);
    }

    var _goto = function (obj) {
        var formId = $(obj).attr("formId");

        var $input = $("input[formId=" + formId + "]");
        var p = Number($input.val());
        if (!/^[0-9]+$/.test(p)) {
            App.Notify("页码格式不正确，请输入数字", true);
            $input.val("");
            return;
        }

        var total = $(".gridpager").find(".timelyTotalPage").attr("timelytotalpage");
        if (p < 1) p = 1;
        if (p > total) p = total;
        $(formId + " input[name=__pageIndex]").val(p);
        $(formId).submit();
    }

    var _initGridView = function () {
        $(".gridpager .size").die().live("change", function (e) {
            var jq = $(this);
            var formId = jq.closest("div.gridpager").attr("form");
            $("#" + formId + " [name=__pageSize]").val(jq.val());
            $("#" + formId).submit();
        });


        $("input.goto").die().live("keydown", function (e) {
            if (e.keyCode == 13)
                _goto(this);
        });

        $("button.goto").die().live("click", function () {
            _goto(this);
        });
    }

    var _init = function () {
        _initGridView();

        $("#btnLoadProfile").click(function () {
            App.LoadModal("#profileModel", "/account/userinfo", { id: $(this).attr("userId") }, { width: 600 });
        });

        $("#btnResetPassword").click(function () {
            App.LoadModal("#resetPasswordModel", "/account/resetpassword", { id: $(this).attr("userId") }, { width: 600 });
        });

        $("button[type=reset]").live("click", function () { 
            $(this).closest("form").find("input").not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
            $(this).closest("form").find("select").not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
            $("input[type=checkbox]").uniform();
            $("input[type=radio]").uniform();
            return false;
        });

    }

    var _registerDate = function (obj) {
        $(obj).each(function (i, obj) {
            var formatType = $(obj).attr("data-format");
            $(obj).datetimepicker({
                autoclose: true,
                language: 'zh-CN',
                formshowSeconds: true,
                format: formatType ? (formatType == "0" ? "yyyy-mm-dd" : "yyyy-mm-dd hh:ii:ss") : "yyyy-mm-dd",
                minView: formatType ? (formatType == "0" ? "month" : "hour") : "month",
                maxView: "decade",
                todayBtn: 1,
                startView: 2,
                clearBtn: 1,
                todayHighlight: 1,
                startDate: $(obj).attr("data-start") ? $(obj).attr("data-start") : null,
                endtDate: $(obj).attr("data-end") ? $(obj).attr("data-start") : null,
                pickerPosition: (Metronic.isRTL() ? "bottom-right" : "bottom-left")
            });
        });
    }

    var _exportFile = function (formId, controller, action) {
        var form = document.createElement("form");
        form.method = 'post';
        form.id = "__exportForm";
        form.style.display = "none";
        form.action = "/" + controller + "/" + action + "";
        form.target = '_self';
        document.body.appendChild(form);
        var para = $("#" + formId).serializeArray();
        $.each(para, function (i, field) {
            var input = document.createElement("input");
            input.type = "hidden";
            input.name = field.name;
            input.value = field.value;
            form.appendChild(input);
        });
        form.submit();
        $("#__exportForm").remove();
    }

    var _reloadBefore = function (decount) {
        var curPageRows = $("tbody tr").length;
        var curPageIndex = $("input[name='__pageIndex']").val();
        var totalPage = $(".gridpager").find(".timelyTotalPage").attr("timelytotalpage");
        if (decount == curPageRows && curPageIndex > 1 && curPageIndex == totalPage) {
            curPageIndex = curPageIndex - 1;
            $("input[name='__pageIndex']").val(curPageIndex);
        }
    }

    return {
        Init: _init,
        LoadModal: _loadModal,
        Alert: _alert,
        Confirm: _confirm,
        Dialog: _dialog,
        Pulsate: _pulsate,
        BlockUI: _blockUI,
        UnblockUI: _unblockUI,
        Notify: _notify,
        exportFile: _exportFile,
        RegisterDate: _registerDate,
        reloadBefore: _reloadBefore
    };

}();

jQuery('body').ready(function () {
    ApplyPermission($("body"));
});

jQuery('body').live('DOMNodeInserted', function (e) {
    ApplyPermission($(e.target));
});

var ApplyPermission = function (obj) {
    var perms = obj.find("[permid]");
    if (perms.length > 0) {
        var admin = localStorage.getItem("spa");
        if (admin == "1") {
            $.each(perms, function (index, value) { 
                if ($(value).get(0).tagName === "TD" || $(value).get(0).tagName === "TH") {
                    $(value).css("display", "table-cell");
                } else {
                    $(value).css("display", "inline");
                }
            });
            return;
        }
        var pcode = localStorage.getItem("pcode");
        $.each(perms, function (index, value) {
            if (pcode.indexOf("," + $(value).attr("permid") + ",") >= 0) { 
                if ($(value).get(0).tagName === "TD" || $(value).get(0).tagName === "TH") {
                    $(value).css("display", "table-cell");
                } else {
                    $(value).css("display", "initial");
                }
            }
        });
    }
}

// JQuery 扩展
jQuery.prototype.serializeObject = function () {
    var obj = new Object();
    $.each(this.serializeArray(), function (index, param) {
        if (!(param.name in obj)) {
            obj[param.name] = param.value;
        }
    });
    return obj;
};

jQuery.done = function (formid, callback, blocker, donnotUnblock) {
    var $form = $(formid);
    $(formid).die().live('submit', function (e) {
        e.preventDefault();
        var el = blocker ? $(blocker) : this;
        var jq = $form.find("button[type=submit]");
        jq.attr("disabled", "disabled");
        App.BlockUI(el);
        $(formid).ajaxSubmit({
            success: function (data, statusText, xhr, $form) {
                callback(data, statusText, xhr, $form);
                if (typeof (donnotUnblock) != undefined && !donnotUnblock)
                    App.UnblockUI(el);
                jq.removeAttr("disabled");
            },
            error: function (XmlHttpRequest, textStatus, errorThrown) {
                if (XmlHttpRequest.responseText) {
                    alert(XmlHttpRequest.responseText);
                }
                else
                    alert("网络不给力，请稍后再试！");
                if (typeof (donnotUnblock) != undefined && !donnotUnblock)
                    App.UnblockUI(el);
                jq.removeAttr("disabled");
            }
        });
    });
};

jQuery.pageRegister = function (formid, containerid) {
    var formSelector = "#" + formid;
    var containerSelector = "#" + containerid;
    $.done(formSelector,
        function (data) {
            $(containerSelector).html(data);
            $(containerSelector + ' input[type=checkbox], ' + containerSelector + ' input[type=radio]').uniform();
        },
        containerSelector + ' .loading');

    $(containerSelector + ' .gridpager a').die().live('click',
        function () {
            //alert($(this).attr('p'));
            var $p = $(this).parent();
            if ($p.hasClass('disabled') || $p.hasClass('active')) return;
            $(formSelector + ' input[name=__pageIndex]').val($(this).attr('p'));
            $(formSelector).submit();
        });

    $(formSelector).click(function () {
        $(this).closest('form').find('input[name=__pageIndex]').val(1);
    });
}

jQuery.prototype.loadHtml = function (url, params, callback) {
    var $container = $(this);
    $container.load(url, params, function () {
        if (callback && callback instanceof Function) {
            callback();
        };
        $container.find("input[type=checkbox]").uniform();
        $container.find(" input[type=radio]").uniform();
    });
}


String.prototype.lTrim = function (s) {
    s = (s ? s : "\\s");                            //没有传入参数的，默认去空格
    s = ("(" + s + ")");
    var reg_lTrim = new RegExp("^" + s + "*", "g");     //拼正则
    return this.replace(reg_lTrim, "");
};

String.prototype.rTrim = function (s) {
    s = (s ? s : "\\s");
    s = ("(" + s + ")");
    var reg_rTrim = new RegExp(s + "*$", "g");
    return this.replace(reg_rTrim, "");
};

String.prototype.trim = function (s) {
    s = (s ? s : "\\s");
    s = ("(" + s + ")");
    var reg_trim = new RegExp("(^" + s + "*)|(" + s + "*$)", "g");
    return this.replace(reg_trim, "");
};

String.prototype.fmtBankCard = function () {
    return this.replace(/(\d{4})\d+(?=\d{4})/g, "$1 **** **** ");
}

jQuery.validator.addMethod("characterlength",
    function (value, element, params) {
        var maxlen = params.maxlength;
        return value.replace(/[^\x00-\xff]/g, "aa").length <= maxlen;
    });

jQuery.validator.unobtrusive.adapters.add("characterlength", ["maxlength"], function (options) {
    options.rules["characterlength"] = {
        maxlength: options.params.maxlength
    };
    options.messages["characterlength"] = options.message;
});

function CheckDate(StartCreateTime, EndCreateTime) {

    if (StartCreateTime == "" || EndCreateTime == "") {
        alert("日期不能为空，请选择正确日期");
        return false;
    }

    if (StartCreateTime > EndCreateTime) {
        alert("开始日期不能大于结束日期");
        return false;
    }
    var dateNum = GetDateDiff(StartCreateTime, EndCreateTime);
    if (dateNum >= 31) {
        alert("超过31天的统计范围");
        return false;
    }

    return true;
}
function GetDateDiff(startDate, endDate) {
    var startTime = new Date(Date.parse(startDate.replace(/-/g, "/"))).getTime();
    var endTime = new Date(Date.parse(endDate.replace(/-/g, "/"))).getTime();
    return Math.abs((startTime - endTime)) / (1000 * 60 * 60 * 24);
}

function audioplayer(id, file, loop) {
    var audioPlayer = document.getElementById(id);
    if (audioPlayer != null) {
        $("#" + id).empty();
    }

    if (typeof (file) != 'undefined') {
        if (navigator.userAgent.indexOf("MSIE") > 0) {// IE 

            var player = document.createElement('bgsound');
            player.id = id;
            player.src = file;
            player.setAttribute('autostart', 'true');
            if (loop) {
                player.setAttribute('loop', 'infinite');
            }
            document.body.appendChild(player);

        } else { // Other FF Chome Safari Opera 

            var player = document.createElement('audio');
            player.id = id;
            player.setAttribute('autoplay', 'autoplay');
            if (loop) {
                player.setAttribute('loop', 'loop');
            }
            document.body.appendChild(player);

            var mp3 = document.createElement('source');
            mp3.src = file;
            mp3.type = 'audio/mpeg';
            player.appendChild(mp3); 

        }
    }
}

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

