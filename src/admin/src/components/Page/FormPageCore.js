import {
  alert,
  groupBy,
  selectMany
} from "@/utils";
import {
  getDefaultModel,
  getFormItems,
  getRules,
  hasManage
} from "@/generate";

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

export let pageProps = function () {
  return {
    ...this.$props,
    ...this.$attrs,
    title: this.title,
    id: this.id,
    form: this.form,
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

export let pageListeners = function () {
  return {
    success: val => this.$emit("success", val),
    cancel: this.cancel,
    chaneShowMode: () => (this.singleLine = !this.singleLine),
    changeShowMamageField: () => (this.showMamageField = !this.showMamageField),
    changed: $event => {
      this.changed = true;
      this.evalRule($event.item.Name);
    },
    reload: this.load,
    update_errorMessages: val => {
      this.formErrorMessages[val.item.Name] = val.value;
    },
    submit: this.submit
  };
};

export let FormPageMixin = {
  props: {
    success: Function,
    close: Function,
    pars: Object
  },
  components: {
    "v-page": () => import("@/components/Page/FormPage.vue")
  },
  data() {
    return {};
  },
  computed: {
    isDialog() {
      return !!this.success;
    },
    id() {
      if (this.pars && this.pars.id) {
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
  },
  async mounted() {
    if (this.name) {
      this.init();
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
  methods: {
    init() {
      let id = this.id;
      if (!id) this.canEdit = true;
      let moduleName = this.name;
      hasManage(moduleName)
        .then(val => {
          this.hasManage = val;
          return getRules(moduleName);
        })
        .then(rules => {
          this.rules = rules;
          return this.load();
        })
        .then(() => {
          return getFormItems(moduleName);
        })
        .then(options => {
          this.options = options;
        });
    },
    DataUpdated({
      Id
    }) {
      if (Id == this.id) this.load();
    },
    DataDeleted() {
      this.$message.alert("提示", "当前内容已被其它人删除!").then(() => {
        this.cancel();
      });
    },
    async load() {
      let id = this.id,
        promise;
      if (id) {
        promise = this.$http.get(`/api/${this.name}/get/${id}`);
      } else {
        let moduleName = this.name;
        promise = getDefaultModel(moduleName);
      }
      promise.then(form => {
        let formErrs = {};
        for (const name of Object.keys(form)) {
          formErrs[name] = [];
        }
        this.formErrorMessages = formErrs;
        this.form = form;
      });
    },
    async evalRule(name) {
      let rules = this.rules[name];
      let val = this.form[name];
      this.formErrorMessages[name] = [];
      for (const rule of rules) {
        if (this.formErrorMessages[name].length == 0) {
          let err = await rule.call(this.form, val);
          if (typeof err == "string") {
            this.formErrorMessages[name].push(err);
            return err;
          }
        }
      }
    },
    async submit() {
      try {
        this.submiting = true;
        let errs = [];
        for (const name of Object.keys(this.rules)) {
          let err = await this.evalRule(name);
          if (err) errs.push(err);
        }

        if (errs.length > 0) {
          alert.error("表单填写不完整");
          return;
        }

        let id = this.id,
          data,
          postData = {
            ...this.form
          };
        delete postData.Create_User;
        delete postData.Modify_User;

        if (!id) {
          data = await this.$http.post(`/api/${this.name}/post`, postData);
        } else {
          data = await this.$http.put(`/api/${this.name}/put`, postData);
        }

        this.$emit("success", data);
        if (typeof this.success == "function") {
          this.success(data);
        } else {
          this.goList();
        }
      } catch (error) {
        alert.error(error.message);
        console.error(error);
      } finally {
        this.submiting = false;
      }
    },
    goList() {
      this.$router.push(`/${this.name}/list`);
    },
    cancel() {
      this.$emit("close");
      if (typeof this.close == "function") {
        this.close();
      } else {
        this.goList();
      }
    }
  }
};