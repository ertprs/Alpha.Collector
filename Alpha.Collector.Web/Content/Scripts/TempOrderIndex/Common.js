

//匹配身份证(15位或18位)
function checkIDCard(str) {
    //身份证正则表达式(15位) 
   // isIDCard1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
   // //身份证正则表达式(18位)

   //// isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/;

   // isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
   // //验证身份证，返回结果 

    // return (isIDCard1.test(str) || isIDCard2.test(str));

    if (IsNullOrEmpty(str)) {
        return false;
    }

    var pattern = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    return pattern.test(str);
}

// 判断输入是否是一个整数
//function IsInt(str) {
//    var result = str.match(/^(-|\+)?\d+$/);
//    if (result == null) return false;
//    return true;
//}

 function IsInt(str) {
    var reg = /^[0-9]*$/;
    return reg.test(str);
}

function IsFloat(str) {
    if (IsInt(str))
        return true;
    //var reg = /^[0-9]+\.{0,1}[0-9]{0,2}$/;
    var reg = /^\d+(\.\d+)?$/
    return reg.test(str);
}

//手机号码
function  checkMobile(str) {
   // var re = /^1\d{10}$/
    var reg = /^1(3|4|5|7|8)\d{9}$/;
    return reg.test(str);
}


// 车架号验证
function isRackNo(rackNo) {
    if (IsNullOrEmpty(rackNo)) {
        return false;
    }
    var pattern = /^[a-zA-Z0-9]{17}$/;

    return pattern.test(rackNo);
}

// 发动机号验证
function isEngineNo(engineNo) {
    if (IsNullOrEmpty(engineNo)) {
        return false;
    }
    var pattern = /^[a-zA-Z0-9]{4,20}$/;

    return pattern.test(engineNo);
}

//车牌号校验
function isVehicleNumber(vehicleNumber) {
    var result = false;
    if (vehicleNumber.length == 7) {
        var express = /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$/;
        result = express.test(vehicleNumber);
    }
    return result;
}

// 是否为空
function IsNullOrEmpty(str) {
    if (str == undefined || str == "" || str == null) {
        return true;
    }

    return false;
}