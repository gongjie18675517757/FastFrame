
import { getDownLoadPath } from "../../config";
import {
  getColumns,
  getModuleStrut,
  getQueryOptions
} from "../../generate";
import {
  distinct, throttle, fmtRequestPars, saveFile, getIconFunc, toHexString, queryBuild
} from '../../utils'
import { cloneDeep } from 'lodash'
import { makeButtons, makeButtonsInputMode } from './handleFlow'
import message from "../Message";

/**
 * 按钮组1
 */
export const makeToolItems = function () {
  return [{
    title: "新增",
    color: "success",
    name: "Add",
    iconName: "add",
    action: this.toEdit,
    visible: () => this.ModuleStrut.HasManage,
    group: 0
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
    action: this.queryDialog,
    group: 1,
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
    },
    group: 1,
  },
  {
    title: "导出",
    color: "primary",
    name: "List",
    key: "export",
    iconName: "mdi-export",
    action: this.exportList,
    group: 1,
  }
  ]
}



/**
 * 要导入的依赖
 */
export const pageInjects = []

/**
 * 生成参数
 */
export const pageProps = {
  /**
   * 过滤参数
   */
  queryFilter: Array,

  /**
   * 是否单选
   */
  single: Boolean,

  /**
   * 是否弹窗
   */
  isDialog: Boolean,

  /**
   * 是否多页签下
   */
  isTab: Boolean,

  /**
   * 上级ID
   */
  superId: String,

  /**
   * 隐藏工具条按钮
   */
  hideToolItems: Boolean,

  /**
   * 请求地址
   */
  requestUrl: String
};

/**
 * 页面数据的名称定义
 */
export const PageDataDefines = {
  /**
   * 模块名称，影响：请求地址
   */
  name: 'name',

  /**
   * 页面结构名：影响：获取列，获取查询方式
   */
  strutName: 'strutName',

  /**
   * 权限名称：影响页面权限定义
   */
  permissionName: 'permissionName',

  /**
   * 操作按钮
   */
  toolItems: 'toolItems',

  /**
   * 操作按钮摆放数据
   */
  toolSpliceCount: 'toolSpliceCount',

  /**
   * 表格列
   */
  columns: 'columns',
  /**
   * 表格行
   */
  rows: 'rows',
  /**
   * 选中行
   */
  selection: 'selection',
  /**
   * 所有行数
   */
  total: 'total',
  /**
   * 加载中
   */
  loading: 'loading',
  /**
   * 表格类名
   */
  tableClassArr: 'tableClassArr',
  /**
   * 表格样式
   */
  tableStyleObj: 'tableStyleObj',
  /**
   * 模块结构
   */
  ModuleStrut: 'ModuleStrut',
  /**
   * 分页属性
   */
  pager: 'pager',
  /**
   * 隐藏分页
   */
  hidePager: 'hidePager',
  /**
   * 显示审计字段
   */
  showMamageField: 'showMamageField',
  /**
   * 查询条件
   */
  query: 'query',
  /**
   * 树表格子级组件
   */
  treeChildComponent: 'treeChildComponent',

  /**
   * 是否初始化完成
   */
  isInited: 'isInited',

  /**
   * 选中的树结点
   */
  treeSelectedItem: 'treeSelectedItem',

  /**
   * 树组件
   */
  treeComponent: 'treeComponent'
}

/**
 * 数据
 */
export const makePageData = function () {
  return {
    [PageDataDefines.name]: null,
    [PageDataDefines.strutName]: null,
    [PageDataDefines.permissionName]: null,
    [PageDataDefines.toolItems]: [],
    [PageDataDefines.toolSpliceCount]: 6,
    [PageDataDefines.columns]: [],
    [PageDataDefines.rows]: [],
    [PageDataDefines.selection]: [],
    [PageDataDefines.total]: 0,
    [PageDataDefines.loading]: false,
    [PageDataDefines.tableClassArr]: [],
    [PageDataDefines.tableStyleObj]: {},
    [PageDataDefines.ModuleStrut]: {},
    [PageDataDefines.pager]: null,
    [PageDataDefines.hidePager]: false,
    [PageDataDefines.showMamageField]: false,
    [PageDataDefines.query]: [],
    [PageDataDefines.treeChildComponent]: null,
    [PageDataDefines.isInited]: false,
    [PageDataDefines.treeSelectedItem]: null,
    [PageDataDefines.treeComponent]: null,
  };
}

/**
 * 页面计算属性的名称定义
 */
export const PageComputedDefines = {
  /**
   * 是否弹窗模式
   */
  dialogMode: 'dialogMode',

  /**
   * 计算要显示的列
   */
  dynamicColumns: 'dynamicColumns',

  /**
   * 计算个个表格高度
   */
  tableHeight: 'tableHeight',

  /**
   * 传递给布局页的内容
   */
  childProps: 'childProps',

  /**
   * 监听布局页的内容
   */
  childListeners: 'childListeners',
}

/**
 * 计算属性
 */
export let pageComputed = {
  [PageComputedDefines.dialogMode]() {
    return this.$store.state.dialogMode;
  },
  [PageComputedDefines.dynamicColumns]() {
    return [...this.columns, ...(this.showMamageField && this.ModuleStrut.HasManage ? [{
      Name: 'Create_User_Value',
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
      Name: 'Modify_User_Value',
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
  [PageComputedDefines.tableHeight]() {
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
  [PageComputedDefines.childProps]() {
    return makeChildProps.call(this)
  },
  [PageComputedDefines.childListeners]() {
    return makeChildListeners.call(this)
  }
}


/**
 * 页面所有方法的名称定义
 */
export const PageMethodsDefines = {
  /**
   * 获取列表的标题
   */
  getPageTitle: "getPageTitle",

  /**
   * 初始化
   */
  init: "init",

  /**
   * 重置查询条件
   */
  resetQuery: "resetQuery",

  /**
   * 获取当前模块结构
   */
  getModuleStrut: "getModuleStrut",

  /**
   * 格式化模块结构
   */
  fmtModuleStrut: "fmtModuleStrut",

  /**
   * 获取当前模块列表列
   */
  getColumns: "getColumns",

  /**
   * 格式化列
   */
  fmtColumns: "fmtColumns",

  /**
   * 获取操作按钮
   */
  getToolItems: "getToolItems",

  /**
   * 获取行操作按钮
   */
  getRowOperateItems: "getRowOperateItems",

  /**
   * 传递给表单页的参数
   */
  getFormPageParsBySelectedTreeItem: "getFormPageParsBySelectedTreeItem",

  /**
   * 传递给表单页的参数
   */
  getFormPagePars: "getFormPagePars",

  /**
   * 跳转编辑页
   */
  toEdit: "toEdit",

  /**
   * 删除操作
   */
  remove: "remove",

  /**
   * 获取查询条件
   */
  getQueryOptions: "getQueryOptions",

  /**
   * 弹窗查询
   */
  queryDialog: "queryDialog",

  /**
   * 获取查询地址
   */
  getRequestUrl: "getRequestUrl",

  /**
   * 获取请求方法
   */
  getRequedtMethod: "getRequedtMethod",

  /**
   * 获取树键名
   */
  getTreeKey: "getTreeKey",

  /**
   * 根据选中的树结点，生成查询参数
   */
  getRequestParsBySelectedTreeItem: "getRequestParsBySelectedTreeItem",

  /**
   * 构造查询参数
   */
  buildQueryFilter: "buildQueryFilter",

  /**
   * 获取请求参数
   */
  getRequestPars: "getRequestPars",

  /**
   * 导出EXCEL的地址
   */
  getExportListUrl: "getExportListUrl",

  /**
   * 加载更新数据(移动端无限翻页时)
   */
  loadMoreList: "loadMoreList",

  /**
   * 加载列表数据
   */
  loadList: "loadList",

  /**
   * 导出
   */
  exportList: "exportList",

  /**
   * 关闭
   */
  close: "close",

  /**
   * 树节点被选中
   */
  handleTreeItemActived: "handleTreeItemActived",
};

/**
 * 页面方法
 */
export let pageMethods = {
  [PageMethodsDefines.getPageTitle]() {
    return this.direction
  },
  async [PageMethodsDefines.init]() {
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
  [PageMethodsDefines.resetQuery]() {
    this.query = this.queryOptions.map(v => ({
      ...v,
      value: v.value ? JSON.parse(JSON.stringify(v.value)) : null
    }))

  },
  [PageMethodsDefines.getModuleStrut]() {
    return getModuleStrut(this.strutName)
  },
  [PageMethodsDefines.fmtModuleStrut](v) {
    return Promise.resolve(v);
  },
  [PageMethodsDefines.getColumns]() {
    return getColumns(this.strutName)
  },
  async [PageMethodsDefines.fmtColumns](arr) {
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
  async [PageMethodsDefines.getToolItems]() {
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

    return [...arr, ...brr,].map(v => {
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
    });
    //.groupBy(v=>v.group).createObject(v=>v.key,v=>v.values);
  },
  [PageMethodsDefines.getRowOperateItems]() {
    const arr = [];
    if (this.ModuleStrut.HasManage && !this.columns.some(v => v.IsLink)) {
      arr.push((function (h, { model }) {
        return h('v-btn', {
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
        }, '编辑')
      }).bind(this))
    }

    if (this.ModuleStrut.HasManage) {
      arr.push((function (h, { model }) {
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
      }).bind(this))
    }

    if (this.ModuleStrut.HaveCheck) {
      arr.push((function (h, { model }) {
        return this.ModuleStrut.HaveCheck ? h('fragments-facatory', null, makeButtons({
          selection: [model],
          mode: makeButtonsInputMode.CELL,
          moduleName: this.name,
          btnAttrs: {
            // text: true,
            // 'x-small': true,
          }
        }).map(v => h(v))) : null
      }).bind(this))
    }

    return Promise.resolve(arr)
  },
  [PageMethodsDefines.getFormPageParsBySelectedTreeItem](v) {
    return {
      super_id: v.Id || "",
      super_name: v.Value || ''
    };
  },
  [PageMethodsDefines.getFormPagePars]() {
    if (this.treeSelectedItem) {
      return this.getFormPageParsBySelectedTreeItem(this.treeSelectedItem);
    }
    else {
      return Promise.resolve({})
    }
  },
  async [PageMethodsDefines.toEdit](model) {
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
  async [PageMethodsDefines.remove](arr = []) {
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
  [PageMethodsDefines.getQueryOptions]() {
    return getQueryOptions(this.dynamicColumns)
  },
  [PageMethodsDefines.queryDialog]() {
    this.$message
      .dialog(() => import('../Dialog/SearchDialog.vue'), {
        title: `查询${this.direction}列表`,
        value: cloneDeep(this.query),
        makeOptionsFunc: () => this.queryOptions.map(v => ({
          ...v,
          value: v.value ? JSON.parse(JSON.stringify(v.value)) : null
        }))
      })
      .then(query => {
        this.query = query;
        this.loadList();
      });
  },
  [PageMethodsDefines.getRequestUrl]() {
    return this.requestUrl || `/api/${this.name}/list`
  },
  [PageMethodsDefines.getRequedtMethod]() {
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
  [PageMethodsDefines.getTreeKey]() {
    return 'Super_Id'
  },
  async [PageMethodsDefines.getRequestParsBySelectedTreeItem](v) {
    let treeKey = await this.getTreeKey(v);
    return [
      {
        Name: treeKey,
        compare: "==",
        value: v.id,
      },
    ]
  },
  async [PageMethodsDefines.buildQueryFilter]() {
    let qb = []
    /**
     * 外部传入的条件
     */
    qb.push(...(this.queryFilter || []))

    /**
     * 查询框的条件
     */
    qb.push(...this.query);

    /**
     * 选择树的ID
     */
    if (this.superId)
      qb.push({
        Name: 'super_Id',
        value: this.superId,
        compare: '=='
      })

    /**
     * 树条件
     */
    if (this.treeSelectedItem) {
      let arr = await this.getRequestParsBySelectedTreeItem(this.treeSelectedItem);

      if (arr.length > 0) {
        qb.push(...arr)
      }
    }

    const querys = queryBuild(JSON.parse(JSON.stringify(qb)));
    return querys;
  },
  async [PageMethodsDefines.getRequestPars](pager) {
    if (pager) this.pager = pager;
    else pager = this.pager;

    let {
      page,
      itemsPerPage,
      sortBy,
      sortDesc
    } = pager;

    const qb = await this.buildQueryFilter();

    let pageInfo = {
      PageIndex: page,
      PageSize: itemsPerPage,
      SortName: sortBy.join(','),
      SortMode: sortDesc.length > 0 && !sortDesc[0] ? "desc" : "asc",
      Filters: qb
    };

    return pageInfo;
  },
  [PageMethodsDefines.getExportListUrl]() {
    return `/api/${this.name}/ExportList`
  },
  [PageMethodsDefines.loadMoreList]: throttle(function (pager) {
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
  [PageMethodsDefines.loadList]: throttle(async function (pager) {
    this.loading = true;
    this.rows = [];

    try {
      const pagePars = await this.getRequestPars(pager);
      let method = this.getRequedtMethod();
      let url = this.getRequestUrl();

      let { Total, Data } = await method(url, pagePars);
      this.rows = Data
      this.total = Total;
    } catch (error) {
      window.console.error(error);
    } finally {
      this.loading = false;
    }
  }, 500),
  [PageMethodsDefines.exportList]: throttle(async function () {
    await message.confirm({
      title:'提示',
      content:'导出可能会耗时较多,是否继续?'
    })
    try {
      this.loading = true;
      const pagePars = await this.getRequestPars();
      let qs = JSON.stringify(pagePars, fmtRequestPars)
      let url = await this.getExportListUrl()
      let func = () => this.$http({
        method: "get",
        url: `${url}?qs=${qs}&fileName=${this.direction}`,
        responseType: "blob",
      })
      saveFile(func, `${this.direction}列表.xlsx`, 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    } catch (error) {
      window.console.error(error)
    } finally {
      this.loading = false;
    }
  }),
  [PageMethodsDefines.close]() {
    console.log(1);
    if (this.isTab) {
      this.$emit('close')
    }
    else if (this.isDialog) {
      this.$emit('close')
    } else {
      this.$router.got(-1)
    }
  },
  [PageMethodsDefines.handleTreeItemActived](val) {
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
            hideToolItems: true,
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
    hideToolItems: this.hideToolItems,
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

// console.log(JSON.stringify(Object.fromEntries(Object.entries(pageMethods).map(([k]) => [k,k]))));


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