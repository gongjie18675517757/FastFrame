import {
  alert,
  groupBy,
  selectMany,
  distinct
} from "@/utils";

import {
  getDefaultModel,
  getFormItems,
  getRules,
  hasManage
} from "@/generate";

/**
 * 要注入的方法名
 */
export let formInject = []

/**
 * 生成参数
 */
export let formProps = {
  success: Function,
  close: Function,
  pars: Object,
  isDialog: Boolean
};

/**
 * 生成数据
 */
export let formData = {
  canEdit: false,
  form: {},
  formErrorMessages: {},
  options: [],
  rules: {},
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
  submiting: false,
  singleLine: false,
  showMamageField: false,
  changed: false,
  hasManage: false
};

/**
 * 计算属性
 */
export let formComputed = {
  id() {
    if (this.pars) {
      return this.pars.id;
    } else {
      let {
        q: id
      } = this.$route.query;
      return id;
    }
  },
  title() {
    if (this.canEdit && this.id) {
      return `修改${this.direction}`;
    } else if (this.id) {
      return `查看${this.direction}`;
    } else {
      return `添加${this.direction}`;
    }
  },
  formGroups() {
    let opts = this.options || [];
    if (this.hasManage && this.showMamageField && this.form && this.form.Id) {
      opts = [...opts, ...this.manageOptions];
    }
    opts = distinct(opts, v => v.Name, (a, b) => ({
      ...a,
      ...b
    }))
    opts = opts.filter(v => {
      if (typeof v.visible == 'function')
        return v.visible.call(this.this.form)
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
    return arr;
  }
}

/**
 * 页面方法
 */
export let formMethods = {
  init() {
    let id = this.id;
    if (!id) this.canEdit = true;
    let moduleName = this.name;
    return hasManage(moduleName)
      .then(val => {
        this.hasManage = val;
      })
      .then(this.getRules)
      .then(rules => {
        this.rules = rules;
      })

      .then(this.getForm)
      .then(this.frmLoadForm)
      .then((form) => {
        let formErrs = {};
        for (const name of Object.keys(form)) {
          formErrs[name] = [];
        }
        this.formErrorMessages = formErrs;
        this.form = form;
      })

      .then(this.getFormItems)
      .then(options => {
        this.options = options;
      })
      .then(() => {
        this.changed = false;
        this.canEdit = false;
      })
  },
  getRules() {
    return getRules(this.name)
  },
  getFormItems() {
    return getFormItems(this.name)
  },
  DataUpdated({
    Id
  }) {
    if (Id == this.id || Id == this.form.Id) {
      this.$message.confirm("提示", "当前内容已被其它人修改,是否重新加载?").then(this.init)
    }
  },
  DataDeleted() {
    this.$message.alert("提示", "当前内容已被其它人删除!").then(() => {
      this.cancel();
    });
  },
  getRequestUrl(id) {
    return `/api/${this.name}/get/${id}`
  },
  getForm() {
    let id = this.id
    if (id) {
      return this.$http.get(this.getRequestUrl(id));
    } else {
      return getDefaultModel(this.name);
    }
  },
  frmLoadForm(frm) {
    return frm;
  },
  evalRule(name) {
    let rules = this.rules[name];
    let val = this.form[name];
    this.formErrorMessages[name] = [];

    let promiseArr = rules.map(v => v.call(this.form, val))
    return Promise.all(promiseArr).then(arr => {
      return arr.fill(v => typeof v == 'string')
    }).then(errs => {
      this.formErrorMessages[name].push(...errs);
      return errs;
    })
  },
  evalRules() {
    let promiseArr = Object.keys(this.rules).map(v => this.evalRule(v));
    return Promise.all(promiseArr).then(arr => {
      return arr.filter(v => v)
    })
  },
  getPostMethod(id) {
    if (!id) {
      return this.$http.post;
    } else {
      return this.$http.put;
    }
  },
  getPostUrl(id) {
    if (!id) {
      return `/api/${this.name}/post`;
    } else {
      return `/api/${this.name}/put`
    }
  },
  getPostData() {
    let postData = JSON.parse(JSON.stringify(this.form))
    delete postData.Create_User;
    delete postData.Modify_User;
    return postData
  },
  async submit() {
    try {
      this.submiting = true;
      let errs = await this.evalRules();
      if (errs.length > 0) {
        alert.error("表单填写不完整");
        return;
      }

      let id = this.id;
      let postData = this.getPostData();
      let method = this.getPostMethod(id);
      let url = this.getPostUrl(id)
      let data = await method(url, postData)
      data = this.frmLoadForm(data)

      this.goList(data)
    } catch (error) {
      alert.error(error.message);
    } finally {
      this.submiting = false;
    }
  },
  goList(data) {
    if (this.isDialog) {
      this.$emit("success", data);
      this.success(data);
    } else {
      this.$router.push(`/${this.name}/list`);
    }
  },
  cancel() {
    if (this.isDialog) {
      this.$emit("close");
      this.close();
    } else {
      this.goList();
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
    form: this.form,
    name: this.name,
    formErrorMessages: this.formErrorMessages,
    options: this.options,
    rules: this.rules,
    submiting: this.submiting,
    singleLine: this.singleLine,
    showMamageField: this.showMamageField,
    canEdit: this.canEdit,
    changed: this.changed,
    formGroups: this.formGroups,
    isDialog: this.isDialog,
    hasManage: this.hasManage
  };
};

/**
 * 生成监听子组件事件
 */
export let makeChildListeners = function () {
  return {
    ...this.$listeners,
    cancel: this.cancel,
    'tooggle:canEdit': () => this.canEdit = !this.canEdit,
    'toggle:singleLine': () => (this.singleLine = !this.singleLine),
    'toggle:showMamageField': () => (this.showMamageField = !this.showMamageField),
    changed: $event => {
      this.changed = true;
      this.evalRule($event.item.Name);
    },
    reload: this.load,
    submit: this.submit
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