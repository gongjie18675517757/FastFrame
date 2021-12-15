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


/**
 * 要注入的方法名
 */
export let formInject = []

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
export let formProps = {
  pars: Object,
  isDialog: Boolean,
  isTab: Boolean,
  id: String,
  super_id: String,
};

/**
 * 生成数据
 */
export let formData = {
  /**
 * 模块名称，影响：请求地址
 */
  name: null,

  /**
   * 页面结构名：影响：获取表单相关
   */
  strutName: null,

  /**
   * 权限名称：影响页面权限定义
   */
  permissionName: null,

  /**
   * 是否可编辑
   */
  canEdit: false,

  /**
   * 表单模型实体
   */
  model: {},

  /**
   * 表单验证异常信息
   */
  formErrorMessages: {},

  /**
   * 页面表单项
   */
  options: [],

  /**
   * 表单验证规则
   */
  rules: {},

  /**
   * 审计字段
   */
  manageOptions: [{
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

  /**
   * 是否提交中
   */
  submiting: false,

  /**
   * 是否单选布局
   */
  singleLine: false,

  /**
   * 是否显示管理字段
   */
  showMamageField: false,

  /**
   * 页面是否有改动过
   */
  changed: false,

  /**
   * 是否有审核字段
   */
  hasManage: false,

  /**
   * 是否有附件
   */
  hasFiles: false,

  /**
   * 工具按钮
   */
  toolItems: []
};



/**
 * 页面方法
 */
export let formMethods = {
  /**
   * 页面初始化
   */
  async init() {
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

  /**
   * 加载模块信息
   * @param {*} name 
   */
  getModuleStrut(name) {
    return getModuleStrut(name);
  },

  /**
   * 格式化模块信息
   * @param {*} obj 
   */
  fmtModuleStrut(obj) {
    return Promise.resolve(obj);
  },

  /**
   * 加载表单验证信息
   */
  getRules() {
    return getRules(this.strutName)
  },

  /**
   * 格式化表单验证信息
   * @param {*} rules 
   */
  fmtRules(rules) {
    return Promise.resolve(rules);
  },

  /**
   * 加载表单项
   */
  getModelObjectItems() {
    return getModelObjectItems(this.strutName).then(arr => {

      arr.forEach(v => {
        if (v.Relate) {
          v.requestUrl = `/api/${this.name}/${v.Relate}List`
        }
      })
      return arr;
    })
  },

  /**
   * 加载工具条
   */
  async getToolItems() {
    let arr = await makeToolItems.call(this);
    let brr = this.haveCheck ? [
      {
        component: {
          functional: true,
          render: (h) => h('fragments-facatory', null, makeButtons({
            selection: [this.model],
            mode: makeButtonsInputMode.FORM,
            editing: this.canEdit,
            moduleName:this.name
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

  /**
   * 格式化表单
   * @param {*} arr 
   */
  fmtModelObjectItems(arr) {
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
      return [
        ...arr
      ]
    });
  },

  /**
   * 点击编辑按钮时
   */
  handleEdit() {
    this.canEdit = !this.canEdit
  },

  /**
   * 当前内容被更新时
   */
  DataUpdated() {

  },

  /**
   * 当前内容被删除时
   */
  DataDeleted() {
    this.$message.alert({
      title: "提示",
      content: "当前内容已被其它人删除!"
    }).then(() => {
      this.close();
    });
  },

  /**
   * 获取请求数据的URL
   * @param {*} id 
   */
  getRequestUrl(id) {
    return `/api/${this.name}/get/${id}`
  },

  /**
   * 获取页面模型
   */
  getModelObject() {
    let id = this.id
    if (id) {
      return this.$http.get(this.getRequestUrl(id));
    } else {
      return getDefaultModel(this.name)
    }
  },

  /**
   * 格式化页面模型
   * @param {*} model 
   */
  fmtModelObject(model) {
    return Promise.resolve(model).then(model => {
      if (this.hasFiles) {
        model.Files = model.Files || []
      }
      return model;
    })
  },

  /**
   * 验证规则
   * @param {*} name 
   */
  evalRule(name) {
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

  /**
   * 验证全部规则
   */
  evalRules() {
    let promiseArr = Object.keys(this.rules).map(v => this.evalRule(v));
    return Promise.all(promiseArr).then(arr => {
      return arr.filter(v => v.length > 0)
    })
  },

  /**
   * 获取提交方法
   * @param {*} id 
   */
  getPostMethod(id) {
    if (!id) {
      return this.$http.post;
    } else {
      return this.$http.put;
    }
  },

  /**
   * 获取提交地址
   * @param {*} id 
   */
  getPostUrl(id) {
    if (!id) {
      return `/api/${this.name}/post`;
    } else {
      return `/api/${this.name}/put`
    }
  },

  /**
   * 获取提交数据
   */
  getPostData() {
    let postData = JSON.parse(JSON.stringify(this.model))
    delete postData.Create_User;
    delete postData.Modify_User;
    return postData
  },

  /**
   * 提交数据
   */
  async submit() {
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

  /**
   * 保存方法调用成功之后
   * @param {*} res 
   */
  onSaveAfter(res) {
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

  /**
   * 返回列表
   * @param {*} data 
   */
  goList(data) {
    if (this.isTab) {
      this.$router.push(`/${this.name}/list`);
    } else if (this.isDialog) {
      this.$emit("success", data);
    } else {
      this.$router.push(`/${this.name}/list`);
    }
  },

  /**
   * 关闭页面
   */
  close() {
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
export let formWatch = {
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
export let makeChildProps = function () {
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
  };
};

/**
 * 生成监听子组件事件
 */
export let makeChildListeners = function () {
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
    close: this.close,
    toolItemClick: item => item.action.call(this, this.model),
  };
};

/**
 * 混入生命周期事件
 */
export let FormPageMixin = {
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
 * 计算属性
 */
export let formComputed = {
  title() {
    if (this.canEdit && this.id) {
      return `${this.direction}`;
    } else if (this.id) {
      return `${this.direction}`;
    } else {
      return `${this.direction}`;
    }
  },
  formGroups() {
    let opts = this.options || [];
    if (this.hasManage && this.showMamageField && this.model && this.model.Id) {
      opts = [...opts, ...this.manageOptions];
    }
    opts = distinct(opts, v => v.Name, (a, b) => ({
      ...a,
      ...b
    }))
    opts = opts.filter(v => {
      if (typeof v.visible == 'function')
        return v.visible.call(this, this.model)
      else if (typeof v.visible == 'boolean')
        return v.visible
      else
        return true;
    })

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
  updateBtnVisible() {
    return this.hasManage && this.id && !this.changed && !this.canEdit
  },
  childProps() {
    let props = makeChildProps.call(this);
    return props;
  },
  childListeners() {
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