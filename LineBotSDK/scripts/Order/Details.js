///Liff初始化載入
liff.init({
    liffId: '1589802303-aVoDyNmv'
}).then(() => {

    $("#button-addon1").datepicker();
    
    GetOrder();
}).catch(error => {
    alert("失敗：" + error);
});


///清空菜單卡片區段
function ClearOrderStatus() {
    $('#accordionExample').html('');
}

///取得當天
function GetOrder(date) {
    debugger;
    var para = "uid=Udaa293df6f3c802cbc2f8ca03c93ceb6";
    if(typeof date !== "undefined"){
        para+="&date="+date;
    }

    ClearOrderStatus();
    /////liff.getContext().userId
    $.getJSON("/LineBotSDK/api/OrderAPI/GetOrder?" +para,
            function (data) {
                $.each(data, function(e){
                    let orderList = [];
                    $.each(this.orders, function(){
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
                        Title:this.restaurant , 
                        Time:this.updateDate,
                        Total:this.total,
                        Notice:this.notice,
                        Menu:"<br/>\n" + orderList.join("<br/>\n"),
                        ID:e,
                        Show: e==0? "show" : "",
                        Aria: e==0? "true" : "false",
                    }), wrapper);
                });
            })
            .fail(
            function (xhr, textStatus, err) {
                $('#status').html('Error: ' + err);
            });
}




/* Component */
class Component {
    constructor (props = {}) {
      this.props = props
    }

setState (state) {
        const oldEl = this.el
        this.state = state
        this.el = this.renderDOM()
        if (this.onStateChange) this.onStateChange(oldEl, this.el)
    }

    renderDOM () {
        this.el = createDOMFromString(this.render())
        if (this.onClick) {
            this.el.addEventListener('click', this.onClick.bind(this), false)
        }
        return this.el
    }
}

const createDOMFromString = (domString) => {
    const div = document.createElement('div')
    div.innerHTML = domString
    return div
}

const mount = (component, wrapper) => {
    wrapper.appendChild(component.renderDOM())
    component.onStateChange = (oldEl, newEl) => {
        wrapper.insertBefore(newEl, oldEl)
        wrapper.removeChild(oldEl)
    }
}



class AccordionCard extends Component {
    constructor (props) {
        super(props)
        this.state = { Title: '' , 
            Content:'',
            Time:'',
            Total:0,
            Notice:'',
            Menu:'',
            ID:"",
            Show:"",
            Aria:"true",
        }
    }

    //onClick () {
    //    this.setState({
    //        isLiked: !this.state.isLiked
    //    })
    //}

    render () {
        return `
             <div class="card">
                    <div class="card-header" id="headingOne${this.props.ID}">
                        <h2 class="mb-0">
                            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne${this.props.ID}" aria-expanded="${this.props.Aria}" aria-controls="collapseOne${this.props.ID}">
                                ${this.props.Title}
                            </button>
                        </h2>
                    </div>
                    <div id="collapseOne${this.props.ID}" class="collapse ${this.props.Show}" aria-labelledby="headingOne${this.props.ID}" data-parent="#accordionExample">
                        <div class="card-body">
                            <p>訂購時間：${this.props.Time}</p>
                            <p>總金額：${this.props.Total}</p>
                            <p>備註：${this.props.Notice}</p>
                            <p>內容：${this.props.Menu}</p>
                            
                            <span style="display:flex; justify-content:flex-end; width:100%; padding:0;">
                                 <button type="button" class="btn btn-danger text-right" onclick="CancelOrder('${this.props.Title}','${this.props.Time}')">取消訂單</button>
                            </span>
                        </div>
                    </div>
                </div>
        `
    }
}

function CancelOrder(restaurant,date){
    if(confirm("確實要刪除嗎?")){
        $.ajax({
            url: "/LineBotSDK/api/OrderAPI/Order?uid=Udaa293df6f3c802cbc2f8ca03c93ceb6&restaurant="+restaurant+"&date="+date,
            cache: false,
            type: 'DELETE',
            contentType: 'application/json; charset=utf-8',
            //data: json,
            success: function () { 
                alert("已經刪除！");
                GetOrder(); 
            }
        })
        .fail(
        function (xhr, textStatus, err) {
            $('#status').html('Error: ' + err);
        });

    }
    else{
        alert("已經取消了刪除操作");
    }
}

function SearchOrderByDate(){
    $('#datepicker').val();

    GetOrder($('#datepicker').val());
}