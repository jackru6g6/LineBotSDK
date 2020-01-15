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


///手風琴伸縮卡片元件_訂單管理頁
class AccordionCard extends Component {
    constructor (props) {
        super(props)
        this.state = { Title: '' , ///標題
            Time:'',///訂購時間
            Total:0,///總金額
            Notice:'',///備註
            Menu:'',///訂餐內容
            ID:"",///ID(不可同名)，同名可能造成不可伸縮
            Show:"",///是否顯示( show 是/ '' 否)
            Aria:"true",///是否顯示( true 是/ false 否)
        }
    }

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