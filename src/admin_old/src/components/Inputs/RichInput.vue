<template>
  <div>
    <div v-if="disabled" v-html="content"></div>
    <quill-editor
      v-else
      v-model="content"
      ref="myQuillEditor"
      :options="editorOption"
      :disabled="disabled"
      @blur="onEditorBlur($event)"
      @focus="onEditorFocus($event)"
      @change="onEditorChange($event)"
      class="rich-input"
    ></quill-editor>
    <v-messages color="error" :value="errorMessages"></v-messages>
  </div>
</template>


<script>
export default {
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    errorMessages: Array
  },
  data() {
    return {
      editorOption: {}
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
      }
    }
  },
  methods: {
    onEditorBlur() {
      //失去焦点事件
    },
    onEditorChange({ html }) {
      //内容改变事件
      this.content = html;
    },
    onEditorFocus() {}
  }
};
</script>

<style>
/* .rich-input {
    height: 200px;
  } */
</style>