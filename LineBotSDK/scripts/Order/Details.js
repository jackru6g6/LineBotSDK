///Liff初始化載入
liff.init({
    liffId: '1589802303-aVoDyNmv'
}).then(() => {
    GetOrder();
}).catch(error => {
    alert("失敗：" + error);
});



function GetOrder() {
    debugger;
    ///liff.getContext().userId
    $.get('/LineBotSDK/Order/GetOrder', { 'uid': "Udaa293df6f3c802cbc2f8ca03c93ceb6" }, function (data) {
        debugger;
        AppendOrder(data);
    });
}

function AppendOrder(data) {
    debugger;
    $(`
        <tr>
            <th scope="row">${data.RestaurantName}</th>
            <td>Mark</td>
            <td>Otto</td>
            <td>1</td>
        </tr>
    `).appendTo($('table tbody'));
}


//function AppendOrder(data) {
//    debugger;
//    const wrapper = document.querySelector('table tbody');

//    mount(new TestM({ RestaurantName: data.RestaurantName }), wrapper)
//}




/* Component */
class Component {
    constructor(props = {}) {
        this.props = props
    }

    setState(state) {
        const oldEl = this.el
        this.state = state
        this.el = this.renderDOM()
        if (this.onStateChange) this.onStateChange(oldEl, this.el)
    }

    renderDOM() {
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
    constructor(props) {
        super(props)
        this.state = { RestaurantName: '' }
    }

    //onClick () {
    //    this.setState({
    //        isLiked: !this.state.isLiked
    //    })
    //}

    render() {
        return `
            <tr>
                <th scope="row">${this.props.RestaurantName}</th>
                <td>Mark</td>
                <td>Otto</td>
                <td>1</td>
            </tr>
        `
    }
}

