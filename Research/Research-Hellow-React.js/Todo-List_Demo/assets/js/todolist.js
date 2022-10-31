//參考 : https://codepen.io/gaearon/pen/LzWZvb?editors=0010
//參考 : https://github.com/johch3n611u/Side-Project-Hellow-Vue.js/blob/master/Todo-List_Demo/index.html#L50

class TodoListHead extends React.Component {
    render() {
        return (
            <div className="todo-list-head title-bar">
                <div className="todo-list-heading title-bar-text">React.js TodoList Demo：今日代辦項目</div>
            </div>
        );
    }
}

class TodoListDisplaySort extends React.Component {
    constructor(props) {
        super(props);
        // 為了讓 `this` 能在 callback 中被使用，這裡的綁定是必要的：
        this.handleListdisplay = this.handleListdisplay.bind(this);
    }
    handleListdisplay(e) {
        // console.log(e.target.value);
        this.props.onSort(e.target.value);
    }
    render() {

        const FackData = this.props.FackData;
        const allCount = FackData.todos.length;
        const completedCount = Object.keys(FackData.todos).filter(function (value) { return FackData.todos[value].isCompleted }).length;
        const incompleteCount = Object.keys(FackData.todos).filter(function (value) { return !FackData.todos[value].isCompleted }).length;

        return (
            <div>
                <button onClick={this.handleListdisplay} value='show_all'>全部 ( {allCount}} )</button>
                <button onClick={this.handleListdisplay} value='show_incomplete'>已完成 ( {completedCount} )</button>
                <button onClick={this.handleListdisplay} value='show_completed'>未完成 ( {incompleteCount} )</button>
            </div>
        );
    }
}

class TodoListNewTodo extends React.Component {
    constructor(props) {
        super(props);
        // 為了讓 `this` 能在 callback 中被使用，這裡的綁定是必要的：
        this.handleAddtodo = this.handleAddtodo.bind(this);
    }
    handleAddtodo(e) {
        if (e.key === 'Enter') {
            this.props.addTodo(e.target.value);
        }
    }
    render() {
        return (
            <div className="todo-list-new-todo">
                <label >增加新項目：</label>
                <input className="new-todo" id="new-todo" placeholder="例如：澆花。Enter ↲ 鍵入" onKeyPress={this.handleAddtodo} />
            </div>
        );
    }
}

class TodoListTodos extends React.Component {
    constructor(props) {
        super(props);
        // 為了讓 `this` 能在 callback 中被使用，這裡的綁定是必要的：
        this.handleDeleteli = this.handleDeleteli.bind(this);
        this.handleCheckbox = this.handleCheckbox.bind(this);
        this.handleEditmode = this.handleEditmode.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    handleDeleteli(e) {
        this.props.onDeleteli(e.target.id);
    }

    handleCheckbox(e) {
        this.props.onCheckbox(e.target.id);
    }

    handleEditmode(e) {
        // console.log('handleEditmode:' + e.target.id);
        this.props.onEditmode(e.target.id);
    }
    // https://stackoverflow.com/questions/27827234/how-to-handle-the-onkeypress-event-in-reactjs
    handleEdit(e) {
        // console.log('key' + e.key);
        // console.log('key' + e.target.id);
        if (e.key === 'Enter') {
            this.props.onEdit(e.target.id, e.target.value);
        }
    }

    render() {
        // https://zh-hant.reactjs.org/docs/components-and-props.html
        // https://zh-hant.reactjs.org/docs/lists-and-keys.html
        // map() 方法會建立一個新的陣列，其內容為原陣列的每一個元素經由回呼函式運算後所回傳的結果之集合。
        // https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Array/map
        // https://zh-hant.reactjs.org/docs/thinking-in-react.html#step-2-build-a-static-version-in-react

        const liItem = [];
        var TagMake = "";
        var ClassMake = "";

        // console.log(this.props.FackData.todos);

        this.props.FackData.todos.forEach((FackData, index) => {
            // https://medium.com/@realdennis/array-%E5%8E%9F%E5%9E%8B%E7%9A%84-foreach-%E6%9C%89%E5%A4%9A%E5%A5%BD%E7%94%A8-%E5%AD%B8%E6%9C%83%E9%AB%98%E9%9A%8E%E5%87%BD%E6%95%B8%E4%B9%8B%E5%BE%8C%E9%83%BD%E4%B8%8D%E6%83%B3%E5%AF%AB-javascript-%E4%BB%A5%E5%A4%96%E7%9A%84%E7%A8%8B%E5%BC%8F%E8%AA%9E%E8%A8%80%E4%BA%86-dc4b594a045a
            //console.log(index)
            if (FackData.isEdit == false) {
                if (FackData.isCompleted == true) {
                    ClassMake = 'completed';
                } else {
                    ClassMake = '';
                }
                TagMake =
                    <div>
                        <label className={ClassMake}>
                            <input
                                id={index}
                                className="chkbox"
                                type="checkbox"
                                defaultChecked={FackData.isCompleted}
                                onClick={this.handleCheckbox} />
                            {FackData.title}
                        </label>
                        <span className="todo-list-todos-controller">
                            <button id={index} onClick={this.handleEditmode}>編輯</button>
                            <button id={index} onClick={this.handleDeleteli}>刪除</button>
                        </span >
                    </div>;
            } else {
                TagMake =
                    <div>
                        {/* https://stackoverflow.com/questions/27827234/how-to-handle-the-onkeypress-event-in-reactjs */}
                        {/* https://stackoverflow.com/questions/43556212/failed-form-proptype-you-provided-a-value-prop-to-a-form-field-without-an-on */}
                        <input id={index} type="text" defaultValue={FackData.title} onKeyPress={this.handleEdit} />
                        <span className="todo-list-todos-controller">
                            <button id={index} onClick={this.handleDeleteli}>刪除</button>
                        </span>
                    </div>
                    ;
            }
            liItem.push(
                <li key={index} className="todo-item">
                    {TagMake}
                </li>
            )
        });
        return (
            <ul className="todo-list-todos">
                {liItem}
            </ul >
        );
    }
}

class TodoListBody extends React.Component {
    constructor(props) {
        super(props);
        this.state = FackData;
        // 為了讓 `this` 能在 callback 中被使用，這裡的綁定是必要的：
        this.handleDeleteli = this.handleDeleteli.bind(this);
        this.handleCheckbox = this.handleCheckbox.bind(this);
        this.handleEditmode = this.handleEditmode.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
        this.handleAddtodo = this.handleAddtodo.bind(this);
        this.handleListdisplay = this.handleListdisplay.bind(this);
    }

    // 這裡與傳遞到子元件的命名是一樣的只差在子元件模板那要注入事件到子元件，再由子元件反饋監聽物件到父元件這做處理。
    handleDeleteli(id) {
        // console.log(id);
        // console.log(this.state);
        this.state.todos.splice(id, 1);
        // console.log(this.state.todos.filter(id => id = iad));
        this.setState(this.state);
    }

    handleCheckbox(id) {
        // console.log(id);
        if (this.state.todos[id].isCompleted == false) {
            this.state.todos[id].isCompleted = true;
        } else {
            this.state.todos[id].isCompleted = false;
        }
        // console.log(this.state);
        this.setState(this.state);
    }

    handleEditmode(id) {
        // console.log('handleEditmode2:' + id);
        // console.log(this.state);
        if (this.state.todos[id].isEdit == false) {
            this.state.todos[id].isEdit = true;
        } else {
            this.state.todos[id].isEdit = false;
        }
        // console.log(this.state);
        this.setState(this.state);
    }

    // https://stackoverflow.com/questions/27827234/how-to-handle-the-onkeypress-event-in-reactjs
    handleEdit(id, title) {
        // console.log(id);
        // console.log(title);
        // console.log(this.state);
        this.state.todos[id].title = title;
        this.state.todos[id].isEdit = false;
        // console.log(this.state);
        this.setState(this.state);
    }

    handleAddtodo(title) {
        // console.log('handleAddtodo:' + title);
        const newid = this.state.todos.length + 1;
        const newitem = { id: newid, title: title, isEdit: false, isCompleted: false, }
        this.state.todos.push(newitem);
        this.setState(this.state);
    }

    handleListdisplay(whattodo) {
        console.log('whattodo:' + whattodo);
        // 轉換顯示類別 1. 分類出 全部 / 已完成 / 未完成
        this.state = this.getTodosMode(this.state.todos, whattodo);
        this.setState(this.state);
    }

    getTodosMode(showallTodos, whattodo) {
        var showcompletedTodos = [];
        var showincompleteTodos = [];
        // newFackData 結構 1. todos 當作 prop 渲染用，依照 whattodo 動態置換 2. showallTodos , showcompletedTodos , showincompleteTodos
        // show_all / show_incomplete / show_completed
        var newFackData = {};
        for (var index in this.state.todos) {
            if (this.state.todos[index].isCompleted === true) {
                //show_completed
                showcompletedTodos.push(this.state.todos[index]);
            } else {//show_incomplete
                showincompleteTodos.push(this.state.todos[index]);
            }
        }
        // console.log(showcompletedTodos);
        // console.log(showincompleteTodos);
        // console.log(whattodo);

        if (whattodo === 'show_all') {
            newFackData = {
                todos: showallTodos,
                showallTodos: showallTodos,
                showcompletedTodos: showcompletedTodos,
                showincompleteTodos: showincompleteTodos
            }
        } else if (whattodo === 'show_completed') {
            newFackData = {
                todos: showcompletedTodos,
                showallTodos: showallTodos,
                showcompletedTodos: showcompletedTodos,
                showincompleteTodos: showincompleteTodos
            }
        } else {
            newFackData = {
                todos: showincompleteTodos,
                showallTodos: showallTodos,
                showcompletedTodos: showcompletedTodos,
                showincompleteTodos: showincompleteTodos
            }
        }
        console.log(newFackData)
        return newFackData;
    }

    render() {
        return (
            <div>
                <TodoListNewTodo addTodo={this.handleAddtodo} />
                {/* https://zh-hant.reactjs.org/docs/components-and-props.html */}
                <TodoListTodos
                    onDeleteli={this.handleDeleteli}
                    onCheckbox={this.handleCheckbox}
                    onEditmode={this.handleEditmode}
                    onEdit={this.handleEdit}
                    FackData={this.state} />
                <TodoListDisplaySort onSort={this.handleListdisplay} FackData={this.state} />
            </div>
        );
    }
}

const FackData = {
    todos: [{
        id: 1,
        title: '吃早餐',
        isEdit: false,
        isCompleted: false,
    }, {
        id: 2,
        title: '起床',
        isEdit: false,
        isCompleted: false,
    }, {
        id: 3,
        title: '打開電腦',
        isEdit: false,
        isCompleted: true,
    }],
};

ReactDOM.render(
    <div className="todo-list window">
        <TodoListHead />
        <TodoListBody className="todo-list-display-sort" />
    </div>,
    document.getElementById('todo-list')
);