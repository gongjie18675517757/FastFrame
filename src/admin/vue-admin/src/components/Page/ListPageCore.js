import {
  getColumns,
  getModuleStrut,
  getQueryOptions
} from "../../generate";
import {
  distinct, throttle
} from '../../utils'

/**
 * 按钮组1
 */
export let makeToolItems = function () {
  return [{
    title: "新增",
    color: "success",
    name: "Add",
    icon: "add",
    action: this.toEdit,
    visible: () => this.ModuleStrut.HasManage
  },
  {
    title: "修改",
    color: "warning",
    name: "Update",
    icon: "edit",
    visible: () => this.ModuleStrut.HasManage,
    disabled: () => {
      let val = this.selection.length != 1;
      return val;
    },
    action: () => this.toEdit(this.selection[0])
  },
  {
    title: "搜索",
    color: "info",
    name: "List",
    key: "Search",
    icon: "search",
    action: this.search
  },
  {
    title: "删除",
    color: "warning",
    name: "Delete",
    icon: "delete",
    visible: () => this.ModuleStrut.HasManage,
    disabled: () => this.selection.length != 1,
    action: () => this.remove(this.selection)
  },
  {
    title: "刷新",
    color: "success",
    name: "List",
    key: "Refresh",
    icon: "refresh",
    action: () => {
      this.query = []
      this.loadList()
    }
  },
  {
    title: "导出",
    color: "basis",
    name: "List",
    key: "export",
    icon: "import_export",
    action: () => alert('未实现')
  }
  ]
}



/**
 * 要导入的依赖
 */
export let pageInjects = []

/**
 * 生成参数
 */
export let pageProps = {
  pars: Object,
  isDialog: Boolean,
  isTab: Boolean,
  superId: String,
  hideToolitem: Boolean
};


/**
 * 数据
 */
export let makePageData = function () {
  return {
    toolItems: makeToolItems.call(this),
    toolSpliceCount: 6,
    columns: [],
    rows: [],
    selection: [],
    total: 0,
    loading: false,
    tableClassArr: [],
    tableStyleObj: {

    },
    ModuleStrut: {},
    pager: null,
    hidePager: false,
    showMamageField: false,
    query: [],
    props: {},
    treeChildComponent: null,
    listeners: {}
  };
}

/**
 * 计算属性
 */
export let pageComputed = {
  dialogMode() {
    return this.$store.state.dialogMode;
  },
  dynamicColumns() {
    return [...this.columns, ...(this.showMamageField && this.ModuleStrut.HasManage ? [{
      Name: 'Create_User.Name',
      Description: '录入人',
      Type: 'String',
      sortable: true
    }, {
      Name: 'CreateTime',
      Description: '录入时间',
      sortable: true,
      Type: 'DateTime',
    },
    {
      Name: 'Modify_User.Name',
      Description: '修改人',
      sortable: true,
      Type: 'String',
    }, {
      Name: 'ModifyTime',
      Description: '修改时间',
      sortable: true,
      Type: 'DateTime',
    }
    ] : [])]
  },
  tableHeight() {
    if (this.superId) {
      return null;
    }
    if (this.isTab) {
      return `calc(100vh - 195px)`
    } else if (this.isDialog) {
      return `calc(100vh - ${300}px)`
    } else {
      return `calc(100vh - 160px)`
    }
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
      .then(arr => this.columns = distinct(arr, v => v.Name, (a, b) => ({
        ...a,
        ...b
      })))
      .then(this.getQueryOptions)
      .then(arr => distinct(arr, v => `${v.Name}${v.compare}`, (a, b) => ({
        ...a, ...b
      }))).then(arr => {
        this.queryOptions = arr.map(v => ({
          ...v,
          value: JSON.parse(JSON.stringify(v.value))
        }));
        this.query = [
          {
            Key: 'and',
            Value: arr
          }
        ]
      })
  },
  getModuleStrut() {
    return getModuleStrut(this.name)
  },
  getColumns() {
    return getColumns(this.name)
  },
  toEdit(model) {
    model = model || {}
    let Id = model.Id || ''
    if (
      this.isDialog ||
      (this.dialogMode  && !this.$vuetify.breakpoint.smAndDown)
    ) {
      let components = this.$router.getMatchedComponents(`/${this.name}/add`);
      if (components.length > 1) {
        this.$message.dialog(components[1], {
          id: Id
        });
      } else {
        this.$message.toast.error('未匹配到页面！')
      }

    } else {
      if (Id)
        this.$router.push(`/${this.name}/${Id}`);
      else
        this.$router.push(`/${this.name}/Add`);
    }
  },
  remove(arr = []) {
    this.$message.confirm({
      title: "提示",
      content: "确认要删除吗?"
    }).then(() => {
      let ids = arr.map(r => r.Id);
      for (const id of ids) {
        let index = this.selection.findIndex(r => r.Id == id);
        this.$http.delete(`/api/${this.name}/delete/${id}`).then(() => {
          this.selection.splice(index, 1);
        });
      }
    });
  },
  getQueryOptions() {
    return getQueryOptions(this.dynamicColumns)
  },
  search() {
    this.$message
      .dialog(() => import('@/components/Dialog/SearchDialog.vue'), {
        title: `查询${this.direction}列表`,
        options: this.query,
        makeOptionsFunc: () => [...this.queryOptions.map(v => ({
          ...v,
          value: JSON.parse(JSON.stringify(v.value))
        }))]
      })
      .then(query => {
        this.query = query;
        this.loadList();
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
      itemsPerPage,
      sortBy,
      sortDesc
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
      PageSize: itemsPerPage,
      SortName: sortBy.join(','),
      SortMode: sortDesc.length > 0 && !sortDesc[0] ? "asc" : "desc",
      Filters: [
        ...this.query,
        {
          Key: 'and',
          Value: [
            ...queryFilter,
            ...(this.superId ? [
              {
                Name: 'super_Id',
                value: this.superId,
                compare: '=='
              }
            ] : [])
          ]
        }
      ].map(kv => {
        return ({
          ...kv,
          Value: kv.Value.filter(v => {
            return Array.isArray(v.value) ? v.value.length > 0 : !!v.value
          }).map(v => ({
            Name: v.Name,
            compare: v.compare,
            value: v.value
          }))
        })
      }).filter(v => v.Value.length > 0)

    };

    return pageInfo;
  },
  loadMoreList: throttle(function (pager) {
    this.loading = true;
    this.loading = true;
    if(pager.page){
      pager.page+=1;
    }
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
        this.rows.push(...Data)
        this.total = Total;
        this.loading = false;
      }).catch(() => {
        this.loading = false;
      })
  }),
  loadList: throttle(function (pager) {
    this.loading = true;
    this.rows = [];
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
        this.rows = Data
        this.total = Total;
        this.loading = false;
      }).catch(() => {
        this.loading = false;
      })
  }, 500),
  formatterToolItems(items) {
    return distinct(items, v => v.key || v.name, (a, b) => ({
      ...a,
      ...b
    }));
  },
  close() {
    if (this.isTab) {
      this.$emit('close')
    }
    else if (this.isDialog) {
      this.$emit('close')
    } else {
      this.$router.got(-1)
    }
  }
}

/**
 * 需要传递的参数
 */
export let makeChildProps = function () {

  return {
    ...this.$props,
    area: this.area,
    moduleName: this.name,
    direction: this.direction + '列表',
    columns: this.dynamicColumns,
    rows: this.rows,
    showMamageField: this.showMamageField,
    total: this.total,
    selection: this.selection,
    loading: this.loading,
    tableClassArr: this.tableClassArr,
    tableStyleObj: this.tableStyleObj,
    toolItems: this.superId ? [] : this.formatterToolItems(this.toolItems),
    toolSpliceCount: this.toolSpliceCount,
    ModuleStrut: this.ModuleStrut,
    isDialog: this.isDialog,
    isTab: this.isTab,
    tableHeight: this.tableHeight,
    singleSelection: this.pars && this.pars.single,
    expandComponent: this.treeChildComponent ? {
      props: ['model'],
      components: {
        'v-tree-child': this.treeChildComponent
      },
      render(h) {
        return h('v-tree-child', {
          props: {
            superId: this.model.Id,
            hideToolitem: true,
            pars: {
              single: true,
            }
          }

        })
      }
    } : null,
    hideToolitem: this.hideToolitem,
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
      this.$emit("success", pars);
    },
    selection_update: val => {
      this.selection = val;
    },
    loadList: this.loadList,
    loadMoreList: this.loadMoreList,
    toolItemClick: item => item.action.call(this, { item, selection: this.selection, rows: this.rows }),
    toEdit: (val) => this.toEdit(val),
    changeShowMamageField: () => this.showMamageField = !this.showMamageField,
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

    this.$eventBus.$on(`${this.name}_update`, this.loadList);
    this.init()
  },
  destroyed() {
    this.$eventBus.$off(`${this.name}_update`, this.loadList);
  }
};

/**
 * 导出基础组件
 */
export default {
  mixins: [ListPageMixin],
  inject: pageInjects,
  props: pageProps,
  data() {
    let data = makePageData.call(this);
    return {
      ...data,
    };
  },
  computed: pageComputed,
  methods: pageMethods,
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);
    return h("v-list-page", {
      props,
      on: listeners
    });
  }
}