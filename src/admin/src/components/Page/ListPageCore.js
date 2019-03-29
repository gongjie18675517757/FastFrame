 import {
   getColumns,
   getModuleStrut,
   getQueryOptions
 } from "@/generate";


 /**
  * 按钮组1
  */
 export let curdToolItems = [{
     title: "新增",
     color: "success",
     name: "Add",
     icon: "add"
   },
   {
     title: "修改",
     color: "warning",
     name: "Update",
     icon: "edit",
     disabled({
       selection
     }) {
       return selection.length != 1;
     }
   }
 ];

 /**
  * 按钮组2
  */
 export let baseToolItems = [{
     title: "搜索",
     color: "info",
     name: "List",
     key: "Search",
     icon: "search"
   },
   {
     title: "删除",
     color: "warning",
     name: "Delete",
     icon: "delete",
     disabled({
       selection
     }) {
       return selection.length == 0;
     }
   },
   {
     title: "刷新",
     color: "success",
     name: "List",
     key: "Refresh",
     icon: "refresh"
   }
 ];

 /**
  * 数据
  */
 export let data = {
   curdToolItems,
   baseToolItems,
   columns: [],
   rows: [],
   selection: [],
   total: 0,
   loading: false,
   tableClassArr: ['elevation-1', 'fixed-header', 'v-table__overflow'],
   tableStyleObj: {
     'max-height': 'calc(100vh - 140px)',
     'backface-visibility': 'hidden'
   },
   ModuleStrut: {},
   pager: null,
   hidePager: false,
   query: [],
   props: {},
   listeners: {}
 };

 /**
  * 需要传递的参数
  */
 export let pageProps = function () {
   return {
     ...this.$props,
     area: this.area,
     name: this.name,
     direction: this.direction,
     columns: this.columns,
     rows: this.rows,
     total: this.total,
     selection: this.selection,
     loading: this.loading,
     tableClassArr: this.tableClassArr,
     tableStyleObj: this.tableStyleObj,
     baseToolItems: [
       ...(this.ModuleStrut.HasManage ? this.curdToolItems : []),
       ...this.baseToolItems
     ].map(r => ({
       ...r,
       moduleName: this.name
     })),
     childToolItems: [...this.childToolItems].map(r => ({
       ...r,
       moduleName: this.name
     })),
     ModuleStrut: this.ModuleStrut,
     isDialog: !!this.success,
     singleSelection: this.pars && this.pars.single,
     ...this.props
   };
 };

 /**
  * 监听事件
  */
 export let pageListeners = function () {
   return {
     close: () => this.close(),
     success: () => {
       let pars = this.selection;
       this.emit("success", pars);
       if (typeof this.success == "function") this.success(pars);
     },
     selection_update: val => (this.selection = val),
     loadList: this.loadList,
     toEdit: (val) => {
       this.toEdit(val)
     },
     Add_toolBtnClick: () => this.toEdit(),
     Update_toolBtnClick: () => this.toEdit(this.selection[0]),
     Delete_toolBtnClick: () => this.remove(this.selection),
     Search_toolBtnClick: this.search,
     Refresh_toolBtnClick: () => {
       this.query = []
       this.loadList()
     },
     ...this.listeners
   };
 };


 /**
  * 组件基础混入
  */
 export let ListPageMixin = {
   inject: ["reload"],
   components: {
     "v-list-page": () => import("@/components/Page/ListPage.vue")
   },
   props: {
     success: Function,
     close: Function,
     pars: Object
   },
   computed: {
     dialogMode() {
       return this.$store.state.dialogMode;
     }
   },
   mounted() {
     this.$eventBus.$on(`${this.name}_DataAdded`, this.DataAdded);
     this.$eventBus.$on(`${this.name}_DataUpdated`, this.DataUpdated);
     this.$eventBus.$on(`${this.name}_DataDeleted`, this.DataDeleted);

     let strutPromise = this.getModuleStrut()
     if (strutPromise instanceof Promise) {
       strutPromise.then(ModuleStrut => {
         this.ModuleStrut = ModuleStrut;
       })
     } else {
       this.ModuleStrut = strutPromise;
     }

     let colPromise = this.getColumns()
     if (colPromise instanceof Promise) {
       colPromise.then(cols => {
         this.columns = cols;
       });
     } else {
       this.columns = colPromise;
     }
   },
   destroyed() {
     this.$eventBus.$off(`${this.name}_DataAdded`, this.DataAdded);
     this.$eventBus.$off(`${this.name}_DataUpdated`, this.DataUpdated);
     this.$eventBus.$off(`${this.name}_DataDeleted`, this.DataDeleted);
   },
   methods: {
     getModuleStrut() {
       return getModuleStrut(this.name)
     },
     getColumns() {
       return getColumns(this.name)
     },
     frmColumns() {

     },
     DataDeleted(id) {
       this.$nextTick(() => {
         let index = this.items.findIndex(r => r.Id == id);
         if (index >= 0) this.items.splice(index, 1);
       });
     },
     DataUpdated(item) {
       let index = this.items.findIndex(r => r.Id == item.Id);
       if (index >= 0) this.items.splice(index, 1, item);
     },
     DataAdded(item) {
       this.items.splice(0, 0, item);
     },
     toEdit({
       Id = ""
     } = {}) {
       if (
         this.success ||
         this.dialogMode ||
         this.$vuetify.breakpoint.smAndDown
       ) {
         this.$message.dialog(`${this.name}_Add`, {
           id: Id
         });
       } else {
         this.$router.push(`/${this.name}/add?q=${Id}`);
       }
     },
     remove(arr = []) {
       this.$message.confirm("提示", "确认要删除吗?").then(() => {
         let ids = arr.map(r => r.Id);
         for (const id of ids) {
           let index = this.selection.findIndex(r => r.Id == id);
           this.$http.delete(`/api/${this.name}/delete/${id}`).then(() => {
             this.selection.splice(index, 1);
           });
         }
       });
     },
     search() {
       getQueryOptions(this.name).then(options => {
         for (const item of this.query) {
           let opt = options.find(
             r => r.Name == item.Name && r.compare == item.compare
           );
           if (opt) {
             opt.value = item.value;
           }
         }

         this.$message
           .dialog(() => import('@/components/Dialog/SearchDialog.vue'), {
             title: `查询${this.direction}列表`,
             options
           })
           .then(query => {
             this.query = query;
             this.loadList();
           });
       });
     },
     async loadList(pager) {
       if (pager) this.pager = pager;
       else pager = this.pager;
       this.loading = true;
       let {
         page,
         rowsPerPage,
         sortBy,
         descending
       } = pager;

       /*条件1 */
       let {
         queryFilter = []
       } = this.pars || {};
       if (typeof queryFilter == "function") {
         queryFilter = await queryFilter.call(this, {
           selection: this.selection,
           rows: this.items
         });
       }

       let pageInfo = {
         PageIndex: page,
         PageSize: rowsPerPage,
         Condition: {
           Filters: [...queryFilter, ...this.query]
         },
         SortInfo: {
           Name: sortBy || "Id",
           Mode: descending ? "asc" : "desc"
         }
       };
       try {
         let {
           Total,
           Data
         } = await this.$http.post(
           "/api/" + this.name + "/list",
           pageInfo
         );
         this.rows = Data;
         this.total = Total;
       } finally {
         this.loading = false;
       }
     }
   }
 };