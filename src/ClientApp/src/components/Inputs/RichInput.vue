<template>
  <quill-editor
    v-model="content"
    ref="myQuillEditor"
    :options="editorOption"
    :disabled="disabled"
    @blur="onEditorBlur($event)"
    @focus="onEditorFocus($event)"
    @change="onEditorChange($event)"
  ></quill-editor>
</template> 
 

<script>
import { quillEditor } from 'vue-quill-editor'
export default {
  props: {
    value: String,
    disabled: Boolean
  },
  data() {
    return {
      editorOption: {}
    }
  },
  computed: {
    content: {
      get() {
        return this.value
      },
      set(val) {
        this.$emit('input', val)
      }
    }
  },
  methods: {
    onEditorBlur() {
      //失去焦点事件
    },
    onEditorFocus(val, editor) {
      // if (this.disabled) {
      //   editor.enable(false)
      // }
    },
    onEditorChange({ html }) {
      //内容改变事件
      this.content = html
    }
  }
}
</script>

<style>
</style>
