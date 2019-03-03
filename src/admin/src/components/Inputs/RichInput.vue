<template>
  <div>
    <v-subheader>{{ label }}</v-subheader>
    <quill-editor v-model="content" ref="myQuillEditor" :options="editorOption" :disabled="disabled" @blur="onEditorBlur($event)"
      @focus="onEditorFocus($event)" @change="onEditorChange($event)" class="rich-input"></quill-editor>
  </div>
</template>


<script>
  export default {
    props: {
      value: String,
      disabled: Boolean,
      label: String
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
      onEditorChange({
        html
      }) {
        //内容改变事件
        this.content = html
      },
      onEditorFocus() {

      }
    }
  }
</script>

<style>
  .rich-input {
    height: 200px;
  }
</style>