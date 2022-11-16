import {

  groupBy,
  selectMany,
  distinct
} from "@/utils";

import {
  getDefaultModel,
  getModelObjectItems,
  getRules,
  getModuleStrut
} from "../../generate";

import { FileDetailTable } from "../Table";
import { makeButtons, makeButtonsInputMode } from './handleFlow'
import FlowStep from './FlowStep.vue'
import { calcInputVisible, calcInputItemCols } from '../Inputs'

/**
 * 要注入的方法名
 */
export const formInject = []

/**
 * 生成工具按钮
 * @returns 
 */
export function makeToolItems() {
  return [
    {
      title: "编辑",
      name: "Update",
      key: 'edit',
      iconName: "edit",
      outlined: true,
      action: this.handleEdit,
      visible: () => this.updateBtnVisible
    },
    {
      title: "关闭",
      color: "warning",
      name: "close",
      iconName: "keyboard_return",
      permission: [],
      action: this.close,
      visible: true,
      outlined: true,
    },
    {
      title: "保存",
      color: "primary",
      name: "Update,Add",
      key: 'save',
      iconName: "mdi-content-save-edit-outline",

      action: this.submit,
      loading: this.submiting,
      visible: () => this.hasManage && this.canEdit,
      disabled: () => false
    },
  ]
}

/**
 * 生成参数
 */
export const formProps = {
  pars: Object,
  isDialog: Boolean,
  isTab: Boolean,
  id: String,
  super_id: String,
};

/**
 * 所有data的定义
 */
export const FormPageDataDefines = {
  /**
  * 模块名称，影响：请求地址
  */
  name: 'name',

  /**
   * 页面结构名：影响：获取表单相关
   */
  strutName: 'strutName',

  /**
   * 权限名称：影响页面权限定义
   */
  permissionName: 'permissionName',

  /**
   * 是否可编辑
   */
  canEdit: 'canEdit',

  /**
   * 表单模型实体
   */
  model: 'model',

  /**
   * 表单验证异常信息
   */
  formErrorMessages: 'formErrorMessages',

  /**
   * 页面表单项
   */
  options: 'options',

  /**
   * 表单验证规则
   */
  rules: 'rules',

  /**
   * 审计字段
   */
  manageOptions: 'manageOptions',

  /**
   * 是否提交中
   */
  submiting: 'submiting',

  /**
   * 是否单选布局
   */
  singleLine: 'singleLine',

  /**
   * 是否显示管理字段
   */
  showMamageField: 'showMamageField',

  /**
   * 页面是否有改动过
   */
  changed: 'changed',

  /**
   * 是否有审核字段
   */
  hasManage: 'hasManage',

  /**
   * 是否有附件
   */
  hasFiles: 'hasFiles',

  /**
   * 工具按钮
   */
  toolItems: 'toolItems',

  /**
    * 几列式布局
    */
  pageFormCols: 'pageFormCols',

  /**
   * 页面布局
   */
  pageFlex: 'pageFlex'
}

/**
 * 生成数据
 */
export const formData = {
  [FormPageDataDefines.name]: null,
  [FormPageDataDefines.strutName]: null,
  [FormPageDataDefines.permissionName]: null,
  [FormPageDataDefines.canEdit]: false,
  [FormPageDataDefines.model]: {},
  [FormPageDataDefines.formErrorMessages]: {},
  [FormPageDataDefines.options]: [],
  [FormPageDataDefines.rules]: {},
  [FormPageDataDefines.manageOptions]: [{
    Name: "Create_User.Name",
    Description: "创建人"
  },
  {
    Name: "CreateTime",
    Description: "创建时间"
  },
  {
    Name: "Modify_User.Name",
    Description: "最后修改人"
  },
  {
    Name: "ModifyTime",
    Description: "最后修改时间"
  }
  ].map(r => {
    return {
      Name: r.Name,
      Type: "String",
      Description: r.Description,
      Readonly: "All",
      GroupNames: ["管理字段"]
    };
  }),
  [FormPageDataDefines.submiting]: false,
  [FormPageDataDefines.singleLine]: false,
  [FormPageDataDefines.showMamageField]: false,
  [FormPageDataDefines.changed]: false,
  [FormPageDataDefines.hasManage]: false,
  [FormPageDataDefines.hasFiles]: false,
  [FormPageDataDefines.toolItems]: [],
  [FormPageDataDefines.pageFormCols]: 2,
  [FormPageDataDefines.pageFlex]: {

    /**
     * > 1904px*
     */
    xl: 10,

    /**
     * 1264px > < 1904px*
     */
    lg: 10,

    /**
     * 960px > < 1264px*
     */
    md: 12,

    /**
     * 600px > < 960px
     */
    sm: 12,

    /**
     * 列数
     */
    cols: 12
  }
};


/**
 * 页面所有方法的定义
 */
export const FormPageMethodsDefines = {
  /**
   * 页面初始化
   */
  init: "init",

  /**
   * 加载模块信息
   */
  getModuleStrut: "getModuleStrut",

  /**
   * 格式化模块信息
   */
  fmtModuleStrut: "fmtModuleStrut",

  /**
   * 加载表单验证信息
   */
  getRules: "getRules",

  /**
   * 格式化表单验证信息
   */
  fmtRules: "fmtRules",

  /**
   * 加载表单项
   */
  getModelObjectItems: "getModelObjectItems",

  /**
   * 加载工具条
   */
  getToolItems: "getToolItems",

  /**
   * 格式化表单
   */
  fmtModelObjectItems: "fmtModelObjectItems",

  /**
   * 点击编辑按钮时
   */
  handleEdit: "handleEdit",

  /**
   * 当前内容被更新时
   */
  DataUpdated: "DataUpdated",

  /**
   * 当前内容被删除时
   */
  DataDeleted: "DataDeleted",

  /**
   * 获取请求数据的URL
   */
  getRequestUrl: "getRequestUrl",

  /**
   * 获取页面模型
   */
  getModelObject: "getModelObject",

  /**
   * 格式化页面模型
   */
  fmtModelObject: "fmtModelObject",

  /**
   * 验证规则
   */
  evalRule: "evalRule",

  /**
   * 验证全部规则
   */
  evalRules: "evalRules",

  /**
   * 获取提交方法
   */
  getPostMethod: "getPostMethod",

  /**
   * 获取提交地址
   */
  getPostUrl: "getPostUrl",

  /**
   * 获取提交数据
   */
  getPostData: "getPostData",

  /**
   * submit
   */
  submit: "submit",

  /**
   * onSaveAfter
   */
  onSaveAfter: "onSaveAfter",

  /**
   * 返回列表
   */
  goList: "goList",

  /**
   * 关闭页面
   */
  close: "close",
}

/**
 * 页面方法
 */
export const formMethods = {
  async [FormPageMethodsDefines.init]() {
    this.canEdit = false;
    /**
   * 未单独定义结构名时
   */
    this.strutName = this.strutName || this.name;

    /**
     * 未单独定义权限名时
     */
    this.permissionName = this.permissionName || this.name;

    let obj = await this.getModuleStrut(this.strutName)
    this.hasManage = obj.HasManage;
    this.hasFiles = obj.HasFiles;
    this.haveCheck = obj.HaveCheck;

    this.rules = await this.fmtRules(await this.getRules());
    let model = await this.fmtModelObject(await this.getModelObject());
    let formErrs = {};
    for (const name of Object.keys(model)) {
      formErrs[name] = [];
    }
    this.formErrorMessages = formErrs;
    this.model = model;

    this.options = distinct(await this.fmtModelObjectItems(await this.getModelObjectItems()), v => v.Name, (a, b) => ({ ...a, ...b }));
    this.toolItems = await this.getToolItems();

    this.changed = false;
    this.canEdit = !this.model.Id;
  },
  [FormPageMethodsDefines.getModuleStrut](name) {
    return getModuleStrut(name);
  },
  [FormPageMethodsDefines.fmtModuleStrut](obj) {
    return Promise.resolve(obj);
  },
  [FormPageMethodsDefines.getRules]() {
    return getRules(this.strutName)
  },
  [FormPageMethodsDefines.fmtRules](rules) {
    return Promise.resolve(rules);
  },
  [FormPageMethodsDefines.getModelObjectItems]() {
    return getModelObjectItems(this.strutName).then(arr => {

      arr.forEach(v => {
        if (v.Relate) {
          v.requestUrl = `/api/${this.name}/${v.Relate}List`
        }
      })
      return arr;
    })
  },
  async [FormPageMethodsDefines.getToolItems]() {
    let arr = await makeToolItems.call(this);
    let brr = this.haveCheck ? [
      {
        component: {
          functional: true,
          render: (h) => h('fragments-facatory', null, makeButtons({
            selection: [this.model],
            mode: makeButtonsInputMode.FORM,
            editing: this.canEdit,
            moduleName: this.name
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
        permission = name.split(',').map(v => `${this.permissionName}.${v}`)
      }

      if (permission && !Array.isArray(permission)) {
        permission = [permission]
      }
      return {
        ...v,
        permission
      };
    })
  },
  [FormPageMethodsDefines.fmtModelObjectItems](arr) {
    return Promise.resolve(arr).then(arr => {
      if (this.hasFiles) {
        arr.push({
          Name: 'Files',
          Description: '附件列表',
          GroupNames: ['附件列表'],
          template: FileDetailTable(),
          fileKey: null,
        })
      }

      if (this.haveCheck) {
        arr.push({
          Name: 'StepList',
          Description: '流程步骤',
          GroupNames: ['流程步骤'],
          template: FlowStep,
        })
      }
      return [
        ...arr
      ]
    });
  },
  [FormPageMethodsDefines.handleEdit]() {
    this.canEdit = !this.canEdit
  },
  [FormPageMethodsDefines.DataUpdated]() {

  },
  [FormPageMethodsDefines.DataDeleted]() {
    this.$message.alert({
      title: "提示",
      content: "当前内容已被其它人删除!"
    }).then(() => {
      this.close();
    });
  },
  [FormPageMethodsDefines.getRequestUrl](id) {
    return `/api/${this.name}/get/${id}`
  },
  [FormPageMethodsDefines.getModelObject]() {
    let id = this.id
    if (id) {
      return this.$http.get(this.getRequestUrl(id));
    } else {
      return getDefaultModel(this.name)
    }
  },
  [FormPageMethodsDefines.fmtModelObject](model) {
    return Promise.resolve(model).then(model => {
      if (this.hasFiles) {
        model.Files = model.Files || []
      }

      return model;
    })
  },
  [FormPageMethodsDefines.evalRule](name) {
    let rules = this.rules[name] || [];
    let val = this.model[name];
    this.formErrorMessages[name] = [];

    let promiseArr = rules.map(v => v.call(this.model, val))
    return Promise.all(promiseArr).then(arr => {
      return arr.filter(v => typeof v == 'string')
    }).then(errs => {
      this.formErrorMessages[name].push(...errs);
      return errs;
    })
  },
  [FormPageMethodsDefines.evalRules]() {
    let promiseArr = Object.keys(this.rules).map(v => this.evalRule(v));
    return Promise.all(promiseArr).then(arr => {
      return arr.filter(v => v.length > 0)
    })
  },
  [FormPageMethodsDefines.getPostMethod](id) {
    if (!id) {
      return this.$http.post;
    } else {
      return this.$http.put;
    }
  },
  [FormPageMethodsDefines.getPostUrl](id) {
    if (!id) {
      return `/api/${this.name}/post`;
    } else {
      return `/api/${this.name}/put`
    }
  },
  [FormPageMethodsDefines.getPostData]() {
    let postData = JSON.parse(JSON.stringify(this.model))
    delete postData.Create_User;
    delete postData.Modify_User;
    return postData
  },
  async [FormPageMethodsDefines.submit]() {
    try {
      this.submiting = true;
      let errs = await this.evalRules();
      if (errs.length > 0) {
        console.log(errs);
        this.$message.alert({
          title: '表单填写不完整',
          content: errs.map(v => `<p style="color:red;">${v}</p>`).join('')
        });
        return;
      }

      let id = this.id;
      let postData = this.getPostData();
      let method = this.getPostMethod(id);
      let url = this.getPostUrl(id)
      let res = await method(url, postData)

      this.onSaveAfter(res);
    } catch (error) {
      // this.$message.toast.error(error.message);
    } finally {
      this.submiting = false;
    }
  },
  [FormPageMethodsDefines.onSaveAfter](res) {
    this.$message.toast.success('保存成功');
    this.$eventBus.$emit(`${this.name}_update`)

    setTimeout(() => {
      if (res) {
        this.$emit('close');

        if (!this.isDialog) {
          this.$nextTick(() => {
            this.$router.replace(`/${this.name}/${res}`);
          })
        }
      } else {
        this.init();
      }
    }, 150);
  },
  [FormPageMethodsDefines.goList](data) {
    if (this.isTab) {
      this.$router.push(`/${this.name}/list`);
    } else if (this.isDialog) {
      this.$emit("success", data);
    } else {
      this.$router.push(`/${this.name}/list`);
    }
  },
  [FormPageMethodsDefines.close]() {
    if (this.isTab) {
      this.$emit("close");
    }
    else if (this.isDialog) {
      this.$emit("close");
    }
  }
}



/**
 * 页面观察属性
 */
export const formWatch = {
  /**
   * ID参数变化时更新加载页面
   * @param {*} val 
   */
  id(val) {
    if (val != this.model.Id) {
      this.init()
    }
  }
}

/**
 * 生成传递给子组件的参数
 */
export const makeChildProps = function () {
  return {
    ...this.$props,
    ...this.$attrs,
    title: this.title,
    id: this.id,
    model: this.model,
    formErrorMessages: this.formErrorMessages,
    options: this.options,
    singleLine: this.singleLine,
    showMamageField: this.showMamageField,
    canEdit: this.canEdit,
    formGroups: this.formGroups,
    isDialog: this.isDialog,
    hasManage: this.hasManage,
    toolItems: this.toolItems,
    isTab: this.isTab,
    pageFormCols: this.pageFormCols,
    pageFlex: this.pageFlex,
  };
};

/**
 * 生成监听子组件事件
 */
export const makeChildListeners = function () {
  return {
    ...this.$listeners,
    'tooggle:changed': () => this.changed = true,
    'toggle:singleLine': () => (this.singleLine = !this.singleLine),
    'toggle:showMamageField': () => (this.showMamageField = !this.showMamageField),
    changed: $event => {
      this.changed = true;
      this.$nextTick(() => {
        this.evalRule($event.item.Name);
      });
    },
    reload: () => this.init(),
    close: this.close
  };
};

/**
 * 混入生命周期事件
 */
export const FormPageMixin = {
  components: {
    "v-page": () => import("@/components/Page/FormPage.vue")
  },

  async mounted() {
    this.init();
    if (this.name) {
      this.$eventBus.$on(`${this.name}_DataUpdated`, this.DataUpdated);
      this.$eventBus.$on(`${this.name}_DataDeleted`, this.DataDeleted);
    }
  },
  destroyed() {
    if (this.name) {
      this.$eventBus.$off(`${this.name}_DataUpdated`, this.DataUpdated);
      this.$eventBus.$off(`${this.name}_DataDeleted`, this.DataDeleted);
    }
  },
};

/**
 * 计算属性名称定义
 */
export const FormPageComputedDefines = {
  /**
   * 页面的标题
   */
  title: 'title',

  /**
   * 页面的分组
   */
  formGroups: 'formGroups',

  /**
   * 更新按钮是否显示
   */
  updateBtnVisible: 'updateBtnVisible',

  /**
   * 布局页的参数
   */
  childProps: 'childProps',

  /**
   * 布局页的事件
   */
  childListeners: 'childListeners',
}

/**
 * 计算属性
 */
export const formComputed = {
  [FormPageComputedDefines.title]() {
    if (this.canEdit && this.id) {
      return `${this.direction}`;
    } else if (this.id) {
      return `${this.direction}`;
    } else {
      return `${this.direction}`;
    }
  },
  [FormPageComputedDefines.formGroups]() {
    const model = this.model;
    let opts = this.options || [];
    if (this.hasManage && this.showMamageField && this.model && this.model.Id) {
      opts = [...opts, ...this.manageOptions];
    }

    opts = distinct(opts, v => v.Name, (a, b) => ({
      ...a,
      ...b
    }))

    opts = opts.filter(item => calcInputVisible(item, model))

    for (const item of opts) {
      item.cols = calcInputItemCols(this.pageFormCols, item)
    }

    let brr = selectMany(opts, r => {
      return (r.GroupNames || ["基础信息"]).map(p => {
        return {
          ...r,
          title: p
        };
      });
    });

    let arr = groupBy(
      brr,
      r => ({
        title: r.title,
        value: true
      }),
      (a, b) => a.title == b.title
    );
    return arr.filter(v => v.values.length > 0);
  },
  [FormPageComputedDefines.updateBtnVisible]() {
    return this.hasManage && this.id && !this.changed && !this.canEdit
  },
  [FormPageComputedDefines.childProps]() {
    let props = makeChildProps.call(this);
    return props;
  },
  [FormPageComputedDefines.childListeners]() {
    let listeners = makeChildListeners.call(this);
    return listeners
  }
}

/**
 * 导出基础类型
 */
export default {
  mixins: [FormPageMixin],
  inject: [...formInject],
  props: {
    ...formProps
  },
  data() {
    return {
      ...formData,
    };
  },
  computed: {
    ...formComputed
  },
  watch: {
    ...formWatch
  },
  methods: {
    ...formMethods
  },
  render(h) {
    let props = this.childProps;
    let listeners = this.childListeners;
    return h("v-page", {
      props,
      on: listeners
    });
  }
};