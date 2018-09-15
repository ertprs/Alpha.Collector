
var RECH_TYPE = {
    "onlinePayment": '在线充值',
    "weixin": '微信转账',
    "alipay": '支付宝转账',
    "cft": '财付通转账',
    "bankTransfer": '银行卡转账',
    "adminAddMoney": '系统加钱',
    "duoma": '多码合一'
};

$.each($(".PaySpanType"), function (index, item) {
    var paySpanType = $(this).html();
    $(this).html(RECH_TYPE[paySpanType]);
});
