export default {
  props: {
    value: Boolean,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean
  },
  render(h) {
    if (this.disabled && !this.isXs) {
      return h('span', null, this.value ? '是' : '否')
    }
    return h('v-checkbox', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        placeholder: this.description,
        errorMessages: this.errorMessages,
      },
      on: {
        ...this.$listeners,
        input: (val) => this.$emit('input', val),
        change: val => this.$emit('change', val)
      }
    })
  }
}