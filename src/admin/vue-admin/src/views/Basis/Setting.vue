 <script>
let pageInfo = {
  area: "Basis",
  name: "Setting",
  direction: "系统设置",
};
import Page from "../../components/Page/FormPageCore.js";
import { FileDetailTable } from "../../components/Table/index.js";
export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
    };
  },
  computed: {
    ...Page.computed,
    title() {
      return this.canEdit ? "编辑系统设置" : "查看系统设置";
    },
    updateBtnVisible() {
      return (
        this.$store.getters.existsPermission("Setting.Update") && !this.canEdit
      );
    },
  },
  methods: {
    ...Page.methods,
    init() {
      return Page.methods.init.call(this).then(() => {
        this.hasManage = true;
        this.canEdit = false;
      });
    },
    getModelObject() {
      return this.$http.get(`/api/Setting/get`);
    },
    getPostUrl() {
      return `/api/Setting/Update`;
    },
    fmtModelObjectItems() {
      return Page.methods.fmtModelObjectItems
        .call(this, ...arguments)
        .then((arr) => {
          return [
            ...arr,
            {
              Name: "VerifyImageList",
              GroupNames: ["验证码背景图片"],
              template: FileDetailTable(),
              accept: "image/*",
              verifyFileFunc: (arr) => {
                if ([...arr].some((x) => !x.type.startsWith("image/"))) {
                  this.$message.toast.error("只允许上传图片类型!");
                  return false;
                }

                return true;
              },
            },
            {
              Name: "VerifyImageList2",
              GroupNames: ["验证码滑块图片"],
              template: FileDetailTable(),
              accept: "image/*",
              verifyFileFunc: (arr) => {
                if ([...arr].some((x) => !x.type.startsWith("image/"))) {
                  this.$message.toast.error("只允许上传图片类型!");
                  return false;
                }

                return true;
              },
            },
          ];
        });
    },
  },
};
</script>  