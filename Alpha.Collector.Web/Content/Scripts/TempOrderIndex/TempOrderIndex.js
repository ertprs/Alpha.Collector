
$(function () {

    $("#chkInsrnt").bind("click", function () {

        if ($("#chkInsrnt").attr("checked") == "checked") {
            $("#Insrnt_Name").val($("#App_Name").val())
            $("#Insrnt_Tel").val($("#App_Tel").val())
            $("#InsurerInfo_Insrnt_cert_no").val($("#InsurerInfo_App_cert_no").val())
            $("#InsurerInfo_Insrnt_Address").val($("#InsurerInfo_App_Address").val())
        }
        else {
            $("#Insrnt_Name").val("")
            $("#Insrnt_Tel").val("")
            $("#InsurerInfo_Insrnt_cert_no").val("")
            $("#InsurerInfo_Insrnt_Address").val("")
        }
    })

    $("#chkOwner").bind("click", function () {

        if ($("#chkOwner").attr("checked") == "checked") {
            $("#CarInfo_OwnerName").val($("#App_Name").val())
            $("#CarInfo_OwnerTel").val($("#App_Tel").val())
            $("#CarInfo_CertNo").val($("#InsurerInfo_App_cert_no").val())
            $("#CarInfo_OwnerAddress").val($("#InsurerInfo_App_Address").val())
        }
        else {
            $("#CarInfo_OwnerName").val("")
            $("#CarInfo_OwnerTel").val("")
            $("#CarInfo_CertNo").val("")
            $("#CarInfo_OwnerAddress").val("")
        }
    })

    $("#TaxInfo_Sum_up_tax").bind("click", function () { CalcAmount() })

    $("#chkIsOnCard").bind("click", function () {
        if ($("#chkIsOnCard").attr("checked") == "checked") {
            $("#CarInfo_VehicleNo").val("新车");
            $("#CarInfo_IsOnCard").val(0);
        }
        else {
            $("#CarInfo_VehicleNo").val("");
            $("#CarInfo_IsOnCard").val(1);
        }
    })

    $("#rdJQ,#rdSY,#rdJS").bind("click", function () {
        ShowCoverageInfo();
        CalcAmount();
    })

    //[type = text]
    $("#tbSYCoverage tbody input[type = text]").bind("blur", function (t) {

        if ($(this).val() != "") {
            var isfloat = IsFloat($(this).val());
            if (!isfloat) {
                App.Alert("请填写正确的金额");
                $(this).val("");
                $(this).focus();
            }
            else {

                var s = $(this).val().indexOf(".");
                if (s > -1) {
                    $(this).val(parseFloat($(this).val()).toFixed(2));
                }

                //$(this).val(parseFloat($(this).val()).toFixed(2));
                GetPremiumAmount();
            }
        }
    })

    $("#tbSYCoverage tbody input[type='checkbox'][name^='chkCoverage']").bind('click', function () {
        var tr = $(this).parent().parent().parent().parent("tr");
        var td = $(tr).children('td');
        var objAmount = $(td).eq(1).find('input[type="text"]');
        var selectAmount = $(td).eq(1).find('select');
        if ($(this).prop("checked")) {
            $(td).eq(3).find('input[type="text"]').attr("disabled", false);
            if (objAmount.length == 1) {
                $(objAmount).attr("disabled", false);
            }
            if (selectAmount.length == 1) {
                $(selectAmount).attr("disabled", false);
            }
        }
        else {
            $(td).eq(3).find('input[type="text"]').attr("disabled", true);
            $(td).eq(3).find('input[type="text"]').val("");
            if (objAmount.length == 1) {
                $(objAmount).attr("disabled", true);
                $(objAmount).val("");
            }
            if (selectAmount.length == 1) {
                $(selectAmount).attr("disabled", true);
            }
        }

        GetPremiumAmount();

    });

    $('#CarInfo_VehicleNo').on('blur', function (e) {
        carPreNumberInputChangeCheck($(this));
    })

    $("#CarInfo_RackNo").on('keyup', function () {
        this.value = this.value.replace(/[^\w\.\/]/ig, '').substr(0, 17).toUpperCase();
    })

    $("#CarInfo_EngineNo").on('keyup', function () {
        this.value = this.value.replace(/[^\w\.\/]/ig, '').substr(0, 20).toUpperCase();
    })

    $("#IsTaxInfo").bind("click", function () {
        LoadTaxInfo();
    })

    $("#SY_Amount,#Premium_Amount,#JQ_Amount,#TaxInfo_Sum_up_tax,#Discount_Amount,#Settlement_Amount").on("blur", function (t) {

        if (!IsFloat($(this).val())) {
            App.Alert("请填写正确的金额");
            $(this).val("");
        }
        else {

            var s = $(this).val().indexOf(".");
            if (s > -1) {
                $(this).val(parseFloat($(this).val()).toFixed(2));
            }
        }
    })

    $("#SY_Amount,#JQ_Amount,#TaxInfo_Sum_up_tax").bind("blur", function () {
        CalcAmount()
    })

    //ddlcompany
    $("#MerchantId").change(function () {
        if ($("#MerchantId").val() == "") {
            $("#ddlcompany").html("");
            $("#OrderSourceName").val("");
        }
        else {

            $("#OrderSourceName").val($("#MerchantId option:selected").text());

            var MerchantID = $("#MerchantId").val();
            GetCompany(MerchantID);
        }
    })




    $("#ddlcompany").change(function () {
        if ($("#ddlcompany").val() != "") {
            $("#InsuranceCompanyEnum").val($("#ddlcompany option:selected").attr("insurancecompanyenum"));
            $("#InsuranceCompanyId").val($("#ddlcompany").val());
            $("#SettleAccountId").val($("#ddlcompany option:selected").attr("settleAccountId"))
        }
        else {
            $("#InsuranceCompanyEnum").val("");
            $("#InsuranceCompanyId").val("");
            $("#SettleAccountId").val("");
        }
    })

    LoadCoverage();
    ShowCoverageInfo();
    LoadTaxInfo();
    SetCompany();
    SetReadOnly();

    $.done("#TempOrderIndexForm", function (data) {
        $("#tempOrderDetail").hide();
        if (data.Success) {
            if (data.Data['tempOrderNo'] != null) {
                $('#tempOrderNo').html('申请单号：' + data.Data['tempOrderNo']);
            }
            $("#success").show();
        }
        else {
            $('#submitMsg').html('提交失败，请返回重新录入');
        }
    });

    //禁用回车事件
    document.onkeydown = function (e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13) {
            ev.keyCode = 0;
            return false;
        }
    }
})

//ajax获取保险公司数据
function GetCompany(MerchantID) {
    $.ajax({
        url: '/CarTempOrder/AjaxGetCompany',
        data: { merchantID: MerchantID },
        async: false,
        type: "post",
        success: function (result) {
            var CompanyList = result.Data["CompanyList"];

            $("#ddlcompany").html("");
            // vehicleInfoData = result.Data["CarList"];
            $("#ddlcompany").append(" <option value='' insurancecompanyenum='' settleAccountId=''>请选择</option>")
            $.each(CompanyList, function (i, tr) {
                // alert(tr.FullName);
                var value = tr.InsuranceCompanyId;
                var text = tr.FullName;
                var insurancecompanyenum = tr.InsuranceSupplierEnum;
                var settleAccountId = tr.SettleAccountId;
                $("#ddlcompany").append(" <option value='" + value + "' insurancecompanyenum='" + insurancecompanyenum + "' settleAccountId='" + settleAccountId + "'  >" + text + "</option>");

            })


        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {

        }
    });
}

function SetCompany() {
    if ($("#InsuranceCompanyId").val() != "") {
        GetCompany($("#MerchantId").val());
        $("#ddlcompany").val($("#InsuranceCompanyId").val());
    }
}

//计算保费金额
function CalcAmount() {
    var syAmout = 0, jqAmout = 0, taxAmout = 0, amount = 0;
    syAmout = $("#SY_Amount").val();
    jqAmout = $("#JQ_Amount").val();
    taxAmout = $("#TaxInfo_Sum_up_tax").val();
    if ($("#rdJQ").attr("checked") == "checked") {
        if (!IsNullOrEmpty(jqAmout)) {
            amount += parseFloat(jqAmout)
        }
    }
    else if ($("#rdSY").attr("checked") == "checked") {
        if (!IsNullOrEmpty(syAmout)) {
            amount += parseFloat(syAmout)
        }
    }
    else if ($("#rdJS").attr("checked") == "checked") {
        if (!IsNullOrEmpty(syAmout) && !IsNullOrEmpty(jqAmout)) {
            amount += (parseFloat(syAmout) * 100 + parseFloat(jqAmout) * 100) / 100
        }
    }

    if ($("#IsTaxInfo").attr("checked") == "checked") {
        if (!IsNullOrEmpty(taxAmout)) {
            amount = (parseFloat(taxAmout) * 100 + amount * 100) / 100
        }
    }
    $("#Premium_Amount").val(amount);
}

//加载车船税
function LoadTaxInfo() {
    if (!$("#IsTaxInfo").prop("checked")) {
        $("#TaxInfo_Sum_up_tax").val("");
        $("#TaxInfo_Sum_up_tax").attr("disabled", true)

    }
    else {
        $("#TaxInfo_Sum_up_tax").attr("disabled", false)
    }
}

//车牌号校验
function carPreNumberInputChangeCheck(element) {

    if (!$("#chkIsOnCard").prop("check")) {
        element.val(element.val().substr(0, 7).toUpperCase());
        //.replace(/[^A-Za-z0-9]/ig, '')
    }
    else {
        element.val("新车");
    }
}

//险种信息展示
function ShowCoverageInfo() {
    if ($("#rdJQ").attr("checked") == "checked") {
        $("#divJQCoverage").show();
        $("#divTaxInfo").show();
        $("#divSYCoverage").hide();

        $("#Discount_Amount").hide();
    }
    else if ($("#rdSY").attr("checked") == "checked") {
        $("#divJQCoverage").hide();
        $("#divTaxInfo").hide();
        $("#divSYCoverage").show();

        $("#Discount_Amount").show();

    }
    else if ($("#rdJS").attr("checked") == "checked") {
        $("#divJQCoverage").show();
        $("#divTaxInfo").show();
        $("#divSYCoverage").show();

        $("#Discount_Amount").show();
    }

}


//加载商业险种
function LoadCoverage() {
    if ($("#CoverageJson").val() != "") {
        var converageJson = $.parseJSON($("#CoverageJson").val());

        $.each($("#tbSYCoverage tbody tr"), function (i, tr) {

            var td = $(this).children('td');
            var isCheck = $(td).eq(0).find('input[type="checkbox"]').prop("checked");
            var insrncCode = $(td).eq(0).find('input[type="checkbox"]').attr("value");
            var converageName = $(td).eq(0).find('label').text().trim();
            var franchiseFlag = $(td).eq(2).find('input[type="checkbox"]').prop("checked");

            $.each(converageJson, function (index, row) {
                if (row.InsrncCode == insrncCode) {
                    $(td).eq(0).find('input[type="checkbox"]').attr("checked", "checked");
                    $(td).eq(0).find("span").addClass("checked");
                    isCheck = true;
                    if (row.FranchiseFlag == 1) {
                        $(td).eq(2).find('input[type="checkbox"]').attr("checked", "checked");
                        $(td).eq(2).find("span").addClass("checked");
                    }

                    if (i == 0 || i == 2) {
                        $(td).eq(1).find('input[type="text"]').val(row.Amount);
                    }
                    //选取下拉框的保额
                    if (i == 1 || i == 3 || i == 4 || i == 8 || i == 9 || i == 10) {
                        $(td).eq(1).find('select').val(row.Amount);
                    }

                    $(td).eq(3).find('input[type="text"]').val(row.PayPremium);
                }
            })

            if (!isCheck) {
                if (i == 0 || i == 2) {
                    $(td).eq(1).find('input[type="text"]').val("");
                    $(td).eq(1).find('input[type="text"]').attr("disabled", true);
                }
                if (i == 1 || i == 3 || i == 4 || i == 8 || i == 9 || i == 10) {
                    $(td).eq(1).find('select').attr("disabled", true);
                }
                $(td).eq(3).find('input[type="text"]').val("");
                $(td).eq(3).find('input[type="text"]').attr("disabled", true);
            }
        })
    }
    else {

        $("#tbSYCoverage tbody tr input[type='text']").attr("disabled", true);
        $("#tbSYCoverage tbody tr select").attr("disabled", true);
    }

}

//统计商业险种金额
function GetPremiumAmount() {
    var totalAmount = 0;
    $.each($("#tbSYCoverage tbody tr"), function (i, tr) {
        var td = $(this).children('td');
        var isCheck = $(td).eq(0).find('input[type="checkbox"]').prop("checked");

        if (isCheck) {

            //判断保险金额
            var premium = $(td).eq(3).find('input[type="text"]').val().trim();
            if (premium != "") {
                totalAmount = (totalAmount * 100 + (parseFloat(premium)) * 100) / 100;
            }

        }
    })

    if (totalAmount == 0) {
        var amount = $("#SY_Amount").val();
        $("#lblTotalPremiun").text(amount);
    }
    else {
        $("#lblTotalPremiun").text(totalAmount.toFixed(2));
    }

}

//校验首页基本信息
function CheckIndex() {

    //if ($.trim($("#OrderSourceType").val()) == "") {
    //    App.Alert("请选择订单来源");
    //    // $("#OrderSourceType").focus();
    //    return false;
    //}
    if ($.trim($("#OrderSourceName").val()) == "") {
        App.Alert("请填写渠道名称");
        $("#OrderSourceName").focus();
        return false;
    }
    if ($.trim($("#InsuranceCompanyEnum").val()) == "") {
        App.Alert("请选择保险公司");
        //$("#InsuranceCompanyEnum").focus();
        return false;
    }
    if ($.trim($("#App_Name").val()) == "") {
        App.Alert("请填写投保人姓名");
        //$("#InsuranceCompanyEnum").focus();
        return false;
    }
    //if ($.trim($("#App_Tel").val()) == "") {
    //    App.Alert("请填写投保人手机号码");
    //    //$("#InsuranceCompanyEnum").focus();
    //    return false;
    //}
    //else {
    //    if (!checkMobile($("#App_Tel").val())) {
    //        App.Alert("请填写正确的投保人手机号码");
    //        return false
    //    }
    //}
    if ($.trim($("#InsurerInfo_App_cert_no").val()) == "") {
        App.Alert("请填写投保人身份证号");
        $("#InsurerInfo_App_cert_no").focus();
        return false;
    }
    else {
        if (!checkIDCard($.trim($("#InsurerInfo_App_cert_no").val()))) {
            App.Alert("投保人身份证号填写不正确");
            $("#InsurerInfo_App_cert_no").focus();
            return false;
        }

    }
    //if ($.trim($("#InsurerInfo_App_Address").val()) == "") {
    //    App.Alert("请选择投保人联系地址");
    //    $("#InsurerInfo_App_Address").focus();
    //    return false;
    //}
    if ($.trim($("#Insrnt_Name").val()) == "") {
        App.Alert("请选择被保险人姓名");
        //$("#InsuranceCompanyEnum").focus();
        return false;
    }
    //if ($.trim($("#Insrnt_Tel").val()) == "") {
    //    App.Alert("请选择被保险人手机号码");
    //    //$("#InsuranceCompanyEnum").focus();
    //    return false;
    //}
    //else {
    //    if (!checkMobile($("#App_Tel").val())) {
    //        App.Alert("请填写正确的被保险人手机号码");
    //        return false
    //    }
    //}

    if ($.trim($("#InsurerInfo_Insrnt_cert_no").val()) == "") {
        App.Alert("请选择被保险人身份证号");
        $("#InsurerInfo_App_cert_no").focus();
        return false;
    }
    else {

        if (!checkIDCard($.trim($("#InsurerInfo_Insrnt_cert_no").val()))) {
            App.Alert("被保险人身份证号填写不正确");
            $("#InsurerInfo_Insrnt_cert_no").focus();
            return false;
        }
    }

    //新加车主的校验
    if ($.trim($("#CarInfo_OwnerName").val()) == "") {
        App.Alert("请选择车主姓名");
        //$("#InsuranceCompanyEnum").focus();
        return false;
    }
    //if ($.trim($("#CarInfo_OwnerTel").val()) == "") {
    //    App.Alert("请选择车主手机号码");
    //    //$("#InsuranceCompanyEnum").focus();
    //    return false;
    //}
    //else {
    //    if (!checkMobile($("#CarInfo_OwnerTel").val())) {
    //        App.Alert("请填写正确的车主手机号码");
    //        return false
    //    }
    //}

    if ($.trim($("#CarInfo_CertNo").val()) == "") {
        App.Alert("请选择车主身份证号");
        $("#CarInfo_CertNo").focus();
        return false;
    }
    else {

        if (!checkIDCard($.trim($("#CarInfo_CertNo").val()))) {
            App.Alert("车主身份证号填写不正确");
            $("#CarInfo_CertNo").focus();
            return false;
        }
    }


    //$("#CarInfo_OwnerName").val("")
    //$("#CarInfo_OwnerTel").val("")
    //$("#CarInfo_CertNo").val("")


    //if ($.trim($("#InsurerInfo_Insrnt_Address").val()) == "") {
    //    App.Alert("请选择被保险人联系地址");
    //    $("#InsurerInfo_Insrnt_Address").focus();
    //    return false;
    //}
    if ($.trim($("#CarInfo_VehicleNo").val()) == "") {
        App.Alert("请选择车牌号");
        $("#CarInfo_VehicleNo").focus();
        return false;
    }
    else {

        if (!$("#chkIsOnCard").prop("checked")) {

            if (!isVehicleNumber($("#CarInfo_VehicleNo").val())) {
                App.Alert("车牌号输入不正确");
                $("#CarInfo_VehicleNo").focus();
                return false;
            }
        }
        else if ($("#CarInfo_VehicleNo").val() != "新车") {
            App.Alert("车牌号输入不正确");
            $("#CarInfo_VehicleNo").focus();
            return false;
        }
    }

    if ($.trim($("#CarInfo_RackNo").val()) == "") {
        App.Alert("请填写车架号");
        $("#CarInfo_RackNo").focus();
        return false;
    }
    else {
        if (!isRackNo($("#CarInfo_RackNo").val())) {
            App.Alert("车架号输入不正确");
            $("#CarInfo_RackNo").focus();
            return false;
        }

    }
    //if ($.trim($("#CarInfo_EngineNo").val()) == "") {
    //    App.Alert("请填写发动机号");
    //    $("#CarInfo_EngineNo").focus();
    //    return false;
    //}
    //else {
    //    if (!isEngineNo($("#CarInfo_EngineNo").val())) {
    //        App.Alert("发动机号输入不正确");
    //        $("#CarInfo_EngineNo").focus();
    //        return false;
    //    }
    //}

    //if ($.trim($("#CarInfo_VehicleName").val()) == "") {
    //    App.Alert("请填写品牌型号");
    //    $("#CarInfo_VehicleName").focus();
    //    return false;
    //}



    //if (!$("#chkIsOnCard").prop("checked")) {

    //    if ($.trim($('input[name="CarInfo.RegisterData"]').val()) == "") {
    //        App.Alert("请填写初登日期");
    //        //$("#CarInfo_RegisterData").focus();
    //        return false;
    //    }
    //    else {
    //        if ($.trim($('input[name="CarInfo.RegisterData"]').val()) == "") {
    //            App.Alert("请填写初登日期");
    //            //$("#CarInfo_RegisterData").focus();
    //            return false;
    //        }
    //        else {
    //            var RegisterData = $('input[name="CarInfo.RegisterData"]').val().trim();
    //            var registerDataTime = new Date(RegisterData.replace("-", "/").replace("-", "/"));

    //            var tday = new Date();
    //            if (tday < registerDataTime) {
    //                App.Alert("初登日期不能大于当前日期");
    //                return false;
    //            }
    //        }
    //    }
    //}

    return true;


}

function ReturnIndex() {
    $("#tempOrderIndex").show();
    $("#tempOrderCoverage").hide();
    $("#tempOrderDetail").hide();

    $("#liIndex").addClass("active");
    $("#liCoverage").removeClass("active");
    $("#liDetail").removeClass("active");
}

function GoToCoverage() {
    if (CheckIndex()) {
        $("#tempOrderIndex").hide();
        $("#tempOrderCoverage").show();
        $("#tempOrderDetail").hide();

        $("#liIndex").removeClass("active");
        $("#liCoverage").addClass("active");
        $("#liDetail").removeClass("active");

        CopyIndex();
    }

}

function ReturnCoverage() {
    $("#tempOrderIndex").hide();
    $("#tempOrderCoverage").show();
    $("#tempOrderDetail").hide();

    $("#liIndex").removeClass("active");
    $("#liCoverage").addClass("active");
    $("#liDetail").removeClass("active");
}


function GoToDetail() {
    if (!CheckCoverage()) {
        return;
    }
    SaveCoverageList();
    CopyCoverageInfo();

    $("#liIndex").removeClass("active");
    $("#liCoverage").removeClass("active");
    $("#liDetail").addClass("active");


    $("#tempOrderDetail").show();
    $("#tempOrderIndex").hide();
    $("#tempOrderCoverage").hide();
}

//校验险种信息
function CheckCoverage() {
    if ($("#rdJS").attr("checked") != "checked" && $("#rdSY").attr("checked") != "checked" && $("#rdJQ").attr("checked") != "checked") {

        App.Alert("请选择投保类型");
        return false;
    }
    else if ($("#rdJQ").attr("checked") == "checked") {
        if (!CheckJQCoverage()) {
            return false;
        }
        if (!CheckTaxInfo()) {
            return false;
        }

    }
    else if ($("#rdSY").attr("checked") == "checked") {
        if (!CheckSYCoverage()) {
            return false;
        }
    }
    else if ($("#rdJS").attr("checked") == "checked") {
        if (!CheckJQCoverage()) {
            return false;
        }
        if (!CheckTaxInfo()) {
            return false;
        }
        if (!CheckSYCoverage()) {
            return false;
        }

    }

    return true;
}

function CheckSYCoverage() {



    if ($("#SY_Ply_No").val().trim() == "") {
        App.Alert("请填写商业险投保单号");
        return false
    }
    if ($('input[name="SY_StartTime"]').val().trim() == "") {
        App.Alert("请填写商业险起保日期");
        return false;
    }
    if ($('input[name="SY_EndTime"]').val().trim() == "") {
        App.Alert("请填写商业险结束日期");
        return false;
    } else {
        var startTime = $('input[name="SY_StartTime"]').val().trim();
        var start = new Date(startTime.replace("-", "/").replace("-", "/"));
        var endTime = $('input[name="SY_EndTime"]').val().trim()
        var end = new Date(endTime.replace("-", "/").replace("-", "/"));
        if (end <= start) {
            App.Alert("商业险结束日期必须大于商业险起保日期");
            return false;
        }
    }
    if ($("#SY_Amount").val().trim() == "") {
        App.Alert("请填写商业险保险费小计");
        return false;
    }
    if (parseFloat($("#SY_Amount").val()) <= 0) {
        App.Alert("商业险保费请输入大于0的数字");
        return;
    }
    //if (!CheckCoverageList()) {
    //    return false;
    //}

    return true;
}

//险别校验费用是否一致
function CheckPremiumAmount() {

    if ($("#rdSY").attr("checked") == "checked") {

        if ($("#SY_Amount").val() == "") {
            App.Alert("请填写商业险保险费小计");
            return false;
        }
        else if (!IsFloat($("#SY_Amount").val())) {
            App.Alert("商业险保险费小计输入不正确");
            return false;
        } else if (parseFloat($("#SY_Amount").val()) <= 0) {

            App.Alert("商业险保费请输入大于0的数字");
            return false;
        }

        //1.校验统计的商业险别跟填写的商业险是否一致
        if (parseFloat($("#lblTotalPremiun").text().trim()) != parseFloat($("#SY_Amount").val())) {
            App.Alert("商业险保险费小计金额值与实际投保金额不一致");
            return false;
        }

        if ($("#Premium_Amount").val() == "") {
            App.Alert("请填写投保金额合计");
            return false;
        }
        else if (!IsFloat($("#Premium_Amount").val())) {
            App.Alert("投保金额输入不正确");
            return false;
        }

        if (parseFloat($("#Premium_Amount").val().trim()) != parseFloat($("#SY_Amount").val())) {
            App.Alert("投保金额合计值与实际投保金额不一致");
            return false;
        }
    }
    else if ($("#rdJS").attr("checked") == "checked") {


        if ($("#SY_Amount").val() == "") {
            App.Alert("请填写商业险保险费小计");
            return false;
        }
        else if (!IsFloat($("#SY_Amount").val())) {
            App.Alert("商业险保险费小计输入不正确");
            return false;
        } else if (parseFloat($("#SY_Amount").val()) <= 0) {

            App.Alert("商业险保费请输入大于0的数字");
            return false;
        }

        //1.校验统计的商业险别跟填写的商业险是否一致
        if (parseFloat($("#lblTotalPremiun").text().trim()) != parseFloat($("#SY_Amount").val())) {
            App.Alert("商业险保险费小计金额值与实际投保金额不一致");
            return false;
        }

        var taxAmount = 0;

        if ($("#IsTaxInfo").attr("checked") == "checked") {
            if (IsFloat($("#TaxInfo_Sum_up_tax").val())) {
                taxAmount = parseFloat($("#TaxInfo_Sum_up_tax").val());
            }
            else {
                App.Alert("请正确填写车船税金额");
                return false;
            }
        }

        if ($("#Premium_Amount").val() == "") {
            App.Alert("请填写投保金额合计");
            return false;
        }
        else if (!IsFloat($("#Premium_Amount").val())) {
            App.Alert("投保金额输入不正确");
            return false;
        }



        if (parseFloat($("#Premium_Amount").val().trim()) != (parseFloat($("#SY_Amount").val()) + parseFloat($("#JQ_Amount").val()) + taxAmount).toFixed(2)) {
            App.Alert("投保金额合计值与实际投保金额不一致");
            return false;
        }

    }
    else if ($("#rdJQ").attr("checked") == "checked") {

        if ($("#JQ_Amount").val() == "") {
            App.Alert("请填写交强险保险费小计");
            return false;
        }
        else if (!IsFloat($("#JQ_Amount").val())) {
            App.Alert("交强险保险费小计输入不正确");
            return false;
        }

        var taxAmount = 0;

        if ($("#IsTaxInfo").attr("checked") == "checked") {
            if (IsFloat($("#TaxInfo_Sum_up_tax").val())) {
                taxAmount = parseFloat($("#TaxInfo_Sum_up_tax").val());
            }
            else {
                App.Alert("请正确填写车船税金额");
                return false;
            }
        }

        if ($("#Premium_Amount").val() == "") {
            App.Alert("请填写投保金额合计");
            return false;
        }
        else if (!IsFloat($("#Premium_Amount").val())) {
            App.Alert("投保金额输入不正确");
            return false;
        }


        if (parseFloat($("#Premium_Amount").val().trim()) != (parseFloat($("#JQ_Amount").val()) + taxAmount).toFixed(2)) {
            App.Alert("投保金额合计值与实际投保金额不一致");
            return false;
        }
    }
    //Premium_Amount

    return true;
}

function CheckCoverageList() {

    var isCheckCoverage = true;
    var isSelectedCoverage = false;
    var isFranchiseFlag = false;

    $.each($("#tbSYCoverage tbody tr"), function (i, tr) {
        var td = $(this).children('td');
        var isCheck = $(td).eq(0).find('input[type="checkbox"]').prop("checked");
        var converageName = $(td).eq(0).find('label').text();
        var franchiseFlag = $(td).eq(2).find('input[type="checkbox"]').prop("checked");
        if (isCheck) {
            if (franchiseFlag) {
                isFranchiseFlag = true;
            }

            isSelectedCoverage = isCheck;
            //获取填写的保额
            if (i == 0 || i == 2) {
                var amount = $(td).eq(1).find('input[type="text"]').val();
                if (amount != "" && IsFloat(amount) && parseFloat(amount) > 0) {
                }
                else {
                    var msg = "请正确填写" + converageName + "的保险金额";
                    App.Alert(msg);
                    isCheckCoverage = false;

                    return;
                }
            }
            //选取下拉框的保额
            if (i == 1 || i == 3 || i == 4 || i == 8 || i == 9 || i == 10) {
                var amount = $(td).eq(1).find('select').val();
                if (amount != "" && IsFloat(amount) && parseFloat(amount) > 0) {
                }
                else {
                    var msg = "请填写" + converageName + "的保险金额";
                    App.Alert(msg);
                    isCheckCoverage = false;
                    return;
                }
            }

            //判断保险金额
            var premium = $(td).eq(3).find('input[type="text"]').val();
            if (premium != "" && IsFloat(premium) && parseFloat(premium) > 0) {
            }
            else {
                var msg = "请填写" + converageName + "的保费";
                App.Alert(msg);
                isCheckCoverage = false;
                return;
            }
        }
    })

    if (!$("#chkCoverageIOP").prop("checked") && isFranchiseFlag == true) {
        App.Alert("请勾选不计免赔险并填写不计免赔险保费");
        return false;
    }
    else if ($("#chkCoverageIOP").prop("checked") && isFranchiseFlag == false) {
        App.Alert("商业险种并没有勾选不计免赔，请取消不计免赔险并清空不计免赔险保费");
        return false;
    }

    //先取消商业险种必须的校验
    if ($("#operateType").val() == "0") {
        if (isSelectedCoverage == false) {
            App.Alert("请选择商业险险种");
            return false;
        }
    }


    return isCheckCoverage;
}

//保存险种信息
function SaveCoverageList() {
    var array = new Array();

    var index = 0;

    if ($("#rdJQ").attr("checked") != "checked") {

        $.each($("#tbSYCoverage tbody tr"), function (i, tr) {
            var td = $(this).children('td');
            var isCheck = $(td).eq(0).find('input[type="checkbox"]').prop("checked");
            var insrncCode = $(td).eq(0).find('input[type="checkbox"]').attr("value");
            var converageName = $(td).eq(0).find('label').text().trim();
            var franchiseFlag = $(td).eq(2).find('input[type="checkbox"]').prop("checked");
            if (isCheck) {

                index++;
                var model = {}
                //model.CoverageId =
                //model.OrderId
                model.InsrncCode = insrncCode;
                model.InsrncName = converageName;
                model.Amount = 0.0;
                model.FranchiseFlag = franchiseFlag == true ? 1 : 0;
                model.Rate = 0;
                model.CoverageIndex = index;
                //获取填写的保额
                if (i == 0 || i == 2) {
                    var amount = $(td).eq(1).find('input[type="text"]').val();
                    model.Amount = parseFloat(amount);
                }
                //选取下拉框的保额
                if (i == 1 || i == 3 || i == 4 || i == 8 || i == 9 || i == 10) {
                    var amount = $(td).eq(1).find('select').val();
                    model.Amount = parseFloat(amount);
                }

                //判断保险金额
                var premium = $(td).eq(3).find('input[type="text"]').val();
                if (premium==null || premium=="") {
                    model.BasePremium = 0;
                    model.BeforePremium = 0;
                    model.PayPremium = 0;
                }
                else {
                    model.BasePremium = parseFloat(premium);
                    model.BeforePremium = model.BasePremium;
                    model.PayPremium = model.BasePremium;
                }             
                model.CarDamageDeductible = 0;
                model.NonDeductible = 0;
                model.RiskPremium = 0;

                array.push(model)

            }
        })
    }


    //需加交强税
    if ($("#rdSY").attr("checked") != "checked") {
        index++;
        var model = {}
        //model.CoverageId =
        //model.OrderId
        model.InsrncCode = "0357";
        model.InsrncName = "机动车交通事故责任强制保险";
        model.Amount = 0;
        model.FranchiseFlag = 0;
        model.Rate = 0;
        model.CoverageIndex = index;

        //判断保险金额
        var premium = $("#JQ_Amount").val();
        if (premium == null || premium == "") {
            model.BasePremium = 0;
            model.BeforePremium = 0;
            model.PayPremium = 0;
        }
        else {
            model.BasePremium = parseFloat(premium);
            model.BeforePremium = model.BasePremium;
            model.PayPremium = model.BasePremium;
        }
        model.CarDamageDeductible = 0;
        model.NonDeductible = 0;
        model.RiskPremium = 0;

        array.push(model)
    }

    if (array.length > 0) {
        $("#CoverageJson").val(JSON.stringify(array));
    }
    else {
        $("#CoverageJson").val("");
    }

}

function CheckJQCoverage() {

    if ($("#JQ_Ply_No").val().trim() == "") {
        App.Alert("请填写交强险投保单号");
        return false
    }
    if ($('input[name="JQ_StartTime"]').val().trim() == "") {
        App.Alert("请填写交强险起保日期");
        return false;
    }
    if ($('input[name="JQ_EndTime"]').val().trim() == "") {
        App.Alert("请填写交强险结束日期");
        return false;
    }
    else {

        var startTime = $('input[name="JQ_StartTime"]').val().trim();
        var start = new Date(startTime.replace("-", "/").replace("-", "/"));
        var endTime = $('input[name="JQ_EndTime"]').val().trim()
        var end = new Date(endTime.replace("-", "/").replace("-", "/"));
        if (end <= start) {
            App.Alert("交强险结束日期必须大于商业险起保日期");
            return false;
        }

    }
    if ($("#JQ_Amount").val().trim() == "") {
        App.Alert("请填写交强险保险费小计");
        return false;
    }

    return true;
}

function CheckTaxInfo() {
    if ($("#rdSY").attr("checked") != "checked") {
        if ($("#IsTaxInfo").attr("checked") == "checked") {
            if ($("#TaxInfo_Sum_up_tax").val().trim() == "") {
                App.Alert("请填写车船税");
                return false;
            }
            else if (!IsFloat($("#TaxInfo_Sum_up_tax").val().trim())) {

                App.Alert("车船税输入不正确");
                return false;
            }
            else {

                if (parseFloat($("#TaxInfo_Sum_up_tax").val()) <= 0) {
                    App.Alert("车船税输入不正确");
                    return false;
                }
            }
        }
    }

    return true;
}


//将基本信息数据，拷贝到详情页面
function CopyIndex() {

    $("#lblOrderSourceType").text($("#OrderSourceType").find("option:selected").text());
    $("#lblOrderSourceName").text($("#OrderSourceName").val());
    $("#lblInsuranceCompanyEnum").text($("#ddlcompany").find("option:selected").text());
    $("#lblApp_Name").text($("#App_Name").val());
    $("#lblApp_Tel").text($("#App_Tel").val());
    $("#lblApp_cert_no").text($("#InsurerInfo_App_cert_no").val());
    $("#lblApp_Address").text($("#InsurerInfo_App_Address").val());
    $("#lblInsrnt_Name").text($("#Insrnt_Name").val());
    $("#lblInsrnt_Tel").text($("#Insrnt_Tel").val());
    $("#lblInsrnt_cert_no").text($("#InsurerInfo_Insrnt_cert_no").val());
    $("#lblInsrnt_Address").text($("#InsurerInfo_Insrnt_Address").val());

    //新加 车主的信息 
    $("#lblCarInfo_Name").text($("#CarInfo_OwnerName").val());
    $("#lblCarInfo_Tel").text($("#CarInfo_OwnerTel").val());
    $("#lblCarInfo_cert_no").text($("#CarInfo_CertNo").val());
    $("#lblCarInfo_Address").text($("#CarInfo_OwnerAddress").val());

    $("#lblVehicleNo").text($("#CarInfo_VehicleNo").val());
    $("#lblRackNo").text($("#CarInfo_RackNo").val());
    $("#lblEngineNo").text($("#CarInfo_EngineNo").val());
    $("#lblVehicleName").text($("#CarInfo_VehicleName").val());
    $("#lblRegisterData").text($('input[name="CarInfo.RegisterData"]').val());

}

function CopyCoverageInfo() {
    var InsuranceType = $('input[type="radio"][name="InsuranceType"]:checked ').val();
    if (InsuranceType == "2") {
        $("#lblInsuranceType").text("交强险");
    }
    else if (InsuranceType == "1") {
        $("#lblInsuranceType").text("商业险");
    }
    else if (InsuranceType == "3") {
        $("#lblInsuranceType").text("交商同保");
    }

    $("#lblPremium_Amount").text($("#Premium_Amount").val());

    if ($("#rdJQ").attr("checked") == "checked") {
        CopyJQCoverage();

        $("#divRowSY").hide();
        $("#divRowJQ").show();
        $("#divRowTax").show();
    }
    else if ($("#rdSY").attr("checked") == "checked") {
        CopySYCoverage();

        $("#divRowJQ").hide();
        $("#divRowSY").show();
        $("#divRowTax").hide();
    }
    else if ($("#rdJS").attr("checked") == "checked") {
        CopyJQCoverage();
        CopySYCoverage();

        $("#divRowJQ").show();
        $("#divRowSY").show();
        $("#divRowTax").show();
    }

}

function CopyJQCoverage() {

    $("#lblJQ_Amount").text($("#JQ_Amount").val());
    $("#lblJQ_Ply_No").text($("#JQ_Ply_No").val());
    $("#lblJQ_StartTime").text($('input[name="JQ_StartTime"]').val());
    $("#lblJQ_EndTime").text($('input[name="JQ_EndTime"]').val());

    if ($("#IsTaxInfo").prop("checked")) {
        $("#lblSum_up_tax").text($("#TaxInfo_Sum_up_tax").val());
    }
    else {
        $("#lblSum_up_tax").text(0);
    }
    var converageJson = $.parseJSON($("#CoverageJson").val());
    $("#rowCoverage").html("")
    $("#ItemCoverageTemplate").tmpl(converageJson).appendTo('#rowCoverage');
    $("<tr><td colspan='4'>不计免赔</td><tr>").appendTo('#rowCoverage');

}

function CopySYCoverage() {
    $("#lblSY_Amount").text($("#SY_Amount").val());
    $("#lblSY_Ply_No").text($("#SY_Ply_No").val());
    $("#lblSY_StartTime").text($('input[name="SY_StartTime"]').val());
    $("#lblSY_EndTime").text($('input[name="SY_EndTime"]').val());

    var converageJson = $.parseJSON($("#CoverageJson").val());
    $("#rowCoverage").html("")

    //当险种为空时不显示 
    if (converageJson != null) {

        $.each(converageJson, function (i, t) {
            if (t.Amount == 0) {
                t.Amount = "";
            }
        })

        $("#ItemCoverageTemplate").tmpl(converageJson).appendTo('#rowCoverage');
        $("<tr><td colspan='4'>不计免赔</td><tr>").appendTo('#rowCoverage');

        $("#rowfootcoverage").html("");

        var isFranchiseFlag = false;
        $.each(converageJson, function (i, t) {
            if (t.FranchiseFlag == 1) {
                isFranchiseFlag = true;
                $("#ItemFootCoverageTemplate").tmpl(t).appendTo('#rowfootcoverage');
            }
        })

        if (isFranchiseFlag) {
            $("#lblFranchiseFlag").text($("#txtIOPPremium").val());
        }
        else {
            $("#lblFranchiseFlag").text(0);
        }

    }
}

function CheckDetail() {
    if ($("#Settlement_Amount").val().trim() == "") {
        App.Alert("请填写结算金额");
        return false;
    }
    else {
        if (!IsFloat($("#Settlement_Amount").val())) {
            App.Alert("结算金额填写不正确");
            return false;
        }
    }

    var discountAmount = 0;
    if ($("#Discount_Amount").val().trim() != "") {
        if (IsFloat($("#Discount_Amount").val().trim())) {
            discountAmount = parseFloat($("#Discount_Amount").val().trim());
        }
        else {
            App.Alert("优惠金额填写不正确");
            return false;
        }
    }

    if ($("#rdJQ").attr("checked") != "checked") {
        if (parseFloat($("#SY_Amount").val().trim()) < discountAmount) {
            App.Alert("优惠金额不能大于商业险投保金额，请修改");
            return false;
        }
    }


    if (parseFloat($("#Settlement_Amount").val().trim()) != parseFloat($("#Premium_Amount").val().trim()) - discountAmount) {
        App.Alert("结算金额计算有误，请核对！");
        return false;
    }

    App.Confirm("您确认提交订单么？", function (e) {
        if (e) {

            $("#rdSY,#rdJQ,#rdJS").attr("disabled", false);
            $("#OrderSourceType,#ddlcompany,#MerchantId").attr("disabled", false);

            $("#btnSave").click();
        }

    })
    return true;

}

function SetReadOnly() {

    if ($("#operateType").val() == "1") {
        $("#App_Name,#App_Tel,#InsurerInfo_App_cert_no").attr("readonly", "readonly");
        $("#SY_Amount,#Premium_Amount,#JQ_Amount,#TaxInfo_Sum_up_tax,#Discount_Amount,#Settlement_Amount,#IsTaxInfo").attr("readonly", "readonly");
        $("#rdSY,#rdJQ,#rdJS").attr("disabled", "disabled");
        $("#OrderSourceType,#ddlcompany,#MerchantId").attr("disabled", "disabled");
    }
}