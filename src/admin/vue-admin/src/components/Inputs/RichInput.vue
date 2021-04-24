<template>
  <v-flex xs12 style="padding: 0 0 5px 0">
    <v-card tile>
      <v-toolbar flat dense color="transparent" height="30px;">
        <v-toolbar-title>{{ title }}:</v-toolbar-title>
        <v-messages color="error" :value="errorMessages"></v-messages>
      </v-toolbar>
      <v-card-text>
        <RemoteJS src="/libs/wangEditor.min.js" @load="onLoad" />
        <div v-if="!canEdit" v-html="content" class="w-e-text"></div>
        <div v-else ref="editor"></div>
      </v-card-text>
    </v-card>
  </v-flex>
</template>


<script>
import { postFiles, upload } from "../../utils";
import RemoteJS from "../RemoteJS.vue";
export default {
  components: { RemoteJS },
  props: {
    value: String,
    canEdit: Boolean,
    title: String,
    errorMessages: Array,
  },
  editor: null,
  data() {
    return {
      isLoad: false,
    };
  },
  computed: {
    content: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
        this.$emit("change", val);
      },
    },
  },

  watch: {
    canEdit() {
      this.$nextTick(this.init);
    },
  },
  mounted() {
    this.init();
  },
  beforeDestroy() {
    // 调用销毁 API 对当前编辑器实例进行销毁
    if (this.editor) this.editor.destroy();
    this.editor = null;
  },
  methods: {
    onLoad() {
      this.isLoad = true;
      this.init();
    },
    init() {
      if (this.canEdit && this.isLoad) {
        var E = window.wangEditor;
        var editor = new E(this.$refs.editor);
        editor.customConfig.onchange = (html) => {
          this.content = html;
        };
        // editor.customConfig.uploadImgShowBase64 = true; // 使用 base64 保存图片
        editor.customConfig.customUploadImg = this.handleFileUpload;
        editor.customConfig.customUploadVideo = this.handleFileUpload;
        editor.create();
        editor.txt.html(this.content);

        this.editor = editor;
      }
    },
    handleFileUpload(resultFiles, insertImgFn) {
      postFiles(resultFiles).then((arr) => {
        for (const f of arr) {
          insertImgFn(`/api/resource/get/${f.Id}/${f.Name}`);
        }
      });
    },
  },
};
</script>

<style>
/* .rich-input {
    height: 200px;
  } */
</style>