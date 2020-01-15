///購物車物件
var _ShoppingCart = [];

///Liff初始化載入
liff.init({
    liffId: '1589802303-WbBnDeNR'
}).then(() => {

}).catch(error => {
});

///訂購鈕跳出視窗_事件
$(function () {
    $('[href="#nav-profile"]').on('click', function () {
        RefalshShoppingCartViewContent();
    });
});


///訂購鈕跳出視窗_事件
$(function () {
    $('#shoppingCartModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);// Button that triggered the modal
        var name = button.data('name');//取得菜名 // Extract info from data-* attributes
        var price = button.data('price');//取得單價
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.

        var modal = $(this);
        modal.find('.modal-title').text('購物車')
        modal.find('.modal-body input[id="dish-name"]').val(name);
        modal.find('.modal-body input[id="dish-price"]').val(price);

        ///清空資料
        modal.find('.modal-body input[id="order-count"]').val("");
        modal.find('.modal-body textarea[id="order-notice"]').val("");
    })
});


///[加入]購物車按鈕
function AddShoppingCart(element) {
    debugger;
    if (!isRealNum(Number($('#order-count').val()))) {
        alert("請輸入正整數");
        return false;
    }

    var order = {
        "name": $('#dish-name').val(),
        "price": Number($('#dish-price').val()),
        "count": Number($('#order-count').val()),
        "notice": $('#order-notice').val(),
    };

    PushShoppingCartArray(order);
    RefalshShoppingCartCountShow();

    $('#shoppingCartModal').modal('hide');
}

///檢查數量是否為數字
function CheckAddItem(num) {
    if (!isNaN(num)) {
        alert('是數字');
    }
    else {
        alert('你輸入的資料不是數字');
    }
}

function isRealNum(val) { // isNaN函數 把空串 空格 以及NUll 按照0來處理 所以先去除 
    if (val === "" || val == null) {
        return false;
    }
    if (!isNaN(val)) {
        return true;
    }
    else {
        return false;
    }
}

///新增餐點進購物車
function PushShoppingCartArray(data) {
    ///比對是否有相同訂單，有覆蓋，沒有則新增
    var orders = _ShoppingCart.filter(t=>t.name == data.name && t.price == data.price);
    if (orders.length == 0) {
        _ShoppingCart.push(data);
    }
    else {
        orders[0].count = data.count;
        orders[0].notice = data.notice;
    }
}

///更新購物車顯示筆數
function RefalshShoppingCartCountShow() {
    var count = 0;
    count = _ShoppingCart.map(t=>t.count).reduce((a, b) =>a + b, 0);
    $("#shoppingCartCount").html(count);
}



function RefalshShoppingCartViewContent() {
    debugger;
    var appendHtml = "";
    $("#nav-profile tbody").html("");

    _ShoppingCart.forEach(function (e) {
        debugger;
        appendHtml += `
            <tr>
                <th scope="row">${e.name}</th>
                <td>${e.price}</td>
                <td>${e.count}</td>
                <td>${e.notice}</td>
            </tr>
            `;
    });

    $("#nav-profile tbody").html(appendHtml);
}