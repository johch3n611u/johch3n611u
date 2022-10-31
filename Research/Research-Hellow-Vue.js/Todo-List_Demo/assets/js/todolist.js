        Vue.component('todo-item', {
            template: '\
    <li class="todo-item">\
      <input v-bind:id="todo.id" type="checkbox" v-on:change="updateStatus(todo)" :checked="todo.isCompleted">\
      <label v-bind:for="todo.id" v-if="!todo.isEdit" v-bind:class="[todo.isCompleted ? \'completed\' : \'\']">{{ todo.title }}</label>\
      <input type="text" v-if="todo.isEdit" v-on:keyup.enter="updateTodo($event, todo)" v-model="todo.title" />\
      <span class="todo-list-todos-controller">\
      <button v-if="!todo.isEdit" v-on:click="editTodo(todo)">編輯</button>\
      <button v-on:click="remove(index)">刪除</button>\
      </span>\
    </li>\
  ',
            props: ['todo', 'index', 'title'],
            methods: {
                remove: function(index) {
                    this.$emit('remove');
                },
                editTodo: function(todo) {
                    todo.isEdit = !todo.isEdit;
                },
                updateTodo: function($event, todo) {
                    if ($event.target.value) {
                        todo.text = $event.target.value;
                    }
                    todo.isEdit = !todo.isEdit;
                },
                updateStatus: function(todo) {
                    todo.isCompleted = !todo.isCompleted;
                },
            }
        })

        var app = new Vue({
            el: '#todo-list',
            data: {
                newTodoText: '',
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
                nextTodoId: 4,
                filter: 'show_all'
            },
            methods: {
                addNewTodo: function() {
                    this.todos.push({
                        id: this.nextTodoId++,
                        title: this.newTodoText,
                        isEdit: false,
                        isCompleted: false,
                    })
                    this.newTodoText = ''
                },
                setFilter: function(filter) {
                    this.filter = filter;
                },
                _getTodos: function(isCompleted) {
                    var list = {};

                    for (var index in this.todos) {
                        if (this.todos[index].isCompleted === isCompleted) {
                            list[index] = this.todos[index];
                        }
                    }
                    return list;
                },
            },
            computed: {
                allCount: function() {
                    return Object.keys(this.todos).length;
                },
                completedCount: function() {
                    var _this = this;

                    return Object.keys(this.todos).filter(function(value) {
                        return _this.todos[value].isCompleted
                    }).length;
                },
                incompleteCount: function() {
                    var _this = this;

                    return Object.keys(this.todos).filter(function(value) {
                        return !_this.todos[value].isCompleted
                    }).length;
                },
                list: function() {
                    if (this.filter === 'show_all') {
                        return this.todos;
                    } else if (this.filter === 'show_completed') {
                        return this._getTodos(true);
                    } else { //show_incomplete
                        return this._getTodos(false);
                    }
                },
            }
        })