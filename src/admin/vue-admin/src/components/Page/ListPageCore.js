
import { getDownLoadPath } from "../../config";
import {
  getColumns,
  getModuleStrut,
  getQueryOptions
} from "../../generate";
import {
  distinct, throttle, fmtRequestPars, saveFile, getIconFunc, toHexString
} from '../../utils'
import { makeButtons, makeButtonsInputMode } from './handleFlow'

/**
 * 按钮组1
 */
export let makeToolItems = function () {
  return [{
    title: "新增",
    color: "success",
    name: "Add",
    iconName: "add",
    action: this.toEdit,
    visible: () => this.ModuleStrut.HasManage
  },
  // {
  //   title: "修改",
  //   color: "warning",
  //   name: "Update",
  //   iconName: "edit",
  //   visible: () => this.ModuleStrut.HasManage,
  //   disabled: () => {
  //     let val = this.selection.length != 1;
  //     return val;
  //   },
  //   action: () => this.toEdit(this.selection[0])
  // },
  {
    title: "搜索",
    color: "info",
    name: "List",
    key: "Search",
    iconName: "search",
    action: this.queryDialog
  },
  // {
  //   title: "删除",
  //   color: "warning",
  //   name: "Delete",
  //   iconName: "delete",
  //   visible: () => this.ModuleStrut.HasManage,
  //   disabled: () => this.selection.length != 1,
  //   action: () => this.remove(this.selection)
  // },
  {
    title: "刷新",
    color: "success",
    name: "List",
    key: "Refresh",
    iconName: "refresh",
    action: () => {
      // this.resetQuery();
      this.loadList()
    }
  },
  {
    title: "导出",
    color: "primary",
    name: "List",
    key: "export",
    iconName: "mdi-export",
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
     * 模块名称，影响：请求地址
     */
    name: null,

    /**
     * 页面结构名：影响：获取列，获取查询方式
     */
    strutName: null,

    /**
     * 权限名称：影响页面权限定义
     */
    permissionName: null,

    /**
     * 操作按钮
     */
    toolItems: [],

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
    treeChildComponent: null,

    /**
     * 是否初始化完成
     */
    isInited: false,

    /**
     * 选中的树结点
     */
    treeSelectedItem: null,
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
  },
  childProps() {
    return makeChildProps.call(this)
  },
  childListeners() {
    return makeChildListeners.call(this)
  }
}

/**
 * 页面方法
 */
export let pageMethods = {
  /**
   * 获取列表的标题
   * @returns 
   */
  getPageTitle() {
    return this.direction + '列表'
  },
  /**
   * 初始化
   */
  async init() {
    /**
     * 未单独定义结构名时
     */
    this.strutName = this.strutName || this.name;

    /**
     * 未单独定义权限名时
     */
    this.permissionName = this.permissionName || this.name;

    /**
     * 标记未初始化
     */
    this.isInited = false;

    /**
     * 加载模块结构
     */
    this.ModuleStrut = await this.fmtModuleStrut(await this.getModuleStrut());

    /**
     * 生成工具条
     */
    this.toolItems = await this.getToolItems();

    /**
     * 生成列
     */
    this.columns = distinct(await this.fmtColumns(await this.getColumns()), v => v.Name, (a, b) => ({
      ...a,
      ...b
    }));

    /**
     * 生成查询项
     */
    this.queryOptions = distinct(await this.getQueryOptions(), v => `${v.Name}${v.compare}`, (a, b) => ({
      ...a, ...b
    }));

    this.resetQuery();

    /**
     * 标记加载完成
     */
    this.isInited = true;
  },
  /**
   * 重置查询条件
   */
  resetQuery() {
    this.query = {
      Key: 'and',
      Value: [
        this.queryOptions.map(v => ({
          ...v,
          value: v.value ? JSON.parse(JSON.stringify(v.value)) : null
        }))
      ]
    }
  },

  /**
   * 获取当前模块结构
   */
  getModuleStrut() {
    return getModuleStrut(this.strutName)
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
    return getColumns(this.strutName)
  },

  /**
   * 格式化列
   */
  async fmtColumns(arr) {
    let items = await this.getRowOperateItems();

    for (const c of arr) {
      if (c.IsLink) {
        c.renderFunc = (h, { text, model }) => h('a', {
          on: {
            click: () => {
              event.stopPropagation();
              this.toEdit(model);
            }
          }
        }, text)
      }
    }

    return [
      ...(items.length > 0 ? [
        {
          Name: 'Operate',
          Description: '操作',
          renderFunc: (h, context) => {
            return h('div', null, items.map(func => func(h, context)))
          }
        }
      ] : []),
      ...arr,
      ...(this.ModuleStrut.HasFiles ? [
        {
          Name: 'Files',
          Description: '附件',
          renderFunc: (h, { value }) => {
            return value && value.length > 0 ? h('v-menu', {
              scopedSlots: {
                activator: props => h('a', {
                  on: props.on,

                }, '查看')
              }
            }, [
              h('v-list', {
                props: {
                  dense: true
                }
              }, value.map(v => h('v-list-item', {
                key: v.Id,
                on: {
                  click: () => {
                    window.open(getDownLoadPath(v.Id, v.Name))
                  }
                }
              }, [
                h('v-list-item-avatar', null, [
                  h('img', {
                    attrs: {
                      src: getIconFunc(v)
                    }
                  })
                ]),
                h('v-list-item-content', null, v.Name)
              ])))
            ]) : null
          }
        }
      ] : [])
    ]
  },

  /**
   * 获取操作按钮
   */
  async getToolItems() {
    let arr = await makeToolItems.call(this);
    let brr = this.ModuleStrut.HaveCheck ? [
      {
        component: {
          functional: true,
          render: h => h('fragments-facatory', null, makeButtons({
            selection: this.selection,
            mode: makeButtonsInputMode.LIST,
            moduleName: this.name,
            btnAttrs: {
            }
          }).map(v => h(v)))
        }
      }
    ] : [];

    return [...brr, ...arr].map(v => {
      /**
       * 未定义权限，但有定义name值时
       */
      let { permission, name } = v;
      if (!permission && name) {
        permission = `${this.permissionName}.${name}`
      }
      if (permission && !Array.isArray(permission)) {
        permission = [permission]
      }
      return {
        ...v,
        permission
      };
    }
    );
  },

  /**
   * 获取行操作按钮
   */
  getRowOperateItems() {
    return Promise.resolve([
      (h, { model }) => {
        return this.ModuleStrut.HasManage && !this.columns.some(v => v.IsLink) ? h('v-btn', {
          props: {
            text: true,
            'x-small': true,
            color: 'primary'
          },
          on: {
            click: () => {
              this.toEdit(model)
            }
          }
        }, '编辑') : null
      },
      (h, { model }) => {
        return this.ModuleStrut.HasManage ? h('v-btn', {
          props: {
            text: true,
            'x-small': true,
            color: 'primary'
          },
          on: {
            click: () => {
              this.remove([model])
            }
          }
        }, '删除') : null
      },
      (h, { model }) => {
        return this.ModuleStrut.HaveCheck ? h('fragments-facatory', null, makeButtons({
          selection: [model],
          mode: makeButtonsInputMode.CELL,
          moduleName: this.name,
          btnAttrs: {
            // text: true,
            // 'x-small': true,
          }
        }).map(v => h(v))) : null
      }
    ])
  },

  /**
   * 传递给表单页的参数
   * @returns 
   */
  getFormPageParsBySelectedTreeItem(v) {
    return {
      super_id: v.Id || ""
    };
  },

  /**
   * 传递给表单页的参数
   * @returns 
   */
  getFormPagePars() {
    if (this.treeSelectedItem) {
      return this.getFormPageParsBySelectedTreeItem(this.treeSelectedItem);
    }
    else {
      return Promise.resolve({})
    }
  },

  /**
   * 跳转编辑页
   * @param {*} model 
   */
  async toEdit(model) {
    let pars = await this.getFormPagePars() || {};
    model = model || {};
    let Id = model.Id || ''
    if (
      this.isDialog ||
      (this.dialogMode && !this.$vuetify.breakpoint.smAndDown)
    ) {
      let components = this.$router.getMatchedComponents(`/${this.name}/add`);
      if (components.length > 1) {
        await this.$message.dialog(components[1], {
          id: Id,
          width: '1024px',
          ...pars,
          pars
        });
        this.loadList();
      } else {
        this.$message.toast.error('未匹配到页面！')
      }

    } else {
      let urlPars = Object.entries(pars).map(([k, v]) => `${k}=${v}`).join('&')
      if (Id)
        this.$router.push(`/${this.name}/${Id}?${urlPars}`);
      else
        this.$router.push(`/${this.name}/Add?${urlPars}`);
    }
  },

  /**
   * 删除操作
   * @param {*} arr 
   */
  async remove(arr = []) {
    await this.$message.confirm({
      title: "提示",
      content: "确认要删除吗?"
    })
    let ids = arr.map(r => r.Id);
    for (const id of ids) {
      let index = this.selection.findIndex(r => r.Id == id);
      await this.$http.delete(`/api/${this.name}/delete/${id}`)
      this.selection.splice(index, 1);

      index = this.rows.findIndex(r => r.Id == id);
      this.rows.splice(index, 1);
    }
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
      .dialog(() => import('../Dialog/SearchDialog.vue'), {
        title: `查询${this.direction}列表`,
        options: this.query,
        makeOptionsFunc: () => [...this.queryOptions.map(v => ({
          ...v,
          value: v.value ? JSON.parse(JSON.stringify(v.value)) : null
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

      let qs = JSON.stringify(pageProps, fmtRequestPars);
      // console.log(qs);
      if (process.env.NODE_ENV != 'development') {
        qs = toHexString(qs)
      }
      let parsQueryString = Object.entries(pars || {}).map((k, v) => `${k}=${v}`).join('&')
      return this.$http.get(`${url}?qs=${qs}&${parsQueryString}`);
    }
  },

  /**
   * 获取树键名
   * @returns 
   */
  getTreeKey() {
    return 'Super_Id'
  },

  /**
   * 根据选中的树结点，生成查询参数
   * @returns 
   */
  async getRequestParsBySelectedTreeItem(v) {
    let treeKey = await this.getTreeKey(v);
    return [
      {
        Name: treeKey,
        compare: "==",
        value: v.id,
      },
    ]
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

    var filters = [
      {
        ...this.query,
      },
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
    ];
    filters = JSON.parse(JSON.stringify(filters));

    for (const f of filters) {
      for (let index = 0; index < f.Value.length; index++) {
        const arr = f.Value[index];
        for (const v of arr) {
          if (Array.isArray(v.value))
            v.value = v.value.join(',')
        }

        f.Value[index] = arr.filter(x => !!x.value)
      }

      f.Value = f.Value.filter(arr => arr.length)
    }

    let pageInfo = {
      PageIndex: page,
      PageSize: itemsPerPage,
      SortName: sortBy.join(','),
      SortMode: sortDesc.length > 0 && !sortDesc[0] ? "asc" : "desc",
      Filters: filters.filter(v => v.Value.length > 0)
    };




    if (this.treeSelectedItem) {
      let arr = await this.getRequestParsBySelectedTreeItem(this.treeSelectedItem);

      if (arr.length > 0) {
        pageInfo.Filters.push({
          key: "and",
          value: arr
        })
      }
    }


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
        // console.log(pagePars);
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
      }).catch((err) => {
        this.loading = false;
        window.console.error(err);
      })
  }, 500),

  /**
   * 导出
   */
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
  },

  /**
   * 树节点被选中
   * @param {*} val 
   */
  handleTreeItemActived(val) {
    this.treeSelectedItem = val;

    this.$nextTick(this.loadList)
  }
}

/**
 * 需要传递的参数
 */
export let makeChildProps = function () {

  return {
    ...this.$props,
    moduleName: this.name,
    direction: this.getPageTitle(this.treeSelectedItem),
    columns: this.dynamicColumns,
    rows: this.rows,
    showMamageField: this.showMamageField,
    total: this.total,
    selection: this.selection,
    loading: this.loading,
    tableClassArr: this.tableClassArr,
    tableStyleObj: this.tableStyleObj,
    toolItems: this.superId ? [] : this.toolItems,
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
    treeComponent: this.treeComponent ? {
      functional: true,
      render: (h) => {
        return h(this.treeComponent, {
          props: {
            height: this.tableHeight
          },
          on: {
            input: this.handleTreeItemActived
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
  watch: {},
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);
    return this.isInited ? h("v-list-page", {
      props,
      on: listeners
    }) : null;
  }
}