import {
  getColumns,
  getModuleStrut,
  getQueryOptions
} from "../../generate";
import {
  distinct, throttle, fmtRequestPars, saveFile
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
    action: this.queryDialog
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
    action: this.exportList
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
  queryFilter: Array,
  single: Boolean,
  isDialog: Boolean,
  isTab: Boolean,
  superId: String,
  hideToolitem: Boolean,
  requestUrl: String
};


/**
 * 数据
 */
export let makePageData = function () {
  return {
    /**
     * 操作项
     */
    toolItems: makeToolItems.call(this),
    /**
     * 操作按钮摆放数据
     */
    toolSpliceCount: 6,
    /**
     * 表格列
     */
    columns: [],
    /**
     * 表格行
     */
    rows: [],
    /**
     * 选中行
     */
    selection: [],
    /**
     * 所有行数
     */
    total: 0,
    /**
     * 加载中
     */
    loading: false,
    /**
     * 表格类名
     */
    tableClassArr: [],
    /**
     * 表格样式
     */
    tableStyleObj: {

    },
    /**
     * 模块结构
     */
    ModuleStrut: {},
    /**
     * 分页属性
     */
    pager: null,
    /**
     * 隐藏分布
     */
    hidePager: false,
    /**
     * 显示审计字段
     */
    showMamageField: false,
    /**
     * 查询条件
     */
    query: [],
    /**
     * 树表格子级组件
     */
    treeChildComponent: null
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
  /**
   * 初始化
   */
  init() {
    Promise.resolve()
      .then(this.getModuleStrut)
      .then(this.fmtModuleStrut)
      .then(strut => this.ModuleStrut = strut)
      .then(this.getColumns)
      .then(this.fmtColumns)
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
  /**
   * 获取当前模块结构
   */
  getModuleStrut() {
    return getModuleStrut(this.name)
  },

  /**
   * 格式化模块结构
   */
  fmtModuleStrut(v) {
    return Promise.resolve(v);
  },

  /**
   * 获取当前模块列表列
   */
  getColumns() {
    return getColumns(this.name)
  },

  /**
   * 格式化列
   */
  fmtColumns(arr) {
    return Promise.resolve(arr).then(arr => {
      return this.getRowOperateItems().then(items => {
        return [
          ...(items.length > 0 ? [
            {
              Name: 'Operate',
              Description: '操作',
              render: (h, context) => {
                return h('div', null, items.map(func => func(h, context)))
              }
            }
          ] : []),
          ...arr
        ]
      })
    })
  },

  /**
   * 获取行操作按钮
   */
  getRowOperateItems() {
    return Promise.resolve([])
  },

  /**
   * 跳转编辑页
   * @param {*} model 
   */
  toEdit(model) {
    model = model || {}
    let Id = model.Id || ''
    if (
      this.isDialog ||
      (this.dialogMode && !this.$vuetify.breakpoint.smAndDown)
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

  /**
   * 删除操作
   * @param {*} arr 
   */
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

  /**
   * 获取查询条件
   */
  getQueryOptions() {
    return getQueryOptions(this.dynamicColumns)
  },

  /**
   * 弹窗查询
   */
  queryDialog() {
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

  /**
   * 获取查询地址
   */
  getRequestUrl() {
    return this.requestUrl || `/api/${this.name}/list`
  },

  /**
   * 获取请求方法
   */
  getRequedtMethod() {
    return (url, pageProps, pars) => {
      let qs = JSON.stringify(pageProps, fmtRequestPars)
      let parsQueryString = Object.entries(pars || {}).map((k, v) => `${k}=${v}`).join('&')
      return this.$http.get(`${url}?qs=${qs}&${parsQueryString}`);
    }
  },

  /**
   * 获取请求参数
   * @param {*} pager 
   */
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
    let queryFilter = this.queryFilter || []

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

  /**
   * 导出EXCEL的地址
   */
  getExportListUrl() {
    return `/api/${this.name}/ExportList`
  },

  /**
   * 加载更新数据(移动端无限翻页时)
   */
  loadMoreList: throttle(function (pager) {
    this.loading = true;
    this.loading = true;
    if (pager.page) {
      pager.page += 1;
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

  /**
   * 加载列表数据
   */
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

  exportList: throttle(function () {
    this.loading = true;
    Promise.resolve(null)
      .then(this.getRequestPars)
      .then(pagePars => {
        let qs = JSON.stringify(pagePars, fmtRequestPars)
        let url = this.getExportListUrl()
        let func = () => this.$http({
          method: "get",
          url: `${url}?qs=${qs}&fileName=${this.direction}`,
          responseType: "blob",
        })
        saveFile(func, `${this.direction}列表.xlsx`, 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
      }).then(() => {
        this.loading = false;
      }).catch(() => {
        this.loading = false;
      })
  }),

  /**
   * 格式化操作按钮
   * @param {*} items 
   */
  fmtToolItems(items) {
    return distinct(items, v => v.key || v.name, (a, b) => ({
      ...a,
      ...b
    }));
  },

  /**
   * 关闭
   */
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
    toolItems: this.superId ? [] : this.fmtToolItems(this.toolItems),
    toolSpliceCount: this.toolSpliceCount,
    ModuleStrut: this.ModuleStrut,
    isDialog: this.isDialog,
    isTab: this.isTab,
    tableHeight: this.tableHeight,
    singleSelection: this.single,
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
            single: true,
          }

        })
      }
    } : null,
    hideToolitem: this.hideToolitem,
  };
};

/**
 * 监听事件
 */
export let makeChildListeners = function () {
  return {
    close: () => this.close(),
    success: () => {
      if (this.isDialog)
        this.$emit("success", this.selection);
    },
    selection_update: val => {
      this.selection = val;
    },
    loadList: this.loadList,
    loadMoreList: this.loadMoreList,
    toolItemClick: item => item.action.call(this, { item, selection: this.selection, rows: this.rows }),
    toEdit: (val) => this.toEdit(val),
    changeShowMamageField: () => this.showMamageField = !this.showMamageField,

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