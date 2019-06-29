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
   },
   {
     title: "导出",
     color: "basis",
     name: "List",
     key: "export",
     icon: "import_export"
   }
 ];

 /**
  * 要导入的依赖
  */
 export let pageInjects = ['reload']

 /**
  * 生成参数
  */
 export let pageProps = {
   success: Function,
   close: Function,
   pars: Object,
   isDialog: Boolean
 };


 /**
  * 数据
  */
 export let makePageData = function () {
   return {
     curdToolItems,
     baseToolItems,
     childToolItems: [],
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
     showMamageField: false,
     query: [],
     props: {},
     listeners: {}
   };
 }

 /**
  * 计算属性
  */
 export let pageComputed = {
   dialogMode() {
     return this.$store.state.dialogMode;
   }
 }

 /**
  * 页面方法
  */
 export let pageMethods = {
   init() {
     Promise.resolve()
       .then(this.getModuleStrut)
       .then(strut => this.ModuleStrut = strut)
       .then(this.getColumns)
       .then(cols => this.columns = cols)
     //  .then(this.loadList)
   },
   getModuleStrut() {
     return getModuleStrut(this.name)
   },
   getColumns() {
     return getColumns(this.name)
   },
   DataDeleted(id) {
     this.$nextTick(() => {
       let index = this.rows.findIndex(r => r.Id == id);
       if (index >= 0) this.rows.splice(index, 1);
     });
   },
   DataUpdated(item) {
     let index = this.rows.findIndex(r => r.Id == item.Id);
     if (index >= 0) this.rows.splice(index, 1, item);
   },
   DataAdded(item) {
     this.rows.splice(0, 0, item);
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
   getRequestUrl() {
     return `/api/${this.name}/list`
   },
   getRequedtMethod() {
     return this.$http.post
   },
   async getRequestPars(pager) {
     if (pager) this.pager = pager;
     else pager = this.pager;
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

     return pageInfo;
   },
   loadList(pager) {
     this.loading = true;
     Promise.resolve(pager)
       .then(this.getRequestPars)
       .then(pagePars => {
         let method = this.getRequedtMethod()
         let url = this.getRequestUrl()
         return method(url, pagePars)
       }).then(({
         Total,
         Data
       }) => {
         this.rows = Data;
         this.total = Total;
         this.loading = false;
       }).catch(() => {
         this.loading = false;
       })
   }
 }

 /**
  * 需要传递的参数
  */
 export let makeChildProps = function () {
   return {
     ...this.$props,
     area: this.area,
     name: this.name,
     direction: this.direction + '列表',
     columns: [...this.columns, ...(this.showMamageField && this.ModuleStrut.HasManage ? [{
         Name: 'Create_User.Name',
         Description: '录入人',
         sortable: true
       }, {
         Name: 'CreateTime',
         Description: '录入时间',
         sortable: true
       },
       {
         Name: 'Modify_User.Name',
         Description: '修改人',
         sortable: true
       }, {
         Name: 'ModifyTime',
         Description: '修改时间',
         sortable: true
       }
     ] : [])],
     rows: this.rows,
     showMamageField: this.showMamageField,
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
 export let makeChildListeners = function () {
   return {
     close: () => this.close(),
     success: () => {
       let pars = this.selection;
       this.emit("success", pars);
       if (typeof this.success == "function") this.success(pars);
     },
     selection_update: val => (this.selection = val),
     loadList: this.loadList,
     toEdit: (val) => this.toEdit(val),
     changeShowMamageField: () => this.showMamageField = !this.showMamageField,
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
   components: {
     "v-list-page": () => import("@/components/Page/ListPage.vue")
   },
   mounted() {
     this.$eventBus.$on(`${this.name}_DataAdded`, this.DataAdded);
     this.$eventBus.$on(`${this.name}_DataUpdated`, this.DataUpdated);
     this.$eventBus.$on(`${this.name}_DataDeleted`, this.DataDeleted);
     this.init()
   },
   destroyed() {
     this.$eventBus.$off(`${this.name}_DataAdded`, this.DataAdded);
     this.$eventBus.$off(`${this.name}_DataUpdated`, this.DataUpdated);
     this.$eventBus.$off(`${this.name}_DataDeleted`, this.DataDeleted);
   }
 };