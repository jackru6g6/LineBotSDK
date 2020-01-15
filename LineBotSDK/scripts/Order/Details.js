///Liff初始化載入
liff.init({
    liffId: '1589802303-aVoDyNmv'
}).then(() => {
    GetOrder();
}).catch(error => {
});


///清空菜單卡片區段
function ClearOrderStatus() {
    $('#accordionExample').html('');
}

///查詢訂單，並附加於畫面
function GetOrder(date) {
    var para = "uid=" + "Udaa293df6f3c802cbc2f8ca03c93ceb6";//liff.getContext().userId;
    if (typeof date !== "undefined") {
        para += "&date=" + date;
    }

    $.getJSON("/LineBotSDK/api/OrderAPI/GetOrder?" + para,
            function (data) {
                ClearOrderStatus();

                $.each(data, function (e) {
                    let orderList = [];
                    $.each(this.orders, function () {
                        orderList.push(
                            `
                                   &emsp;&emsp;${this.name} /
                                   $${this.price} /
                                   ${this.count}份
                               `
                            );
                    })

                    const wrapper = document.querySelector('div#accordionExample');
                    mount(new AccordionCard({
                        Title: this.restaurant,
                        Time: this.updateDate,
                        Total: this.total,
                        Notice: this.notice,
                        Menu: "<br/>\n" + orderList.join("<br/>\n"),
                        ID: e,
                        Show: e == 0 ? "show" : "",
                        Aria: e == 0 ? "true" : "false",
                    }), wrapper);
                });
            })
            .fail(
            function (xhr, textStatus, err) {
                $('#status').html('Error: ' + err);
            });
}




function CancelOrder(restaurant, date) {
    if (confirm("確實要刪除嗎?")) {
        $.ajax({
            url: "/LineBotSDK/api/OrderAPI/Order?uid=Udaa293df6f3c802cbc2f8ca03c93ceb6&restaurant=" + restaurant + "&date=" + date,
            cache: false,
            type: 'DELETE',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                alert("已刪除訂單！");
                GetOrder();
            }
        })
        .fail(
        function (xhr, textStatus, err) {
            $('#status').html('Error: ' + err);
        });
    }
    else {
        alert("已經取消了刪除操作");
    }
}

///搜尋鈕動作
function SearchOrderByDate() {
    var searchDate = $('#datepicker').val();
    if (dateValidationCheck(searchDate)) {
        GetOrder(searchDate);
    }
}


function dateValidationCheck(str) {
    var re = new RegExp("^([0-9]{4})[./]{1}([0-9]{1,2})[./]{1}([0-9]{1,2})$");
    var strDataValue;
    var infoValidation = true;

    if ((strDataValue = re.exec(str)) != null) {
        var i;
        i = parseFloat(strDataValue[1]);
        if (i <= 0 || i > 9999) { // 年
            infoValidation = false;
        }
        i = parseFloat(strDataValue[2]);
        if (i <= 0 || i > 12) { // 月
            infoValidation = false;
        }
        i = parseFloat(strDataValue[3]);
        if (i <= 0 || i > 31) { // 日
            infoValidation = false;
        }
    } else {
        infoValidation = false;
    }

    if (!infoValidation) {
        alert('請輸入 YYYY/MM/DD 日期格式');
    }
    return infoValidation;
}