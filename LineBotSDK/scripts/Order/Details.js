alert("Details.js載入");

///Liff初始化載入
liff.init({
    liffId: '1589802303-aVoDyNmv'
}).then(() => {
    //alert(liff.getLanguage());
    //alert(liff.isLoggedIn());
    //alert(liff.getContext().userId);
    GetOrder();
}).catch(error => {
    alert("失敗：" + error);
    //GetOrder();
});

function GetOrder() {
    //alert("GetOrder_Start");
    //$.ajax({
    //    url: '/LineBotSDK/Order/GetOrder',
    //    //cache: false,
    //    //traditional: true,
    //    data: {
    //        uid: "Udaa293df6f3c802cbc2f8ca03c93ceb6",
    //    },
    //    //dataType: 'json',
    //    type: 'GET',
    //    //async: true,
    //    async: false,//不啟用非同步
    //    success: function (data) {
    //        alert("api成功");
    //    }
    //});


    ///liff.getContext().userId
    $.get('/LineBotSDK/Order/GetOrder', { 'uid': "Udaa293df6f3c802cbc2f8ca03c93ceb6" }, function (data) {
        //alert("data：" + data.RestaurantName);
        AppendOrder(data);
    });
}


function AppendOrder(data) {
    debugger;

    const wrapper = document.querySelector('.wrapper');
    
    mount(new TestM({ RestaurantName: data.RestaurantName }), wrapper)

    //$.each(data, function (aaa) {
    //    debugger;
    //    mount(new TestM({ RestaurantName: 'red' }), wrapper)

       

    //});


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



class TestM extends Component {
    constructor (props) {
        super(props)
        this.state = { RestaurantName: '' }
    }

    //onClick () {
    //    this.setState({
    //        isLiked: !this.state.isLiked
    //    })
    //}

    render () {
        return `
             <div data-role="collapsible">
                   <h3>${this.props.RestaurantName}</h3>
                   <p>I'm the collapsible content. By default I'm closed, but you can click the header to open me.</p>
                   <p>2。I'm the collapsible content. By default I'm closed, but you can click the header to open me.</p>
               </div>
        `
    }
}
